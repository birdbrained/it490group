<?php
($db = mysqli_connect('127.0.0.1', 'user', 'password', 'Project')) or die ("poop");

$file = fopen("FoodStats.csv", "r") or die ("No file!");
$i = 0;

while (!feof($file))
{
	$line = fgets($file);
	$wordList = explode("|", $line);
	$cleanList = array();

	foreach ($wordList as $word)
	{
		//$cleanWord = str_replace('"', '', $word);
		array_push($cleanList, $word);
	}
	
	$id = $cleanList[0];
	if ($id <= 43)
		continue;
	echo "id: '$id' ";
	$name = $cleanList[1];
	$type = $cleanList[2];
	$att = $cleanList[3];
	$def = $cleanList[4];
	$val = $cleanList[5];
	$fusable = $cleanList[6];
	if ($fusable == 'TRUE')
		$fusable = 1;
	else
		$fusable = 0;
	$hp = $cleanList[7];
	$desc = $cleanList[8];
	echo "desc: '$desc' \n";
	$imgFile = $cleanList[9];
	
	$stmt = "insert into Cards values(
		'$id',
		'$name',
		'$type',
		'$att',
		'$def',
		'$val',
		'$fusable',
		'$hp',
		'$desc',
		'$imgFile'
	)";
	($table = mysqli_query($db, $stmt, MYSQLI_USE_RESULT)) or die (mysqli_error($db));
	mysqli_free_result($table);
	echo !feof($file);
}
?>
