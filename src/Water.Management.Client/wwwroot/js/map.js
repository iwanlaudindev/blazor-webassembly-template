
export async function initMap(dataPoints) {
    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");

    const mapOptions = {
        zoom: 11,
        mapId: "DEMO_MAP_ID",
        mapTypeId: "hybrid",
        //mapTypeControl: false,
        streetViewControl: false,
        gestureHandling: "greedy"
    };

    let currentPosition;
    try {
        const position = await new Promise((resolve, reject) => {
            navigator.geolocation.getCurrentPosition(resolve, reject);
        });

        currentPosition = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
        };
    } catch (error) {
        console.error("Error getting current location:", error);
        currentPosition = { lat: -8.620743, lng: 120.728321 };
    }

    const map = new Map(document.getElementById("map"), { ...mapOptions, center: currentPosition });
    const infoWindow = new InfoWindow();

    function createMarkerIcon(type) {
        const icon = document.createElement("div");
        icon.innerHTML = `<i class="bi bi-${type === "WaterSource" ? 'droplet-half' : 'geo'}"></i>`;
        return icon;
    }

    function createMarker(data) {
        const icon = createMarkerIcon(data.type);

        const pin = new PinElement({
            glyph: icon,
            glyphColor: "white",
            background: data.type === "WaterSource" ? "#0d6efd" : "#dc3545",
            borderColor: "black",
        });

        return new AdvancedMarkerElement({
            map,
            position: { lat: data.latitude, lng: data.longitude },
            content: pin.element,
            title: data.title,
        });
    }

    async function handleMarkerClick(marker) {
        try {
            const detailData = await showDetailData(marker.data.type, marker.data.id);
            const htmlContent = setInfoWindowContent(detailData, marker.data.type);

            infoWindow.close();
            infoWindow.setContent(htmlContent);
            infoWindow.open(marker.map, marker);
        } catch (error) {
            console.error("Error:", error);
        }
    }

    function addMarker(data) {
        const marker = createMarker(data);

        marker.data = {
            id: data.id,
            type: data.type,
        };

        marker.addListener("click", () => handleMarkerClick(marker));
    }

    dataPoints.forEach((dataPoint) => {
        addMarker(dataPoint, map)
    });
}

function showDetailData(type, id) {
    const method = type === "WaterSource"
        ? "ShowDetailWaterSourceAsync"
        : "ShowDetailGeophysicsAsync";

    return DotNet.invokeMethodAsync('Water.Management.Client', method, id);
};

function setInfoWindowContent(data, type) {
    return type === "WaterSource"
        ? setHtmlContentWaterSource(data)
        : setHtmlContentGeophysics(data);
}

function setHtmlContentWaterSource(data) {
    const htmlContent = `
        <div id="content" style="width: 25rem;">
            <div id="bodyContent" class="card-body">
                <div class="row justify-content-center">
                    <div class="col-12 text-center">
                        <h5 class="fw-bold">Detailed Information</h5>
                        <p>Latitude: ${data.latitude} | Longitude: ${data.longitude}</p>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold ">Location</div>
                    <div class="col-8">${data.SubDistrict}, ${data.district}, ${data.city}, ${data.province}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold ">Detail Location</div>
                    <div class="col-8">${data.detailLocation ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold ">Type</div>
                    <div class="col-8">${data.type ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">Ph</div>
                    <div class="col-8">${data.ph ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">TDS_ppm</div>
                    <div class="col-8">${data.tdsppm ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                <div class="col-4 fw-bold">Depth</div>
                    <div class="col-8">${data.depth ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">Water Table</div>
                    <div class="col-8">${data.waterTable ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">Elevation</div>
                    <div class="col-8">${data.elevation ?? "-"}</div>
                </div>
            </div>
        </div>
        `;

    return htmlContent;
}

function setHtmlContentGeophysics(data) {
    const htmlContent = `
        <div id="content" style="width: 25rem;">
            <div id="bodyContent" class="card-body">
                <div class="row justify-content-center">
                    <div class="col-12 text-center">
                        <h5 class="fw-bold">Detailed Information</h5>
                        <p>Latitude: ${data.latitude} | Longitude: ${data.longitude}</p>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold ">Location</div>
                    <div class="col-8">${data.SubDistrict}, ${data.district}, ${data.city}, ${data.province}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold ">Detail Location</div>
                    <div class="col-8">${data.detailLocation ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">Aquifer</div>
                    <div class="col-8">${data.aquifer ?? "-"}</div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-4 fw-bold">Elevation</div>
                    <div class="col-8">${data.elevation ?? "-"}</div>
                </div>
            </div>
        </div>
        `;

    return htmlContent;
}

// Define the getCurrentPosition() function
export async function getCurrentPosition() {
    // Try to get the current position
    try {
        const position = await new Promise((resolve, reject) => {
            navigator.geolocation.getCurrentPosition(resolve, reject);
        });

        if (position) {
            return [position.coords.latitude, position.coords.longitude];
        } else {
            console.error("Location access denied or unavailable.");
        }
    } catch (error) {
        console.error("Error fetching location:", error);
    }
}