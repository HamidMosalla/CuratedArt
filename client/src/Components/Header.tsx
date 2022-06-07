import React from "react";
import { Helmet } from "react-helmet-async";
import { NavLink } from "react-router-dom";
import favicon from "../assets/curated-art-icon.png";

const Header = () => {
    const activeStyle = { color: "#F15B2A" };
    return (
        <>
            <Helmet htmlAttributes={{ lang: "en-US" || undefined }}>
                <title>{"Curated Art"}</title>
                <meta property="og:title" content="Curated Art" />
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
