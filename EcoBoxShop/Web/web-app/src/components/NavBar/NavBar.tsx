import { FC, ReactElement, useState, useEffect } from 'react';
import {
    Box,
    Link,
    Container,
    IconButton,
    Menu,
    MenuItem,
    Toolbar,
    Typography
} from '@mui/material';
import { routes } from '../../routes';
import { NavLink } from 'react-router-dom';
import UserAuth from '../UserAuth/UserAuth';
import { observer } from 'mobx-react-lite';
import React from 'react';

const siteLabel = 'Eco Box'

const NavBar: FC = (): ReactElement => {
    const [anchorElNav, setAnchorElNav] = useState(null);

    const handleOpenNavMenu = (event: any) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    return (
        <Box alignContent='space-between' sx={{ width: '100%', height: 'auto', backgroundColor: 'primary.dark', color: 'white' }}>
            <Container maxWidth='xl'>
                <Toolbar disableGutters>
                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                        <IconButton
                            size='large'
                            aria-label='account of current user'
                            aria-controls='menu-appbar'
                            aria-haspopup='true'
                            onClick={handleOpenNavMenu}
                            color='inherit'
                        >
                        </IconButton>
                        <Menu
                            id='menu-appbar'
                            anchorEl={anchorElNav}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left'
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left'
                            }}
                            open={Boolean(anchorElNav)}
                            onClose={handleCloseNavMenu}
                            sx={{
                                display: {
                                    xs: 'block',
                                    md: 'none'
                                }
                            }}>
                            {routes.map((page) => (
                                (page.place == 'NavBar' && !!page.enabled) && <Link
                                    key={page.key}
                                    component={NavLink}
                                    to={page.path}
                                    color='black'
                                    underline='none'
                                    variant='button'
                                >
                                    <MenuItem onClick={handleCloseNavMenu}>
                                        <Typography textAlign='center'>
                                            {page.title}
                                        </Typography>
                                    </MenuItem>
                                </Link>
                            ))}
                        </Menu>
                    </Box>
                    <Typography variant='h6' noWrap component='div' sx={{ justifyContent: 'center', flexGrow: 3, display: { xs: 'flex', md: 'none' } }}>
                        {siteLabel}
                    </Typography>
                    <Typography variant='h5' noWrap sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}>
                        {siteLabel}
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'flex-start', alignItems: 'center', marginLeft: '1rem' }}>
                            {routes.map((page) => (
                                (page.place == 'NavBar' && !!page.enabled) && <Link
                                    key={page.key}
                                    component={NavLink}
                                    to={page.path}
                                    color='white'
                                    underline='none'
                                    variant='button'
                                    sx={{
                                        fontSize: 'large',
                                        marginLeft: '2rem'
                                    }}
                                >
                                    {page.title}
                                </Link>
                            ))}
                        </Box>
                    </Box>
                    <Box sx={{ flexGrow: 1, justifyContent: 'end', display: 'flex' }}>
                        <UserAuth />
                    </Box>
                </Toolbar >
            </Container >
        </Box >
    );
};

export default observer(NavBar);