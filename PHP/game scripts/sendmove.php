<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$playerID = $_POST['id'];
	$gameID = $_POST['game'];
	$move = $_POST['space'];
	$type = (int)$_POST['movetype']; //1 is x, 2 is o

	$query = "SELECT player1id, player2id, turn, boardstate FROM Games WHERE gameid = $gameID";
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: game not found');

	$gameinfo = mysqli_fetch_assoc($result);
	$board = json_decode($gameinfo['boardstate']);

	//confirm move
	$validMove = false;
	if($type == 1 && $playerID == $gameinfo['player1id'] && $board[$move] == 0)
		$validMove = true;
	else if($type == 2 && $playerID == $gameinfo['player2id'] && $board[$move] == 0)
		$validMove = true;

	if(!$validMove)
		die('ERROR: not a valid move');

	$board[$move] = $type;
	$board = json_encode($board);
	//$board = mysqli_escape_string($DB_link, $board);

	$turn = ($type == 1) ? 2 : 1;

	$query = "UPDATE Games SET turn = $turn, boardstate = \"$board\" WHERE gameid = $gameID";
	$result = mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));

	echo $move;
?>