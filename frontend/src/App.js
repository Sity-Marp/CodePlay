import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Background from "./components/Background";
import Footer from "./components/Footer";  
import {useState} from "react";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Register from "./pages/Register";
import ResetPassword from "./pages/ResetPassword";
import Levels from "./pages/Levels";
import Profile from "./pages/Profile";
import Fakta from "./pages/Fakta";
import Quiz from "./pages/Quiz";
import Results from "./pages/Results";  


import "./App.css";

function App() {
  const [user, setUser] = useState(() => {
    const saved = localStorage.getItem("username");
    return saved ? { name: saved } : null; // ger “inloggad” vid refresh om username finns
  });

  // Kallas av Login när inloggning lyckas
  const login = (username) => {
    setUser({ name: username });         // uppdatera UI direkt
    localStorage.setItem("username", username); 
  };

  //  Kallas av Navbar när man klickar Logga ut
  const logout = () => {
    setUser(null);                         // göm logout-knappen direkt
    localStorage.removeItem("username");   // städa
    localStorage.removeItem("token");      
  };
 
  
  return (
    <BrowserRouter>
      <Background />
      <Navbar user={user} logout={logout} />

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login login={login} />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/register" element={<Register />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="levels" element={<Levels />} />
        <Route path="/profile" element={<Profile />} />
        <Route path="/fakta" element={<Fakta />} />
        <Route path="/quiz" element={<Quiz />} />
        <Route path="/results" element={<Results />} />
      </Routes>

      <Footer />
    </BrowserRouter>  
  );
}

export default App;
