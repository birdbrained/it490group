<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = "transaction";
$request['username'] = $_GET['username']; //get from url string from unity cs
$request['price'] = $_GET['price'];
$request['ID'] = $_GET['id'];

$response = $client->send_request($request);
echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
?>
