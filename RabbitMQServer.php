#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function logErrors($request){
	echo $request['type']." : ";	
	echo $request['message'].PHP_EOL;
	file_put_contents('errors/ERROR_LOG.help', $request['message'].PHP_EOL, FILE_APPEND | LOCK_EX);
}

class DBi
{
	public static $mydb;
}

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
	$s = "INSERT into UserHistory VALUES (NOW(), '$u', 'logout');";
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
		$s = "INSERT into UserHistory VALUES (NOW(), '$u', 'login');";
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
		//Create user inventory and default deck		
		$s = "INSERT INTO UserInventory (username) values('$u');";
		$t = mysqli_query($database, $s);
		$s = "INSERT INTO UserDeck (username, deckID, lru) values('$u', 0 , lru 0);";
		$t = mysqli_query($database, $s);
		//Log history
		$s = "INSERT into UserHistory VALUES (NOW(), '$u', 'register');";
		mysqli_query($database, $s) or die(mysqli_error($database));
		return true;
	}
	else
	{
		$request['message'] = "Error registering user";
		logErrors($request);
		return false;
	}
}

function purchase($database, $ID, $price, $u)
{	
	if ($ID == NULL)
		return;
	if ($price == NULL)
		return;
	if ($u == NULL)
		return;
	$u = mysqli_real_escape_string($database,$u);
	
	$stmt = "Select * from Users where username = '$u';";
	$t = mysqli_query($database, $stmt);
	$money = 0;
	
	while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
	{
		$money = $row['totalMoney']; 
	}
	
	if ($money >= intval($price))
	{
		//purchase successful
		$s = "update UserInventory set numCard" . $ID . "= numCard" . $ID . " + 1 where username = '$u';";
		$t = mysqli_query($database, $s);
		$s = "update Users set totalMoney = totalMoney - '$price' where username = '$u';";
		$t = mysqli_query($database, $s);
		echo "gave user '$u' card number '$ID'".PHP_EOL;
		$s= "INSERT INTO UserTransactions VALUES (NOW(), '$u', '$ID', '$price');";
		$t = mysqli_query($database, $s);
		//Add new card to deck

		$s = "Select * from UserDeck where username = '$u' and deckID = '0';";
		$t = mysqli_query($database, $s);
		$lru = 0;
		while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
		{
			$lru = $row['lru']; 
		}
		if($lru >= 29)
		{
			$s = "UPDATE UserDeck SET card" . $lru . " = " . $ID . ", lru = 0 where username = '$u' and deckID = '0';";
		}
		else 
		{
			$s = "UPDATE UserDeck SET card" . $lru . " = " . $ID . ", lru = lru + 1 where username = '$u' and deckID = 0;";	
		}		
		mysqli_query($database, $s);
	}
	else
		echo "get more money!";
}

function requestProcessor($request)
{
	echo "received request of type: ".$request['type'].PHP_EOL;
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
		logErrors($request);
		if ($success == false)
		{
			$request['message'] = "Invalid login credentials";
			logErrors($request);			
			return array("returnCode" => '1', "message" => "Invalid login credentials");
		}
		logErrors($request);		
		break;
	case "register":
		$success = doRegister(DBi::$mydb, $request['email'], $request['username'], $request['password']);
		if ($success == false)
		{
			$request['message'] = "Registration error";
			logErrors($request);						
			return array("returnCode" => '2', "message" => "Registration error");
		}
		logErrors($request);		
		break;
	case "validate_session":
		logErrors($request);
		return doValidate($request['sessionId']);
		break;		
	case "transaction":
		logErrors($request);
		return purchase(DBi::$mydb, $request['ID'], $request['price'], $request['username']);
		break;
	case "error":
		logErrors($request);
		return false;		
		break;
	case "logout":
		doLogout(DBi::$mydb, $request['username']);
		logErrors($request);
		break;
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

