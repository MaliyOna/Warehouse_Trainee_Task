import React, { useEffect, useState } from 'react';
import { PageHead } from '../../shared/components/PageHead/PageHead';
import { PageContent } from '../../shared/components/PageContent/PageContent';
import './WorkersPage.scss';
import { Menu } from '../../shared/components/Menu/Menu';
import { createWorker, deleteWorker, getWorkers, updateWorker } from '../../shared/api/workersApi';
import { UsersCreate } from './UsersCreate/UsersCreate';
import { UsersUpdate } from './UsersUpdate/UsersUpdate';
import { UsersDelete } from './UsersDelete/UsersDelete';

export function WorkersPage() {
    const [users, setUsers] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState(null);

    useEffect(() => {
        async function fetchData() {
            const data = await getWorkers();
            if (data) {
                setUsers(data);
            }
        }

        fetchData();
    }, [])

    async function loadUsers() {
        const data = await getWorkers();
        if (data) {
            setUsers(data);
        }
    }

    async function handleUserUpdated(user) {
        await updateWorker(user);
        await loadUsers();
    }

    async function handleUserCreated(user) {
        await createWorker(user);
        await loadUsers();
    }

    async function handleUserDelete(userId) {
        await deleteWorker(userId);
        setSelectedUserId(null);
    
        await loadUsers();
    }

    function handleUserSelected(id) {
        setSelectedUserId(id)
    }

    return (
        <>
            <PageHead></PageHead>
            <Menu></Menu>

            <PageContent>
                <div className='workersPage__content'>
                    <table className='workersPage__content__table'>
                        <thead>
                            <tr>
                                <th>First name</th>
                                <th>Last name</th>
                            </tr>
                        </thead>

                        <tbody>
                            {
                                users.map(user =>
                                    <tr key={user.id} onClick={() => handleUserSelected(user.id)} className={selectedUserId === user.id ? "workersPage__selectedUser" : ""}>
                                        <td>{user.firstName}</td>
                                        <td>{user.lastName}</td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>

                    <UsersCreate onUserCreated={handleUserCreated} />

                    {selectedUserId && <UsersUpdate
                        user={users.find(x => x.id === selectedUserId)}
                        onUserUpdated={handleUserUpdated} />}

                    {selectedUserId && <UsersDelete
                        user={users.find(x => x.id === selectedUserId)}
                        onUserDelete={handleUserDelete} />}
                </div>
            </PageContent>
        </>
    );
}