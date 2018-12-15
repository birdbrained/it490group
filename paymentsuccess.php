<?php
session_start();
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

echo '<html>';
	echo '<head>';
		echo '<title>FLOUR POWER | Payment Successful</title>';
		echo '<center><h2>FLOUR POWER</h2></center>';
		echo '<center><h3>Fistful of Dough</h3><center>';
		echo '<link rel="stylesheet" href="game.css">';
	echo '</head><body>';
		//<?php	
			

			echo "Payment successful!<br>";

			$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
			$request = array();
			$request['type'] = "addfunds";
			$request['amount'] = $_SESSION['amount'];
			$request['email'] = $_SESSION['email'];
			$request['message'] = "Adding funds...";
			$response = $client->send_request($request);
			echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;

			echo "Returning to the game page...";
			header( "refresh:5; url=game.php");
		
	echo '</body>';
echo '</html>';
?>
