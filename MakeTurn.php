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
$request['type'] = $_GET['turntype'];
$request['playerID'] = $GET['playerID'];
//cook only 
if (in_array("base", $_GET))
{
	$request['base'] = $GET['base'];
	$request['spice'] = $GET['spice'];
	$request['valueSum'] = $GET['valueSum'];
}
//attack only
if (in_array("attackVal", $_GET))
{
	$request['attackVal'] = $GET['attackVal'];
	$request['defenseVal'] = $GET['defenseVal'];
	$request['defPlayerHealth'] = $GET['defPlayerHealth'];
	$request['defCardHealth'] = $GET['defCardHealth'];
}
//eat only
if (in_array("cardValue", $_GET))
{
	$request['health'] = $GET['health'];
	$request['cardValue'] = $GET['cardValue'];
}
$request['message'] = "Making turn of type " . $request['type'] . PHP_EOL;

$response = $client->send_request($request);
echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
?>
