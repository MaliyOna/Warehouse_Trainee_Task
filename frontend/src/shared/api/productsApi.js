import { api } from "./api";

export async function createProduct(newProduct) {
    await api.post(`/products`, newProduct);
}


export async function updateProduct(product) {
    await api.put(`/products`, product);
}

export async function deleteProduct(productId) {
    await api.delete(`/products/${productId}`);
}