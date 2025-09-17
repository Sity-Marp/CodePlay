import { Link, useNavigate } from "react-router-dom";
import "./Navbar.css";

// Syns på alla sidor
export default function Navbar({ user, logout }) {
  const navigate = useNavigate();

  // Robust: betrakta som inloggad om vi har user eller en JWT i localStorage
  const isAuthed = !!user || !!localStorage.getItem("token");

  const handleLogout = () => {
    logout?.();
    navigate("/");
  };

  return (
    <header className="navbar">
      <div className="navbar-inner">
        {/* <-- Ändringen: dynamisk länk beroende på inloggning */}
        <Link
          to={isAuthed ? "/dashboard" : "/"}
          className="navbar-logo"
          aria-label="CodePlay – gå till startsida"
          title={isAuthed ? "Till Dashboard" : "Till startsidan"}
        >
          <img src="/images/logo.png" alt="CodePlay logga" />
        </Link>

        {isAuthed && (
          <button onClick={handleLogout} className="navbar-logout">
            Logga ut
          </button>
        )}
      </div>
    </header>
  );
}
