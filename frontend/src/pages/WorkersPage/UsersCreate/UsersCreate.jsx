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
            <Button value='Create worker' size='small' onClick={handleUserCreateClick}/>

            <PopupWindow title="Create worker" open={showUserCreateWindow}>
                <Form className='usersCreate__form' onSubmit={handleSubmit} mode="onBlur">
                    <Input
                        label="First name"
                        type="text"
                        value={firstName}
                        onChange={(event) => setFirstName(event.target.value)}
                        name="firstName"
                        rules={{ required: "Required field" }} />
                    <Input
                        label="Last name"
                        type="text"
                        value={lastName}
                        onChange={(event) => setLastName(event.target.value)}
                        name="lastName"
                        rules={{ required: "Required field" }} />
                    
                    <Button value="Ok" type="submit" />
                    <Button value="Cancel" onClick={() => setShowUserCreateWindow(false)} color="red" />

                </Form>
            </PopupWindow>
        </>
    );
}