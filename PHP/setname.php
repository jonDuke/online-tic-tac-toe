<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerid = $_POST['id'];
	$newName = $_POST['name'];

	$query = 'UPDATE Players SET name=\'' . $newName . '\' WHERE playerid = ' . $playerid;
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

	echo true;
?>