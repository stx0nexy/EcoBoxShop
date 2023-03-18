import { FC } from 'react';

export interface Route {
    place: string,
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<{}>
};