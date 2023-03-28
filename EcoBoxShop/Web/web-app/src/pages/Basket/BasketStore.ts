import { makeAutoObservable, runInAction } from "mobx";
import * as basketApi from "../../api/modules/basket";
import { IBasket } from "../../interfaces/basket";
import { IBasketItem } from "../../interfaces/basketItem";
import AuthStore from "../../stores/AuthStore";

class BasketStore {
    store = new AuthStore();
    basket: IBasket[] = [];
    basketItems: IBasketItem[] = [];
    totalCost = 0;
    isLoading = false;
    userId = '';
    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }
    private check = (userId: string | undefined) => {
        if (userId) {
            this.userId = userId;
        }
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            this.check(this.userId);
            const result = await basketApi.getBasket(this.userId);
            this.basket = result;
            this.basketItems = result.basketList;
            this.totalCost = result.totalCost;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }

    async add(userId: string, itemId: number, catalogItemId: number, title: string, subTitle: string, pictureUrl: string, price: number) {
        this.check(userId)
        await basketApi.add(this.userId, itemId, catalogItemId, title, subTitle, pictureUrl, price);
    }

    async remove(userId: string, itemId: number) {
        this.check(userId);
        await basketApi.deletefrombasket(this.userId, itemId);
        await this.prefetchData();
    }

    async makeAnOrder(userId: string) {
        this.check(userId);
        await basketApi.createorder(this.userId);
        await this.prefetchData();
    }

    async get(userId: string) {
        this.check(userId);
        await this.prefetchData();
    }
}

export default BasketStore;