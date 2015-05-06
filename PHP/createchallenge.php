<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerid = $_POST['id'];
	
	$query = "INSERT INTO Challenges (playerid) VALUES ($playerid)";
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

	echo 1;
?>