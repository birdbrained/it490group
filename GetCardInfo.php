<?php

$database = mysqli_connect('127.0.01', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}

$query = "select * from Cards;";
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
	
	echo $id . "|" . $name . "|" . $type . "|" . $att . "|" . $def . "|" . $val . "|" . $fuse . "|" . $hp . "|" . $desc . "|" . $img . ";";
}

?>
