<?php
    $con = include_once 'connection.php';
    $username = $_POST['username'];
    $password = $_POST['password'];

    $query = "SELECT ID FROM users WHERE username='" . $username . "';";
    $result = mysqli_query($con, $query) or die("1");

    if(mysqli_num_rows($result) != 1) {
        echo "2";
        exit();
    }

    $query = "SELECT ID FROM users WHERE username='" . $username . "' AND password='" . $password . "';";
    $result = mysqli_query($con, $query) or die("1");

    if(mysqli_num_rows($result) != 1) {
        echo "3";
        exit();        
    }

    echo "0";
?>