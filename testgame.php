<!DOCTYPE HTML>
<html>
	<head>
		<title>IT490 Cooking Card Game</title>
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
echo '<br> <iframe src="Testbuild/index.html" height="100%" width="100%" ></iframe><br><br>';
//}
/*else {
echo "error, plese log in";
}*/
echo "<a href='logout.php'> <button>Logout </button></a>";
?>
	</body>
</html>
