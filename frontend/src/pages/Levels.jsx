import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import "./Levels.css";

export default function Levels() {
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
                    setUserProgress({
                        Html: { HighestUnlockedLevel: 1 },
                        Css: { HighestUnlockedLevel: 0 },
                        JavaScript: { HighestUnlockedLevel: 0 },
                        LastTrack: { HighestUnlockedLevel: 0 }
                    });
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
                    console.log("API progressData:", progressData);

                    //array → objekt
                    const progressByTrack = progressData.reduce((acc, track) => {
                        acc[track.track] = {
                            HighestUnlockedLevel: track.highestUnlockedLevel,
                            CurrentLevel: track.currentLevel,
                            TotalPoints: track.totalPoints,
                            TotalCorrect: track.totalCorrect,
                            TotalIncorrect: track.totalIncorrect,
                            LastUpdated: track.lastUpdated
                        };
                        return acc;
                    }, {
                        Html: { HighestUnlockedLevel: 1 },
                        Css: { HighestUnlockedLevel: 0 },
                        JavaScript: { HighestUnlockedLevel: 0 },
                        LastTrack: { HighestUnlockedLevel: 0 }
                    });

                    setUserProgress(progressByTrack);
                } else {
                    setUserProgress({
                        Html: { HighestUnlockedLevel: 1 },
                        Css: { HighestUnlockedLevel: 0 },
                        JavaScript: { HighestUnlockedLevel: 0 },
                        LastTrack: { HighestUnlockedLevel: 0 }
                    });
                }
            } catch (error) {
                console.error('fel', error);
                setUserProgress({
                    Html: { HighestUnlockedLevel: 1 },
                    Css: { HighestUnlockedLevel: 0 },
                    JavaScript: { HighestUnlockedLevel: 0 },
                    LastTrack: { HighestUnlockedLevel: 0 }
                });
            } finally {
                setLoading(false);
            }
        };

        fetchUserProgress();
    }, []);

    if (loading) {
        return <div className="levels">Laddar...</div>;
    }

    const isCssUnlocked = userProgress.Html?.CurrentLevel > 1;
    const isJavaScriptUnlocked = userProgress.Css?.HighestUnlockedLevel > 0;
    const isEarthUnlocked = userProgress.JavaScript?.HighestUnlockedLevel > 0;

    return (
        <div className="levels">
            <div className="levels-planet">
                <div className="planet html-planet">
                    <p className="name-planet">HTML</p>
                    <Link to="/fakta?level=1" className="btn-html">
                        <img src="/images/html.png" alt="HTML icon" className="planet-icon" />
                    </Link>
                </div>

                <div className={`planet css-planet ${!isCssUnlocked ? 'locked' : ''}`}>
                    <p className="name-planet">CSS</p>
                    {isCssUnlocked ? (
                        <Link to="/fakta?level=2" className="btn-css">
                            <img src="/images/css.png" alt="CSS icon" className="planet-icon" />
                        </Link>
                    ) : (
                        <div className="btn-css locked-planet" aria-disabled="true">
                            <img src="/images/css.png" alt="CSS icon" className="planet-icon" />
                        </div>
                    )}
                </div>

                <div className={`planet javascript-planet ${!isJavaScriptUnlocked ? 'locked' : ''}`}>
                    <p className="name-planet">JAVASCRIPT</p>
                    {isJavaScriptUnlocked ? (
                        <Link to="/fakta?level=3" className="btn-javascript">
                            <img src="/images/javascript.png" alt="JavaScript icon" className="planet-icon" />
                        </Link>
                    ) : (
                        <div className="btn-javascript locked-planet" aria-disabled="true">
                            <img src="/images/javascript.png" alt="JavaScript icon" className="planet-icon" />
                        </div>
                    )}
                </div>

                <div className={`planet earth-planet ${!isEarthUnlocked ? 'locked' : ''}`}>
                    <p className="name-planet">HTML, CSS & <br /> JavaScript</p>
                    {isEarthUnlocked ? (
                        <Link to="/fakta?level=4" className="btn-earth">
                            <img src="/images/html-css-javascript.png" alt="Earth icon" className="planet-icon" />
                        </Link>
                    ) : (
                        <div className="btn-earth locked-planet" aria-disabled="true">
                            <img src="/images/html-css-javascript.png" alt="Earth icon" className="planet-icon" />
                        </div>
                    )}
                </div>
            </div>

            <img src="/images/player.png" alt="Astronaut" className="levels-astro" draggable="false" />
            <img src="images/cb-levels.png" alt="chatbubble" className="levels-cb" draggable="false" />
            <img src="images/rocket.png" alt="rocket" className="levels-rocket" />
        </div>
    );
}
