const bpmDisplay = document.querySelector('.metronome-display');
const startButton = document.getElementById('start');
const stopButton = document.getElementById('stop');
const bpmInput = document.getElementById('bpmField');
const increaseButton = document.getElementById('increase');
const decreaseButton = document.getElementById('decrease');
const clap = new Audio('../assets/click.wav');
const bing = new Audio('../assets/bing.wav');
const timer = document.getElementById('timer');
const bpmToSave = (document.getElementById('bpmToStore') != null) ? document.getElementById('bpmToStore') : {value:0};
//const timeSignatureInput = document.getElementById('timeSignature');
const metronomeField = document.getElementById('metronomeField');
const preferenceField = document.getElementById('preferenceField');
const bpmPrefValue = document.getElementById('bpmPrefValue');
const tempoItem = document.getElementById('tempoItem');

let seconds = 0;
let beat = 0;
let bpm = 60;
let timerInterval = null;
let intervalId = null;
let timeSignature = 4;
bpmToSave.value = bpm;
metronomeField.hidden = false;
preferenceField.hidden = false;
function calculateInterval() {
    return 60000 / bpm; // Convert BPM to milliseconds
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
    seconds = 0;
    timer.textContent = parseSeconds(seconds);

}
function startMetronome() {
    const interval = calculateInterval();
    startButton.disabled = true;
    startElapesedTime();
    stopButton.disabled = false;

    intervalId = setInterval(() => {
        clap.play();
    }, interval);
}

function stopMetronome() {
    clearInterval(intervalId);
    intervalId = null;
    stopButton.disabled = true;
    stopTimer();
    startButton.disabled = false;
    beat = 0;
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


startButton.addEventListener('click', startMetronome);
stopButton.addEventListener('click', stopMetronome);
increaseButton.addEventListener('click', increaseBPM);
decreaseButton.addEventListener('click', decreaseBPM);

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
    return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
}
//bpmPrefValue.textContent.slice(0, 2))
const handleSetBpm = (event) => { updateBpm(Number(event.target.textContent.slice(0, 2))); };
const toggleSettings = () => {
    (metronomeField.hidden = !metronomeField.hidden, preferenceField.hidden = !preferenceField.hidden);
}
