<!DOCTYPE HTML>
<html>
	<head>
		<title>FLOUR POWER: Fistful of Dough</title>
		<center><h2>FLOUR POWER</h2></center>
		<center><h3>Fistful of Dough</h3><center>
		<link rel="stylesheet" href="game.css">
	</head>
	<body>
<?php
session_start();
/*if (isset($_SESSION['type'])) {
echo "Session Set<br><br>";
}*/
//if (isset($_SESSION['returnCode']) && $_SESSION['returnCode'] == 0){
echo "<a href='addfunds.html'> <button class='funds' style='position:relative; left:450px'>Add Funds</button></a>";
echo '<br> <iframe src="Testbuild/index.html" height="100%" width="100%" ></iframe><br><br>';
echo "<a href='logout.php'> <button class='log'>Logout </button></a>";
//}
/*else {
echo "error, plese log in";
}*/
?>
	</body>
</html>
