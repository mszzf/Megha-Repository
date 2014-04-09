			var recommendation = '';
			
            if (data.simulation.temperature <= 65 && data.simulation.temperature >= 60) {
                recommendation += 'Temperature Setting is perfect'+"<br/>";
				
				
            } else if (data.simulation.temperature < 60) {
                recommendation += "Looks like your thermostat temperature is too low. " +"<br/>"+"Please maintain your thermostat temperature between 60 and 65."+'\r\n';
				recommendation_speak += "Looks like your thermostat temperature is too low. "+"Please maintain your thermostat temperature between 60 and 65."+'\r\n';
				
            } else if (data.simulation.temperature > 65) {

                if (data.external.humidity >= 35 && data.external.humidity <= 45 && data.external.temperature >= 60 && data.external.humidity <= 65){
                    recommendation += "Please turn off your heating system if on. "+"<>br/"+"Open your windows to maintain your home temperature between 60 and 65. ";
					recommendation_speak += "Please turn off your heating system if on. "+"Open your windows to maintain your home temperature between 60 and 65. ";
					}
				else
				{
				   recommendation += "Please lower your thermostat setting for temperature to 65. "+"<br/>";
				   recommendation_speak += "Please lower your thermostat setting for temperature to 65. ";
				}

            } 
			
			if (data.simulation.humidity >= 35 && data.external.humidity <= 45) 
					{recommendation += 'Humidity Setting is perfect. '+"<br/>";	
					recommendation_speak += 'Humidity Setting is perfect. ';	
					}
             else if (data.simulation.humidity >45){
			   
			   if(data.external.humidity >= 35 && data.external.humidity <= 45 && data.external.temperature >= 60 && data.external.humidity <= 65)
                  {  recommendation +='Maintain your humidity between 35 and 45. '+"<br/>"+"Please open your windows to reduce humidity. "+"<br/>";
					recommendation_speak +='Maintain your humidity between 35 and 45. '+"Please open your windows to reduce humidity. ";
					}
                else
                   { recommendation += "Please turn on the dehumidifier. "+"<br/>";
					recommendation_speak += "Please turn on the dehumidifier. ";
					}
            }
            else if (data.simulation.humidity < 35) {
				
                recommendation += 'Maintain your humidity between 35 and 45. '+"<br/>";
				recommendation_speak += 'Maintain your humidity between 35 and 45. ';
                if (data.external.humidity >=35 && data.external.humidity <=45 && data.external.temperature >= 60 && data.external.humidity <= 65)
                  {  recommendation += 'Open the windows to increase the humidity. '+"<br/>";
					recommendation_speak += 'Open the windows to increase the humidity. ';
				}	
				else
					{recommendation += 'Turn on the Humidifier. '+"<br/>";
					recommendation_speak += 'Turn on the Humidifier. ';
					}
            }
            			
            var recommendations = document.getElementById('recommendation');
            recommendations.innerHTML = recommendation;