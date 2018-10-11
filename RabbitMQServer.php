#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

class DBi
{
	public static $mydb;
}

//(DBi::$mydb = mysqli_connect('192.168.1.9', 'user', 'password', 'testDatabase', '3306') ) or die ("failed to connect".PHP_EOL);
(DBi::$mydb = mysqli_connect('192.168.1.6', 'user', 'password', 'testDatabase', '3306') ) or die ("failed to connect".PHP_EOL);

//($mydb = mysqli_connect('127.0.0.1', 'user', 'Pasta_Fazool!?', 'testDatabase', '3306') ) or die ("failed to connect".PHP_EOL);
if (DBi::$mydb->errno != 0)
{
	echo "you fail: ".DBi::$mydb->error.PHP_EOL;
	exit(0);
}
echo "mysqli connected.\n";

function doLogin($database,$e,$u,$p)
{
	$p = mysqli_real_escape_string($database,$p);


	$u = mysqli_real_escape_string($database,$u);

	$s = "select * from testTable where username = '$u' and password = '$p'";
	//$s = "insert into testTable values('$e', '$u', '$p')";
	echo "about to make a query with '$u', '$p', '$e'\n";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));

	if (mysqli_num_rows($t) > 0)

	{ 

		echo "Found user '$u' with password '$p' and email '$e'" . PHP_EOL;
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

	$stmt = "insert into testTable values('$e', '$u', '$p')";
	$t = mysqli_query($database, $stmt);

	if ($database->errno == 0)
	{
		echo "Registering user with email ('$e'), username ('$u'), and password ('$p')\n";
		return true;
	}
	else
	{
		echo "Error registering user\n";
		return false;
	}
}

function requestProcessor($request)
{
	echo "received request of type: $request".PHP_EOL;
	var_dump($request);
	$success = false;
	if(!isset($request['type']))
	{
		return "ERROR: unsupported message type";
	}
	switch ($request['type'])
	{
	case "login":
		$success = doLogin(DBi::$mydb, $request['email'], $request['username'],$request['password']);

		if ($success == false)
		{
			return array("returnCode" => '1', "message" => "Invalid login credentials");
		}
		break;
	case "register":
		$success = doRegister(DBi::$mydb, $request['email'], $request['username'], $request['password']);
		if ($success == false)
		{
			return array("returnCode" => '2', "message" => "Registration error");
		}
		break;
	case "validate_session":
		return doValidate($request['sessionId']);
		break;
	}
	echo "done with request.\n";
	if ($success == false)
		return array("returnCode" => '1', "message" => "Invalid login credentials");
	return array("returnCode" => '0', 'message'=>"Server received request and processed");
}

$server = new rabbitMQServer("testRabbitMQ.ini","testServer");
echo "RabbitMQ Server: BEGIN".PHP_EOL;
$server->process_requests('requestProcessor');
exit();
?>

