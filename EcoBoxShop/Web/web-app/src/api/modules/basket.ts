import apiClient from '../client';

const url = "http://www.alevelwebsite.com:5003/api/v1/basketbff"

export const add = (userId: string, itemId: number, catalogItemId: number, title: string, subTitle: string, pictureUrl: string, price: number) => apiClient({
    path: `${url}/addtobasket`,
    method: 'POST',
    data: { userId, itemId, catalogItemId, title, subTitle, pictureUrl, price }
});

export const deletefrombasket = (userId: string, itemId: number) => apiClient({
    path: `${url}/deleteitemfrombasket`,
    method: 'POST',
    data: { userId, itemId }
});

export const createorder = (userId: string) => apiClient({
    path: `${url}/createorder`,
    method: 'POST',
    data: { userId }
});

export const getBasket = (userId: string) => apiClient({
    path: `${url}/getbasket`,
    method: 'POST',
    data: { userId }
});
