<?php
($db = mysqli_connect('127.0.0.1', 'user', 'password', 'Project')) or die ("poop");

$file = fopen("CardFusions.csv", "r") or die ("No file!");
$i = 0;

while (!feof($file))
{
	$line = fgets($file);
	echo $line . PHP_EOL;
	$wordList = explode(",", $line);
	$cleanList = array();

	foreach ($wordList as $word)
	{
		//$cleanWord = str_replace('\n', '', $word);
		array_push($cleanList, $word);
	}
	
	$base = $wordList[0];
	$spice = $wordList[1];
	$val = $wordList[2];
	$meep = $wordList[3];
	$result = substr($meep, 0, -2);
	
	echo "base (" . $base . ") spice (" . $spice . ") val (" . $val . ") result(" . $result . ")" . PHP_EOL;
	
	$stmt = "insert into CardFusions values(
		'$base',
		'$spice',
		'$val',
		'$result'
	)";
	($table = mysqli_query($db, $stmt, MYSQLI_USE_RESULT)) or die (mysqli_error($db));
	mysqli_free_result($table);
	//echo !feof($file);
}
?>
