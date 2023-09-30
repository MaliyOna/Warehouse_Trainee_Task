import React from 'react';
import './MenuList.scss';

export function MenuList(props) {

  return (
    <div className="menu">
      <input id="menu__input" type="checkbox" />
      <label className="menu__button" htmlFor="menu__input">
        <span></span>
      </label>

      <div className='menuItems'>
        <h2 className='menuName'>Меню</h2>
        {props.children}
      </div>
    </div>
  );
}