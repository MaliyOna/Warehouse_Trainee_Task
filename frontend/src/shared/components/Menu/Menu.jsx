import React from 'react';
import { MenuList } from '../MenuList/MenuList';
import { MenuItem } from '../MenuItem/MenuItem';

export function Menu() {
  return (
    <MenuList>
      <MenuItem route="/departments" text="Отделы"></MenuItem>
      <MenuItem route="/workers" text="Работники"></MenuItem>
    </MenuList>
  );
}