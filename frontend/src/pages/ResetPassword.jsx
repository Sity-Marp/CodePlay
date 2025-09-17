import { Link } from "react-router-dom";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ResetPassword.css";


export default function ResetPassword() {
  const [identifier, setIdentifier] = useState(""); 
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate(); 

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);

    try {
      const res = await fetch("/api/auth/reset-password", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ 
          username: identifier, 
          email: identifier, 
          newPassword, 
          confirmPassword })
      });

      const text = await res.text();
      const data = text ? JSON.parse(text) : {};

      if (res.ok) {
        setMessage("Lösenordet har återställts!");
        navigate("/login");
      } else {
        setMessage(data.message || `Återställning misslyckades (HTTP ${res.status})`);
      }
    } catch (err) {
      setMessage("Fel vid anslutning till backend: " + err.message);
    } finally {
      setLoading(false);
    }
  };



  return (
    <div className="reset-password-page">
      <div className="reset-password-card">
      <form className="reset-password-form" onSubmit={handleSubmit}>
        
        <span>E-post/Användarnamn</span>
        <input 
            type="text"
            value={identifier}
            onChange={(e) => setIdentifier(e.target.value)}
            required/>

        <span>Nytt lösenord</span>
        <input 
          type="password"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          required/>

        <span>Bekräfta lösenord</span>
        <input 
          type="password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          required
          />

          <button type="submit" className="btn-reset" disabled={loading}>
            {loading ? "Återställer..." : "Återställ lösenord"}
          </button>


        {message && <p className="message">{message}</p>}
      </form>
      
      </div>
    </div>
  );
}



