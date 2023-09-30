import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Input } from '../../shared/components/Input/Input';
import './LoginPage.scss';
import { Button } from '../../shared/components/Button/Button';
import { Form } from '../../shared/components/Form/Form';
import { login } from '../../shared/api/authApi';
import { setToken } from '../../shared/helpers/token';
import toast from 'react-hot-toast';

export function LoginPage() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    async function handleLogin() {
        try {
            const token = await login(userName, password);
            setToken(token);
            navigate("/workers");
        } catch (error) {
            toast.error("Неправильное имя или пароль")
            setPassword("");
        }
    }

    return (
        <div className='loginPage'>
            <div className='loginPage__title'>Вход</div>
            <Form className='loginPage__form' onSubmit={handleLogin}>
                <Input name="userName"
                    label="Введите имя пользователя:"
                    type="text"
                    value={userName}
                    onChange={event => setUserName(event.target.value)}
                    rules={{ required: "Обязательное поле" }} />

                <Input name="password"
                    label="Введите пароль:"
                    type="password"
                    value={password}
                    onChange={event => setPassword(event.target.value)}
                    rules={{ required: "Обязательное поле" }} />

                <Button value="Войти" type="submit" />
            </Form>
        </div>
    );
}