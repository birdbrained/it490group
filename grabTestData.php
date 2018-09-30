<?php

$database = mysqli_connect('127.0.0.1', 'user', 'Pasta_Fazool!?', 'testDatabase', '3306');

if ($database->errno != 0)
{
	exit(0);
}

$query = "select * from testTable;";
$response = $database->query($query);
while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	$username = $row["user"];
	$email = $row["email"];
	$password = $row["password"];

	echo "username:" . $username . "|email:" . $email . "|password:" . $password . ";";
}

?>
