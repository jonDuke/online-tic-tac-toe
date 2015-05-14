<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$id = $_POST['id'];
	
	$query = "DELETE FROM Challenges WHERE playerid = $id";
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

	echo $result;
?>