document.write('script type="text/javascript" src="soapclient.js" ></script>');

var MyMap = function () {
    this.name = 'megha';
    this.latitude = 0;
    this.longitude = 0;
    this.center = 0;
    this.zip = '66223';
    this.zoom = 15;
    this.mapOptions = '';
    this.nearMeType = '';
    this.radius = 5000;
    this.source = '';
    this.destination = '';
    this.soapContainer = '';
    this.pl='';
}


MyMap.prototype = {
    initSoap: function (container) {
        this.soapContainer = document.getElementById(container);
        this.pl = new SOAPClientParameters();
        this.soapRefresh(true, false);

    },
    init: function (containerId, which, type, fromId, toId) {
        var that = this;
        if (this.zip) {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({
                'address': this.zip
            }, function (location, status) {
                if (status == 'OK') {
                    that.latitude = location[0].geometry.location.lat();
                    that.longitude = location[0].geometry.location.lng();
                    that.initMap(containerId, which, type, fromId, toId);
                }
            });
        } else if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                that.latitude = position.coords.latitude;
                that.longitude = position.coords.longitude;
                that.initMap(containerId, which, type, fromId, toId);
            });
        }
    },
    initMap: function (containerId, which, type, fromId, toId) {
        var container = document.getElementById(containerId);;
        var that = this;
        that.center = new google.maps.LatLng(that.latitude, that.longitude);
        that.mapOptions = {
            zoom: that.zoom,
            mapTypeId: that.mapTypeId,
            center: that.center
        }
        switch (which) {
        case 'r':
            if (container) {
                that.centeredMap(container);
            }
            break;
        case 'd':
            if (container) {
                var fromElement = document.getElementById(fromId);
                if (fromElement) {
                    var from = fromElement.value;
                    that.source = from;
                    var toElement = document.getElementById(toId);
                    if (toElement) {
                        var to = toElement.value;
                        that.destination = to;
                    }
                }

                that.direction(container);
            }
            break;
        case 'n':
            that.nearMeType = type;
            if (container) {
                that.nearMe(container);
            }
            break;
        case 'w':
            if (container) {
                that.weather(container);
            }
            break;
        case 'l':

            var userName = document.getElementById('userid').value;
            var password = document.getElementById('password').value;
            $.ajax({
                url: "http://192.168.1.9/aspnet_client/WcfService1/WcfService1/WcfService1/Service1.svc/data/" + userName + "/" + password
            }).then(function (data) {
                if (data != 'false') {
                    document.getElementById('userName').innerHTML = data;
                }
            });

        }
    },
    centeredMap: function (container) {
        var map = new google.maps.Map(container, this.mapOptions);
        var pos = new google.maps.LatLng(this.latitude, this.longitude);
        var infowindow = new google.maps.InfoWindow({
            map: map,
            position: pos,
            content: 'I am here'
        });
    },
    direction: function (container) {
        var myOptions = {
            zoom: 8,
            mapTypeId: this.mapTypeId,
            center: new google.maps.LatLng(0, 0)
        }
        var map2 = new google.maps.Map(container, myOptions);

        var directionsDisplay = new google.maps.DirectionsRenderer();
        directionsDisplay.setMap(map2);
        var request = {
            origin: this.source,
            destination: this.destination,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        var directionsService = new google.maps.DirectionsService();
        var directionInstructions = document.getElementById('directionInstructions');
        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                var route = response.routes[0];
                var leg = route.legs[0]
                var steps = leg.steps;
                var instructions = '';
                for (var i = 0; i < steps.length; i++) {
                    var step = steps[i];
                    instructions = instructions + step.instructions + '<br/>';
                }
                if (directionInstructions) {
                    directionInstructions.innerHTML = instructions;
                }

            }
        });
    },
    weather: function (container) {

        var myOptions = {
            zoom: 8,
            mapTypeId: this.mapTypeId,
            center: this.mapOptions.center
        }
        var weatherLayer = new google.maps.weather.WeatherLayer({
            temperatureUnits: google.maps.weather.TemperatureUnit.FAHRENHEIT
        });
        var map = new google.maps.Map(container, myOptions);
        weatherLayer.setMap(map);

        var cloudLayer = new google.maps.weather.CloudLayer();
        cloudLayer.setMap(map);
    },
    nearMe: function (container) {
        var request = {
            location: this.mapOptions.center,
            radius: this.radius,
            types: [this.nearMeType]
        };

        var myOptions = {
            zoom: 12,
            mapTypeId: this.mapTypeId,
            center: this.mapOptions.center
        }
        var map = new google.maps.Map(container, myOptions);
        var that = this;
        var service = new google.maps.places.PlacesService(map);
        service.nearbySearch(request, function (results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                for (var i = 0; i < results.length; i++) {
                    var place = results[i];
                    that.createMarker(map, results[i]);
                }
            }
        });

    },
    createMarker: function (map, result) {
        var marker = new google.maps.Marker({
            map: map,
            position: result.geometry.location
        });
    },

    soap: function (containerId, functionName) {
        var that = this;
        var url = "http://134.193.136.38/aspnet_client/Group5/WebSite5/WebService.asmx";
		var pl = new SOAPClientParameters();
        this.soapMethod = functionName;
		pl = this.soapRefresh(false, true, pl);


        SOAPClient.invoke(url, functionName, pl, true, function (r) {

            if (that.soapContainer) {
                that.soapContainer.innerHTML = r;
            }
        });

    },

    soapRefresh: function (load, displayResult, pl) {				

        switch (this.soapMethod) {
        case "QueryTable":
                pl.add("user_name1", document.getElementById("nameSelect").value);
            break;
        case "InsertTable":
                pl.add("user_name1", document.getElementById("usernamesignup").value);
                pl.add("email", document.getElementById("emailsignup").value);
                pl.add("password", document.getElementById("passwordsignup").value);
				pl.add("address", document.getElementById("addresssignup").value);
                pl.add("phonenumber", document.getElementById("phonenumbersignup").value);
            break;

        case "DeleteTable":
                pl.add("name", document.getElementById("nameDelete").value);
            break;
        }
		return pl;
    }

}
var myMap = new MyMap();