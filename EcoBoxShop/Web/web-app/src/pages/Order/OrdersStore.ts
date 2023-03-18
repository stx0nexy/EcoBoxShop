import { makeAutoObservable, runInAction } from "mobx";
import { IOrder } from "../../interfaces/order";
import { getOrder } from '../../api/modules/order'

class OrdersStore {
    orders: IOrder[] = []
    currentPage = 1;
    pageSize = 50;
    userId = '';
    orderCount = 0;
    isLoading = false;

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
            const result = await getOrder(this.userId)
            this.orderCount = result.ordersCount;
            this.orders = result.orders;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    }

    async getOrders(userId: string) {
        this.check(userId);
        await this.prefetchData();
    }
}

export default OrdersStore;