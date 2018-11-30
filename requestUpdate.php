<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('DeployFunctions.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = 'update';
$request['bundleType'] = $argv[1];
$request['user'] = $argv[2];
$request['ip'] = $argv[3];

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
	extractClient($contents, $filename);
}
else
{
	$msg= "incorrect response return code";
	$request['type'] = 'error';
	$request['message'] = $msg;
	$client->send_request($request);
}

?>
