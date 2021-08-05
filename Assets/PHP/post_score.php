<?php

    $con = include_once 'connection.php';

    //$username = $_POST['username'];
    $score = $_POST['score'];
    $deaths = $_POST['deaths'];
    $kills = $_POST['kills'];
    $time = $_POST['time'];
    $distance = $_POST['distance'];


    $username = "carlos";
    $uid = GetUserID($username,$con);
    $query = "SELECT ID FROM users WHERE username='" . $username . "';";
    echo $query;
    $query_result = mysqli_query($con, $query) or die("2: Id User check query failed");
    if (is_null(query_result)) {
         echo "es nulo";
    }
    if(mysqli_num_rows($query_result) != 1) {
        echo "3: User no exist!";
        exit();
    }

    $userid = mysqli_fetch_row($query_result);
    $query = "SELECT user_id FROM scoreboard WHERE user_id='" . $userid[0] . "';";
    $query_result = mysqli_query($con, $query);
    $row_cnt = mysqli_num_rows($query_result);
    if($row_cnt != 1) {
        $query = "INSERT INTO scoreboard VALUES (NULL, '" . $uid . "','" . $score . "','" . $distance . "','" . $time . "','" . $kills . "','" . $deaths . "');";
        echo "new value as insert";
    }
    else {
        $query =  "UPDATE scoreboard SET 
                    score = '" . $score . "', 
                    distance= '" . $distance . "',
                    time= '" . $time . "', 
                    kills= '" . $kills . "', 
                    deaths = '" . $deaths ."'
                    WHERE user_id = '" . $uid . "';";
                    echo "new value as update";
                    //echo $query;
    }

    $query_result = mysqli_query($con, $query) or die("4: Insert/Update score query failed");

    echo "0";








    /*$uid = GetUserID($username,$con);
    $aux = 'SELECT * FROM scoreboard WHERE user_id = "'.$uid.'"';
    $sql = "0";
    //la consulta se puede hacer.
    if ($con->multi_query($aux)) 
    {   //se puede guardar los resultados.
        if ($result = $con->store_result()) 
        {   //se guarda el primer resultado.
            $row = $result->fetch_row();
            if(is_null($row))
            {
                if(is_null($uid))
                {
                    echo "Register does not been created. At least 1 register was null.";
                }
                else
                {
                    $sql = "INSERT INTO scoreboard VALUES (NULL, '" . $uid . "','" . $score . "','" . $distance . "','" . $time . "','" . $kills . "','" . $deaths . "');";
                        echo "new value as insert";
                }

            }
            else
            {   //pregunto si el score es  mayor.
                if(isActualGreater($username,$score,$con))
                {
                    $sql = "UPDATE scoreboard SET 
                    score = '" . $score . "', 
                    distance= '" . $distance . "',
                    time= '" . $time . "', 
                    kills= '" . $kills . "', 
                    deaths = '" . $deaths ."'
                    WHERE user_id = '" . $uid . "';";
                    echo "new value as update";
                }
                else
                {
                    echo "Not Updater cos' not higher.";
                }

            }
            $result->free();
            //echo $sql;
        }
    }
    if($sql!="0")
    {
        $query = $con->query($sql) or die("Not executed any query");
    }*/

    function GetUserID($name,$conect)
{
    $aux = "SELECT ID FROM users WHERE username='" . $name . "';";
    if ($conect->multi_query($aux)) 
    {
        if ($result = $conect->store_result()) 
        {
            $row = $result->fetch_row();
            $result->free();
            return $row[0];
        }
    }
}

/*function isActualGreater($name,$actualScore,$conect) 
{
    $uid = GetUserID($name,$conect);
    $aux = 'SELECT score FROM `scoreboard` WHERE user_id = "'.$uid.'"';
    if ($conect->multi_query($aux)) 
    {
        if ($result = $conect->store_result()) 
        {
            $row = $result->fetch_row();
            $result->free();
            return ($actualScore > $row[0]);
        }
    }
    $AvoidCrash = "1";
    return $AvoidCrash;
}*/
?>