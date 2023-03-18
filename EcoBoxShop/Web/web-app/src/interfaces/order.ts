import { IOrderItem } from "./orderItem";

export interface IOrder {
    "id": number,
    "userId": string,
    "items": IOrderItem[],
}