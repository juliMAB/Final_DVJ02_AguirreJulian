<?php

    $con = include_once 'connection.php';

    $query = "  SELECT B.username, A.score 
                FROM scoreboard A
                INNER JOIN users B
                ON (A.user_id = B.ID)
                ORDER BY score DESC LIMIT 10";

    $result = mysqli_query($con, $query) or die("1");

    $num_results = mysqli_num_rows($result);
    for($i = 0; $i < $num_results; $i++)
    {
        $row = mysqli_fetch_array($result);
        echo $row['username'] . "#" . $row['score'] . "\t";
    }

?>