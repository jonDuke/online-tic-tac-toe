<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerID = $_POST['playerID'];
	
	$query = "SELECT name, wins, losses, draws FROM Players WHERE playerid = " . $playerID;
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));
	
	if (mysqli_num_rows($result) == 0)
		die("ERROR: no data found for playerid " . $playerid);

	$row = mysqli_fetch_assoc($result);

	$data = array('name' => $row['name'], 'wins' => $row['wins'], 'losses' => $row['losses'], 'draws' => $row['draws']);

	echo json_encode($data);
?>