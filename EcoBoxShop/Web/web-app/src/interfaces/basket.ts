import { IBasketItem } from "./basketItem";

export interface IBasket {
    "userId": string,
    "totalCost":  number,
    "items": IBasketItem[],
}