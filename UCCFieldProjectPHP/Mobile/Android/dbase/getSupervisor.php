<?php
    include'../../../ConnectDB/connect.php';

    $sql = "SELECT `FirstName`,`MiddleName`,`LastName` FROM `employees` 
            WHERE `Serial`=(SELECT `Acting` FROM `employees` 
            WHERE `Serial`=(SELECT `Supervisor` FROM `employees` WHERE `EmpFn` ='".$_REQUEST['idSaved']."'))";   
    $row = $dbo->prepare($sql);
    $row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC);
    print(json_encode($result));    

?>