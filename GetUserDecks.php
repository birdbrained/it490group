<?php
//echo "hi\n";
$database = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306');
if ($database->errno != 0)
{
	exit(0);
}

//query the shop table
$u = $_GET['username'];
$id = $_GET['id'];
//echo $u . PHP_EOL;
//echo $id . PHP_EOL;
$query = "select * from UserDeck where username='$u' and deckID='$id';";
$response = $database->query($query);
$result = "";
while ($row = mysqli_fetch_array($response, MYSQLI_ASSOC))
{
	//username, deckID, lru, card0, etc...29
	for ($i = 0; $i < 30; $i++)
	{
		$data = $row['card' . strval($i)];
		$result .= $data . "|";
	}
	$result = substr($result, 0, -1);
}
echo $result;

?>
