<!DOCTYPE HTML>
<html>
	<head>
		<title>FLOUR POWER | Payment Successful</title>
		<center><h2>FLOUR POWER</h2></center>
		<center><h3>Fistful of Dough</h3><center>
		<link rel="stylesheet" href="game.css">
	</head>
	<body>
		<?php	
			session_start();
			require_once('path.inc');
			require_once('get_host_info.inc');
			require_once('rabbitMQLib.inc');

			echo "Payment successful!<br>";

			$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
			$request = array();
			$request['type'] = "addfunds";
			$request['amount'] = $_SESSION['amount'];
			$request['email'] = $_SESSION['email'];
			$request['message'] = "Adding funds...";
			$response = $client->send_request($request);
			echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;*/

			echo "Returning to the game page...";
			header( "refresh:5; url=game.php");
		?>
	</body>
</html>
