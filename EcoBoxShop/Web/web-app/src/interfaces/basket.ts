import { IBasketItem } from "./basketItem";

export interface IBasket {
    "userId": string,
    "items": IBasketItem[],
}