<?php

$database = mysqli_connect('192.168.1.16', 'user', 'password', 'testDatabase', '3306');

if ($database->errno != 0)
{
	exit(0);
}

$query = "select * from testTable;";
$response = $database->query($query);
while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	$username = $row["username"];
	$email = $row["email"];
	$password = $row["password"];

	echo "username:" . $username . "|email:" . $email . "|password:" . $password . ";";
}

?>
