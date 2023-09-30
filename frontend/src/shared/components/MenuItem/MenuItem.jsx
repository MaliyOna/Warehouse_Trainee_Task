import React from 'react';
import { Button } from '../Button/Button';
import './MenuItem.scss';
import { useNavigate } from 'react-router-dom';

export function MenuItem(props) {
  const navigate = useNavigate();

  function handleRedirect () {
    navigate(props.route);
  }

  return (
      <div className="menuItem">
        <Button value={props.text} onClick={handleRedirect}></Button>
      </div>
  );
}