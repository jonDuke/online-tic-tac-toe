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

	//check if game is over (yes, there's probably a better way of checking this)
	$gameover = false;
	if($board[0] != 0 && $board[0] == $board[1] && $board[1] == $board[2])
		$gameover = true;
	else if($board[3] != 0 && $board[3] == $board[4] && $board[4] == $board[5])
		$gameover = true;
	else if($board[6] != 0 && $board[6] == $board[7] && $board[7] == $board[8])
		$gameover = true;
	else if($board[0] != 0 && $board[0] == $board[3] && $board[3] == $board[6])
		$gameover = true;
	else if($board[1] != 0 && $board[1] == $board[4] && $board[4] == $board[7])
		$gameover = true;
	else if($board[2] != 0 && $board[2] == $board[5] && $board[5] == $board[8])
		$gameover = true;
	else if($board[0] != 0 && $board[0] == $board[4] && $board[4] == $board[8])
		$gameover = true;
	else if($board[2] != 0 && $board[2] == $board[4] && $board[4] == $board[6])
		$gameover = true;

	$moveResult = 'move confirmed';
	if($gameover)
	{
		update_stats($playerID, 1, $DB_link);
		update_stats(get_other_player($playerID, $gameID, $DB_link), -1, $DB_link);
		$moveResult = 'win';
	}

	$boardFilled = true;
	for($i = 0; $i < 9; $i++)
	{
		if($board[$i] == 0)
		{
			$boardFilled = false;
			break;
		}
	}

	if(!$gameover && $boardFilled) //the game is a draw
	{
		update_stats($playerID, 0, $DB_link);
		update_stats(get_other_player($playerID, $gameID, $DB_link), 0, $DB_link);
		$moveResult = 'draw';
	}

	$board = json_encode($board);
	$turn = ($type == 1) ? 2 : 1;

	$query = "UPDATE Games SET turn = $turn, boardstate = \"$board\" WHERE gameid = $gameID";
	mysqli_query($DB_link, $query) or die('ERROR3: ' . mysqli_error($DB_link));

	echo $moveResult;


	function update_stats($player, $result, $DB_link)
	{
		if($result == -1) //loss
			$query = "UPDATE Players SET losses = losses + 1 WHERE playerid = $player";
		else if($result == 0) //draw
			$query = "UPDATE Players SET draws = draws + 1 WHERE playerid = $player";
		else if($result == 1) //win
			$query = "UPDATE Players SET wins = wins + 1 WHERE playerid = $player";

		mysqli_query($DB_link, $query) or die('ERROR updating stats: ' . mysqli_error($DB_link));
	}

	function get_other_player($player, $game, $DB_link)
	{
		$query = "SELECT player1id, player2id FROM Games WHERE gameid = $game";
		$result = mysqli_query($DB_link, $query) or die('ERROR finding game: ' . mysqli_error($DB_link));

		if (mysqli_num_rows($result) == 0)
			die('ERROR: game not found');

		$result = mysqli_fetch_assoc($result);

		return ($result['player1id'] == $player) ? $result['player2id'] : $result['player1id'];
	}
?>