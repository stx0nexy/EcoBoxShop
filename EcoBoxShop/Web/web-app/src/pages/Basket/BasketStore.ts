import { makeAutoObservable, runInAction } from "mobx";
import * as basketApi from "../../api/modules/basket";
import { IBasket } from "../../interfaces/basket";
import { IBasketItem } from "../../interfaces/basketItem";

class BasketStore {
    basket: IBasket[] = [];
    basketItems: IBasketItem[] = [];
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
        else {
        }
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            this.check(this.userId);
            const result = await basketApi.getBasket(this.userId);
            this.basket = result.basket;
            this.basketItems = result.basketList
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
        this.prefetchData();
    }

    async makeAnOrder(userId: string) {
        this.check(userId);
        await basketApi.createorder(this.userId);
        this.basket = [];
    }

    async get(userId: string) {
        this.check(userId);
        const result = await basketApi.getBasket(this.userId);
        this.basket = result.basketList;
        this.prefetchData();
    }
}

export default BasketStore;