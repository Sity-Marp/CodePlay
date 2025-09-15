import { Link, useSearchParams, useNavigate } from "react-router-dom";
import "./Fakta.css";

export default function Fakta() {
  const [params] = useSearchParams();
  const navigate = useNavigate();

  // 1) Läs level från URL (fallback till 1 om den saknas/ogiltig)
  const levelParam = params.get("level");
  const level = clampLevel(parseInt(levelParam ?? "1", 10));

  // 2) Räkna ut ämnet/track från level
  const { trackKey, title } = trackFromLevel(level);

  // 3) Dummy-fakta – olika texter per level bara för känsla
  const dummyFacts = getDummyFacts(level);

  // 4) Länk till quiz-sidan för samma level (skickar med både track & level)
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
        {/* Rubrik */}
        <h1 className="fakta-title">{title}</h1>

        {/* Vänster: dummy-fakta-lista */}
        <ul className="fakta-list" aria-live="polite">
          {dummyFacts.map((it) => (
            <li key={it.id} className="fakta-item">
              <div className="fakta-dot" aria-hidden="true" />
              <p className="fakta-item-title">{it.title}</p>
              <p className="fakta-item-sub">{it.sub}</p>
            </li>
          ))}
        </ul>

        {/* Höger: dekor */}
        <div className="fakta-right" aria-hidden="true">
          <div className="fakta-speech">
            Detta är nivå {level}.<br />
            Dummy-info tills backend kopplas!
          </div>
          <img
            src="/images/player.png"
            alt=""
            className="fakta-astro"
            draggable="false"
          />
        </div>

        {/* Nederst: navigering */}
        <div className="fakta-next">
          {/* Tillbaka till levels */}
          {/* <button className="fakta-btn-next" onClick={() => navigate("/levels")}>Till nivåer</button> */}
          <Link to={nextHref} className="fakta-btn-next">
            NÄSTA SIDA →
          </Link>
        </div>
      </section>
    </main>
  );
}

/* ===================== Hjälpfunktioner ===================== */

/** Säkerställ 1..4 ( nivåer) */
function clampLevel(n) {
  if (Number.isNaN(n)) return 1;
  if (n < 1) return 1;
  if (n > 4) return 4;
  return n;
}

/** Mappar level → { trackKey (till backend), title (för visning) } */
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

/** Dummy-fakta per level */
function getDummyFacts(level) {
  const base = [
    {
      id: 1,
      title: "Lorem ipsum dolor sit amet.",
      sub: "Consectetur adipiscing elit. Integer posuere erat a ante.",
    },
    {
      id: 2,
      title: "Sed posuere consectetur est at lobortis.",
      sub: "Aenean lacinia bibendum nulla sed consectetur.",
    },
    {
      id: 3,
      title: "Maecenas faucibus mollis interdum.",
      sub: "Vestibulum id ligula porta felis euismod semper.",
    },
    {
      id: 4,
      title: "Cras mattis consectetur purus sit amet fermentum.",
      sub: "Donec sed odio dui. Etiam porta sem malesuada magna mollis euismod.",
    },
    {
      id: 5,
      title: "Nullam id dolor id nibh ultricies vehicula.",
      sub: "Morbi leo risus, porta ac consectetur ac, vestibulum at eros.",
    },
  ];
  // Små variationer så varje level känns unik 
  if (level === 2)
    return base.map((x) => ({ ...x, title: x.title.replace(".", " (CSS).") }));
  if (level === 3)
    return base.map((x) => ({ ...x, title: x.title.replace(".", " (JS).") }));
  if (level === 4)
    return base.map((x) => ({
      ...x,
      title: x.title.replace(".", " (HTML+CSS+JS)."),
    }));
  return base;
}
