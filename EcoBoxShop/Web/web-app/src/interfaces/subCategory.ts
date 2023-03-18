import { ICategory } from "./category";

export interface ISubCategory {
    "id": number,
    "title": string,
    "category": ICategory
}