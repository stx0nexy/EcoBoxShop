import { ReactElement, FC, useContext } from 'react';
import { Card,
    CardActionArea,
    CardContent,
    CardMedia,
    Typography,
    IconButton,
    Button,
    Snackbar,
    Grid
} from '@mui/material';
import { AppStoreContext } from '../../../App';
import { IItem } from '../../../interfaces/item';
import { useNavigate } from 'react-router-dom';
import ShoppingBasketIcon from '@mui/icons-material/ShoppingBasket';
import CloseIcon from '@mui/icons-material/Close';
import React from 'react';
import BasketStore from '../../Basket/BasketStore';
import { IBasketItem } from '../../../interfaces/basketItem';

interface ItemCardProps {
    basketItem: IBasketItem,
    isClicable: boolean
};
const store = new BasketStore();
const BasketCard: FC<ItemCardProps> = (card): ReactElement => {
    const app = useContext(AppStoreContext);
    const navigate = useNavigate();
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
        <Card
            sx={{ width: "50vw", height: "20vh" }}
        >
            <Grid container>
                <Grid item xs={3}>
                    <CardMedia
                        sx={{ width: 200, height: 200 }}
                        component='img'
                        image={card.basketItem?.pictureUrl}
                    />
                </Grid>
                <Grid item xs={7}>
                <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    {card.basketItem?.title}
                </Typography>
                <Typography noWrap gutterBottom variant='subtitle2' component='div'>
                    {card.basketItem?.subTitle}
                </Typography>
                <Typography variant='body2' textAlign='end' color='text.secondary'>
                    {card.basketItem?.price.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                </Typography>
                    </CardContent>
                </Grid>
                <Grid item xs={2}>
                    <Typography textAlign='end'
                    onClick={() => store.remove(app.authStore.user?.profile.sub!, card.basketItem?.itemId)}>
                        <Button onClick={handleClick}><CloseIcon /></Button>
                        <Snackbar
                            open={open}
                            autoHideDuration={6000}
                            onClose={handleClose}
                            message="Item removed from basket"
                            action={action}
                        />
                    </Typography>
                </Grid>
            </Grid>
        </Card>
    );
};

export default BasketCard;