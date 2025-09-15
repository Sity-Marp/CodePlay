import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Background from "./components/Background";
import Footer from "./components/Footer";  

import Home from "./pages/Home";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Register from "./pages/Register";
import ResetPassword from "./pages/ResetPassword";
import Profile from "./pages/Profile";
import Fakta from "./pages/Fakta";
import Quiz from "./pages/Quiz";

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
        <Route path="/profile" element={<Profile />} />
        <Route path="/fakta" element={<Fakta />} />
        <Route path="/quiz" element={<Quiz />} />
      </Routes>

      <Footer />
    </BrowserRouter>  
  );
}

export default App;
