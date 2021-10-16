
var lngVal = $('#viewlng');
var latVal = $('#viewlat');
var markAs = $('#viewmark');

console.log(`lattitude is: ${latVal.val()} lng is ${lngVal.val()}`);

var latlng = [ latVal.val(), lngVal.val()];

var map = L.map('map').setView(latlng, 20);

var marker = L.marker(latlng);

$('#show-map-modal').on('show.bs.modal', function () {
    setTimeout(function () {
        map.invalidateSize();
        marker.addTo(map);
        showPopUp();
    }, 500);
});

var geocodeService = L.esri.Geocoding.geocodeService({
    apikey: "AAPKd7808b63d89347da988baa42d47d7b61JOvhjB3LJp55tjF_9mGsLh_Gy6BS2Ww9V98uyiRdQAZYFgbaVrfmbh-f60nCAdv0"
});

var osm = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
});

osm.addTo(map);

var showPopUp = function () {
    geocodeService.reverse().latlng(latlng).run(function (error, result) {
        if (error) {
            return;
        }
        console.log(result.address.Match_addr);

        marker.bindPopup(result.address.Match_addr).openPopup();
    })
};

marker.on('click', showPopUp);
map.on('click', showPopUp);





