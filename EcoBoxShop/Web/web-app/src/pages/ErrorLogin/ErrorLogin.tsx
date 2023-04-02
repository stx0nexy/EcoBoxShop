import { ReactElement, FC, useContext } from 'react';
import {
    Box,
    Button,
    Container,
    Grid,
    Typography,
} from '@mui/material';
import { AppStoreContext } from '../../App';
import { observer } from 'mobx-react-lite';

const ErrorLogin: FC<any> = (): ReactElement => {
    const app = useContext(AppStoreContext);
    
    const signin = async () => {
        await app.authStore.login();
    }

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
                    <Grid>
                        <Typography>
                            Sorry, you don't seem to be logged into your account.
                        </Typography>
                        <Typography>
                            Try to sign in and try again
                        </Typography>
                    </Grid>
                    <Grid>
                        <Button variant='contained' color='success' onClick={signin}>
                            Login
                        </Button>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
};

export default observer(ErrorLogin);