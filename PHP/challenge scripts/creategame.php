<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerID = $_POST['id'];
	$otherID = $_POST['otherid'];

	$turn = rand(1, 2);

	//create the game
	$query = "INSERT INTO Games (player1id, player2id, turn) VALUES ($playerID, $otherID, $turn)";
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));
	$gameID = mysqli_insert_id($DB_link);

	//update player profiles
	update_profile($playerID, $gameID, $DB_link);
	update_profile($otherID, $gameID, $DB_link);

	//remove challenge listing from database
	$query = "DELETE FROM Challenges WHERE playerid = $otherID";
	mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));

	echo json_encode(array('newgame' => $gameID));


	function update_profile($id, $newGameID, $DB_link)
	{
		$query = "SELECT gameids FROM Players WHERE playerid = $id";
		$result = mysqli_query($DB_link, $query) or die('ERROR updating: ' . mysqli_error($DB_link));

		if (mysqli_num_rows($result) == 0)
			die('ERROR: player not found');

		$result = mysqli_fetch_assoc($result);
		$gameids = json_decode($result['gameids']);

		for($i = 0; $i<10; $i++)
		{
			if($gameids[$i] == -1)
			{
				$gameids[$i] = (int)$newGameID;
				break;
			}
		}

		$gameids = json_encode($gameids);
		$query = "UPDATE Players SET gameids = \"$gameids\" WHERE playerid = $id";
		mysqli_query($DB_link, $query) or die('ERROR updating: ' . mysqli_error($DB_link));
	}
?>