<?php
function hotswapServer($ip)
{
	
	exec("ssh -t ankit@" . $ip . "'/var/www/html/it490group/RabbitMQServer.php'");
}


function errorLoop($db, $ip)
{ 
	if ($db ->errno !=0)
	{
	hotswapServer();
	}
	else 
	{
		sleep(15);
		errorLoop();
	}
}

$ip = "10.0.0.33";
class DBi
{
	public static $dp;
}
DBi::$db = mysqli_connect($ip, 'user', 'password', 'Project', '3306');
errorLoop(DBi::$dp, $ip);

?>
