<?php
	//this script checks if a profile exists for a given player id, and creates a new one if needed

	require_once 'app_config.php';
	$DB_link = connect();

	$playerID = $_POST['playerID'];

	$query = "SELECT name FROM Players WHERE playerid = " . mysqli_escape_string($DB_link, $playerID);
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));
	
	if (mysqli_num_rows($result) == 0) //create new player
	{
		$query = "SELECT playerid FROM Players ORDER BY playerid DESC";
		$result = mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));
		$row = mysqli_fetch_assoc($result);

		$newid = $row['playerid'] + 1;

		$query = "INSERT INTO Players (playerid) VALUES (" . $newid . ")";
		$result = mysqli_query($DB_link, $query) or die('ERROR3: ' . mysqli_error($DB_link));

		echo json_encode(array('id' => $newid));
	}
	else
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['name'];	//return the name for confirmation
	}
?>