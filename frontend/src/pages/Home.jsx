import {Link } from "react-router-dom";
import "./Home.css";

//TEMPORÄR KOD, VÅGAR INTE PUSHA ÄN DÅ DEN ÄR BUGGAD
export default function Home() {
  return (
    <>
      <main className="home">
        <section className="home-card" role="region" aria-label="Välkomstkort">
          <h1 className="home-title">
            VÄLKOMMEN TILL <br /> CODEPLAY
          </h1>
          <img
            src="/images/player.png"
            alt="Astronaut"
            className="home-astro"
            draggable="false"
          />
          <div className="home-actions">
            <Link to="/register" className="btn btn-primary">
              Registrera Dig
            </Link>
            <Link to="/login" className="btn btn-primary">
              Logga In
            </Link>
          </div>
        </section>
      </main>
    </>
  );
}
