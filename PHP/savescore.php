<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$newName = $_POST['name'];
	$newScore = $_POST['score'];

	$query = "INSERT INTO Test_Scores (Name, Score) VALUES ('" . $newName . "', '" . $newScore . "')";
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysql_error());

	echo $result;
?>