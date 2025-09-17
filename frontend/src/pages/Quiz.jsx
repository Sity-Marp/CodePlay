import { useEffect, useState } from "react";
import { Link, useSearchParams, useNavigate } from "react-router-dom";
import "./Quiz.css";

/* Rätt svar-karta från dataseed*/
const CORRECT_BY_QUESTION = {
  1: 1,
  2: 5,
  3: 9,
  4: 13,
  5: 17,
  6: 21,
  7: 25,
  8: 29,
  9: 33,
  10: 37,
  11: 41,
  12: 45,
  13: 49,
  14: 53,
  15: 57,
  16: 61,
  17: 65,
  18: 69,
  19: 73,
  20: 77,
};

/* LocalStorage helpers (för resultat-sidan) */
const QUIZ_SESSION_KEY = (trackKey, level) =>
  `quizSession:${trackKey}:${level}`;

function loadSession(trackKey, level) {
  try {
    return JSON.parse(
      localStorage.getItem(QUIZ_SESSION_KEY(trackKey, level)) || "{}"
    );
  } catch {
    return {};
  }
}

function saveSession(trackKey, level, data) {
  localStorage.setItem(QUIZ_SESSION_KEY(trackKey, level), JSON.stringify(data));
}

function clearSession(trackKey, level) {
  localStorage.removeItem(QUIZ_SESSION_KEY(trackKey, level));
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

/* Komponent */
export default function Quiz() {
  const [params] = useSearchParams();
  const navigate = useNavigate();

  const level = clamp(parseInt(params.get("level") ?? "1", 10), 1, 4);
  const q = clamp(parseInt(params.get("q") ?? "1", 10), 1, 5);
  const { trackKey, title } = trackFromLevel(level);

  const resume = params.get("resume") === "1"; // <-- endast återuppta om resume=1 i URL

  const [questions, setQuestions] = useState([]);
  const [loading, setLoading] = useState(true);

  // { questionId: answerOptionId }
  const [answers, setAnswers] = useState({});
  // { questionId: { correctId, chosenId } }
  const [feedback, setFeedback] = useState({});
  // { questionId: true } => låst efter första valet
  const [locked, setLocked] = useState({});

  useEffect(() => {
    const fetchQuiz = async () => {
      try {
        // Hämta quiz-lista för vald nivå
        const resList = await fetch(
          `/api/quiz/by-level?track=${trackKey}&level=${level}`
        );
        const list = await resList.json();
        if (!list.length) throw new Error("Inget quiz hittades");

        //Hämta själva quizet med frågor/svarsalternativ
        const quizId = list[0].id;
        const resQuiz = await fetch(`/api/quiz/${quizId}`);
        const quiz = await resQuiz.json();

        // Plocka ut endast det UI:t behöver
        setQuestions(
          quiz.questions.map((q) => ({
            id: q.id,
            text: q.text,
            options: q.answerOptions,
          }))
        );

        // Återställ endast om användaren uttryckligen vill återuppta
        if (resume) {
          const prev = loadSession(trackKey, level);
          if (prev.answers) setAnswers(prev.answers);
          if (prev.feedback) setFeedback(prev.feedback);
          if (prev.locked) setLocked(prev.locked);
        } else {
          // börja fräscht
          setAnswers({});
          setFeedback({});
          setLocked({});
          clearSession(trackKey, level);
        }
      } catch (e) {
        console.error(e);
      } finally {
        setLoading(false);
      }
    };
    fetchQuiz();
  }, [trackKey, level, resume]);

  if (loading) return <p>Laddar frågor…</p>;
  if (!questions.length) return <p>Inga frågor hittades!</p>;

  //Beräkna "current" fråga baserat på q (1-baserat index)
  const current = questions[q - 1];
  const chosen = answers[current.id]; // ev. valt svar för nuvarande fråga
  const isLocked = !!locked[current.id]; // om frågan är låst (kan ej byta svar)

  // Byter fråga genom att uppdatera q i URL (bevarar resume=1 om det är aktivt)
  const gotoQ = (n) => {
    const next = clamp(n, 1, questions.length);
    navigate(`/quiz?level=${level}&q=${next}${resume ? "&resume=1" : ""}`);
  };

  // Lås direkt vid första valet + spara session
  const handleChoose = (answerId) => {
    if (locked[current.id]) return;

    const correctId = CORRECT_BY_QUESTION[current.id] ?? current.options[0]?.id;

    const nextAnswers = { ...answers, [current.id]: answerId };
    const nextFeedback = {
      ...feedback,
      [current.id]: { correctId, chosenId: answerId },
    };
    const nextLocked = { ...locked, [current.id]: true };

    setAnswers(nextAnswers);
    setFeedback(nextFeedback);
    setLocked(nextLocked);

    // spara session om man vill återuppta senare (via resume=1)
    saveSession(trackKey, level, {
      answers: nextAnswers,
      feedback: nextFeedback,
      locked: nextLocked,
      totalQuestions: questions.length,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!chosen) {
      alert("Du måste välja ett alternativ!");
      return;
    }

    // spara innan vi går vidare (för ev. Resultat-sida)
    saveSession(trackKey, level, {
      answers,
      feedback,
      locked,
      totalQuestions: questions.length,
    });

    if (q === questions.length) {
      navigate(`/resultat?level=${level}&track=${trackKey}`);
    } else {
      gotoQ(q + 1);
    }
  };

  const handleReset = () => {
    clearSession(trackKey, level);
    setAnswers({});
    setFeedback({});
    setLocked({});
    navigate(`/quiz?level=${level}&q=1`);
  };

  return (
    <div className="quiz-page">
      <div
        className="quiz-card"
        role="region"
        aria-label={`Quiz – nivå ${level} (${title})`}
      >
        <h3 className="quiz-topline">
          <span className="quiz-label">Fråga:</span>
          <span className="quiz-meta">
            Nivå {level} – {title}
          </span>
        </h3>

        <h2 className="quiz-question">{current.text}</h2>

        <form className="quiz-form" onSubmit={handleSubmit}>
          <fieldset className={`quiz-options ${isLocked ? "locked" : ""}`}>
            <legend className="sr-only">Välj ett svar</legend>

            {current.options.map((opt) => {
              const fb = feedback[current.id];
              let cls = "quiz-option";
              if (fb) {
                if (opt.id === fb.correctId) cls += " correct";
                else if (opt.id === fb.chosenId) cls += " incorrect";
              }
              return (
                <label key={opt.id} className={cls}>
                  <input
                    type="radio"
                    name={`q-${current.id}`}
                    value={opt.id}
                    checked={answers[current.id] === opt.id}
                    onChange={() => handleChoose(opt.id)}
                    disabled={isLocked}
                  />
                  <span className="quiz-option-text">{opt.text}</span>
                </label>
              );
            })}
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

          <div
            className="quiz-aux"
            style={{
              display: "flex",
              justifyContent: "space-between",
              marginTop: 8,
            }}
          >
            <Link to={`/fakta?level=${level}`}>Tillbaka till fakta</Link>
            <button
              type="button"
              className="btn-secondary"
              onClick={handleReset}
            >
              Starta om
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
