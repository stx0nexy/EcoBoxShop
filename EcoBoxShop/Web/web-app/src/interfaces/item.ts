import { IBrand } from "./brand";
import { ISubCategory } from "./subCategory";

export interface IItem {
    "id": number,
    "title": string,
    "subTitle": string,
    "description": string,
    "pictureUrl": string,
    "price": number,
    "availableStock": number,
    "subCategory": ISubCategory,
    "brand": IBrand
}