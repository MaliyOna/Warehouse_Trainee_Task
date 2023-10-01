import React, { useEffect, useState } from 'react';
import './DepartmentsPage.scss';
import { PageHead } from '../../shared/components/PageHead/PageHead';
import { Menu } from '../../shared/components/Menu/Menu';
import { PageContent } from '../../shared/components/PageContent/PageContent';
import { useNavigate } from 'react-router-dom';
import { Button } from '../../shared/components/Button/Button';
import { createDepartment, getDepartments } from '../../shared/api/departmentsApi';
import { PopupWindow } from '../../shared/components/PopupWindow/PopupWindow';
import { Input } from '../../shared/components/Input/Input';
import { Form } from '../../shared/components/Form/Form';


export function DepartmentsPage() {
    const navigate = useNavigate();
    const [showAddDepartmentWindow, setShowAddDepartmentWindow] = useState(false);
    const [departments, setDepartments] = useState([]);
    const [newDepartmentName, setNewDepartmentName] = useState();

    useEffect(() => {
        async function load() {
            var data = await getDepartments();

            if (data) {
                setDepartments(data)
            }
        }

        load();
    }, [])

    async function loadDepartments() {
        async function load() {
            var data = await getDepartments();

            if (data) {
                setDepartments(data)
            }
        }

        load();
    }

    async function AddDepartment(){
        var newDepartment = {
            name: newDepartmentName
        }

        await createDepartment(newDepartment);
        loadDepartments();
        setShowAddDepartmentWindow(false);
    }

    function handleOpenPopup() {
        setShowAddDepartmentWindow(true);
        setNewDepartmentName("");
    }

    return (
        <>
            <PageHead></PageHead>
            <Menu></Menu>

            <PageContent>
                <Button value='Добавить отдел' onClick={() => handleOpenPopup()} />

                {
                    departments.map(department =>
                        <div key={department.id} className="departmentsPage__department" onClick={() => navigate(`/departments/${department.id}`)}>
                            {department.name}
                        </div>
                    )
                }

                <PopupWindow title="Создание отдела" open={showAddDepartmentWindow}>
                    <Form className='departmentsPage__form' onSubmit={AddDepartment} mode="onBlur">
                        <Input
                            label="Название отдела"
                            type="text"
                            value={newDepartmentName}
                            onChange={(event) => setNewDepartmentName(event.target.value)}
                            name="newDepartmentName"
                            rules={{ required: "Обязательное поле" }} />

                        <Button value="Создать" type="submit" />
                        <Button value="Отмена" onClick={() => setShowAddDepartmentWindow(false)} color="red" />
                    </Form>
                </PopupWindow>
            </PageContent>
        </>
    );
}