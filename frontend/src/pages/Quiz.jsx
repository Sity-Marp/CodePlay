import { Link, useSearchParams, useNavigate } from "react-router-dom";
import "./Quiz.css";

export default function Quiz() {
  const [params] = useSearchParams();
  const navigate = useNavigate();

  // 1) Läs och clamp:a level (1..4) & frågenummer (1..5)
  const level = clamp(parseInt(params.get("level") ?? "1", 10), 1, 4);
  const q = clamp(parseInt(params.get("q") ?? "1", 10), 1, 5);

  // 2) Sätt spårnamn för rubriker
  const { title } = trackFromLevel(level);

  // 3) Dummy-frågor (5 st) för denna level
  const questions = getDummyQuestions(level);
  const current = questions[q - 1];

  // 4) Hjälpare för att byta fråga (uppdaterar query i URL)
  const gotoQ = (n) => {
    const next = clamp(n, 1, 5);
    navigate(`/quiz?level=${level}&q=${next}`);
  };

  return (
    <div className="quiz-page">
      <div
        className="quiz-card"
        role="region"
        aria-label={`Quiz – nivå ${level} (${title})`}
      >
        {/* ÖVERST: “Fråga:” + nivå/ämne */}
        <h3 className="quiz-topline">
          <span className="quiz-label">Fråga:</span>
          <span className="quiz-meta">
            Nivå {level} – {title}
          </span>
        </h3>

        {/* FRÅGETEXT */}
        <h2 className="quiz-question">{current.question}</h2>

        {/* SVARSALTERNATIV (radio) */}
        <form
          className="quiz-form"
          onSubmit={(e) => {
            e.preventDefault();
            gotoQ(q + 1);
          }}
        >
          <fieldset className="quiz-options">
            <legend className="sr-only">Välj ett svar</legend>
            {current.options.map((opt, i) => (
              <label key={i} className="quiz-option">
                <input type="radio" name="answer" />
                <span className="quiz-option-text">
                  {i + 1}. {opt}
                </span>
              </label>
            ))}
          </fieldset>

          {/* KNAPPAR NEDERST */}
          <div className="quiz-actions">
            <button
              type="button"
              className="btn-secondary"
              onClick={() => gotoQ(q - 1)}
              disabled={q === 1}
            >
              ← Föregående
            </button>

            <button type="submit" className="btn-primary">
              {q < 5 ? "Nästa →" : "Slutför ✔"}
            </button>
          </div>

          {/* Liten länk för att lämna quizet (valfritt) */}
          <div className="quiz-aux">
            <Link to="/fakta?level=1">Tillbaka till nivåer</Link>
          </div>
        </form>
      </div>
    </div>
  );
}

/*  Hjälpfunktionerrs */

function clamp(n, min, max) {
  if (Number.isNaN(n)) return min;
  return Math.max(min, Math.min(max, n));
}

function trackFromLevel(level) {
  switch (level) {
    case 1:
      return { title: "HTML" };
    case 2:
      return { title: "CSS" };
    case 3:
      return { title: "JavaScript" };
    case 4:
      return { title: "HTML + CSS + JavaScript" };
    default:
      return { title: "HTML" };
  }
}

function getDummyQuestions(level) {
  // Fem frågor per level 
  const tag =
    level === 2
      ? "(CSS)"
      : level === 3
      ? "(JS)"
      : level === 4
      ? "(ALLA)"
      : "(HTML)";
  return [
    {
      question: `Vad är ${tag} fråga 1?`,
      options: [
        "Det är bla bla bla…",
        "Det är bla bla bla…",
        "Det är bla bla bla…",
        "Det är bla bla bla…",
      ],
    },
    {
      question: `Vad är ${tag} fråga 2?`,
      options: [
        "Lorem ipsum dolor sit amet…",
        "Consectetur adipiscing elit…",
        "Integer posuere erat a ante…",
        "Vestibulum id ligula porta…",
      ],
    },
    {
      question: `Vad är ${tag} fråga 3?`,
      options: [
        "Sed posuere consectetur…",
        "Aenean lacinia bibendum…",
        "Maecenas faucibus mollis…",
        "Donec sed odio dui…",
      ],
    },
    {
      question: `Vad är ${tag} fråga 4?`,
      options: [
        "Nullam id dolor id nibh…",
        "Cras mattis consectetur…",
        "Etiam porta sem malesuada…",
        "Morbi leo risus…",
      ],
    },
    {
      question: `Vad är ${tag} fråga 5?`,
      options: [
        "Option A lorem ipsum…",
        "Option B lorem ipsum…",
        "Option C lorem ipsum…",
        "Option D lorem ipsum…",
      ],
    },
  ];
}
