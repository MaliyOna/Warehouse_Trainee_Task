import {api} from "./api";

export async function login(userName, password) {
    const response = await api.post(`/auth/login`, {userName, password});
    return response.data;
}