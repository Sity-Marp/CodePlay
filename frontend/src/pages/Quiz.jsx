import { useEffect, useState } from "react";
import { Link, useSearchParams, useNavigate } from "react-router-dom";
import "./Quiz.css";

export default function Quiz() {
  const [params] = useSearchParams();
  const navigate = useNavigate();

  const level = clamp(parseInt(params.get("level") ?? "1", 10), 1, 4);
  const q = clamp(parseInt(params.get("q") ?? "1", 10), 1, 5);

  const { trackKey, title } = trackFromLevel(level);

  // state för quizdata
  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [answers, setAnswers] = useState({}); // sparar valt svar per fråga

  useEffect(() => {
    const fetchQuiz = async () => {
      try {
        // 1. Hämta quiz-id för den här nivån
        const resList = await fetch(
          `/api/quiz/by-level?track=${trackKey}&level=${level}`
        );
        const list = await resList.json();
        if (!list.length) throw new Error("Inget quiz hittades");

        const quizId = list[0].id;

        // 2. Hämta quiz + frågor
        const resQuiz = await fetch(`/api/quiz/${quizId}`);
        const quiz = await resQuiz.json();

        setQuestions(
          quiz.questions.map((q) => ({
            id: q.id,
            text: q.text,
            options: q.answerOptions,
          }))
        );
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchQuiz();
  }, [trackKey, level]);

  if (loading) return <p>Laddar frågor…</p>;
  if (!questions.length) return <p>Inga frågor hittades!</p>;

  const current = questions[q - 1];

  const gotoQ = (n) => {
    const next = clamp(n, 1, questions.length);
    navigate(`/quiz?level=${level}&q=${next}`);
  };

  return (
    <div className="quiz-page">
      <div className="quiz-card">
        <h3 className="quiz-topline">
          <span className="quiz-label">Fråga:</span>
          <span className="quiz-meta">
            Nivå {level} – {title}
          </span>
        </h3>

        <h2 className="quiz-question">{current.text}</h2>

        <form
          className="quiz-form"
          onSubmit={(e) => {
            e.preventDefault();
            gotoQ(q + 1);
          }}
        >
          <fieldset className="quiz-options">
            <legend className="sr-only">Välj ett svar</legend>
            {current.options.map((opt) => (
              <label key={opt.id} className="quiz-option">
                <input
                  type="radio"
                  name={`q-${current.id}`}
                  value={opt.id}
                  checked={answers[current.id] === opt.id}
                  onChange={() =>
                    setAnswers({ ...answers, [current.id]: opt.id })
                  }
                />
                <span className="quiz-option-text">{opt.text}</span>
              </label>
            ))}
          </fieldset>

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
              {q < questions.length ? "Nästa →" : "Slutför ✔"}
            </button>
          </div>

          <div className="quiz-aux">
            <Link to={`/fakta?level=${level}`}>Tillbaka till fakta</Link>
          </div>
        </form>
      </div>
    </div>
  );
}

/* Hjälpfunktioner */
function clamp(n, min, max) {
  if (Number.isNaN(n)) return min;
  return Math.max(min, Math.min(max, n));
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
