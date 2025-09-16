import { Link } from "react-router-dom";
import "./Levels.css";

export default function Levels() {
    return (
        
        <div className="levels">
            
            <div className="levels-planet">
            <div className="planet html-planet">
                <p className="name-planet"> HTML </p>

                <Link to="/fakta" className="btn-html">
                <img src="/images/html.png" alt="HTML icon" className="planet-icon" draggable="false"/>
            </Link>

            </div>

            <div className="planet css-planet locked">
                <p className="name-planet"> CSS </p>

                <Link to="/css" className="btn-css" aria-disabled="true">
                <img src="/images/css.png" alt="CSS icon" className="planet-icon" draggable="false"/>
            </Link>
  
            </div>

            <div className="planet javascript-planet locked">
                <p className="name-planet"> JAVASCRIPT </p>

                <Link to="/javascript" className="btn-javascript" aria-disabled="true">
                <img src="/images/javascript.png" alt="JavaScript icon" className="planet-icon" draggable="false"/>
            </Link> 

            </div>

            <div className="planet earth-planet locked">
                <p className="name-planet"> HTML, CSS & <br/> JavaScript  </p>

                <Link to="/earth" className="btn-earth" aria-disabled="true">
                <img src="/images/html-css-javascript.png" alt="Earth icon" className="planet-icon" draggable="false"/> 
            </Link>
            </div>

            </div>
           
                <img src="/images/player.png" alt="Astronaut" className="levels-astro" draggable="false" />
                <img src="images/cb-levels.png" alt="chatbubble" className="levels-cb" draggable="false" />
                <img src="images/rocket.png" alt="rocket" className="levels-rocket" draggable="false" />    
            
        </div>
                
    );
}