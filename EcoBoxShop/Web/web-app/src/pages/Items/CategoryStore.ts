import { makeAutoObservable, runInAction } from "mobx";
import * as catalogApi from "../../api/modules/catalog";
import { IBrand } from "../../interfaces/brand";
import { ICategory } from "../../interfaces/category";
import { ISubCategory } from "../../interfaces/subCategory";

class CategoryStore {
    category: ICategory[] = [];
    subCategory: ISubCategory[] = [];
    brand: IBrand[] = [];
    categoryTitle = "";
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData)
    };

    async chooseCategory(category: string) {
        this.categoryTitle = category;
        await this.prefetchData();
    };

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const category = await catalogApi.getCategories();
            this.category = category.data;
            const subCategory = await catalogApi.getSubCategoryByCategory( this.categoryTitle);
            this.subCategory = subCategory.data;
            const brand = await catalogApi.getBrands();
            this.brand = brand.data;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }
}

export default CategoryStore;