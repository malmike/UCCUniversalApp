<?php
    include"../../Mobile/PushNotifications/pushNotifications.php";
    include"../Mobile/Android/GCMDemo";

	function sendNotification($dbo)
	{ 

		if ( isset( $_POST['CheckIn'] ) ) 
		{
			$check = "CheckIn";
			choose($dbo,  $_SESSION['SESS_EMPLOYEE_FNAME']." ".$_SESSION['SESS_EMPLOYEE_LNAME'], 
                $_SESSION['SESS_SUPERVISOR_ID'], $_SESSION['SESS_EMPLOYEE_ID'], 
                clean($_POST['location']), clean($_POST['reason']), $check);
                
		}
		if ( isset( $_POST['CheckOut'] ) )			
		{
			$check = "CheckOut";
			choose($dbo, $_SESSION['SESS_EMPLOYEE_FNAME']." ".$_SESSION['SESS_EMPLOYEE_LNAME'],
                $_SESSION['SESS_SUPERVISOR_ID'], $_SESSION['SESS_EMPLOYEE_ID'],
                clean($_POST['location']), clean($_POST['reason']), $check);

		}
	}
	
	function choose($dbo, $employeeName, $supervisorID, $employeeID, $location, $reason, $check)
	{
        //Insert values into the employeelog table
        date_default_timezone_set('Africa/Kampala');
		$checkTime = date('Y-m-d H:i:s', time());
		//$ins  = "INSERT INTO `employeelogs`(`EmpFn`, `EmpName`, `SupervisorEmpFn`, `CheckTime`, `CheckType`, `Approval`, `Location`, `Reason`) VALUES 
		//('$employeeID', '$employeeName', '$supervisorID','2015-07-14 15:59:24','$check', 'Pending','$location','$reason')";

        $ins = "INSERT INTO `employeelogs`(`EmpFn`, `EmpName`, `SupervisorEmpFn`, `CheckTime`, `CheckType`, `Approval`, `Location`, `Reason`) VALUES 
		('$employeeID', '$employeeName', '$supervisorID','$checkTime','$check', 'Pending','$location','$reason')";
		
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

        //echo $CheckID;
        //echo "<br/>1. ".$checkTime."<br/>2. ".$employeeName."<br/>3. ".$supervisorID."<br/>4. ".$employeeID."<br/>5. ".$location."<br/>6. ".$reason."<br/>7. ".$check;
	
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
	}
?>