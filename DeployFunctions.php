<?php

function deploy()
{

}

function extractClient($content, $filename)
{
//read string, write file
file_put_contents($filename, $content);
//extract tarball
echo shell_exec("sh extractTar.sh it490group/$filename");
//replace files in directory
}

function extractServer($request, $filename)
{
//read string, write file
$filename = "backup_" . $request['bundletype'] . "_v" . $request['versionNumber'] . ".tgz";
file_put_contents($filename, $request['contents']);
//extract tarball
echo shell_exec("sh extractTar.sh it490group/$filename");
//replace files in directory
}

function returnTarBinary($request, $filename)
{
echo "called returnTarBinary";
$file = fopen($filename, "rb");
$contents = fread($file, filesize($filename));
fclose($file);
echo "Contents: $contents" . PHP_EOL;
return $contents;
}

function rollback($request)
{
//query database to find best version
//set request['bundletype'] and $request['versionNumber']
//deploy
}
?>
