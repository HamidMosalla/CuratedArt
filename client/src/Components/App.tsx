import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import HomePage from "./HomePage";
import AboutPage from "./About";
import Header from "./Header";
import PageNotFound from "./PageNotFound";

function App() {
  return (
    <div className="container-fluid">
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/about" element={<AboutPage />} />
          <Route element={<PageNotFound />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;

// import React from "react";
// import "./App.css";
// import ArtSubmission from "./Components/ArtSubmission";
// import MediaCard from "./Components/MediaCard";

// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <MediaCard />
//         <hr />
//         <ArtSubmission intitialTitle="Mandy" />
//       </header>
//     </div>
//   );
// }

// export default App;
