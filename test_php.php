#!/var/www/html
<?php
echo "hello".PHP_EOL;

($mydb = new mysqli('192.168.1.10', 'testDatabase', 'gohawks2012', 'testing', '3306') ) or die ("failed to connect");
//echo "hello".PHP_EOL;

if ($mydb->errno != 0)
{
	echo "you fail: ".$mydb->error.PHP_EOL;
	exit(0);
}

//echo "yay ya did itttt".PHP_EOL;

$email = "mwk9@njit.edu";
$user = "Matt";
$pass = "secretPass";
$id = 7;

$query = "insert into testUsers values('$email', '$user', '$pass');";
$response = $mydb->query($query);
if ($mydb->errno != 0)
{
	echo "Failed to execute query:".PHP_EOL;
	exit(0);
}

?>

