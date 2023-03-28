import { observer } from "mobx-react-lite";
import {
    Box,
    CircularProgress,
    Container,
    Grid,
    CardContent,
    Card,
    Typography,
} from '@mui/material';
import { FC, ReactElement, useContext, useEffect } from "react";
import { AppStoreContext } from "../../App";
import OrdersStore from "./OrdersStore";
import OrderCard from "./components/OrderCard";


const store = new OrdersStore();

const Orders: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    useEffect(() => {
        store.getOrders(app.authStore.user?.profile.sub!)
    }, [])

    return (
        <Box
            sx={{
                flexGrow: 1,
                display: 'flex',
                justifyContent: 'center',
                backgroundColor: 'whitesmoke'
            }}
        >
            <Container>
            {store.isLoading ? (
                                <CircularProgress />
                            ) : (
                            <Box>
                            {store.orders?.map((order) => (
                                <>
                                    <Grid key={order.orderListId} justifyContent="center" item my={1} lg={3} md={4} sm={6} xs={12} >
                                        <Card
                                        sx={{
                                            maxWidth: "100vw",
                                            minWidth: "100vh",
                                            display: "flex",
                                            justifyContent: "center"
                                                }}>
                                                <Grid>
                                                <CardContent>
                                                    <Typography noWrap gutterBottom variant='h6' component='div'>
                                                        Order id {order.orderListId} of user: {app.authStore.user?.profile.name!}
                                                    </Typography>
                                                    {store.isLoading ? (
                                                        <CircularProgress />
                                                    ) : (
                                                    <Box>
                                                    {order.orderListItems?.map((orderItem) => (
                                                        <>
                                                            <Grid key={orderItem.itemId} justifyContent="center" item my={1} lg={3} md={4} sm={6} xs={12} >
                                                                <Box style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                                                    <OrderCard {...{ orderListItem: orderItem, isClicable: true, }} />
                                                                </Box>
                                                            </Grid>
                                                        </>
                                                    ))}
                                                    </Box>
                                                        )}
                                                        <Typography textAlign='end'>
                                                            Total Cost: {order.totalCost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                                                        </Typography>
                                                        <Typography textAlign='end'>
                                                            Your order is coming to you...
                                                        </Typography>
                                                    </CardContent>
                                                </Grid>
                                        </Card>
                                    </Grid>
                                </>
                            ))}
                            </Box>
                         )}          
            </Container>
        </Box >
    );
}

export default observer(Orders);