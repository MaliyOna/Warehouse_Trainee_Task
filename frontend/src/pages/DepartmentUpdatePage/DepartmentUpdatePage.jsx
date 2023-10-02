import React, { useEffect, useState } from 'react';
import './DepartmentUpdatePage.scss';
import { PageHead } from '../../shared/components/PageHead/PageHead';
import { Menu } from '../../shared/components/Menu/Menu';
import { PageContent } from '../../shared/components/PageContent/PageContent';
import { useNavigate, useParams } from 'react-router-dom';
import { addDepartmentWorker, deleteDepartment, deleteDepartmentWorker, getDepartmentById, getProductsByDepartment, getWorkersByDepartment, updateDepartment } from '../../shared/api/departmentsApi';
import { Input } from '../../shared/components/Input/Input';
import { createProduct, deleteProduct, updateProduct } from '../../shared/api/productsApi';
import { Button } from '../../shared/components/Button/Button';
import { PopupWindow } from '../../shared/components/PopupWindow/PopupWindow';
import { Form } from '../../shared/components/Form/Form';
import { getWorkers } from '../../shared/api/workersApi';

export function DepartmentUpdatePage() {
    const params = useParams();
    const navigate = useNavigate();
    const [department, setDepartment] = useState()
    const [departmentName, setDepartmentName] = useState()
    const [products, setProducts] = useState([])
    const [newProductName, setNewProductName] = useState([])
    const [workers, setWorkers] = useState([])
    const [allWorkers, setAllWorkers] = useState([])
    const [showCreateProductWindow, setShowCreateProductWindow] = useState(false)
    const [showAddWorkerWindow, setShowAddWorkerWindow] = useState(false)
    const [showUserFindWindow, setShowUserFindWindow] = useState(false);
    const [showProductUpdateDelete, setShowProductUpdateDelete] = useState(false);
    const [showDepartmentWorkerDelete, setShowDepartmentWorkerDelete] = useState(false);
    const [partLastName, setPartLastName] = useState("");
    const [targetProduct, setTargetProduct] = useState();
    const [targetWorker, setTargetWorker] = useState();

    useEffect(() => {
        const departmentId = params.departmentId;

        if (!departmentId) {
            return;
        }

        async function load() {
            const department = await getDepartmentById(departmentId);
            const products = await getProductsByDepartment(departmentId);
            const workers = await getWorkersByDepartment(departmentId)
            const allWorkers = await getWorkers();

            if (department) {
                setDepartment(department);
                setDepartmentName(department.name);
            }

            if (products) {
                setProducts(products);
            }

            if (workers) {
                setWorkers(workers);
            }

            if (allWorkers) {
                setAllWorkers(allWorkers);
            }
        }
        load();
    }, [])

    async function loadDepartments() {
        const department = await getDepartmentById(params.departmentId);

        if (department) {
            setDepartment(department);
            setDepartmentName(department.name);
        }
    }

    async function loadWorkers() {
        const workers = await getWorkersByDepartment(params.departmentId)

        if (workers) {
            setWorkers(workers);
        }
    }

    async function loadProducts() {
        const products = await getProductsByDepartment(params.departmentId);

        if (products) {
            setProducts(products);
        }
    }

    async function updateDepartmentName() {
        var department = {
            id: params.departmentId,
            name: departmentName,
        }

        await updateDepartment(department);
        loadDepartments();
    }

    async function addWorker(workerId) {
        await addDepartmentWorker(params.departmentId, workerId);

        loadWorkers();
        setShowUserFindWindow(false);
        setPartLastName("");
    }

    async function handleCreateProduct() {
        var product = {
            name: newProductName,
            departmentId: params.departmentId
        }

        await createProduct(product);

        setShowCreateProductWindow(false)
        loadProducts();
        setNewProductName("");
    }

    function handleFindClose() {
        setShowUserFindWindow(false);
        setPartLastName("");
    }

    function handleProductUpdateDelete(id) {
        const targetProduct = products.find(product => product.id === id)

        if (targetProduct) {
            console.log(targetProduct)
            setTargetProduct(targetProduct);
            setNewProductName(targetProduct.name);
        }

        setShowProductUpdateDelete(true);
    }

    function handleDepartmentWorkerDeleteOpen(id) {
        const targetWorker = workers.find(worker => worker.id === id)

        if (targetWorker) {
            setTargetWorker(targetWorker);
        }

        setShowDepartmentWorkerDelete(true);
    }

    async function handleUpdateProduct() {
        var product = {
            id: targetProduct.id,
            name: newProductName
        }

        await updateProduct(product);
        setNewProductName("");
        loadProducts();

        setShowProductUpdateDelete(false);
    }

    async function handleDeleteProduct() {
        await deleteProduct(targetProduct.id);
        setNewProductName("");
        loadProducts();

        setShowProductUpdateDelete(false);
    }

    async function handleDepartmentWorkerDelete() {
        await deleteDepartmentWorker(params.departmentId, targetWorker.id)

        loadWorkers();
        setShowDepartmentWorkerDelete(false);
    }

    async function handleDeleteDepartment() {
        await deleteDepartment(params.departmentId);
        navigate("/departments");
    }

    return (
        <>
            <PageHead></PageHead>
            <Menu></Menu>

            <PageContent>
                <div className="departmentUpdatePage__block">
                    <Input label="Department name" type="text" value={departmentName} onChange={event => setDepartmentName(event.target.value)} />

                    <Button value="Save" onClick={() => updateDepartmentName()} />
                </div>

                <div className="departmentUpdatePage__block">
                    {products.map((product, index) =>
                        <div key={product.id} className="departmentUpdatePage__product" onClick={() => handleProductUpdateDelete(product.id)}>
                            {product.name}
                        </div>
                    )}

                    <Button value="Create product" onClick={() => setShowCreateProductWindow(true)} />
                </div>

                <div className="departmentUpdatePage__block">
                    {workers.map((worker, index) =>
                        <div key={worker.id} className="departmentUpdatePage__product" onClick={() => handleDepartmentWorkerDeleteOpen(worker.id)}>
                            {worker.lastName + " " + worker.firstName}
                        </div>
                    )}

                    <Button value="Add worker" onClick={() => setShowUserFindWindow(true)} />
                </div>

                {targetWorker && <PopupWindow title="Remove user from department" open={showDepartmentWorkerDelete}>
                    <div>{targetWorker.lastName} {targetWorker.firstName}</div>

                    <Button value="Ok" onClick={() => handleDepartmentWorkerDelete()} />
                    <Button value="Cancel" onClick={() => setShowDepartmentWorkerDelete(false)} color="red" />
                </PopupWindow>}

                <PopupWindow title="Find worker" open={showUserFindWindow}>
                    <Input label="Enter last name:" type="text" onChange={(event) => setPartLastName(event.target.value)} />

                    <div className='departmentUpdatePage__popupWindow__filter'>
                        {allWorkers.filter(worker =>
                            worker.lastName.toUpperCase().includes(partLastName.toUpperCase())
                        ).map(worker =>
                            <div className='departmentUpdatePage__worker' onClick={() => addWorker(worker.id)}>{worker.lastName} {worker.firstName}</div>
                        )}

                        <Button value="Cancel" onClick={() => handleFindClose()} color="red" />
                    </div>
                </PopupWindow>

                {targetProduct && <PopupWindow title="Edit product" open={showProductUpdateDelete}>
                    <Input
                        label="Product name"
                        type="text"
                        value={newProductName}
                        onChange={(event) => setNewProductName(event.target.value)}
                        name="targetProductName"
                        rules={{ required: "Required field" }} />

                    <Button value="Save" onClick={() => handleUpdateProduct()} />
                    <Button value="Delete" onClick={() => handleDeleteProduct()} color="red" />
                    <Button value="Cancel" onClick={() => setShowProductUpdateDelete(false)} color="red" />
                </PopupWindow>}

                <PopupWindow title="Create product" open={showCreateProductWindow}>
                    <Form className='departmentUpdatePage__form' onSubmit={handleCreateProduct} mode="onBlur">
                        <Input
                            label="Product name"
                            type="text"
                            value={newProductName}
                            onChange={(event) => setNewProductName(event.target.value)}
                            name="newProductName"
                            rules={{ required: "Required field" }} />

                        <Button value="Save" type="submit" />
                        <Button value="Cancel" onClick={() => setShowCreateProductWindow(false)} color="red" />
                    </Form>
                </PopupWindow>

                <Button value="Удалить отделение" onClick={() => handleDeleteDepartment()} color="red" />
            </PageContent>
        </>
    );
}