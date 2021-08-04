<?php

$con = include_once 'connection.php';

$username = $_POST['username'];
$password = $_POST['password'];

$checkquery = "SELECT ID FROM users WHERE username='" . $username . "';";

$namecheck = mysqli_query($con, $checkquery) or die("2"); // Name check query failed

if(mysqli_num_rows($namecheck) > 0) {
    echo "3"; // Name already exist!
    exit();
}

$insertUserQuerry = "INSERT INTO user (username,password)VALUES('".$username."','".$password."');";
mysqli_query($con,$insertUserQuerry)or die("4: Insert Player Quert Failed"); // Insert user query failed

echo "0";

?>