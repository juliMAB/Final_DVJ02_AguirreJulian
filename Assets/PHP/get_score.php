<?php

    $con = include_once 'connection.php';

    $query = "SELECT score FROM scoreboard ORDER BY score DESC";
    $result = mysqli_query($con, $query) or die("2: Top score query failed");

    $num_results = mysqli_num_rows($result);
    for($i = 0; $i < $num_results; $i++)
    {
        $row = mysqli_fetch_array($result);
        echo $row['score'] . "," ;
    }

?>