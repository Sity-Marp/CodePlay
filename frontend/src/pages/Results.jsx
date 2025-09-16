import { useState, useEffect } from "react";
import "./Results.css";
import { Link, useLocation, useNavigate } from "react-router-dom";


export default function Results() {

    const {state} = useLocation();
    const navigate = useNavigate();

    const quizId = state?.quizId;
    const answers = state?.answers;

    const [result, setResult] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

 

  return (
    <main className="results-page">
        <section className="results-card" role="region" aria-label="Resultat">
            <h2>Resultat</h2>
            <p>Dina resultat kommer att visas här.</p>
            <Link to="/dashboard" className="back-link">Tillbaka till Dashboard</Link>

        </section>
        <div className="result-bubble" aria-hidden="true">
            Bra jobbat!<br />
            Du har slutfört quizen!
            
        </div>

        <div>
            <img src="/images/player.png" alt="Astronaut" className="results-astro" draggable="false" />
        </div>
        
    </main>
    );
    


}
