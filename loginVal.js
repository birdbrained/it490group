function validateForm() 
{
	var errors = "";
	
	var email = document.forms["RegLog Form"]["email"].value;

	if (email == "") {
		errors += "Email required\n";
	}

	var uname = document.forms["RegLog Form"]["username"].value;

	if (uname == "") {
		errors += "Username required\n";
	}

	var passwd = document.forms["RegLog Form"]["password"].value;

	if (passwd == "") {
		errors += "Password required\n";
	}

	var radio = document.forms["RegLog Form"]["sessionType"].value;

	if (radio == "") {
		errors += "Session type required\n";
	}

	if (errors == "") {
		return true;
	}

	else {
		alert(errors);
		return false;
	} 
}


