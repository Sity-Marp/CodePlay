import { Link } from "react-router-dom";
import "./Dashboard.css";

export default function Dashboard() {
  return (
    <main className="dashboard">
      <section className="db-card" role="region" aria-label="Dashboard">

        {/* Överst: titel */}
        <h1 className="db-title">
          VÄLKOMMEN TILL <br /> CODEPLAY
        </h1>

        {/* Mitten: introtext  */}
        <p className="db-intro">
          Lås upp planeterna genom att klara varje uppdrag i HTML, CSS och
          JavaScript. Få minst 80% rätt för att gå vidare. Besök varje planet
          för att komma hem till jorden. Lycka till, kodastronaut!
        </p>

        {/* Primär-CTA precis under introt */}
        <div className="db-cta">
          <Link to="/level-1" className="btn btn-start">
            Starta
          </Link>
          {/* TODO/PLACEHOLDER:
              Byt "/level-1" till den riktiga rutten när första nivån finns,
              t.ex. "/levels/html/1". */}
        </div>

        {/* Höger: astronaut */}
        <div className="db-right">
          <img
            src="/images/player.png"
            alt="Astronaut"
            className="db-astro"
            draggable="false"
          />
        </div>

        {/* Nederst: runda ikon-knappar */}
        <nav className="db-actions" aria-label="Snabblänkar">
          <Link
            to="/profile"
            className="db-action"
            aria-label="Gå till profil"
            title="Profil"
          >
            <img src="/images/profile.png" alt="" aria-hidden="true" />
          </Link>

          <Link
            to="/leaderboard"
            className="db-action"
            aria-label="Gå till leaderboard"
            title="Leaderboard"
          >
            <img src="/images/trophy.png" alt="" aria-hidden="true" />
          </Link>

          <Link
            to="/levels"
            className="db-action"
            aria-label="Gå till nivåer"
            title="Nivåer"
          >
            <img src="/images/rocket.png" alt="" aria-hidden="true" />
          </Link>
        </nav>
      </section>
    </main>
  );
}
