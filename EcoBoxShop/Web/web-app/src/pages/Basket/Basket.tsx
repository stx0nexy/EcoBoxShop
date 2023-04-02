import {
    Box, 
    Button, 
    Container, 
    CircularProgress, 
    Grid, 
    Typography, 
    List,
    Snackbar,
    IconButton,
    Divider,
} from "@mui/material";
import { observer } from "mobx-react-lite";
import { FC, ReactElement, useContext, useEffect } from "react";
import { AppStoreContext } from "../../App";
import BasketStore from "./BasketStore";
import CloseIcon from '@mui/icons-material/Close';
import BasketCard from './components/BasketCard';
import React from "react";

const store = new BasketStore();
const Basket: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    useEffect(() => {
        store.get(app.authStore.user?.profile.sub!);
    }, [])
    const [open, setOpen] = React.useState(false);

    const handleClick = () => {
        setOpen(true);
    };

    const handleClose = (event: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === 'clickaway') {
        return;
        }

        setOpen(false);
    };
    const action = (
        <React.Fragment>
        <IconButton
            size="small"
            aria-label="close"
            color="inherit"
            onClick={handleClose}
        >
            <CloseIcon fontSize="small" />
        </IconButton>
        </React.Fragment>
    );

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
                            <Typography textAlign='end'>
                                Total Cost: {store.totalCost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                            </Typography>
                        </Grid>
                            <Grid item>
                                <Box textAlign='end'
                                onClick={() => store.makeAnOrder(app.authStore.user?.profile.sub!)}>
                                    <Button variant="contained" onClick={handleClick}>Make Order</Button>
                                    <Snackbar
                                        open={open}
                                        autoHideDuration={6000}
                                        onClose={handleClose}
                                        message="The order has been sent. Check it in orders"
                                        action={action}
                                    />
                                </Box>
                            </Grid>
                        </Grid>
                    </Container>
                </Container>
            </Box >
        </>
    );
}

export default observer(Basket);