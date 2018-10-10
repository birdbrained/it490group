<?php
session_start();
$_SESSION = array();
session_destroy();
echo "You have been logged out.<br>Please wait...";
header( "refresh:5; url=login.html");
?>
