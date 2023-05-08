
import React from "react";
import './Home.css';

const Home: React.FC = () => (
  <>
    <div className="player-container">
      {/* Song */}
      <div className="img-container">
        <img src="assets/img/jacinto-1.jpg" alt="Album Art" />
      </div>

      <h2 id="title">Electrict Chill Machine</h2>
      <h3 id="artist">Jacinto</h3>
      <audio src="music/jacinto-1.mp3"></audio>

      {/* Progress */}
      <div className="progress-container" id="progress-container">
        <div className="progress" id="progress"></div>
        <div className="duration-wrapper">
          <span id="current-time">0:00</span>
          <span id="duration">2:06</span>
        </div>
      </div>

      {/* Controls */}
      <div className="player-controls">
        <i className="fas fa-backward" id="prev" title="Voltar"></i>
        <i className="fas fa-play main-button" id="play" title="Tocar"></i>
        <i className="fas fa-forward" id="next" title="Avançar"></i>
      </div>
    </div>
  </>
);

export default Home;
