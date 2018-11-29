#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = 'bundle';
$request['bundletype'] = $argv[1];
$request['versionnumber'] = $argv[2];
$filepath = 'backups/backup_' . $argv[1] . '_v' . $argv[2] . '.tgz';
$request['filepath'] = $filepath;
echo shell_exec('sh backup.sh ' .$argv[1] . ' ' .$argv[2]) . PHP_EOL;
$file = fopen($filepath, "rb");
$contents = fread($file, filesize($filepath));
fclose($file);
echo $contents . PHP_EOL;
$request['contents'] = $contents;

//$writeFile = fopen("backups/poop.tgz", "wb");
//fwrite($writeFile, $contents);
//fclose($writeFile);

$response = $client->send_request($request);

if ($response['returnCode'] == 0)
{
	echo "Successfully sent tarball to the server!\n";
}
else
{
	$msg= "incorrect response return code";
	$request['type'] = 'error';
	$request['message'] = $msg;
	$client->send_request($request);
}

?>
