import { makeAutoObservable, runInAction } from "mobx";
import { getItems } from "../../api/modules/catalog";
import { IItem } from "../../interfaces/item";

class ItemsStore {
    item: IItem[] = [];
    currentPage = 1;
    totalPages = 0;
    pageSize = 8;
    brand = 0;
    subCategory = 0;
    filters: Record<string, number> = {};
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        this.filters = null!;
        runInAction(this.prefetchData)
    };

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    };

    async reset() {
        this.currentPage = 1;
        this.filters = null!;
        await this.prefetchData();
    }

    async back() {
        await this.prefetchData();
    }

    async makeSubCategoryFilter(id: number){
        this.subCategory = id;
        this.makeFilter();
        await this.prefetchData();
    }
    async makeBrandFilter(id: number){
        this.brand = id;
        this.makeFilter();
        await this.prefetchData();
    }
    async makeFilter(){
        if(this.subCategory !== 0 && this.brand === 0)
        {
            this.filters = {subCategory:this.subCategory};
        }
        else if(this.brand !== 0 && this.subCategory === 0)
        {
            this.filters = {brand:this.brand};
        }
        else if(this.subCategory !== 0 && this.brand !== 0)
        {
            this.filters = { brand:this.brand, subCategory:this.subCategory};
        }
        else if(this.subCategory === 0 && this.brand === 0)
        {
            this.filters = null!;
        }
        await this.prefetchData();
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const pageIndex = this.currentPage - 1;
            const result = await getItems(pageIndex, this.pageSize, this.filters);
            this.item = result.data;
            this.totalPages = Math.ceil(result.count / this.pageSize);
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }
}

export default ItemsStore;