import { Box, Tab, Tabs } from "@mui/material";
import { Link, matchPath, useLocation } from "react-router-dom";

export const Navigation = () => {
    const location = useLocation();

    let currentPath = "/";
    if (!!matchPath("/about/*", location.pathname)) {
        currentPath = "/about";
    } else if (!!matchPath("/submissions/*", location.pathname)) {
        currentPath = "/submissions";
    }

    return (
        <>
            <Box>
                <Tabs value={currentPath} aria-label="Navigation Tabs" indicatorColor="secondary" textColor="inherit">
                    <Tab label={"Home"} component={Link} to="/" value="/" />
                    <Tab label={"About"} component={Link} to="/about" value="/about" />
                    <Tab label={"Submissions"} component={Link} to="/submissions" value="/submissions" />
                </Tabs>
            </Box>
        </>
    );
};
