import React from "react";
import { Helmet } from "react-helmet-async";
import { NavLink } from "react-router-dom";
import favicon from "../assets/curated-art-icon.png";
import { APP_TITLE, APP_DESCRIPTION } from "../utils/constants";

const Header = () => {
    const activeStyle = { color: "#F15B2A" };
    return (
        <>
            <Helmet htmlAttributes={{ lang: "en-US" || undefined }}>
                <title>{APP_TITLE}</title>
                <meta name="description" content={APP_DESCRIPTION} />
                <meta property="og:title" content={APP_TITLE} />
                <meta name="viewport" content="initial-scale=1, width=device-width" />
                <link rel="shortcut icon" href={favicon} type="image/x-icon" />
            </Helmet>
            <nav>
                <NavLink to="/" style={activeStyle}>
                    Home
                </NavLink>
                {" | "}
                <NavLink to="/about" style={activeStyle}>
                    About
                </NavLink>
                {" | "}
                <NavLink to="/nana" style={activeStyle}>
                    Submissions
                </NavLink>
            </nav>
        </>
    );
};

export default Header;
