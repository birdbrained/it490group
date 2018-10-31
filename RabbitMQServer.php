#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function logErrors($request){
	echo $request['message'].PHP_EOL;
	file_put_contents('errors/ERROR_LOG.help', $request['message'].PHP_EOL, FILE_APPEND | LOCK_EX);
}

class DBi
{
	public static $mydb;
}

//(DBi::$mydb = mysqli_connect('192.168.1.9', 'user', 'password', 'Project', '3306') ) or die ("failed to connect".PHP_EOL);
//Ankit DB
(DBi::$mydb = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306') ) or die ("failed to connect".PHP_EOL);

//($mydb = mysqli_connect('127.0.0.1', 'user', 'Pasta_Fazool!?', 'Project', '3306') ) or die ("failed to connect".PHP_EOL);
if (DBi::$mydb->errno != 0)
{
	echo "you fail: ".DBi::$mydb->error.PHP_EOL;
	exit(0);
}
echo "mysqli connected.\n";

function doLogout($database, $u)
{
	$s = "INSERT into UserHistory VALUES ('NOW()', '$u', 'Logout');";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));
	return true;
}

function doLogin($database,$e,$u,$p)
{
	$p = mysqli_real_escape_string($database,$p);


	$u = mysqli_real_escape_string($database,$u);

	$s = "select * from Users where username = '$u' and password = '$p'";
	//$s = "insert into testTable values('$e', '$u', '$p')";
	echo "about to make a query with '$u', '$p', '$e'\n";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));

	if (mysqli_num_rows($t) > 0)

	{ 

		echo "Found user '$u' with password '$p' and email '$e'" . PHP_EOL;
		$s = "INSERT into UserHistory VALUES ('NOW()', '$u', 'Login');";
		mysqli_query($database, $s) or die(mysqli_error($database));
		return true;

	}

	else
	{
		echo "Error finding num rows" . PHP_EOL;
		return false;
	}
}

function doRegister($database, $e, $u, $p)
{
	$p = mysqli_real_escape_string($database,$p);
	$u = mysqli_real_escape_string($database,$u);

	$stmt = "insert into Users values('$u', '$p', '$e', '200')";
	$t = mysqli_query($database, $stmt);

	if ($database->errno == 0)
	{
		echo "Registering user with email ('$e'), username ('$u'), and password ('$p')\n";
		return true;
	}
	else
	{
		$request['message'] = "Error registering user";
		logErrors($request);
		return false;
	}
}

//function purchase($database, $ID, $price, $u)
//{	
	//$u = mysqli_real_escape_string($database,$u);
	
	//mysqli_query(
//}

function requestProcessor($request)
{
	echo "received request of type: $request".PHP_EOL;
	var_dump($request);
	$success = false;
	if(!isset($request['type']))
	{
		$request['message'] = "ERROR: unsupported message type";
		logErrors($request);
		return "ERROR: unsupported message type";
	}
	switch ($request['type'])
	{
	case "login":
		$success = doLogin(DBi::$mydb, $request['email'], $request['username'],$request['password']);

		if ($success == false)
		{
			$request['message'] = "Invalid login credentials";
			logErrors($request);			
			return array("returnCode" => '1', "message" => "Invalid login credentials");
		}
		break;
	case "register":
		$success = doRegister(DBi::$mydb, $request['email'], $request['username'], $request['password']);
		if ($success == false)
		{
			$request['message'] = "Registration error";
			logErrors($request);						
			return array("returnCode" => '2', "message" => "Registration error");
		}
		break;
	case "validate_session":
		return doValidate($request['sessionId']);
		break;
	case "transaction":
		//($request['ID'], $request['price'], $request['username']);
		break;
	case "error":
		logErrors($request);
		return false;		
		break;
	case "logout":
		doLogout(DBi::$mydb, $request['username'])
	}
	

	echo "done with request.\n";
	if ($success == false)
	{
		$request['message'] = "Invalid login credentials";
		logErrors($request);				
		return array("returnCode" => '1', "message" => "Invalid login credentials");
	}	
	return array("returnCode" => '0', 'message'=>"Server received request and processed");
}

$server = new rabbitMQServer("testRabbitMQ.ini","testServer");
echo "RabbitMQ Server: BEGIN".PHP_EOL;
$server->process_requests('requestProcessor');
exit();
?>

