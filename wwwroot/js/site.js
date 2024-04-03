const eMobileBorder = document.getElementById('eMobileBorder');
const eMobiles = document.getElementsByClassName('eMobiles');
const eMobileButton = document.getElementById('eMobileButton');

function toggleMobile() {
    Array.from(eMobiles).forEach(e => e.classList.contains('hidden') ? (eMobileBorder.classList.add('border-b-[1px]'), e.classList.remove('hidden'), (eMobileButton.children[0].classList.add('hidden'), eMobileButton.children[1].classList.remove('hidden'))) : (e.classList.add('hidden'), (eMobileBorder.classList.remove('border-b-[1px]'), eMobileButton.children[0].classList.remove('hidden'), eMobileButton.children[1].classList.add('hidden'))));
}

function resetMobile() {
    Array.from(eMobiles).forEach(e => window.matchMedia('(min-width: 1024px)') ? (eMobileBorder.classList.remove('border-b-[1px]'), e.classList.add('hidden'), eMobileButton.children[0].classList.remove('hidden'), eMobileButton.children[1].classList.add('hidden')) : {});
}

function rememberFormValues() {
    const eRegisterName = document.getElementById('eRegisterName')
    const eRegisterUsername = document.getElementById('eRegisterUsername')
    const sRegisterName = sessionStorage.getItem('register-name');
    const sRegisterUsername = sessionStorage.getItem('register-username');

    if (eRegisterName && sRegisterName) document.forms[0].name.value = sRegisterName;
    if (eRegisterUsername && sRegisterUsername) document.forms[0].username.value = sRegisterUsername;
}

function storeFormValues() {
    sessionStorage.setItem('register-name', document.forms[0].name.value);
    sessionStorage.setItem('register-username', document.forms[0].username.value);
}

function checkScroll() {
    const eBtn = document.getElementById('buttonScrollToTop');
    let classes = [];

    classes = document.body.scrollTop > 20 || document.documentElement.scrollTop > 20 ? ['invisible', 'visible'] : ['visible', 'invisible'];

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
    rememberFormValues();
    checkScroll();
});
window.addEventListener('resize', resetMobile);
window.addEventListener('scroll', checkScroll);
window.addEventListener('touchmove', checkScroll);
