import React from 'react';
import { MenuList } from '../MenuList/MenuList';
import { MenuItem } from '../MenuItem/MenuItem';

export function Menu() {
  return (
    <MenuList>
      <MenuItem route="/departments" text="Departments"></MenuItem>
      <MenuItem route="/workers" text="Workers"></MenuItem>
    </MenuList>
  );
}