import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { LoginPage } from './pages/LoginPage/LoginPage';
import { Toaster } from 'react-hot-toast';
import { WorkersPage } from './pages/WorkersPage/WorkersPage';
import { DepartmentsPage } from './pages/DepartmentsPage/DepartmentsPage';

export class App extends React.Component {
  render() {
    return (
      <>
        <Toaster />
        <div className='app__page'>
          <BrowserRouter>
            <Routes>
              <Route path="/login" element={<LoginPage />} />
              <Route path="/workers" element={<WorkersPage />} />
              <Route path="/departments" element={<DepartmentsPage />} />
              <Route
                path="*"
                element={<Navigate to="/login" />}
              />
            </Routes>
          </BrowserRouter>
        </div>
      </>
    );
  }
}
