#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

//($mydb = mysqli_connect('192.168.1.10', 'user', 'password', 'testDatabase', '3306') ) or die ("failed to connect".PHP_EOL);
global $mydb;
($mydb = mysqli_connect('127.0.0.1', 'user', 'Pasta_Fazool!?', 'testDatabase', '3306') ) or die ("failed to connect".PHP_EOL);
if ($mydb->errno != 0)
{
	echo "you fail: ".$mydb->error.PHP_EOL;
	exit(0);
}

function doLogin($database,$u,$p)
{
	$p = mysqli_real_escape_string ($database,$p);


	$u = mysqli_real_escape_string($database,$u);

	$s = "select * from testTable where user = '$u' and password = '$p'";

	$t = mysqli_query($database, $s) or die(mysqli_error($database));

	if (mysqli_num_rows($t) > 0)

	{ 

		echo "Yaaay" . PHP_EOL;
		return true;

	}

	else
	{
		echo "Error finding num rows" . PHP_EOL;
		return false;
	}
}

function requestProcessor($request)
{
  echo "received request".PHP_EOL;
  var_dump($request);
  if(!isset($request['type']))
  {
    return "ERROR: unsupported message type";
  }
  switch ($request['type'])
  {
    case "login":
      return doLogin($mydb, $request['username'],$request['password']);
    case "validate_session":
      return doValidate($request['sessionId']);
  }
  return array("returnCode" => '0', 'message'=>"Server received request and processed");
}

$server = new rabbitMQServer("testRabbitMQ.ini","testServer");
echo "RabbitMQ Server: BEGIN".PHP_EOL;
$server->process_requests('requestProcessor');
exit();
?>

