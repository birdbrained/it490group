<?php
class DBi
{
	public static $db;
}
function hotswapServer($ip)
{
	echo "hotswap server called\n";	
	exec("ssh -t ankit@" . $ip . " '/var/www/html/it490group/RabbitMQServer.php'");
	echo "server started\n";
}


function errorLoop($db, $ip)
{ 
	$t = $db->query("select * from VersionControl;");
	/*	
	while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
	{
		echo "i got a " . $row['status'] . PHP_EOL;
	}
	*/
	if (isset($db))
	{

		if ($db ->errno !=0)
		{
		hotswapServer($ip);
		}
		else 
		{
			echo"zzz... I sleep...\n";
			sleep(4);
			errorLoop($db, $ip);
		}
	}
	else
	hotswapServer($ip);
}

$ip = "10.0.0.33";
echo "about to connect to mysql\n";
DBi::$db = mysqli_connect($ip, 'user', 'password', 'Project', '3306');
echo "connection established\n";
errorLoop(DBi::$db, $ip);

?>
