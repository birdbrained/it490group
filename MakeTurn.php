<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function processresponse($response)
{
	//echo "processing request or whatever \n";
	var_dump($request);
	//echo $response['message'] . PHP_EOL; 
}
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = $_GET['turntype'];
$request['playerID'] = $_GET['playerID'];
//cook only 
if ($request['type'] == 'cook')
{
	$request['base'] = $_GET['base'];
	$request['spice'] = $_GET['spice'];
	$request['valueSum'] = $_GET['valueSum'];
}
//attack only
if ($request['type'] == 'attack')
{
	$request['attackVal'] = $_GET['attackVal'];
	$request['defenseVal'] = $_GET['defenseVal'];
	$request['defPlayerHealth'] = $_GET['defPlayerHealth'];
	$request['defCardHealth'] = $_GET['defCardHealth'];
}
//eat only
if ($request['type'] == 'eat')
{
	$request['health'] = $_GET['health'];
	$request['cardValue'] = $_GET['cardValue'];
}
$request['message'] = "Making turn of type " . $request['type'] . PHP_EOL;
//echo "about to send request" . PHP_EOL;
$response = $client->send_request($request);
echo $response['message'];
//echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
?>
