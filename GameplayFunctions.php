<?php

function ProcessCook($database)
{
	$base = $request['base'];
	$spice = $request['spice'];
	$valueSum = $request['valueSum'];
	$result = "no result";
	$s = "select * from CardFusions where base = '$base' and spice = '$spice'";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));
	if (mysqli_num_rows($t) > 0)
	{
		while ($row = mysqli_fetch_array{$table, MYSQLI_ASSOC))
		{
			$product = $row['product']
			break;
		}
		PrintCardStats($database, $product);
		return;
	}
	$s = "select * from CardFusions where base = 'any' and spice = '$spice' and valueReq <= '$valueSum' order by valueReq DESC";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));
	if (mysqli_num_rows($t) > 0)
	{
		while ($row = mysqli_fetch_array{$table, MYSQLI_ASSOC))
		{
			$product = $row['product']
			break;
		}
		PrintCardStats($database, $product);
		return;
	}
	$s = "select * from Cards where Type = 'Monster' and isFusable = '0' and Value <= '$valueSum' order by Value DESC";
	$t = mysqli_query($database, $s) or die(mysqli_error($database));
	while ($row = mysqli_fetch_array{$table, MYSQLI_ASSOC))
		{
			$product = $row['product']
			break;
		}
		PrintCardStats($database, $product);
		return;
	

}

function PrintCardStats($database, $cardname)
{
	$stmt = "select * from Cards where name='$cardname';";
	$reponse = $database->query($stmt);
	while ($row = mysqli_fetch_array($reponse, MYSQLI_ASSOC))
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
?>
