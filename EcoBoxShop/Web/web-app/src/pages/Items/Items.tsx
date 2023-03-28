import { ReactElement, FC, useEffect, useState } from 'react';
import {
    Box,
    CircularProgress,
    Container,
    Grid,
    Pagination,
    ListItem,
    List
} from '@mui/material';
import * as React from 'react';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import ItemsStore from './ItemsStore';
import CategoryStore from './CategoryStore';
import { observer } from 'mobx-react-lite';
import ItemCard from './components/ItemCard';

const store = new ItemsStore();
const categoryStore = new CategoryStore();
const Items: FC<any> = (): ReactElement => {
    useEffect(() => {
        store.reset();
    }, [])

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
    setAnchorEl(null);
    };
    return (
        <Box>
            <Box>
                {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                        {categoryStore.category?.map((category) => (
                            <Box sx={{ display: 'inline-block', 
                                        gap: 2, 
                                        justifyContent: 'center',
                                        alignItems: 'center'}}>
                                <Box 
                                onClick={() => categoryStore.chooseCategory(category.title)}>
                                    <Button
                                        id="basic-button"
                                        aria-controls={open ? 'basic-menu' : undefined}
                                        aria-haspopup="true"
                                        aria-expanded={open ? 'true' : undefined}
                                        onClick={handleClick}
                                    >
                                        {category.title}
                                    </Button>
                                </Box>
                            </Box>
                        ))}
                        <Button 
                                onClick={() => store.makeSubCategoryFilter(0)}>
                                    All Categories
                                </Button>
                        </>
                )}
                <Menu
                    id="basic-menu"
                    anchorEl={anchorEl}
                    open={open}
                    onClose={handleClose}
                    MenuListProps={{
                    'aria-labelledby': 'basic-button',
                    }}
                >
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {categoryStore.subCategory?.map((subCategory) => (
                                <Box
                                onClick={() => store.makeSubCategoryFilter(subCategory.id)}
                                >
                                <MenuItem
                                 onClick={handleClose}>
                                    {subCategory.title}
                                </MenuItem>
                                </Box>
                            ))}
                        </>
                    )}
                </Menu>
                         
                </Box>
        <Box
            sx={{
                flexGrow: 1,
                backgroundColor: 'whitesmoke',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}
        >
            <Grid container spacing={2}>
            <Grid item xs={1}>
                <List>
                {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {categoryStore.brand?.map((brand) => (
                                <ListItem>
                                <Button 
                                color="secondary"
                                onClick={() => store.makeBrandFilter(brand.id)}>
                                    {brand.title}
                                </Button>
                                </ListItem>
                            ))}
                            <ListItem>
                            <Button 
                            color="secondary"
                            onClick={() => store.makeBrandFilter(0)}>
                                    All Brands
                                </Button>
                            </ListItem>
                        </>
                    )}
                </List>
            </Grid>
            <Grid item xs={11}>
            <Container>
                <Grid container justifyContent="center" my={4}>
                    {store.isLoading ? (
                        <CircularProgress />
                    ) : (
                        <>
                            {store.item?.map((item) => (
                                <Grid key={item.id} justifyContent="center" item my={1} lg={3} md={4} sm={6} xs={12} >
                                    <Box style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                        <ItemCard {...{ item: item, isClicable: true, }} />
                                    </Box>
                                </Grid>
                            ))}
                        </>
                    )}
                </Grid>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center'
                    }}>
                    <Pagination
                        count={store.totalPages}
                        page={store.currentPage}
                        onChange={async (event, page) => await store.changePage(page)} />
                </Box>
            </Container>
            </Grid>
            </Grid>
        </Box >
        </Box>
    );
};

export default observer(Items);
