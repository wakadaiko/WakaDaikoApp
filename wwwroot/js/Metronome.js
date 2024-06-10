const bpmDisplay = document.querySelector('.metronome-display');
const startButton = document.getElementById('start');
const pauseBtn = document.getElementById('pause');
const bpmInput = document.getElementById('bpmField');
const increaseButton = document.getElementById('increase');
const decreaseButton = document.getElementById('decrease');
const clap = new Audio('../assets/click.wav');
const bing = new Audio('../assets/bing.wav');
const tick = new Audio('../assets/SoftTick.wav');
const timer = document.getElementById('timer');
const bpmToSave = (document.getElementById('bpmToStore') != null) ? document.getElementById('bpmToStore') : { value: 0 };
//const timeSignatureInput = document.getElementById('timeSignature');
const metronomeField = document.getElementById('metronomeField');
const preferenceField = document.getElementById('preferenceField');
const bpmPrefValue = document.getElementById('bpmPrefValue');
const tempoItem = document.getElementById('tempoItem');
const bouncer = document.getElementById('bouncer');
const muteBtn = document.getElementById('muteBtn');
const deleteBtn = document.getElementById('deleteBtn');

let isMuted = true;
let isTimerPaused = false;
let seconds = 0;
let beat = 0;
let bpm = 60;
let timerInterval = null;
let intervalId = null;
let bouncerInterval = null;
let timeSignature = 4;
bpmToSave.value = bpm;
metronomeField.hidden = false;
preferenceField.hidden = false;
function calculateInterval() {
    return 60000 / bpm; // Convert BPM to milliseconds
}
function startBouncer(interval) {
    bouncerInterval = setInterval(() => {
        bouncer.hidden = !bouncer.hidden;
    }, interval);
}
function stopBouncer() {
    clearInterval(bouncerInterval);
    bouncer.hidden = false;
}
function updateTimeSignature() {
    // Get the time signature from the input field
    stopMetronome();
    timeSignature = parseInt(timeSignatureInput.value);
}
function startElapesedTime() {

    // start the timer
    timerInterval = setInterval(() => {
        //increment the time
        seconds++;
        //set the timer to the new universal time code time
        timer.textContent = parseSeconds(seconds);
    }, 1000);
};
function stopTimer() {
    //set the initial time to 00:00
    clearInterval(timerInterval);
    seconds = (isTimerPaused) ? seconds : 0;
    timer.textContent = parseSeconds(seconds);

}

function startMetronome() {
    clearInterval(timerInterval);
    const interval = calculateInterval();
    startElapesedTime();
    startBouncer(interval);
    pauseBtn.textContent = 'Pause';
    intervalId = setInterval(() => {
        if (!isMuted) { tick.play(); }
    }, interval);
}

function stopMetronome() {
    clearInterval(intervalId);
    intervalId = null;
    stopTimer();
    stopBouncer();
    beat = 0;
}
function handleStartStopBtn() {
    if (startButton.textContent == 'Start') {
        updateStartButtonToStop();
        startMetronome();
    } else {
        updateStartButtonToStop();
        stopMetronome();
    }
}
function increaseBPM() {
    bpm++;
    updateBPMField();
}
function decreaseBPM() {
    if (bpm > 0) {
        bpm--;
        updateBPMField();
    }
}
function updateBPMField() {
    bpmInput.value = bpm;
    bpmToSave.value = bpm;
    bpmDisplay.value = `${bpm} BPM`;
}
async function updateBpm(bpmToSet) {
    bpm = await bpmToSet;
    updateBPMField();
}
function updateStartButtonToStop() {
    startButton.textContent = (startButton.textContent == 'Start') ? 'Stop' : 'Start';
    //startButton.classList.remove('btn-success');
    //startButton.classList.add('btn-danger');
}

function pauseTimer() {
    isTimerPaused = !isTimerPaused;
    stopMetronome();
    updateStartButtonToStop();
    pauseBtn.textContent = (isTimerPaused) ? 'Resume' : 'Pause';

}
function handlePauseBtn() {
    if (seconds > 0) {
        if (pauseBtn.textContent == 'Resume') {
            isTimerPaused = !isTimerPaused;
            startMetronome();
            startButton.disabled = false;
            startButton.textContent = 'Stop';
        } else {
            pauseTimer();
            startButton.disabled = true;
        }
    }
}

function handleMuteBtn() {
    isMuted = !isMuted;
    if (isMuted) {
        muteBtn.style.color = 'red';
    } else {
        muteBtn.style.color = 'white';
    }
}
startButton.addEventListener('click', handleStartStopBtn);
pauseBtn.addEventListener('click', handlePauseBtn);
increaseButton.addEventListener('click', increaseBPM);
decreaseButton.addEventListener('click', decreaseBPM);
muteBtn.addEventListener('click', handleMuteBtn);
deleteBtn.addEventListener('click', () => {
    deleteBtn.disabled = true;
    setTimeout(() => { deleteBtn.disabled = false; }, 4000);
});

// Update BPM display on input change
document.addEventListener('input', (event) => {
    if (event.target.id === 'bpmField') {
        bpm = parseInt(event.target.value);
        bpmDisplay.textContent = `${bpm} BPM`;
        stopMetronome(); // Stop if running and update interval
    }
});

//parse seconds into minutes and seconds
function parseSeconds(seconds) {
    let mins = Math.floor(seconds / 60);
    let secs = seconds % 60;
    return `${(mins < 10) ? 0 : ''}${mins}:${secs < 10 ? '0' : ''}${secs}`;
}
//bpmPrefValue.textContent.slice(0, 2))
const handleSetBpm = (event) => { updateBpm(Number(event.target.textContent.slice(0, 2))); };
const toggleSettings = () => {
    (metronomeField.hidden = !metronomeField.hidden, preferenceField.hidden = !preferenceField.hidden);
}



