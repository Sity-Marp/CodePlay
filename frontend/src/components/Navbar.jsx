import { Link } from "react-router-dom";
import "./Navbar.css";

//syns på alla sidor
export default function Navbar() {
  return (
    <header className="navbar">
      <div className="navbar-inner">
        <Link to="/" className="navbar-logo">
          <img src="/images/logo.png" alt="CodePlay logga" />
        </Link>
      </div>
    </header>
  );
}
