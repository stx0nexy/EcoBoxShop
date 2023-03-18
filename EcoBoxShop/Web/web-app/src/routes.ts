import { Route } from './interfaces/route';
import Callback from './stores/Callback';
import Basket from './pages/Basket';
import Orders from './pages/Order/Orders';
import Items from './pages/Items';
import Item from './pages/Item';

export const routes: Array<Route> = [
    {
        place: 'NavBar',
        key: 'home-route',
        title: 'Catalog',
        path: '/',
        enabled: true,
        component: Items
    },
    {
        place: 'NavBar',
        key: 'info-user',
        title: 'Basket',
        path: '/basket',
        enabled: true,
        component: Basket
    },
    {
        place: 'void',
        key: 'callback',
        title: 'CallBack',
        path: '/callback',
        enabled: false,
        component: Callback
    },
    {
        place: 'UserMenu',
        key: 'order',
        title: 'Orders',
        path: '/order',
        enabled: true,
        component: Orders
    },
    {
        place: 'userMenu',
        key: 'item-user',
        title: 'Item Info',
        path: 'item/:id',
        enabled: false,
        component: Item
    },
] 