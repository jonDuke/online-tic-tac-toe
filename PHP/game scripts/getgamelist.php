<?php
	require_once 'app_config.php';
	$DB_link = connect();

	$id = $_POST['id'];
	
	//$query = "SELECT activegamecount, game1, game2, game3, game4, game5, game6, game7, game8, game9, game10 FROM Players WHERE playerid = $id";
	$query = "SELECT gameids FROM Players WHERE playerid = $id";
	$result = mysqli_query($DB_link, $query) or die('ERROR1: ' . mysqli_error($DB_link));

	if (mysqli_num_rows($result) == 0)
		die('ERROR: player not found');

	$result = mysqli_fetch_assoc($result);
	$gameids = json_decode($result['gameids']);

	$gameCount = 0;
	for($i=0; $i<10; $i++)
		if($gameids[$i] != -1)
			$gameCount++;

	$data = array($gameCount);

	$dataIndex = 1;
	for($g = 0; $g < 10; $g++)
	{
		$gameid = $gameids[$g];

		if($gameid != -1)
		{
			//get game data
			$query = "SELECT player1id, player2id, turn FROM Games WHERE gameid = $gameid";
			$gamedata = mysqli_query($DB_link, $query) or die('ERROR2: ' . mysqli_error($DB_link));
			if(mysqli_num_rows($gamedata) == 0)
				die('ERROR: game not found');

			//determine other player id
			$gamedata = mysqli_fetch_assoc($gamedata);
			$otherPlayerid = ($gamedata['player1id'] == $id) ? $gamedata['player2id'] : $gamedata['player1id'];

			//get other player name
			$query = "SELECT name FROM Players WHERE playerid = $otherPlayerid";
			$otherName = mysqli_query($DB_link, $query) or die('ERROR3: ' . mysqli_error($DB_link));
			if(mysqli_num_rows($otherName) == 0)
				die('ERROR: other player not found');

			$otherName = mysqli_fetch_assoc($otherName);

			//determine if it's the player's turn
			$turn = false;
			if($gamedata['player1id'] == $id && $gamedata['turn'] == 1)
				$turn = true;
			else if($gamedata['player2id'] == $id && $gamedata['turn'] == 2)
				$turn = true;

			//game data: gameid, player2name, turn
			$data[$dataIndex] = array('gameid' => $gameid, 'player2name' => $otherName['name'], 'turn' => $turn);
			$dataIndex++;
		}
	}

	echo json_encode($data);
?>