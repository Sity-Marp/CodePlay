import { Link,useNavigate } from "react-router-dom";
import "./Navbar.css";


//syns på alla sidor
export default function Navbar({user,logout}) {
   const navigate = useNavigate();

  // När man klickar på logga ut:
  const handleLogout = () => {
    logout();          
    navigate("/");    
  };
  return (
    <header className="navbar">
      <div className="navbar-inner">
        <Link to="/" className="navbar-logo">
          <img src="/images/logo.png" alt="CodePlay logga" />
        </Link>

        {user && (
          <button onClick={handleLogout} className="navbar-logout">
            Logga ut
          </button>
        )}
      </div>
    </header>
  );
}
