import React, { useState } from 'react';
import './UsersCreate.scss';
import { Button } from '../../../shared/components/Button/Button';
import { PopupWindow } from '../../../shared/components/PopupWindow/PopupWindow';
import { Form } from '../../../shared/components/Form/Form';
import { Input } from '../../../shared/components/Input/Input';

export function UsersCreate(props) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [showUserCreateWindow, setShowUserCreateWindow] = useState(false);

    function handleUserCreateClick() {
        setFirstName("");
        setLastName("");

        setShowUserCreateWindow(true);
    }

    function handleSubmit() {
        var user = {
            firstName: firstName,
            lastName: lastName,
        };

        props.onUserCreated(user);
        setShowUserCreateWindow(false);
    }

    return (
        <>
            <Button value='Создать работника' size='small' onClick={handleUserCreateClick}/>

            <PopupWindow title="Создание работника" open={showUserCreateWindow}>
                <Form className='usersCreate__form' onSubmit={handleSubmit} mode="onBlur">
                    <Input
                        label="Имя"
                        type="text"
                        value={firstName}
                        onChange={(event) => setFirstName(event.target.value)}
                        name="firstName"
                        rules={{ required: "Обязательное поле" }} />
                    <Input
                        label="Фамилия"
                        type="text"
                        value={lastName}
                        onChange={(event) => setLastName(event.target.value)}
                        name="lastName"
                        rules={{ required: "Обязательное поле" }} />
                    
                    <Button value="Создать" type="submit" />
                    <Button value="Отмена" onClick={() => setShowUserCreateWindow(false)} color="red" />

                </Form>
            </PopupWindow>
        </>
    );
}