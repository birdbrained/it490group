function validateForm() 
{
	var errors = "";
	
	var email = document.forms["Paypal Info Form"]["payer_email"].value;

	if (email == "") {
		errors += "Email required\n";
	}

	var fname = document.forms["Paypal Info Form"]["firstname"].value;

	if (fname == "") {
		errors += "First name required\n";
	}

	var passwd = document.forms["Paypal Info Form"]["password"].value;

	if (passwd == "") {
		errors += "Password required\n";
	}

	var lname = document.forms["Paypal Info Form"]["lastname"].value;

	if (lname == "") {
		errors += "Last name required\n";
	}

	if (errors == "") {
		return true;
	}

	else {
		alert(errors);
		return false;
	} 
}


