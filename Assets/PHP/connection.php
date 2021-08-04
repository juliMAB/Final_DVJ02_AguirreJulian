<?php

    $host = "localhost";
    $username = "root";
    $password = "root";
    $database = "shooter";

    $con = mysqli_connect($host, $username, $password, $database);

    if(mysqli_connect_errno()) {
        echo "1: Connection Failed!";
        exit();
    }

    return $con;

?>