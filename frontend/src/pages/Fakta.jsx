import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import "./Fakta.css";

export default function Fakta() {
  const [params] = useSearchParams();

  // 1) Läs level från URL (fallback till 1)
  const levelParam = params.get("level") || "1";
  const level = clampLevel(parseInt(levelParam, 10));

  // 2) Hitta rätt ämne (track) + titel
  const { trackKey, title } = trackFromLevel(level);

  // STATE
  const [facts, setFacts] = useState([]);
  const [loading, setLoading] = useState(true);

  // 3) Hämta fakta från backend
  useEffect(() => {
    async function loadFacts() {
      try {
        const res = await fetch(
          `/api/quiz/facts/by-track?track=${trackKey}&level=${level}`
        );
        if (!res.ok) throw new Error("Kunde inte hämta fakta");
        const data = await res.json();
        setFacts(data.items || []); // backend returnerar { items: [...] }
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    }
    loadFacts();
  }, [trackKey, level]);

  // 4) Länk till quiz-sidan
  const nextHref = `/quiz?track=${encodeURIComponent(
    trackKey
  )}&level=${encodeURIComponent(level)}`;

  return (
    <main className="fakta">
      <section
        className="fakta-card"
        role="region"
        aria-label={`Fakta för ${title}`}
      >
        <h1 className="fakta-title">{title}</h1>

        {loading ? (
          <p>Laddar fakta...</p>
        ) : (
          <ul className="fakta-list" aria-live="polite">
            {facts.map((fact, i) => (
              <li key={fact.questionId || i} className="fakta-item">
                <div className="fakta-dot" aria-hidden="true" />
                <p className="fakta-item-title">Fakta {i + 1}</p>
                <p className="fakta-item-sub">{fact.fact}</p>
              </li>
            ))}
          </ul>
        )}

        <div className="fakta-right" aria-hidden="true">
          <div className="fakta-speech">
            Detta är nivå {level}.<br />
            {loading ? "Hämtar fakta..." : `Totalt ${facts.length} fakta.`}
          </div>
          <img
            src="/images/player.png"
            alt=""
            className="fakta-astro"
            draggable="false"
          />
        </div>

        <div className="fakta-next">
          <Link to={nextHref} className="fakta-btn-next">
            NÄSTA SIDA →
          </Link>
        </div>
      </section>
    </main>
  );
}

/* Hjälpfunktionerssr */
function clampLevel(n) {
  if (Number.isNaN(n)) return 1;
  if (n < 1) return 1;
  if (n > 4) return 4;
  return n;
}

function trackFromLevel(level) {
  switch (level) {
    case 1:
      return { trackKey: "Html", title: "HTML" };
    case 2:
      return { trackKey: "Css", title: "CSS" };
    case 3:
      return { trackKey: "JavaScript", title: "JavaScript" };
    case 4:
      return { trackKey: "LastTrack", title: "HTML + CSS + JavaScript" };
    default:
      return { trackKey: "Html", title: "HTML" };
  }
}
