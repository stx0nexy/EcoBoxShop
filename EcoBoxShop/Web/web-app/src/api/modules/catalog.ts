import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5000/api/v1/catalogbff"

export const getItem = (id: number) => apiClient({
    path: `${url}/item`,
    method: 'POST',
    data: { id: id }
});

export const getItems = (pageIndex: number, pageSize: number, filters: object) => apiClient({
    path: `${url}/items`,
    method: 'POST',
    data: { pageIndex, pageSize, filters }
});

export const getCategories = () => apiClient({
    path: `${url}/categories`,
    method: 'POST'
});

export const getSubCategories = () => apiClient({
    path: `${url}/subcategories`,
    method: 'POST'
});

export const getBrands = () => apiClient({
    path: `${url}/brands`,
    method: 'POST'
});

export const getSubCategoryByCategory = (category: string) => apiClient({
    path: `${url}/subcategoriesbycategory`,
    method: 'POST',
    data: { category: category }
});