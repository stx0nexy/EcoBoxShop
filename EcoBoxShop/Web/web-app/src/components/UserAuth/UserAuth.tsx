import { useContext, useState } from 'react';
import {
    Avatar,
    Link,
    IconButton,
    Menu,
    MenuItem,
    Typography,
    Button
} from '@mui/material';
import { AppStoreContext } from '../../App';
import { routes } from '../../routes';
import { NavLink } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import React from 'react';

const UserAuth = () => {
    const [anchorElUser, setAnchorElUser] = useState(null);

    const app = useContext(AppStoreContext);

    const handleOpenUserMenu = (event: any) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const signout = async () => {
        await app.authStore.logout();
    }

    const signin = async () => {
        await app.authStore.login();
    }

    return (
        <>
            {!!app.authStore.user ?
                (
                    <>
                        <IconButton
                            size='large'
                            aria-label='account of current user'
                            aria-controls='user-appbar'
                            aria-haspopup='true'
                            onClick={handleOpenUserMenu}
                            color='primary'
                        >
                            <Avatar />
                        </IconButton>
                        <Menu
                            id='user-appbar'
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left'
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left'
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                            sx={{
                                display: 'block'
                            }}>
                            {routes.map((page) => (
                                (page.place == 'UserMenu' && !!page.enabled) && <Link
                                    key={page.key}
                                    component={NavLink}
                                    to={page.path}
                                    color='black'
                                    underline='none'
                                    variant='button'
                                >
                                    <MenuItem onClick={handleCloseUserMenu}>
                                        <Typography textAlign='center'>
                                            {page.title}
                                        </Typography>
                                    </MenuItem>
                                </Link>
                            ))}
                        </Menu>
                        <Button onClick={signout}>
                        </Button>
                    </>
                ) : (
                    <>
                        <Button variant='contained' color='success' onClick={signin}>
                            Login
                        </Button>
                    </>
                )}
        </>
    );
};

export default observer(UserAuth);