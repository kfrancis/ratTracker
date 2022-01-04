$(function () {
    abp.log.debug('Index.js initialized!');

    function createMap() {
        var template = kendo.template($("#info-template").html());
        var emptyTemplate = kendo.template($("#empty-info-template").html());
        var activeShape;

        $("#map").kendoMap({
            center: [44.2471971, -76.5796658],
            zoom: 12,
            layers: [{
                type: "tile",
                urlTemplate: "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png",
                subdomains: ["a", "b", "c"],
                attribution: "&copy; <a href='https://osm.org/copyright'>OpenStreetMap contributors</a>"
            }, {
                type: "bubble",
                attribution: "Population data from Nordpil and UN Population Division.",
                style: {
                    fill: {
                        color: "#00f",
                        opacity: 0.4
                    },
                    stroke: {
                        width: 0
                    }
                },
                dataSource: {
                    transport: {
                        read: {
                            url: "../content/dataviz/map/urban-areas.json",
                            dataType: "json"
                        }
                    }
                },
                locationField: "Location",
                valueField: "Pop2010"
            }]
        });
    }
    $(document).ready(createMap);
});
