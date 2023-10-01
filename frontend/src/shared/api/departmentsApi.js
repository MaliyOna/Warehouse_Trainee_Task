import { api } from "./api";

export async function getDepartments() {
    const response = await api.get(`/departments`);
    return response.data;
}

export async function getDepartmentById(departmentId) {
    const response = await api.get(`/departments/${departmentId}`);
    return response.data;
}

export async function createDepartment(newDepartment) {
    await api.post(`/departments`, newDepartment);
}

export async function getDepartment(departmentId) {
    const response = await api.get(`/departments/${departmentId}`);
    return response.data;
}

export async function getProductsByDepartment(departmentId) {
    const response = await api.get(`/departments/${departmentId}/products`);
    return response.data;
}

export async function getWorkersByDepartment(departmentId) {
    const response = await api.get(`/departments/${departmentId}/workers`);
    return response.data;
}

export async function updateDepartment(department) {
    await api.put(`/departments`, department);
}

export async function addDepartmentWorker(departmentId, workerId) {
    await api.put(`/departments/${departmentId}/workers/${workerId}`);
}

export async function deleteDepartmentWorker(departmentId, workerId) {
    await api.delete(`/departments/${departmentId}/workers/${workerId}`);
}

export async function deleteDepartment(departmentId) {
    await api.delete(`/departments/${departmentId}`);
}