<?php

	include'../../ConnectDB/Sanitize.php';
	include'../../ConnectDB/connect.php';
	include'pushNotifications.php';
    include'../Android/GCMDemo';
	
	$employeeID = clean($_POST['UserID']);
	//$employeeID = "EMP001";
    
    $employeeName = clean($_POST['EmployeeName']);

	$supervisorID = clean($_POST['SupervisorID']);
	
	$approval = "Pending";
	
	$checkType = clean($_POST['CheckType']);
	
	//$supervisorID = "EMP004";
	$location = clean($_POST['Location']);
    //$location = "MTN";
	$reason = clean($_POST['Reason']);
	
	date_default_timezone_set('Africa/Kampala');
	$checkTime = date('Y-m-d H:i:s', time());
	
	$pushURI = "";
	
	$ins  = "INSERT INTO `employeelogs`(`EmpFn`, `EmpName`, `SupervisorEmpFn`, `CheckTime`, `CheckType`, `Approval`, `Location`, `Reason`) VALUES 
		('$employeeID', '$employeeName', '$supervisorID','$checkTime','$checkType','$approval','$location','$reason')";
		
	$row = $dbo->prepare($ins);
	$row->execute();

    //Get the CheckID entry in the employeelogs table
    $sel  = "Select `CheckID` From `employeelogs` Where `EmpFn`= '$employeeID' ORDER BY `CheckID` DESC  LIMIT 1";		
	$row2 = $dbo->prepare($sel);
	$row2->execute();
    
    //Create an array of the values we have fetched from the table
    $result2=$row2->fetchAll(PDO::FETCH_ASSOC);
   
    $CheckID = "";
    if(count($result2) == 1) 
	{
		foreach($result2 as $item)
		{
			$CheckID = $item['CheckID'];
		}
	}
	
	//$sql = "SELECT `WPushURI`,`WPPushURI` FROM `registeredemployees` WHERE `EmpFn`='$supervisorID'";
	//$row = $dbo->prepare($sql);
	//$row->execute();
	//$result=$row->fetchAll(PDO::FETCH_ASSOC);
	//	
	//if(count($result) == 1) 
	//{
	//	foreach($result as $item)
	//	{
	//		$WPushURI = $item['WPushURI'];
 //           $WPPushURI = $item['WPPushURI'];
	//	}
	//}


	//if($WPushURI != "-" || $WPPushURI != "-")
	//{
	//	pushNotification($WPushURI, $WPPushURI, $employeeName, $CheckID, $location, $check);
	//}

    $WPushURI = "-";
    $WPPushURI = "-";
    $reg_id = "-";
	$sql = "SELECT `WPushURI`,`WPPushURI`,`registration_id` FROM `registeredemployees` WHERE `EmpFn`='$supervisorID'";
	$row = $dbo->prepare($sql);
	$row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC); 
        
			
	if(count($result) == 1) 
	{
		foreach($result as $item)
		{
			$WPushURI = $item['WPushURI'];
            $WPPushURI = $item['WPPushURI'];
            $reg_id = $item['registration_id'];
		}

        if($WPushURI != "-" || $WPPushURI != "-" || $reg_id != "-")
		{
            if($WPushURI != "-" || $WPPushURI != "-")
            {
                pushNotification($WPushURI, $WPPushURI, $employeeName, $CheckID, $location, $check);
            }
			if($reg_id != "-")
            {
                $empName = $_SESSION['SESS_EMPLOYEE_FNAME'] . "___ ___" . $_SESSION['SESS_EMPLOYEE_LNAME'];
                $messageAuto = $empName . "~~~" . $check . "~~~" . $supervisorID. "~~~" . $CheckID;
                sendAndroidPN($checkTime, $check, $location, $reason, $messageAuto, $reg_id);
            }
            
		}
        else
        {
            echo "<label>REQUEST SENT</label>";
        }	
	}
    else
    {
        echo "<label>REQUEST SENT</label>";
    }	
	
                         
?>