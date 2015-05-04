<?php
	define('DB_NAME', '***********');
	define('DB_USER', '***********');
	define('DB_PASSWORD', '***********');
	define('DB_HOST', 'localhost');

	function connect() {
		$link = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME) or die('Error connecting to the database: ' . mysql_error());

		if(!$link) {
			die("Connection error: " . mysqli_connect_error());
		}

		return $link;
	}
?>