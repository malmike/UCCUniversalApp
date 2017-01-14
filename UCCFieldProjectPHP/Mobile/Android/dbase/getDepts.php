<?php
    include'../../../ConnectDB/connect.php';

    $sql = "SELECT * FROM departments";
    $row = $dbo->prepare($sql);
    $row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC);
    print(json_encode($result));
?>