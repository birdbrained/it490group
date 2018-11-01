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
$request['username'] = $_REQUEST['username']; //get from url string from unity cs
$request['price'] = $_REQUEST['price'];
$request['ID'] = $_REQUEST['id'];
//$request['username'] = "b"; //get from url string from unity cs
//$request['price'] = 50;
//$request['ID'] = 1;

$response = $client->send_request($request);
echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
?>
