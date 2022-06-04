import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import HomePage from "./HomePage";
import AboutPage from "./About";
import Header from "./Header";
import PageNotFound from "./PageNotFound";
import ArtSubmission from "./ArtSubmission";

function App() {
  return (
    <div className="container-fluid">
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/about" element={<AboutPage />} />
          <Route
            path="/nana"
            element={<ArtSubmission intitialTitle="Mandy" />}
          />
          <Route element={<PageNotFound />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
