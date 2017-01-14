<?php
 
$regId = $_GET['regId'];
 
 
 $con = mysql_connect('localhost', '872852', 'root1234');//change server name  //pass username according your settings
    
    
 if(!$con){
  die('MySQL connection failed'.mysql_error());
 }
 
 $db = mysql_select_db('872852');// also change the Mysql database name
 if(!$db){
  die('Database selection failed'.mysql_error());
 }
 
 $sql = "INSERT INTO registeredemployees (registration_id) values ('$regId')";
 
 if(!mysql_query($sql, $con)){
  die('MySQL query failed'.mysql_error());
 }
 
mysql_close($con);

?>