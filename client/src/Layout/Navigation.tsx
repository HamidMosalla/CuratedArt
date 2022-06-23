import { Button, Menu, MenuItem, useTheme } from "@mui/material";
import { NavLink } from "react-router-dom";
import React from "react";
import { LIGHT_MODE_THEME } from "../utils/constants";

export const Navigation = () => {
    const activeStyle = { color: "#F15B2A" };
    const theme = useTheme();
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <Button
                style={{ position: "fixed" }}
                id="demo-positioned-button"
                aria-controls={open ? "demo-positioned-menu" : undefined}
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
                color={theme.palette.mode === LIGHT_MODE_THEME ? "warning" : "primary"}
                size="large"
                onClick={handleClick}
            >
                Dashboard
            </Button>
            <Menu
                id="demo-positioned-menu"
                aria-labelledby="demo-positioned-button"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: "top",
                    horizontal: "left",
                }}
                transformOrigin={{
                    vertical: "top",
                    horizontal: "left",
                }}
            >
                <MenuItem>
                    <NavLink to="/" style={activeStyle}>
                        Home
                    </NavLink>
                </MenuItem>

                <MenuItem>
                    <NavLink to="/about" style={activeStyle}>
                        About
                    </NavLink>
                </MenuItem>

                <MenuItem>
                    <NavLink to="/submissions" style={activeStyle}>
                        Submissions
                    </NavLink>
                </MenuItem>
            </Menu>
        </>
    );
};
