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
import OrdersStore from '../../Order/OrdersStore';
import { IBasketItem } from '../../../interfaces/basketItem';
import { IOrderItem } from '../../../interfaces/orderItem';

interface ItemCardProps {
    orderListItem: IOrderItem,
    isClicable: boolean
};
const store = new OrdersStore();
const OrderCard: FC<ItemCardProps> = (card): ReactElement => {
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
                        image={card.orderListItem?.pictureUrl}
                    />
                </Grid>
                <Grid item xs={7}>
                <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'>
                    {card.orderListItem?.title}
                </Typography>
                <Typography noWrap gutterBottom variant='subtitle2' component='div'>
                    {card.orderListItem?.subTitle}
                </Typography>
                <Typography variant='body2' textAlign='end' color='text.secondary'>
                    {card.orderListItem?.price.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                </Typography>
                    </CardContent>
                </Grid>
            </Grid>
        </Card>
    );
};

export default OrderCard;