<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('DeployFunctions.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = 'update';
$request['bundleType'] = $argv[1];

$response = $client->send_request($request);

if ($response['returnCode'] == 0)
{
	echo "Successfully requested update.\n";
	$contents = $request['contents'];
	$filename = $request['filename'];
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
