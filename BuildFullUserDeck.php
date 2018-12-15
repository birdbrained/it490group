<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
//include 'account.php';
/*$database = mysqli_connect('127.0.0.1', $username, $password, $project, '3306');
if ($database->errno != 0)
{
	exit(0);
}*/
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = "buildFullUserDeck";
$request['username'] = $_GET['username'];
$request['id'] = $_GET['id'];

/*$query = "select * from UserDeck where username='$u' and deckID='$id';";
$response = $database->query($query);
$result = "";

while ($Row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	for ($i = 0; $i < 30; $i++)
	{
		$cardname = $Row['card' . strval($i)];
		$stmt = "select * from Cards where name='$cardname';";
		$reponse2 = $database->query($stmt);
		while ($row = mysqli_fetch_array($reponse2, MYSQLI_ASSOC))
		{
			$id = $row['ID'];
			$name = $row['Name'];
			$type = $row['Type'];
			$att = $row['Attack'];
			$def = $row['Defense'];
			$val = $row['Value'];
			$fuse = $row['isFusable'];
			$hp = $row['HP'];
			$desc = $row['Description'];
			$img = $row['ImageFilepath'];
	
			$result .= $id . "|" . $name . "|" . $type . "|" . $att . "|" . $def . "|" . $val . "|" . $fuse . "|" . $hp . "|" . $desc . "|" . $img . ";";
		}
	}
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
