<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

/*$database = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}*/
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = "getCardInfo";

/*
//query the shop table
$query = "select * from Shop";
$response = $database->query($query);
$IDCost = array();
$ValidID = "";

while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	$IDCost[$row['ID']] = $row['price']; 
}
//print_r($IDCost);

//append the where clause 
foreach ($IDCost as $key => $value)
{
	$ValidID .= " ID = '$key' or";
}
//echo $ValidID;
//take out the last ' or'
$ValidID = substr($ValidID, 0, -3);

$query = "select * from Cards where". $ValidID . ";";
//echo $query;
$response = $database->query($query);
while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
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
	
	echo $id . "|" . $name . "|" . $type . "|" . $att . "|" . $def . "|" . $val . "|" . $fuse . "|" . $hp . "|" . $desc . "|" . $img . "|". $IDCost[$id] .";";
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
