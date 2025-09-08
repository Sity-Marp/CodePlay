import { Link } from "react-router-dom";
import "./ResetPassword.css";

export default function ResetPassword() {
  return (
    <div className="reset-password-page">
      <div className="reset-password-card">
      <form className="reset-password-form">
        
        <span>E-post/Användarnamn</span>
        <input type="text" required />

        <span>Nytt lösenord</span>
        <input type="password"required />

        <span>Bekräfta lösenord</span>
        <input type="password"required />
        <button type="submit">Återställ</button>
      </form>
      
      </div>
    </div>
  );
}
