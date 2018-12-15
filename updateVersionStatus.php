#!/usr/bin/php
<?php
session_start();
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$bundleType = readline("Enter the bundle type to update (server|client|frontend): ");
readline_add_history($bundleType);

$num = readline("Enter the version number to update: ");
readline_add_history($num);

$status = readline("Enter the new status of bundle (good|bad|testing): ");
readline_add_history($status);

$request = array();
$request['type'] = "updateBundle";
$request['bundleType'] = $bundleType;
$request['versionNum'] = $num;
$request['status'] = $status;

echo "About to send a request to the server..." . PHP_EOL;
$response = $client->send_request($request);

if ($response['returnCode'] == 0)
{
	echo $bundleType . " version " . $num . " successfully updated to type " . $status . PHP_EOL;
}
else 
{
	echo "Error: Something went wrong. Return Code: " . $response['returnCode'] . " Message: " . $response['message'] . PHP_EOL;
}

?>
