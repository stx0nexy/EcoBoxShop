import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5004/api/v1/orderbff"

export const getOrder = (userId: string) => apiClient({
    path: `${url}/getorder`,
    method: 'POST',
    data: { userId }
});