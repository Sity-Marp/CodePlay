import { Link } from "react-router-dom";
import "./Profile.css";

export default function Profile() {
  return (
    <main className="profile">
      <section className="profile-card" role="region" aria-label="Profil">
        {/* Profilbild */}
        <img
          src="/images/profile.png"
          alt="Profilbild"
          className="profile-img"
          draggable="false"
        />

        {/* Användarnamn */}
        <h2 className="profile-name">John Snow</h2>

        {/* Planeter som nivå-knappar! */}
        <div className="profile-levels">
          <Link to="/html/1/q/1" className="planet">
            <img src="/images/HTML.png" alt="Nivå 1" />
          </Link>
          <Link to="/html/2/q/1" className="planet locked">
            <img src="/images/CSS.png" alt="Nivå 2" />
          </Link>
          <Link to="/html/3/q/1" className="planet locked">
            <img src="/images/JavaScript.png" alt="Nivå 3" />
          </Link>
          <Link to="/html/4/q/1" className="planet locked">
            <img src="/images/html-css-javascript.png" alt="Nivå 4" />
          </Link>
        </div>

        {/* Ändra-knappen */}
        <div className="profile-actions">
          <button className="btn btn-edit">Ändra</button>
        </div>
      </section>
    </main>
  );
}
