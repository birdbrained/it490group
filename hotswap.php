<?php
class DBi
{
	public static $db;
	public static $successfulConnect = true;
}


function hotswapServer($ip)
{
	echo "hotswap server called\n";	
	exec("scp /var/www/html/it490group/sqlbackup.sql ankit@" . $ip . ":/var/www/html/it490group/sqlbackup.sql");
	echo "scp done\n";
	exec("ssh -t ankit@" . $ip . " 'mysql -u slave -p ProjectBackup < /var/www/html/it490group/sqlbackup.sql'");
	echo "dump restored\nstarting server...\n";
	exec("sshpass -p 'gohawks2012' ssh -t ankit@" . $ip . " '/var/www/html/it490group/RabbitMQServer.php'");
	
	echo "server closed\n";
}

function sqlDump()
{
	exec("sshpass -p 'gohawks2012' ssh -t ankit@" . "10.0.0.33" . " 'sudo mysqldump -u root -p Project > /var/www/html/it490group/sqlbackup.sql'");
	exec("ssh -t ankit@" . "10.0.0.33" . " 'scp /var/www/html/it490group/sqlbackup.sql sq42@10.0.0.34:/var/www/html/it490group/sqlbackup.sql'");
	echo "scp done\n";
	echo "took a dump :)\n";
}

function errorLoop($db, $ip)
{ 
	(DBi::$db = mysqli_connect("10.0.0.33", 'user', 'password', 'Project', '3306')) or DBi::$successfulConnect = false;	
	//$t = $db->query("select * from VersionControl;") or hotswapServer($ip);
	/*	
	while ($row = mysqli_fetch_array($t, MYSQLI_ASSOC))
	{
		echo "i got a " . $row['status'] . PHP_EOL;
	}
	*/
	if (DBi::$db)
	{

		if (DBi::$successfulConnect== true)
		{
			sqlDump();
			echo"zzz... I sleep...\n";
			sleep(15);
			echo"I woke!\n";
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
(DBi::$db = mysqli_connect("10.0.0.33", 'user', 'password', 'Project', '3306')) or DBi::$successfulConnect = false;
echo "connection established\n";
errorLoop(DBi::$db, $ip);

?>
