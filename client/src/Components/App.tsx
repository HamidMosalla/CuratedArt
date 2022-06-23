import { BrowserRouter, Route, Routes } from "react-router-dom";
import HomePage from "./HomePage";
import AboutPage from "./About";
import PageNotFound from "./PageNotFound";
import ArtSubmission from "./ArtSubmission";
import { DARK_MODE_THEME, LIGHT_MODE_THEME } from "../utils/constants";
import { getAppTheme } from "../styles/theme";
import { ThemeModeContext } from "../contexts/ThemeModeContext";
import { ThemeProvider } from "@mui/material";
import { CssBaseline } from "@mui/material";
import { useMemo, useState } from "react";
import { Layout } from "../Layout";

function App() {
    const [mode, setMode] = useState<typeof LIGHT_MODE_THEME | typeof DARK_MODE_THEME>(DARK_MODE_THEME);

    const themeMode = useMemo(
        () => ({
            toggleThemeMode: () => {
                setMode((prevMode) => (prevMode === LIGHT_MODE_THEME ? DARK_MODE_THEME : LIGHT_MODE_THEME));
            },
        }),
        []
    );

    const theme = useMemo(() => getAppTheme(mode), [mode]);

    return (
        <ThemeModeContext.Provider value={themeMode}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <div className="container-fluid">
                    <Layout>
                        <BrowserRouter>
                            <Routes>
                                <Route path="/" element={<HomePage />} />
                                <Route path="/about" element={<AboutPage />} />
                                <Route path="/submissions" element={<ArtSubmission intitialTitle="Mandy" />} />
                                <Route element={<PageNotFound />} />
                            </Routes>
                        </BrowserRouter>
                    </Layout>
                </div>
            </ThemeProvider>
        </ThemeModeContext.Provider>
    );
}

export default App;
