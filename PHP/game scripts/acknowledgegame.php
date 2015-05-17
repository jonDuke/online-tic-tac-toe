<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerID = $_POST['id'];
	$gameID = $_POST['game'];

	$query = "SELECT acknowledged FROM Games WHERE gameid = $gameID";
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: game not found');

	$result = mysqli_fetch_assoc($result);

	//delete the game once both players have acknowledged the game
	if($result['acknowledged'] == 0)
		$query = "UPDATE Games SET acknowledged = 1 WHERE gameid = $gameID";
	else
		$query = "DELETE FROM Games WHERE gameid = $gameID";

	mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));


	//remove this gameid from the player's list
	$query = "SELECT gameids FROM Players WHERE playerid = $playerID";
	$result = mysqli_query($DB_link, $query) or die('ERROR3: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: player not found');

	$result = mysqli_fetch_assoc($result);
	$gamelist = json_decode($result['gameids']);

	for($i=0; $i<9; $i++)
		if($gamelist[$i] == $gameID)
			$gamelist[$i] = -1;

	$gamelist = json_encode($gamelist);
	$query = "UPDATE Players SET gameids = \"$gamelist\" WHERE playerid = $playerID";
	mysqli_query($DB_link, $query) or die('ERROR4: ' . mysqli_error($DB_link));
?>