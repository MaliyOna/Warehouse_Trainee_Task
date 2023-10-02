import React from 'react';
import './PageHead.scss';
import { useNavigate } from 'react-router-dom';
import { removeToken } from '../../helpers/token';

export function PageHead() {
  const navigate = useNavigate();

  function handleLogoutClick() {
    removeToken();
    navigate(`/login`);
  }

  return (
    <div className='pageHead'>
      <div className='pageHead__content'>
        <div className='pageHead__text'>
          <p>Warehouse</p>
        </div>
        <div className='pageHead__logout' onClick={handleLogoutClick}>
          <span>Logout</span>
        </div>
      </div>
    </div>
  );
}