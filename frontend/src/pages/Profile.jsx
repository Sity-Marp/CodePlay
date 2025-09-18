import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import "./Profile.css";

export default function Profile() {
  const username = localStorage.getItem("username") || "Gäst";
  const [userProgress, setUserProgress] = useState({
    Html: { HighestUnlockedLevel: 1 },
    Css: { HighestUnlockedLevel: 0 },
    JavaScript: { HighestUnlockedLevel: 0 },
    LastTrack: { HighestUnlockedLevel: 0 }
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUserProgress = async () => {
      try {
        const token = localStorage.getItem("token") ||
          localStorage.getItem("authToken") ||
          sessionStorage.getItem("token") ||
          (() => {
            try {
              return JSON.parse(localStorage.getItem("auth") || "{}").token;
            } catch {
              return null;
            }
          })();

        if (!token) {
          setLoading(false);
          return;
        }

        const response = await fetch('/api/auth/progress', {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const progressData = await response.json();

          const progressByTrack = progressData.reduce((acc, track) => {
            acc[track.track] = {
              HighestUnlockedLevel: track.highestUnlockedLevel,
              CurrentLevel: track.currentLevel
            };
            return acc;
          }, {
            Html: { HighestUnlockedLevel: 1 },
            Css: { HighestUnlockedLevel: 0 },
            JavaScript: { HighestUnlockedLevel: 0 },
            LastTrack: { HighestUnlockedLevel: 0 }
          });

          setUserProgress(progressByTrack);
        }
      } catch (err) {
        console.error("Kunde inte hämta progress:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchUserProgress();
  }, []);

  if (loading) {
    return <main className="profile">Laddar...</main>;
  }

  const isCssUnlocked = userProgress.Html?.CurrentLevel > 1;
  const isJavaScriptUnlocked = userProgress.Css?.HighestUnlockedLevel > 0;
  const isEarthUnlocked = userProgress.JavaScript?.HighestUnlockedLevel > 0;

  return (
    <main className="profile">
      <section className="profile-card" role="region" aria-label="Profil">
        <img src="/images/profile.png" alt="Profilbild" className="profile-img" draggable="false" />
        <h2 className="profile-name">{username}</h2>

        <div className="profile-levels">
          <div className="planet-profile">

            <Link to="/fakta?level=1">
              <img src="/images/HTML.png" alt="HTML nivå 1" />
            </Link>
          </div>

          <div className={`planet-profile ${isCssUnlocked ? '' : 'locked'}`}>
            {isCssUnlocked ? (
              <Link to="/fakta?level=2">
                <img src="/images/CSS.png" alt="CSS nivå 2" />
              </Link>
            ) : (
              <img src="/images/CSS.png" alt="CSS låst nivå 2" />
            )}
          </div>

          <div className={`planet-profile ${isJavaScriptUnlocked ? '' : 'locked'}`}>
            {isJavaScriptUnlocked ? (
              <Link to="/fakta?level=3">
                <img src="/images/JavaScript.png" alt="JavaScript nivå 3" />
              </Link>
            ) : (
              <img src="/images/JavaScript.png" alt="JavaScript låst nivå 3" />
            )}
          </div>

          <div className={`planet-profile ${isEarthUnlocked ? '' : 'locked'}`}>
            {isEarthUnlocked ? (
              <Link to="/fakta?level=4">
                <img src="/images/html-css-javascript.png" alt="HTML, CSS & JS nivå 4" />
              </Link>
            ) : (
              <img src="/images/html-css-javascript.png" alt="HTML, CSS & JS låst nivå 4" />
            )}
          </div>
          <div className="planet-line">


          </div>
        </div>
      </section>
    </main>
  );
}
