class AdminObj {
    constructor() {
        this.init();
        
    }
    //#region Class object inits
    initFieldObjects() {
        this.field.Team = document.getElementById('Team');
        this.field.User = document.getElementById('User');
    }
    initBtnObjects() {
        this.btn.TeamToggle = document.getElementById('TeamToggle');
        this.btn.UserToggle = document.getElementById('UserToggle');
        this.btn.VideoToggle = document.getElementById('VideoToggle');
        this.btn.TextToggle = document.getElementById('TextToggle');
        this.btn.AudioToggle = document.getElementById('AudioToggle');
    }
    //#endregion
    init() {
        this.field = {};
        this.initFieldObjects();
        this.btn = {};
        this.initBtnObjects();
        this.handleBtnTeam = this.handleBtnTeam.bind(this);
        this.handleBtnUser = this.handleBtnUser.bind(this);
        this.handleBtnTextResource = this.handleBtnTextResource.bind(this);
        this.handleBtnVideoResource = this.handleBtnVideoResource.bind(this);
        this.handleBtnAudioResource = this.handleBtnAudioResource.bind(this);
        this.clearField = this.clearField.bind(this);
        this.btn.TeamToggle.addEventListener('click', this.handleBtnTeam);
        this.btn.UserToggle.addEventListener('click', this.handleBtnUser);
        this.btn.VideoToggle.addEventListener('click', this.handleBtnVideoResource);
        this.btn.TextToggle.addEventListener('click', this.handleBtnTextResource);
        this.btn.AudioToggle.addEventListener('click', this.handleBtnAudioResource);
        console.log('Page Loaded');
    }
    //#region Render
    renderCreateTeam() {}
    renderCreateUser() {}
    renderVideoResource() {}
    renderTextResource() {}
    renderAudioResource() {}
    //#endregion
    //#region Handlers
    handleBtnTeam() {
        console.log('Team Button')
        this.field.Team.hidden = !this.field.Team.hidden;
    }
    handleBtnUser() {
        console.log('User Button')
        this.field.User.hidden = !this.field.User.hidden;
    }
    handleBtnVideoResource() { console.log('Video Button')}
    handleBtnTextResource() { console.log('Text Button')}
    handleBtnAudioResource() { console.log('Audio Button')}
    //#endregion
    //#region Form Controls
    clearField() { }
    //#endregion

}
window.onload = () => { const vm = new AdminObj(); }