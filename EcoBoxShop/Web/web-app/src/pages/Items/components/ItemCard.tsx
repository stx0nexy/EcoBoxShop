import { ReactElement, FC, useContext } from 'react';
import { Card,
    CardActionArea,
    CardContent,
    CardMedia,
    Typography,
    IconButton,
    Button,
    Snackbar,
    Box
} from '@mui/material';
import { AppStoreContext } from '../../../App';
import { IItem } from '../../../interfaces/item';
import { useNavigate } from 'react-router-dom';
import ShoppingBasketIcon from '@mui/icons-material/ShoppingBasket';
import CloseIcon from '@mui/icons-material/Close';
import React from 'react';
import BasketStore from '../../Basket/BasketStore';

interface ItemCardProps {
    item: IItem,
    isClicable: boolean
};
const store = new BasketStore();
const ItemCard: FC<ItemCardProps> = (card): ReactElement => {
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
            sx={{
                maxWidth: 250,
                minWidth: 250,
                display: "flex",
                justifyContent: "center"
            }}
        >
            {
                card.isClicable &&
                <CardActionArea>
                   <>
            <CardMedia
                component='img'
                height='250'
                image={card.item?.pictureUrl}
                alt={card.item?.title}
                onClick={() => navigate(`/item/${card.item?.id}`)}
            />
            <CardContent>
                <Typography noWrap gutterBottom variant='h6' component='div'
                onClick={() => navigate(`/item/${card.item?.id}`)}>
                    {card.item?.title}
                </Typography>
                <Typography noWrap gutterBottom variant='subtitle2' component='div'
                onClick={() => navigate(`/item/${card.item?.id}`)}>
                    {card.item?.subTitle}
                </Typography>
                <Typography variant='body2' color='text.secondary'>
                    {card.item?.price.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}
                </Typography>
                <Box textAlign='end'
                onClick={() => store.add(app.authStore.user?.profile.sub!, card.item?.id, card.item?.id, card.item?.title, card.item?.subTitle, card.item?.pictureUrl, card.item?.price)}>
                    <Button onClick={handleClick}><ShoppingBasketIcon /></Button>
                    <Snackbar
                        open={open}
                        autoHideDuration={6000}
                        onClose={handleClose}
                        message="Item added to the basket"
                        action={action}
                    />
                </Box>
            </CardContent>
        </>
                </CardActionArea>
            }
        </Card >
    );
};

export default ItemCard;