import { observer } from "mobx-react-lite";
import {
    Box,
    CircularProgress,
    Container,
    Grid,
    List,
    ListItem,
    ListItemText,
    IconButton,
    Pagination,
    Typography,
    Divider,
    Accordion,
    AccordionSummary,
    AccordionDetails,
} from '@mui/material';
import { FC, ReactElement, useContext, useEffect } from "react";
import { AppStoreContext } from "../../App";
import OrdersStore from "./OrdersStore";


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
                        {store.orders?.map((item) => (
                            <>
                                <Accordion>
                                        <Grid container justifyContent='space-between' direction="row">
                                            <Grid item>
                                                <ListItemText primary={item.id} />
                                            </Grid>
                                            <Grid item>
                                                <ListItemText primary={item.userId} />
                                            </Grid>
                                        </Grid>
                                    <AccordionDetails>
                                        <ListItemText primary="Details for item 1" />
                                    </AccordionDetails>
                                </Accordion>
                                <Divider />
                            </>
                        ))}
                    </List>
                )}
            </Container>
        </Box >
    );
}

export default observer(Orders);