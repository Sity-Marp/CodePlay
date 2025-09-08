import { useState } from "react";
import "./Register.css";

export default function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await fetch("/api/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, email, password }),
      });

      const data = await res.json();
      if (res.ok) {
        setMessage("Registrering lyckades! UserId: " + data.userId);
        setUsername("");
        setEmail("");
        setPassword("");
      } else {
        setMessage("Fel: " + data.message);
      }
    } catch (err) {
      setMessage("Fel vid anslutning till backend: " + err.message);
    }
  };

  return (
    <div className="register-page">
      <div className="register-card">

        <form className="register-form" onSubmit={handleSubmit}>
          <span>Användarnamn</span>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />

          <span>E-post</span>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <span>Lösenord</span>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button type="submit">Registrera</button>
        </form>

        {message && <p style={{ marginTop: "1rem" }}>{message}</p>}

      </div>
    </div>
  );
}