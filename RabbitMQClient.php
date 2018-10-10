#!/usr/bin/php
<?php
session_start();
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function processresponse($response)
{
	echo "processing request or whatever \n";
	var_dump($request);
	echo $response['message'] . PHP_EOL; 
}
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
if (isset($argv[1]))
{
  $msg = $argv[1];
}
else
{
  $msg = "test Satchel";
}

$request = array();
//$request['type'] = "login";
//$request['username'] = 'b';
//$request['password'] = '444';
//$request['email'] = 'satchel@gmail';

$request['type'] = $_POST['sessionType'];
$request['username'] = $_POST['username'];
$request['password'] = $_POST['password'];
$request['email'] = $_POST['email'];
$_SESSION['type'] = $_POST['sessionType'];
$_SESSION['username'] = $_POST['username'];
$_SESSION['password'] = $_POST['password'];
$_SESSION['email'] = $_POST['email'];
//echo "a\n";
$response = "no";
//$client->process_response($response);
$response = $client->send_request($request);
//echo "b\n";
//$response = $client->publish($request);
$_SESSION['returnCode'] = $response['returnCode'];
echo "client received response: returnCode: " . $response['returnCode'] . " message: " . $response['message'] .PHP_EOL;
echo "\n\n";
if ($response['returnCode'] == 0){
	//print_r($response);
	echo $argv[0]." END".PHP_EOL;
	header( "referesh:5; url=game.php");
}

