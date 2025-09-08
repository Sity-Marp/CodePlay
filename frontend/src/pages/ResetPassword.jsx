import { Link } from "react-router-dom";
import "./ResetPassword.css";

export default function ResetPassword() {
  return (
    <div className="reset-password-page">
      <div className="reset-password-card">
      <form className="reset-password-form">
        
        <span>E-post/Användarnamn</span>
        <input type="text"/>

        <span>Nytt lösenord</span>
        <input type="password"/>

        <span>Bekräfta lösenord</span>
        <input type="password"/>
        <Link to="/login" className="btn-reset">
        Återställ</Link>
      </form>
      
      </div>
    </div>
  );
}
