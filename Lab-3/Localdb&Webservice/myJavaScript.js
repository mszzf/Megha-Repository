var MyMap = function () {
    this.name = 'megha';
    this.latitude = 0;
    this.longitude = 0;
    this.center = 0;
    this.zip = '66223';
    this.zoom = 15;
    this.mapTypeId = google.maps.MapTypeId.ROADMAP;
    this.mapOptions = '';
    this.nearMeType = '';
    this.radius = 5000;
    this.source = '';
    this.destination = '';
}


MyMap.prototype = {
    init: function (which, type,fromId,toId) {
        var that = this;
        if (this.zip) {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': this.zip }, function (location, status) {
                if (status == 'OK') {
                    that.latitude = location[0].geometry.location.lat();
                    that.longitude = location[0].geometry.location.lng();
                    that.initMap(which, type,fromId,toId);
                }
            });
        } else if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                that.latitude = position.coords.latitude;
                that.longitude = position.coords.longitude;
                that.initMap(which, type,fromId,toId);
            });
        }
    },
    initMap: function (which, type,fromId,toId) {
        var that = this;
        that.center = new google.maps.LatLng(that.latitude, that.longitude);
        that.mapOptions = {
            zoom: that.zoom,
            mapTypeId: that.mapTypeId,
            center: that.center
        }
        switch (which) {
            case 'r':
                var mapContainer = document.getElementById('mapRegular');
                if (mapContainer) {
                    that.centeredMap(mapContainer);
                }
                break;
            case 'd':
                var directionContainer = document.getElementById('mapDirection');
                if (directionContainer) {
					var fromElement=document.getElementById(fromId);
					if(fromElement){
					var from=fromElement.value;
					that.source=from;
					var toElement=document.getElementById(toId);
					if(toElement){
					var to=toElement.value;
					that.destination=to;					
					}
					}
					
                    that.direction(directionContainer);
                }
                break;
            case 'n':
                that.nearMeType = type;
                var nearMeContainer = document.getElementById('mapNearMe');
                if (nearMeContainer) {
                    that.nearMe(nearMeContainer);
                }
                break;
            case 'w':
                var weatherContainer = document.getElementById('mapWeather');
                if (weatherContainer) {
                    that.weather(weatherContainer);
                }
                break;
			case 'l':
			
				var userName = document.getElementById('userid').value;
				var password = document.getElementById('password').value;
			$.ajax({
				url: "http://localhost/aspnet_client/WcfService1/WcfService1/WcfService1/Service1.svc/data/"+userName+"/" + password
				}).then(function(data){
				if(data!='false'){
				document.getElementById('userName').innerHTML = data;
				}
				});
				
        }
    },
    centeredMap: function (mapContainer) {
        var map = new google.maps.Map(mapContainer, this.mapOptions);
        var pos = new google.maps.LatLng(this.latitude, this.longitude);
        var infowindow = new google.maps.InfoWindow({
            map: map,
            position: pos,
            content: 'I am here'
        });
    },
    direction: function (directionContainer) {
        var myOptions = {
            zoom: 8,
            mapTypeId: this.mapTypeId,
            center: new google.maps.LatLng(0, 0)
        }
        var map2 = new google.maps.Map(directionContainer, myOptions);

        var directionsDisplay = new google.maps.DirectionsRenderer();
        directionsDisplay.setMap(map2);
        var request =
    {
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
    weather: function (weatherContainer) {

        var myOptions = {
            zoom: 8,
            mapTypeId: this.mapTypeId,
            center: this.mapOptions.center
        }
        var weatherLayer = new google.maps.weather.WeatherLayer({
            temperatureUnits: google.maps.weather.TemperatureUnit.FAHRENHEIT
        });
        var map = new google.maps.Map(weatherContainer, myOptions);
        weatherLayer.setMap(map);

        var cloudLayer = new google.maps.weather.CloudLayer();
        cloudLayer.setMap(map);
    },
    nearMe: function (nearMeContainer) {
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
        var map = new google.maps.Map(nearMeContainer, myOptions);
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
    }
	
}
var myMap = new MyMap();


var returnMsg;
var stores = [];

function saveStatusLocally() {
	
//var inputField = document.getElementById('inputString');
//var stringToSave = inputField.value;
		
		var fromElement=document.getElementById('From');
					if(fromElement){
					var from=fromElement.value;
					}
					var toElement=document.getElementById('To');
					if(toElement){
					var to=toElement.value;

	}
	if ((from != null) && (to != null)) {
    
	var currentdate = new Date();   
	
		stores.push(currentdate);
		window.localStorage.setItem("1", stores.join("@ From: "));
        stores.push(from);
		window.localStorage.setItem("1", stores.join("To  "));
		//clear the input field for visual 
        stores.push(to);
		 //print that value into the local storage named database and joing by a non-breaking space
        window.localStorage.setItem("1", stores.join("  "));
        //confirm write
		//document.getElementById('myPlaces').innerHTML = "data stored.";
		 
			}
		}
		
		
		// read the string
function readStatus() {

//print the value of the local storage "database" key

if (window.localStorage.getItem("1") == null) {
        document.getElementById('myPlaces').innerHTML = "nothing stored.";
		
		 } else {
        document.getElementById('myPlaces').innerHTML = window.localStorage.getItem("1");
		}
		}
		
