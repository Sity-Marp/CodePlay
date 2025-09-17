import "./Results.css";
import { Link, useSearchParams, useNavigate } from "react-router-dom";
import { useMemo } from "react";

const QUIZ_SESSION_KEY = (trackKey, level) =>
  `quizSession:${trackKey}:${level}`;

export default function Results() {
  const [params] = useSearchParams();
  const navigate = useNavigate();

  const level = Number(params.get("level") || 1);
  const trackKey = params.get("track") || "Html";

  const data = useMemo(() => {
    try {
      return JSON.parse(
        localStorage.getItem(QUIZ_SESSION_KEY(trackKey, level)) || "{}"
      );
    } catch {
      return {};
    }
  }, [trackKey, level]);

  const total = data.totalQuestions || 0;
  const correct = data.submitResult?.correct ?? 0;
  const incorrect = data.submitResult?.incorrect ?? 0;
  //const passed = !!data.submitResult?.passed;
  const percent = total > 0 ? Math.round((correct / total) * 100) : 0;

  const passed = data.submitResult?.passed ?? (percent >= 80);
  //Navigering-skickar alltid med track
  // const goRetry = () => navigate(`/quiz?level=${level}&q=1`);
  // const goNext = () => navigate(`/fakta?level=${level + 1}`);
const goRetry = () =>
    navigate(`/quiz?level=${level}&q=1&track=${trackKey}`);

  const goNext = () =>
    navigate(`/fakta?level=${level + 1}&track=${trackKey}`);
  return (
    <main className="results-page">
      <section className="results-card" role="region" aria-label="Resultat">
        <h2>Resultat</h2>
        {total === 0 ? (
          <p>Hittade inga poäng – gör om quizet.</p>
        ) : (
          <>
            <p>
              <strong>
                {correct}/{total}
              </strong>{" "}
              rätt ({percent}%).
            </p>
            <p>
              Status: {passed ? "✅ Godkänd (≥80%)" : "❌ Inte godkänd (<80%)"}
            </p>

            {data.submitResult?.incorrectQuestionIds?.length ? (
              <details style={{ marginTop: 8 }}>
                <summary>
                  Visa felaktiga frågor (
                  {data.submitResult.incorrectQuestionIds.length})
                </summary>
                <ul>
                  {data.submitResult.incorrectQuestionIds.map((id) => (
                    <li key={id}>Fråga #{id}</li>
                  ))}
                </ul>
              </details>
            ) : null}

            <div
              className="results-actions"
              style={{ display: "flex", gap: 12, marginTop: 12 }}
            >
              <Link to="/dashboard" className="back-link">
                Till Dashboard
              </Link>
              {!passed && (
                <button className="btn-secondary" onClick={goRetry}>
                  Försök igen
                </button>
              )}
              {passed && level < 4 && (
                <button className="btn-primary" onClick={goNext}>
                  Gå vidare till nivå {level + 1} →
                </button>
              )}
            </div>
          </>
        )}
      </section>
      <div className="result-bubble" aria-hidden="true">
        Bra jobbat!
        <br />
        Du har slutfört quizen!
      </div>

      <div>
        <img
          src="/images/player.png"
          alt="Astronaut"
          className="results-astro"
          draggable="false"
        />
      </div>
    </main>
  );
}
