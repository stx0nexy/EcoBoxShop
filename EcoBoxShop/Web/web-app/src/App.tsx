import { CssBaseline, ThemeProvider } from '@mui/material';
import { createTheme } from '@mui/material/styles';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { routes as appRoutes } from './routes';
import AuthStore from './stores/AuthStore';
import Layout from './components/Layout/./Layout'
import { IAppStore } from './interfaces/appStore';
import { createContext, useState } from 'react';

const store: IAppStore = {
  'authStore': new AuthStore()
};
export const AppStoreContext = createContext(store);

function App() {
  const theme = createTheme({
    palette: {
      primary: {
        light: "#85dcb",
        main: "#e8a87c",
        dark: "#c38d9e",
        contrastText: "#000",
      },
      secondary: {
        main: "#7395ae",
        light: "#edf5e1",
        dark: "#e27d60",
        contrastText: "#000",
      },
    },
  });

  const [appStore, setAppStore] = useState(store)

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <AppStoreContext.Provider value={appStore}>
          <Layout>
            <Routes>
              {appRoutes.map((route) => (
                <Route
                  key={route.key}
                  path={route.path}
                  element={<route.component />}
                />
              ))}
            </Routes>
          </Layout>
        </AppStoreContext.Provider>
      </Router>
    </ThemeProvider>
  );
};

export default App;
