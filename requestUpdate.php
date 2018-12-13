<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('DeployFunctions.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = 'update';

$bundleType = readline("Enter the type of bundle you are requesting (server|client|frontend): ");
readline_add_history($bundleType);
$request['bundleType'] = $bundleType;

$user = readline("Enter your username: ");
readline_add_history($user);
$request['user'] = $user;

$ip = readline("Enter your IP: ");
readline_add_history($ip);
$request['ip'] = $ip;

echo "Calling rabbitmq...\n";
$response = $client->send_request($request);
var_dump($response);
echo "Got a response\n";

if ($response['returnCode'] == 0)
{
	echo "Successfully requested update.\n";
	//$contents = $response['contents'];
	$filename = $response['filepath'];
	//echo "contents: ($contents)\nfilename: ($filename)\n";
	extractClient("", $filename);
}
else
{
	$msg= "incorrect response return code";
	$request['type'] = 'error';
	$request['message'] = $msg;
	$client->send_request($request);
}

?>
