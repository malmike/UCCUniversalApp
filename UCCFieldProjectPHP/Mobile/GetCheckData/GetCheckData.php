<?php
    include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";
	
	$id = clean($_REQUEST['UserID']);

    $sql = "SELECT `CheckID`,`EmpName`,`CheckType`,`Approval`,`Location` 
            FROM `employeelogs` WHERE `Approval`='Pending' 
            AND `SupervisorEmpFn`='". $id ."'";
    $row = $dbo->prepare($sql);
    $row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC);
    $main = array('data'=>$result);
	echo json_encode($main); 
?>
