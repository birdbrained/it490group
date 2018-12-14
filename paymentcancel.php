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
			echo "Payment cancelled.<br>";
			echo "Returning to the game page...";
			header( "refresh:5; url=game.php");
		?>
	</body>
</html>
