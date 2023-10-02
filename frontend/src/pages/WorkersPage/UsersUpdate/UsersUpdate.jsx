import React, { useState } from 'react';
import './UsersUpdate.scss';
import { Button } from '../../../shared/components/Button/Button';
import { Input } from '../../../shared/components/Input/Input';
import { PopupWindow } from '../../../shared/components/PopupWindow/PopupWindow';

export function UsersUpdate(props) {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [showUserUpdateWindow, setShowUserUpdateWindow] = useState(false);

  function handleSubmit() {
    var user = {
      id: props.user.id,
      firstName: firstName,
      lastName: lastName,
    };

    props.onUserUpdated(user);
    setShowUserUpdateWindow(false);
  }

  function handleUserUpdateClick() {
    setFirstName(props.user.firstName);
    setLastName(props.user.lastName);
    setShowUserUpdateWindow(true);
  }

  return (
    <>
      <Button value='Edit worker' size='small' onClick={handleUserUpdateClick}/>

      <PopupWindow title="Edit worker" open={showUserUpdateWindow}>
        <div className='usersUpdate__form'>

          <Input label="First name" type="text" value={firstName} onChange={event => setFirstName(event.target.value)} />
          <Input label="Last name" type="text" value={lastName} onChange={event => setLastName(event.target.value)} />

          <Button value="Save" onClick={handleSubmit} />
          <Button value="Cancel" onClick={() => setShowUserUpdateWindow(false)} color="red" />
        </div>
      </PopupWindow>
    </>
  );
}