<?php /*error_reporting(E_ALL ^ E_NOTICE);*/?>

<?php
    //Online server connect information
    $username = "872852";
	$password = "root1234";
	$hostname = "localhost";
	$db = "872852";


    //Offline server connect information
	//$username = "root";
	//$password = "root";
	//$hostname = "localhost";
	//$db = "uccfield";

	$con = mysqli_connect($hostname, $username, $password, $db);
	
	try {
		$dbo = new PDO('mysql:host='.$hostname.';dbname='.$db, $username, $password);
		
	} catch (PDOException $e) {
		print "Error!: " . $e->getMessage() . "<br/>";
		die();
	}
?>
    