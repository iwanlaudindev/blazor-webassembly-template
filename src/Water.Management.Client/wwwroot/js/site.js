
export function addToggleEvent() {
    const body = document.querySelector('body');
    const toggleBtn = document.querySelector('.toggle-sidebar-btn');

    toggleBtn.addEventListener('click', function () {
        body.classList.toggle('toggle-sidebar');
    });
};

export function loadMap() {
    let map = L.map('map').setView([0, 0], 16);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            let lat = position.coords.latitude;
            let lon = position.coords.longitude;
            map.setView([lat, lon], 9);
            L.marker([lat, lon]).addTo(map);
        });
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
}

export function saveAsFile(fileName, byteBase64) {
    const byteCharacters = atob(byteBase64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}