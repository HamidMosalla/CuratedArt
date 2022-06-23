import { Helmet } from "react-helmet-async";
import favicon from "../assets/curated-art-icon.png";
import { APP_TITLE, APP_DESCRIPTION } from "../utils/constants";
import { AppBar, Box, Toolbar } from "@mui/material";
import { ThemeSwitcher } from "../components/Actions/ThemeSwitcher";
import { Navigation } from "./Navigation";

export const Header = () => {
    return (
        <>
            <Helmet htmlAttributes={{ lang: "en-US" || undefined }}>
                <title>{APP_TITLE}</title>
                <meta name="description" content={APP_DESCRIPTION} />
                <meta property="og:title" content={APP_TITLE} />
                <meta name="viewport" content="initial-scale=1, width=device-width" />
                <link rel="shortcut icon" href={favicon} type="image/x-icon" />
            </Helmet>

            <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
                <Navigation />

                <Toolbar disableGutters variant="dense">
                    <Box sx={{ flexGrow: 1 }} />
                    <Box sx={{ display: { xs: "none", md: "flex", alignItems: "center" } }}>
                        <ThemeSwitcher />
                    </Box>
                </Toolbar>
            </AppBar>
        </>
    );
};
