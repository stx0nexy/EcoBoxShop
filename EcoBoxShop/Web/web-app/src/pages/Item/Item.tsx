import { ReactElement, FC, useEffect, useState, useContext } from 'react';
import {
    Box,
    Button,
    Container,
    CircularProgress,
    Grid,
    Card,
    CardMedia,
    CardContent,
    Typography,
    Snackbar,
    IconButton
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { IItem } from '../../interfaces/item';
import { useParams } from 'react-router-dom';
import { getItem } from '../../api/modules/catalog';
import { AppStoreContext } from '../../App';
import { observer } from 'mobx-react-lite';
import BasketStore from '../Basket/BasketStore';
import React from 'react';

const Item: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    const basketStore = new BasketStore();
    const [item, setItem] = useState<IItem | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            const getCatalogItem = async () => {
                try {
                    setIsLoading(true);
                    const res = await getItem(Number(id));
                    setItem(res)
                }
                catch (e) {
                    if (e instanceof Error) {
                        console.error(e.message);
                    }
                }
                setIsLoading(false);
            };
            getCatalogItem();
        };
    }, [id])

    const addToBasket = async () => {
        await basketStore.add(app.authStore.user?.profile.sub!, item?.id!, item?.id!, item?.title!, item?.subTitle!, item?.pictureUrl!, item?.price!);
    }

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
        <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
            }}
        >
            <Container>
                <Grid item container justifyContent='center'>
                    {isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {!!item &&
                                <Card>
                                    <Grid container>
                                        <Grid item>
                                            <CardMedia
                                                sx={{ width: 400, height: 400 }}
                                                component='img'
                                                image={item.pictureUrl}
                                            />
                                        </Grid>
                                        <Grid item>
                                            <CardContent>
                                                <Typography variant="h4" gutterBottom>
                                                    {item.title}
                                                </Typography>
                                                <Typography variant="h5" gutterBottom>
                                                    {item.subTitle}
                                                </Typography>
                                                <Typography variant="h6" gutterBottom>
                                                    {item.description}
                                                </Typography>
                                                
                                            </CardContent>
                                        </Grid>
                                    </Grid>
                                    <Typography variant="h5" textAlign='right'>
                                        {item.price.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                                    </Typography>
                                    <Box textAlign='end'
                                    onClick={addToBasket}>
                                        <Button variant='contained' color="secondary"
                                         onClick={handleClick}>Add To Basket
                                        </Button>
                                        <Snackbar
                                            open={open}
                                            autoHideDuration={6000}
                                            onClose={handleClose}
                                            message="Item added to the basket"
                                            action={action}
                                        />
                                    </Box>
                                </Card>
                            }
                        </>
                    )}
                </Grid>
            </Container>
        </Box>
    );
};

export default observer(Item);