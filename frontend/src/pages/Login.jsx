import { Link } from "react-router-dom";
import "./Login.css";

export default function Login() {
  return (
    <div className="login-page">
      <div className="login-card">
      <form className="login-form">
        
        <span>E-post/Användarnamn</span>
        <input type="text" required />

        <span>Lösenord</span>
        <input type="password"required />

        <Link to ="/dashboard" className="btn-login">
        Logga in
        </Link>

        <Link to ="/reset-password" className="btn-reset-password">
        Glömt lösenord
        </Link>
      </form>
      
      </div>
    </div>
  );
}
