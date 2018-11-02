<?php
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

$request = array();
$request['type'] = "transaction";
$request['username'] = $_GET['username']; //get from url string from unity cs
$request['price'] = $_GET['price'];
$request['ID'] = $_GET['id'];
$request['message'] = "Making transaction of card " . $request['ID'] . " with price " . $request['price'] . PHP_EOL;

$response = $client->send_request($request);
echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
?>
