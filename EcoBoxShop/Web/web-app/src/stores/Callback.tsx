import React, { FC, ReactElement, useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AppStoreContext } from "../App";


const Callback: FC<any> = (): ReactElement => {
    var app = useContext(AppStoreContext);
    var navigate = useNavigate();

    useEffect(() => {
        async function handleCallback() {
            app.authStore.handleCallback()
            navigate('/');
        }
        handleCallback();
    }, []);

return (
    <>
        Login
    </>
);
}

export default Callback; 