<?php

$database = mysqli_connect('192.168.1.9', 'user', 'Pasta_Fazool!?', 'testDatabase', '3306');

function auth ($database, $u , $p , &$t){
    $p = mysqli_real_escape_string ($database,$p);
    //$p = sha1($p);
    $u = mysqli_real_escape_string($database,$u);
    $s = "select * from testTable where user = '$u' and password = '$p'";
    $t = mysqli_query($database, $s) or die(mysqli_error($database));
    if (mysqli_num_rows($t) > 0)
    { 
	echo "Yaaay";
	return true;
    }
    else {return false;}
}

if ($database->errno != 0)
{
	echo "Connection failed " . $database->error . PHP_EOL;
	exit(0);
}

$email = $_POST["email"];
$username = $_POST["username"];
$password = $_POST["password"];
//$email = "boi@njit.edu";
//$username = "bigboi";
//$password = "passwdB01";

//$query = "insert into testTable values('$email', '$username', '$password');";

//$response = $database->query($query);
if (auth($database, $username, $password, $response)) 
{
	//redirect to another page
}

else 
{
	echo "User not found.";
	exit(0);
}

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
