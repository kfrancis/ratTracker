$(function () {
    abp.log.debug('Index.js initialized!');

    function createMap() {
        $("#map").kendoMap({
            center: [30.268107, -97.744821],
            zoom: 3,
            layers: [{
                type: "tile",
                urlTemplate: "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png",
                subdomains: ["a", "b", "c"],
                attribution: "&copy; <a href='https://osm.org/copyright'>OpenStreetMap contributors</a>"
            }],
            markers: [{
                location: [30.268107, -97.744821],
                shape: "pinTarget",
                tooltip: {
                    content: "Austin, TX"
                }
            }]
        });
    }
    createMap();
});
