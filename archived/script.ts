const image = document.querySelector('img');
const title = document.getElementById('title');
const artist = document.getElementById('artist');
const music = document.querySelector('audio');
const prevBtn = document.getElementById('prev');
const playBtn = document.getElementById('play');
const nextBtn = document.getElementById('next');
const progressContainer = document.getElementById('progress-container');
const progress = document.getElementById('progress');
const currentTimeEl = document.getElementById('current-time');
const durationEl = document.getElementById('duration');

const songs  = [
  {
    name: 'metric-1',
    displayName: 'Front Row (Remix)',
    artist: 'Metric/Jacinto Design',
  },
  {
    name: 'jacinto-1',
    displayName: 'Electrict Chill Machine',
    artist: 'Jacinto Design',
  },
  {
    name: 'jacinto-2',
    displayName: 'Seven Nation Army (Remix)',
    artist: 'Jacinto Design',
  },
  {
    name: 'jacinto-3',
    displayName: 'Electrict Chill Machine',
    artist: 'Jacinto Design',
  },
]

let isPlaying = false;
let songIndex = 0;


function playSong() {
  isPlaying = true;
  playBtn?.classList.replace('fa-play', 'fa-pause');
  playBtn?.setAttribute('title', 'Pausar');
  music?.play();
}

function pauseSong() {
  isPlaying = false;
  playBtn?.classList.replace('fa-pause', 'fa-play');
  playBtn?.setAttribute('title', 'Tocar');
  music?.pause();
}

function loadSong(song) {
  title.textContent = song.displayName;
  artist.textContent = song.artist;
  music.src = `music/${song.name}.mp3`;
  image.src = `img/${song.name}.jpg`;
}

function nextSong() {
  songIndex ++;
  if (songIndex >= songs.length) {
    songIndex = 0;
  }
  loadSong(songs[songIndex]);
  playSong();
}

function prevSong() {
  songIndex --;
  if (songIndex < 0) {
    songIndex = songs.length -1;
  }
  loadSong(songs[songIndex]);
  playSong();
}

function getDuration(duration) {
  const durationMin = Math.floor(duration / 60);
  let durationSec = Math.floor(duration % 60);

  // Avoiding NaN
  if (durationSec) {
    durationEl.textContent = `${durationMin}:${durationSec.toString().padStart(2,'0')}`;
  }
}

function updateProgressBar(e) {
  if (isPlaying) {
    const { duration, currentTime } = e.srcElement;
    const perc = (currentTime / duration) * 100;

    progress.style.width = `${perc}%`;

    getDuration(duration);

    // Calculate current time
    const currentMin = Math.floor(currentTime / 60);
    let currentSec = Math.floor(currentTime % 60);

    // Avoiding NaN
    if (currentSec) {
      currentTimeEl.textContent = `${currentMin}:${currentSec.toString().padStart(2,'0')}`;
    }
  }
}

function initDuration(e) {
  const { duration } = e.srcElement;
  getDuration(duration);
}

function setProgressBar(e) {
  const width = this.clientWidth;
  const clickX = e.offsetX;
  const { duration } = music;
  music.currentTime = (clickX / width * duration);
}

// EVENT LISTENERS

playBtn?.addEventListener('click', () => (
  isPlaying ? pauseSong() : playSong()
));

prevBtn?.addEventListener('click', prevSong);
nextBtn?.addEventListener('click', nextSong);

music.addEventListener('timeupdate', updateProgressBar);
music.addEventListener('ended', nextSong);
music.addEventListener('loadeddata', initDuration);

progressContainer.addEventListener('click', setProgressBar);

// ON LOAD - Select First Song

loadSong(songs[songIndex]);
