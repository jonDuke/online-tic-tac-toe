<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$id = $_POST['id'];

	$query = "SELECT playerid FROM Challenges WHERE playerid = $id";
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		echo 'false';
	else
		echo 'true';
?>