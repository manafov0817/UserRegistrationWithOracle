
var lngVal = $('#longitude');
var latVal = $('#latitude');
var markAs = $('#markAs');

var latlng = [latVal.val(),lngVal.val()];

var map = L.map('map').setView(latlng, 15);

var popUpMessage = null;

var oldMarker = L.marker(latlng);

$('#show-map-modal').on('show.bs.modal', function () {
    setTimeout(function () {
        map.invalidateSize();
        oldMarker.addTo(map);
     }, 500);
});


var geocodeService = L.esri.Geocoding.geocodeService({
    apikey: "AAPKd7808b63d89347da988baa42d47d7b61JOvhjB3LJp55tjF_9mGsLh_Gy6BS2Ww9V98uyiRdQAZYFgbaVrfmbh-f60nCAdv0"
});

//osm layer
var osm = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
});

osm.addTo(map);



oldMarker = L.marker(latlng);

function onMapClick(e) {


    latVal.val(e.latlng.lat);
    lngVal.val(e.latlng.lng);

    var newMarker = L.marker(e.latlng).addTo(map);

    if (oldMarker != null) {
        oldMarker.remove();
    }

    oldMarker = newMarker;

    geocodeService.reverse().latlng(e.latlng).run(function (error, result) {
        if (error) {
            return;
        }

        oldMarker.addTo(map).bindPopup(result.address.Match_addr).openPopup();

        markAs.val(result.address.Match_addr);
    });

};

var showPopUp = function () {
    geocodeService.reverse().latlng(latlng).run(function (error, result) {
        if (error) {
            return;
        }
        console.log(result.address.Match_addr);
        console.log(`lattitude is: ${latVal.val()} lng is ${lngVal.val()}`);

        oldMarker.bindPopup(result.address.Match_addr).openPopup();
    })
};

map.on('click', showPopUp);
map.on('click', onMapClick);