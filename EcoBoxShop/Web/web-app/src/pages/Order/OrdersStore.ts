import { makeAutoObservable, runInAction } from "mobx";
import { IOrder } from "../../interfaces/order";
import { getOrder } from '../../api/modules/order'
import { IOrderItem } from "../../interfaces/orderItem";

class OrdersStore {
    orders: IOrder[] = []
    orderItem: IOrderItem[] = []
    orderNumber = 0;
    userId = '';
    totalCost = 0;
    orderCount = 0;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }

    private check = (userId: string | undefined) => {
        if (userId) {
            this.userId = userId;
        }else{
            window.location.href = '/errorlogin';
        }
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const result = await getOrder(this.userId)
            this.orders = result.data;
            this.orderItem = result.data.orderListItems;
            this.totalCost = result.data.totalCost;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }

    async getOrders(userId: string) {
        this.check(userId);
        const result = await getOrder(this.userId);
        this.orders = result.data;
        this.orderItem = result.data.orderListItems;
        this.orderNumber = result.data.orderListId;
        await this.prefetchData();
    }
}

export default OrdersStore;