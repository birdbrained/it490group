#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = 'newbundle';

$bundleType = readline("Input bundle type (server|client|frontend): ");
readline_add_history($bundleType);
$request['bundletype'] = $bundleType;

$vn = readline("Input numerical version number: ");
readline_add_history($vn);
$request['versionnumber'] = $vn;

$filepath = 'backups/backup_' . $bundleType . '_v' . $vn . '.tgz';
$request['filepath'] = $filepath;
echo "filepath: $filepath".PHP_EOL;

echo shell_exec('sh backup.sh ' . $bundleType . ' ' . $vn) . PHP_EOL;

$path = "/var/www/html/it490group/" . $filepath;
$ip = readline("Enter IP of deployment server: ");
readline_add_history($ip);
echo shell_exec('sh SCPscript.sh ' . $path . ' ankit ' . $ip);

$response = $client->send_request($request);

if ($response['returnCode'] == 0)
{
	echo "Successfully sent tarball to the deployment server!\n";
}
else
{
	$msg= "incorrect response return code";
	$request['type'] = 'error';
	$request['message'] = $msg;
	$client->send_request($request);
}

?>
