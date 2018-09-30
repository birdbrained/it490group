<?php

$database = mysqli_connect('127.0.0.1', 'user', 'Pasta_Fazool!?', 'testDatabase', '3306');

if ($database->errno != 0)
{
	echo "Connection failed " . $database->error . PHP_EOL;
	exit(0);
}
else
{
	echo "Connection successful.";
}

$email = $_POST["email"];
$username = $_POST["username"];
$password = $_POST["password"];

$query = "insert into testTable values('$email', '$username', '$password');";
$response = $database->query($query);

if ($database->errno != 0)
{
	echo "Failed query:" . PHP_EOL;
	exit(0);
}

else 
{
	echo "\nConnected to the database!";
	echo "\nYour entry for $username with email $email was submitted.";
}

?>
