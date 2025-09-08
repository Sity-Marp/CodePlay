import { Link } from "react-router-dom";
import "./Register.css";

export default function Register() {
  return (
    
    <div className="register-page">
      <div className="register-card">
      <form className="register-form">
        
        <span>Användarnamn</span>
        <input type="text"/>

        <span>E-post</span>
        <input type="password" />

        <span>Lösenord</span>
        <input type="password" required />
        <Link to="/login" className="btn-register">Registrera</Link>
      </form>
      
      </div>
    </div>
  );
}
