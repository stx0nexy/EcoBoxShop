import { IOrderItem } from "./orderItem";

export interface IOrder {
    "orderListId": number,
    "userId": string,
    "totalCost":  number,
    "orderListItems": IOrderItem[],
}