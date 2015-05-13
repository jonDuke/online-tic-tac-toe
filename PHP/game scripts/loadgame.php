<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$gameID = $_POST['gameid'];
	$playerID = $_POST['id'];

	$query = "SELECT player1id, player2id, turn, boardstate FROM Games WHERE gameid = $gameID";
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: game not found');

	$gameinfo = mysqli_fetch_assoc($result);

	//determine turn
	$turn = false;
	if($gameinfo['player1id'] == $playerID && $gameinfo['turn'] == 1)
		$turn = true;
	else if($gameinfo['player2id'] == $playerID && $gameinfo['turn'] == 2)
		$turn = true;

	//get other player info
	$otherPlayerID = ($gameinfo['player1id'] == $playerID) ? $gameinfo['player2id'] : $gameinfo['player1id'];
	$query = "SELECT name FROM Players WHERE playerid = $otherPlayerID";
	$result = mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: player not found');

	$playerinfo = mysqli_fetch_assoc($result);

	//$data sent: [turn bool, opponent name, opponent id, boardstate]
	$data = array('turn' => $turn, 'othername' => $playerinfo['name'], 'otherid' => $otherPlayerID, 'boardstate' => $gameinfo['boardstate']);
	echo json_encode($data);
?>