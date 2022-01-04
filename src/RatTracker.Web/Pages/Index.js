$(function () {
    abp.log.debug('Index.js initialized!');

    function createMap() {
        var template = kendo.template($("#info-template").html());
        var emptyTemplate = kendo.template($("#empty-info-template").html());
        var activeShape;

        var markerData = new kendo.data.DataSource({
            transport: {
                read: function (opts) {
                    var centerPoint = $("#map").data("kendoMap").options.center;
                    window.ratTracker.schools.schools.getGeoCoordinate(centerPoint[0], centerPoint[1]).then(function (result) {
                        opts.success(result);
                    });
                }
            }
        });

        $("#map").kendoMap({
            center: [44.2471971, -76.5796658],
            zoom: 12,
            layers: [{
                type: "tile",
                urlTemplate: "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png",
                subdomains: ["a", "b", "c"],
                attribution: "&copy; <a href='https://osm.org/copyright'>OpenStreetMap contributors</a>"
            }, {
                type: "marker",
                dataSource: markerData,
                locationField: "latLng",
                titleField: "name"
            }]
        });
        $("#map").unbind("mousewheel");
        $("#map").unbind("DOMMouseScroll");
    }
    $(document).ready(createMap);
});
