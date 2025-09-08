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

        <button type="submit">Logga in</button>

        <p>Glömt lösenord</p>
      </form>
      
      </div>
    </div>
  );
}
