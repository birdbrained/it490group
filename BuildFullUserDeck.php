<?php
//echo "hi\n";
$database = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}
$u = $_GET['username'];
$id = $_GET['id'];

$query = "select * from UserDeck where username='$u' and deckID='$id';";
$response = $database->query($query);
$result = "";

while ($Row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	for ($i = 0; $i < 30; $i++)
	{
		$cardname = $Row['card' . strval($i)];
		$stmt = "select * from Cards where name='$cardname';";
		$reponse2 = $database->query($stmt);
		while ($row = mysqli_fetch_array($reponse2, MYSQLI_ASSOC))
		{
			$id = $row['ID'];
			$name = $row['Name'];
			$type = $row['Type'];
			$att = $row['Attack'];
			$def = $row['Defense'];
			$val = $row['Value'];
			$fuse = $row['isFusable'];
			$hp = $row['HP'];
			$desc = $row['Description'];
			$img = $row['ImageFilepath'];
	
			$result .= $id . "|" . $name . "|" . $type . "|" . $att . "|" . $def . "|" . $val . "|" . $fuse . "|" . $hp . "|" . $desc . "|" . $img . ";";
		}
	}
}

echo $result;

?>
