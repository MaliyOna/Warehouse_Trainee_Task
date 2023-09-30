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
      <Button value='Изменить работника' size='small' onClick={handleUserUpdateClick}/>

      <PopupWindow title="Редактирование работника" open={showUserUpdateWindow}>
        <div className='usersUpdate__form'>

          <Input label="Имя" type="text" value={firstName} onChange={event => setFirstName(event.target.value)} />
          <Input label="Фамилия" type="text" value={lastName} onChange={event => setLastName(event.target.value)} />

          <Button value="Сохранить" onClick={handleSubmit} />
          <Button value="Отмена" onClick={() => setShowUserUpdateWindow(false)} color="red" />
        </div>
      </PopupWindow>
    </>
  );
}