<?php

$database = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}

//query the shop table
$query = "select * from Shop";
$response = $database->($query);
$IDCost = array();
$ValidID = "";

while ($row = mysqli_fetch_array(response, MYSQLI_ASSOC);
{
	$IDCost[$row['ID']] = $row['price']; 
}

//append the where clause 
foreach ($IDCost as $key => $value)
{
	$ValidID .= " ID = '$key' or"
}
//take out the last ' or'
$ValidID = substr($ValidID, 0, -3);

$query = "select * from Cards where". $ValidID . ";";
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
	
	echo $id . "|" . $name . "|" . $type . "|" . $att . "|" . $def . "|" . $val . "|" . $fuse . "|" . $hp . "|" . $desc . "|" . $img . "|". $IDcost[$id] .";";
}

?>
