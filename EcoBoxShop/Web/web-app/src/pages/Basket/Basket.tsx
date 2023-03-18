import {
    Box, Button, Container, CircularProgress, Grid, Typography, List,
    ListItem,
    ListItemText,
    IconButton,
    Divider,
} from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, ReactElement, useContext, useEffect } from "react";
import { AppStoreContext } from "../../App";
import BasketStore from "./BasketStore";
import BasketCard from './components/BasketCard';

const store = new BasketStore();
const id = 5;
const Basket: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    useEffect(() => {
        store.get(app.authStore.user?.profile.sub!);
    }, [])

    return (
        <>
            <Box
                sx={{
                    flexGrow: 1,
                    backgroundColor: 'whitesmoke',
                    display: 'flex',
                    justifyContent: 'center',
                }}
            >
                <Container>
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <List sx={{ width: '100%', height: '80%', overflow: 'auto' }}>
                            {store.basketItems?.map((item) => (
                                <>
                                   <Grid key={item.itemId} justifyContent="center" item my={1} lg={3} md={4} sm={6} xs={12} >
                                    <Box style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                        <BasketCard {...{ basketItem: item, isClicable: true, }} />
                                    </Box>
                                </Grid>
                                    <Divider />
                                </>
                            ))}
                        </List>
                    )}
                    <Container>
                        <Grid container justifyContent="end">
                            <Grid item>
                                <Button variant="contained" onClick={() => store.makeAnOrder(app.authStore.user?.profile.sub!)}>
                                    Make Order
                                </Button>
                            </Grid>
                        </Grid>
                    </Container>
                </Container>
            </Box >
        </>
    );
}

export default observer(Basket);