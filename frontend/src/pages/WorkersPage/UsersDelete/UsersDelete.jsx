import React, { useState } from 'react';
import './UsersDelete.scss';
import { Button } from '../../../shared/components/Button/Button';
import { PopupWindow } from '../../../shared/components/PopupWindow/PopupWindow';

export function UsersDelete(props) {
  const [showUserDeleteWindow, setShowUserDeleteWindow] = useState(false);

  async function handleUserDelete () {
    props.onUserDelete(props.user.id);
    setShowUserDeleteWindow(false);
  }

  return (
    <>
      <Button value='Delete worker' size='small' color='red' onClick={() => setShowUserDeleteWindow(true)}/>

      <PopupWindow title={`Delete user ${props.user.lastName} ${props.user.firstName}`} open={showUserDeleteWindow}>
        <div>
          <Button value="Ok" onClick={handleUserDelete}/>
          <Button value="Cancel" onClick={() => setShowUserDeleteWindow(false)} color="red" />
        </div>
      </PopupWindow>
    </>
  );
}