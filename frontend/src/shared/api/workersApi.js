import { api } from "./api";

export async function getWorkers() {
    const response = await api.get(`/workers`);
    return response.data;
}

export async function createWorker(user) {
    await api.post(`/workers`, user);
}

export async function updateWorker(user) {
    await api.put(`/workers`, user);
}

export async function deleteWorker(userId) {
    await api.delete(`/workers/${userId}`);
}