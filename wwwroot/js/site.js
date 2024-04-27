const eMobileBorder = document.getElementById('eMobileBorder');
const eMobiles = document.getElementsByClassName('eMobiles');
const eDropdowns = document.getElementsByClassName('eDropdowns');
const eMobileButton = document.getElementById('eMobileButton');

function toggleMobile() {
    Array.from(eMobiles).forEach(e => e.classList.contains('hidden') ? (eMobileBorder.classList.add('border-b-[1px]'), e.classList.remove('hidden'), (eMobileButton.children[0].classList.add('hidden'), eMobileButton.children[1].classList.remove('hidden'))) : (e.classList.add('hidden'), (eMobileBorder.classList.remove('border-b-[1px]'), eMobileButton.children[0].classList.remove('hidden'), eMobileButton.children[1].classList.add('hidden'))));
}

function resetMobile() {
    Array.from(eMobiles).forEach(e => window.matchMedia('(min-width: 1024px)') ? (eMobileBorder.classList.remove('border-b-[1px]'), e.classList.add('hidden'), eMobileButton.children[0].classList.remove('hidden'), eMobileButton.children[1].classList.add('hidden')) : {});

    // Prevent transition flickering
    Array.from(eDropdowns).forEach(e => e.classList.add('transition-none'));

    setInterval(() => {
        Array.from(eDropdowns).forEach(e => e.classList.remove('transition-none'));
    }, 300);
}

function removeDisclaimer() {
    document.getElementById('disclaimer').classList.replace('scale-1', 'scale-0');
}

function checkScroll() {
    const eBtn = document.getElementById('buttonScrollToTop');
    let classes = [];

    classes = document.body.scrollTop > 20 || document.documentElement.scrollTop > 20 ? ['scale-0', 'scale-1'] : ['scale-1', 'scale-0'];

    eBtn.classList.replace(classes[0], classes[1]);
}

function scrollToTop() {
    document.body.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
    document.documentElement.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
}

window.addEventListener('load', () => {
    checkScroll();
});
window.addEventListener('resize', resetMobile);
window.addEventListener('scroll', checkScroll);
window.addEventListener('touchmove', checkScroll);
