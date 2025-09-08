import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import "./Login.css";

export default function Login() {
  const [identifier, setIdentifier] = useState(""); 
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);

    try {
      const res = await fetch("/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        
        body: JSON.stringify({ username: identifier, email: identifier, password })
      });

      const text = await res.text();
      const data = text ? JSON.parse(text) : {};

      if (res.ok) {
        if (data.token) localStorage.setItem("token", data.token);
        setMessage("Inloggning lyckades!");
        navigate("/dashboard");
      } else {
        setMessage(data.message || `Inloggning misslyckades (HTTP ${res.status})`);
      }
    } catch (err) {
      setMessage("Fel vid anslutning till backend: " + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-page">
      <div className="login-card">
        <form className="login-form" onSubmit={handleSubmit}>
          <span>E-post/Användarnamn</span>
          <input
            type="text"
            value={identifier}
            onChange={(e) => setIdentifier(e.target.value)}
            required
          />

          <span>Lösenord</span>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button type="submit" className="btn-login" disabled={loading}>
            {loading ? "Loggar in..." : "Logga in"}
          </button>

          <Link to="/reset-password" className="btn-reset-password">
            Glömt lösenord
          </Link>

          {message && <p className="form-message">{message}</p>}
        </form>
      </div>
    </div>
  );
}
