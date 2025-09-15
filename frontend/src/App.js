import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Background from "./components/Background";
import Footer from "./components/Footer";  

import Home from "./pages/Home";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Register from "./pages/Register";
import ResetPassword from "./pages/ResetPassword";
import Levels from "./pages/Levels";

import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Background />
      <Navbar />

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/register" element={<Register />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="levels" element={<Levels />} />
      </Routes>

      <Footer />
    </BrowserRouter>  
  );
}

export default App;
