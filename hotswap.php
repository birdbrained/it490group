<?php
class DBi
{
	public static $db;
}
$successfulConnect = true;

function hotswapServer($ip)
{
	echo "hotswap server called\n";	
	exec("scp /var/www/html/it490group/sqlbackup.sql ankit@" . $ip . ":/var/www/html/it490group/sqlbackup.sql");
	echo "scp done\n";
	exec("ssh -t ankit@" . $ip . " 'mysql -u slave -p ProjectBackup < /var/www/html/it490group/sqlbackup.sql'");
	echo "dump restored\nstarting server...\n";
	exec("ssh -t ankit@" . $ip . " '/var/www/html/it490group/RabbitMQServer.php'");
	
	echo "server closed\n";
}

function sqlDump()
{
	exec("ssh -t ankit@" . "10.0.0.33" . " 'sudo mysqldump -u root -p Project > /var/www/html/it490group/sqlbackup.sql'");
	echo "took a dump :)\n";
}

function errorLoop($db, $ip)
{ 
	//$t = $db->query("select * from VersionControl;");
	/*	
	while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
	{
		echo "i got a " . $row['status'] . PHP_EOL;
	}
	*/
	if (isset($db))
	{

		if ($successfulConnect== true)
		{
			echo"zzz... I sleep...\n";
			sqlDump();			
			sleep(15);
			errorLoop($db, $ip);
		
		}
		else 
		{
			
			hotswapServer($ip);
		}
	}
	else
	hotswapServer($ip);
}

$ip = "10.0.0.35";
echo "about to connect to mysql\n";
(DBi::$db = mysqli_connect("10.0.0.33", 'user', 'password', 'Project', '3306')) or $successfulConnect = false;
echo "connection established\n";
errorLoop(DBi::$db, $ip);

?>
