<?php
session_start();
//create new client object
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
$request =array();
$request['type'] = 'logout';
$request['username'] = $_SESSION['username'];
$request['password'] = $_SESSION['password'];
$request['email'] = $_SESSION['email'];
$response = $client->send_request($request);

//destroy cookie and redirect
$_SESSION = array();
session_destroy();
setcookie("PHPSESSID","",time()-3600,'/',"",0,0);
echo "You have been logged out.<br>Please wait...";
header( "refresh:5; url=login.html");
?>
