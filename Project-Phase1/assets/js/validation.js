function SaveUser(){
	var userName;
	var password;
	var emailAddress;
	userName =	document.getElementById('usernamesignup').value;
	password =	document.getElementById('passwordsignup').value;
	emailAddress =	document.getElementById('emailsignup').value;
	address =	document.getElementById('addresssignup').value;
	phoneNumber =	document.getElementById('phonenumbersignup').value;

	var loginDetails = {};
	
	loginDetails['userName'] = userName;
	loginDetails['password'] = password;
	loginDetails['emailAddress'] = emailAddress;
	loginDetails['address'] = address;
	loginDetails['phoneNumber'] = phoneNumber;

	localStorage.clear('loginDetails');
	localStorage.setItem( 'loginDetails', JSON.stringify(loginDetails));
	document.write('<iframe src="login.htm" height = "100%" width="100%"></iframe>');
}

function VerifyUser(){
	var userName;
	var password;
	userName =	document.getElementById('username').value;
	password =	document.getElementById('password').value;

	var loginDetails = new Array();
	loginDetails = JSON.parse(localStorage.getItem("loginDetails"));

var sUserName; var sPassword;
	for (var k in loginDetails) {
    if (loginDetails.hasOwnProperty(k)) {
		if(k == 'userName'){
			sUserName = loginDetails[k]
	}
					if(k == 'password'){
			sPassword = loginDetails[k]
	}
    }
	}
	if(sUserName == userName){
		if(sPassword == password){	
			document.write('<iframe src="homepage.html" height = "100%" width="100%"></iframe>');
		}
	else{
		alert("Password does not match");
		document.write('<iframe src="login.htm" height = "100%" width="100%"></iframe>');
	}
	}
	else
	{
		alert("Username " + userName + " does not exist");
		document.write('<iframe src="login.htm" height = "100%" width="100%"></iframe>');
	}
}
