#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('DeployFunctions.php');
require_once('GameplayFunctions.php');
include 'account.php';

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
(DBi::$mydb = mysqli_connect('127.0.0.1', $username, $password, $project, '3306') ) or die ("failed to connect".PHP_EOL);

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

function addFunds($database, $email, $amount)
{
	echo "email ($email) amount ($amount)\n";
	$money = (int)$amount * 100;
	$moneyy = $money * -1;

	$stmt = "update Users set totalMoney=totalMoney+'$money' where email='$email';";
	$t = mysqli_query($database, $stmt);
	
	$stmt = "insert into UserTransactions values(NOW(), '$email', '0', '$moneyy');";
	$t = mysqli_query($database, $stmt);
}

function retreiveFilepath($database, $type)
{
	$filepath = "";

	$query = "SELECT * FROM VersionControl WHERE type = '$type' AND status = 'good' ORDER BY version DESC";

	$table = mysqli_query($database, $query);

	while ($row = mysqli_fetch_array($table, MYSQLI_ASSOC))
	{
		// make this version the one to send to client
		$filepath = $row['path'];
		break;
	}

	return $filepath;
}

function newBackup($db, $vn, $type, $filepath, $status)
{
	$stmt = "insert into VersionControl values('$vn', '$type', '$filepath', '$status');";
	$t = mysqli_query($db, $stmt);
}

function updateBundleInformation($db, $bundleType, $num, $status)
{
	$stmt = "select * from VersionControl where type='$bundleType' and version='$num';";
	$t = mysqli_query($db, $stmt);
	if (mysqli_num_rows($t) == 0)
	{
		$arr = array();
		$arr['returnCode'] = 2;
		$arr['message'] = $bundleType . " version " . $num . " not in the database; could not update.";
		return $arr;
	}
	else
	{
		$stmt = "update VersionControl set status='$status' where type='$bundleType' and version='$num';";
		$t = mysqli_query($db, $stmt);
		$arr = array();
		$arr['returnCode'] = 0;
		$arr['message'] = "Successfully updated entry.";
		return $arr;
	}
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
		$success = true;
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
		$success = true;
		break;
	case "cook":
		ProcessCook(DBi::$mydb, $request);	
		$success = true;
		break;
	case "update":
		$bundleType = $request['bundleType'];
		$path = retreiveFilepath(DBi::$mydb, $bundleType);
		$path = "/var/www/html/it490group/" . $path;
		scpCopy($path, $request['user'], $request['ip']);
		//$binary = returnTarBinary($request, $path);
		$returnArray = array();
		$returnArray['returnCode'] = '0';
		$returnArray['message'] = "Server received request and processed";
		$returnArray['filepath'] = $path;
		echo "Client successfully updated!\n";
		return $returnArray;
		break;
	case "updateBundle":
		$bundleType = $request['bundleType'];
		$num = $request['versionNum'];
		$status = $request['status'];
		return updateBundleInformation(DBi::$mydb, $bundleType, $num, $status);
		break;
	case "newBundle":
		$bundleType = $request['bundleType'];
		$vn = $request['versionnumber'];
		$filepath = $request['filepath'];
		$status = $request['status'];
		newBackup(DBi::$mydb, $vn, $bundleType, $filepath, $status);
		$success = true;
		break;
	case "addfunds":
		$amount = $request['amount'];
		addFunds(DBi::$mydb, $request['email'], $amount);
		$success = true;
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
//echo "RabbitMQ Server: BEGIN".PHP_EOL;
$server->process_requests('requestProcessor');
exit();
?>

