import { Link } from "react-router-dom";
import "./Register.css";

export default function Register() {
  return (
    
    <div className="register-page">
      <div className="register-card">
      <form className="register-form">
        
        <span>Användarnamn</span>
        <input type="text" required />

        <span>E-post</span>
        <input type="password"required />

        <span>Lösenord</span>
        <input type="password" required />
        <button type="submit">Registrera</button>
      </form>
      
      </div>
    </div>
  );
}
