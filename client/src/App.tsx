import React from "react";
import "./App.css";
import ArtSubmission from "./Components/ArtSubmission";
import MediaCard from "./Components/MediaCard";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <MediaCard />
        <hr />
        <ArtSubmission intitialTitle="Mandy" />
      </header>
    </div>
  );
}

export default App;
