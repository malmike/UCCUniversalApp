<?php
    include'../../../ConnectDB/connect.php';
    include'sendAndroidPN';
    include'../../PushNotifications/pushNotifications.php';

    $tablename = 'registeredemployees'; 
    $gcm_api_key_passed = 'AIzaSyC6yYTcXC2Urp1hVcAOP6oO4bIpATGaES8';

    //message from url
    $messageAuto = $_GET['messageAuto'];

	$splitMessage = explode('~~~', $messageAuto);
	$empName = $splitMessage[0];
	$checkName = $splitMessage[1];
	$superEmpFn = $splitMessage[2];
	$logId = $splitMessage[3];


	$namesArray = explode('___', $empName);
	$fname = $namesArray[0];
	$mname = $namesArray[1];
	$lname = $namesArray[2];
    $eName = $fname." ".$lname;
 
   
	$sqlFetchCheckDetails = "SELECT * FROM employeelogs WHERE CheckID = '" . $logId . "'";
    $row = $dbo->prepare($sqlFetchCheckDetails);
    $row->execute();
    $result=$row->fetchAll(PDO::FETCH_ASSOC);

	//$checkDetails = mysql_query($sqlFetchCheckDetails, $con);
	foreach($result as $getRow)
    {
		$checktime = $getRow['CheckTime'];
		$checktype = $getRow['CheckType'];
		$loc = $getRow['Location'];
		$reason = $getRow['Reason'];
	}


    $WPushURI = "-";
    $WPPushURI = "-";
    $reg_id = "-";

    $sql = "SELECT `WPushURI`,`WPPushURI`,`registration_id` FROM " . $tablename . " WHERE EmpFn = '" . $superEmpFn ."'";
    $row2 = $dbo->prepare($sql);
    $row2->execute();
    $result2=$row2->fetchAll(PDO::FETCH_ASSOC);

    if(count($result2) == 1) 
	{
		foreach($result2 as $item)
		{
			$WPushURI = $item['WPushURI'];
            $WPPushURI = $item['WPPushURI'];
            $reg_id = $item['registration_id'];
		}

        if($WPushURI != "-" || $WPPushURI != "-" || $reg_id != "-")
		{
            if($WPushURI != "-" || $WPPushURI != "-")
            {
                pushNotification($WPushURI, $WPPushURI, $eName, $CheckID, $loc, $checktype);
            }
			if($reg_id != "-")
            {
                sendAndroidPN($checktime, $checktype, $loc, $reason, $messageAuto, $reg_id);
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
