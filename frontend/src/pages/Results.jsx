import "./Results.css";
import { Link } from "react-router-dom";

export default function Results() {
  return (
    <main className="results-page">
        <section className="results-card" role="region" aria-label="Resultat">
            <h2>Resultat</h2>
            <p>Dina resultat kommer att visas här.</p>
            <Link to="/dashboard" className="back-link">Tillbaka till Dashboard</Link>
        </section>
    </main>
    );
}
