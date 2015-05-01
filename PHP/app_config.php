<?php
	define('DB_NAME', 'noblehou_GameDB');
	define('DB_USER', 'noblehou_tictact');
	define('DB_PASSWORD', 'q8p94bvy38qpn4');
	define('DB_HOST', 'localhost');

	function connect() {
		$link = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die('Error connecting to the database: ' . mysql_error());

		if(!$link) {
			die("Connection error: " . mysqli_connect_error());
		}

		return $link;
	}
?>