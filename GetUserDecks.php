<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
//echo "hi\n";
/*$database = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}*/
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

//query the shop table
$request = array();
$request['type'] = "getUserDecks";
$request['username'] = $_GET['username'];
$request['id'] = $_GET['id'];
//echo $u . PHP_EOL;
//echo $id . PHP_EOL;

/*$query = "select * from UserDeck where username='$u' and deckID='$id';";
$response = $database->query($query);
$result = "";
while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	//username, deckID, lru, card0, etc...29
	for ($i = 0; $i < 30; $i++)
	{
		$data = $row['card' . strval($i)];
		$result .= $data . "|";
	}
	$result = substr($result, 0, -1);
}
echo $result;*/

$response = $client->send_request($request);

if ($response['returnCode'] == 0)
{
	echo $response['message'];
}
else 
{
	echo "Error: Something went wrong. Return Code: " . $response['returnCode'] . " Message: " . $response['message'] . PHP_EOL;
}

?>
