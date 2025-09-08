import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Background from "./components/Background";
import Footer from "./components/Footer";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import ResetPassword from "./pages/ResetPassword";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Background />
      <Navbar />
      
      <Routes>
        <Route path="/" element={<Home />} />{" "}
        {/* Startsida - utloggad användare */}
        <Route path="/login" element={<Login />} /> 
        <Route path="/register" element={<Register />} />
        <Route path="/reset-password" element={<ResetPassword />} />
      </Routes>
      <Footer />
    </BrowserRouter>
    
  );
}

export default App;
