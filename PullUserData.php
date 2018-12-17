<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
//include 'account.php';
//$_REQUEST['type'] = username, password, email, totalMoney

//$db = mysqli_connect('127.0.0.1', $username, $password, $project, '3306')  or die ("failed to connect".PHP_EOL);

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = "pullUserData";
$request['username'] = $_GET['username'];
$request['infoType'] = $_GET['type'];

/*$t = mysqli_query($db, "select * from Users where username='$user';");
while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
{
	echo $row[$type];
}*/

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
