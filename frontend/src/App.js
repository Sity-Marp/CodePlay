import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Background from "./components/Background";
import Home from "./pages/Home";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Background />
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} /> {/* Startsida - utloggad användare */}
      </Routes>
    </BrowserRouter>
  );
}

export default App;
