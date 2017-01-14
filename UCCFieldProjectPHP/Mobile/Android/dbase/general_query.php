<?php
    include'../../../ConnectDB/connect.php';

    $sql = "". $_REQUEST['query'] . "";
    $row = $dbo->prepare($sql);
    $row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC);
    print(json_encode($result));
?>

