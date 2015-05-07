<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$offset = $_POST['offset']; //number to start at
	
	$query = "SELECT playerid FROM Challenges ORDER BY datecreated ASC LIMIT $offset, 10";
	$result = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

	$numrows = mysqli_num_rows($result);
	if ($numrows == 0)
		die('ERROR: no data found');

	$data = array($numrows);

	for($i=1; $i<=$numrows; $i++)
	{
		$row = mysqli_fetch_assoc($result);
		$query = "SELECT name, wins, losses, draws FROM Players WHERE Playerid = " . $row['playerid'];
		$playerdata = mysqli_query($DB_link, $query) or die('ERROR: ' . mysqli_error($DB_link));

		$playerdata = mysqli_fetch_assoc($playerdata);
		$data[$i] = array('id' => $row['playerid'], 'name' => $playerdata['name'], 'wins' => $playerdata['wins'], 'losses' => $playerdata['losses'], 'draws' => $playerdata['draws']);
	}

	echo json_encode($data);
?>