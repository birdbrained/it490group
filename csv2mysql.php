<?php 

$query = <<<eof
    LOAD DATA INFILE 'FoodStats.csv'
    INTO TABLE Cards
    FIELDS TERMINATED BY ',' OPTIONALLY ENCLOSED BY '"'
    LINES TERMINATED BY '\n'
    (ID, Name, Type, Attack, Defense, Value, isFusable, HP, Description, ImageFilepath)
eof;

($db = mysqli_connect('127.0.0.1', 'user', 'password', 'Project', '3306') ) or die ("failed to connect".PHP_EOL);
$db->query($query);

?>
