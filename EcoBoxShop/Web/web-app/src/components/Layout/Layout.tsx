import { FC, ReactNode} from 'react';
import {
    Box,
    CssBaseline,
} from '@mui/material';
import NavBar from '../NavBar';
import Footer from '../Footer';
import React from 'react';

interface LayoutProps {
    children: ReactNode
};

const Layout: FC<LayoutProps> = ({ children }) => {
    return (
        <>
            <CssBaseline />
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'flex-start',
                    minHeight: '100vh',
                    maxWidth: '100vw',
                    flexGrow: 1
                }}
            >
                <NavBar />
                {children}
                <Footer />
            </Box>
        </>
    );
};

export default Layout;