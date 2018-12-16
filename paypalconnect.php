<?php
session_start();
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$enableSandbox = true;

$dbConfig = [
    'host' => 'localhost',
    'username' => 'user',
    'password' => 'password',
    'name' => 'Project'
];

$paypalConfig = [
    'email' => 'am2272@njit.edu',
    'return_url' => 'http://10.0.0.34/it490group/paymentsuccess.php',
    'cancel_url' => 'http://10.0.0.34/it490group/paymentcancel.php',
    'notify_url' => 'http://10.0.0.34/it490group/index.html'
];

$paypalUrl = $enableSandbox ? 'https://www.sandbox.paypal.com/cgi-bin/webscr' : 'https://www.paypal.com/cgi-bin/webscr';

$itemName = 'Coin';
$itemAmount = 5.00;

require 'functions.php';

if (!isset($_POST["txn_id"]) && !isset($_POST["txn_type"])) {
    $data = [];
    foreach ($_POST as $key => $value) {
        $data[$key] = stripslashes($value);
    }
    file_put_contents('test.txt', print_r($data, true));
    
    $data['business'] = $paypalConfig['email'];

    $data['return'] = stripslashes($paypalConfig['return_url']);
    $data['cancel_return'] = stripslashes($paypalConfig['cancel_url']);
    $data['notify_url'] = stripslashes($paypalConfig['notify_url']);
    
    $data['item_name'] = $itemName;
    $data['amount'] = $itemAmount;
    //$data['currency_code'] = 'coin';

    // Add any custom fields for the query string.
    //$data['custom'] = USERID;

    // Build the query string from the data.
    $queryString = http_build_query($data);

    // Redirect to paypal IPN
    header('location:' . $paypalUrl . '?' . $queryString);
    exit();
}

else {
    $db = new mysqli($dbConfig['host'], $dbConfig['username'], $dbConfig['password'], $dbConfig['name']);
    
    $data = [
    'item_name' => $_POST['item_name'],
    'item_number' => $_POST['item_number'],
    'payment_status' => $_POST['payment_status'],
    'payment_amount' => $_POST['mc_gross'],
    'payment_currency' => $_POST['mc_currency'],
    'txn_id' => $_POST['txn_id'],
    'receiver_email' => $_POST['receiver_email'],
    'payer_email' => $_POST['payer_email'],
    'custom' => $_POST['custom'],
    ];

	file_put_contents('test.txt', print_r($data, true));

	$_SESSION['amount'] = $_POST['mc_gross'];
	$_SESSION['email'] = $_POST['payer_email'];

	if (verifyTransaction($_POST) && checkTxnid($data['txn_id'])) {
	    if (addPayment($data) !== false) {
		// Payment successfully added.
		/*		
		$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
		$request = array();
		$request['type'] = "addfunds";
		$request['amount'] = 5;
		$request['email'] = $_POST['payer_email'];
		$request['message'] = "Adding funds...";
		$response = $client->send_request($request);
		echo "Client received response: returnCode: ". $response['returnCode'] . " message: " . $response['message'] . PHP_EOL;
		*/
	    }
	}
}
?>
