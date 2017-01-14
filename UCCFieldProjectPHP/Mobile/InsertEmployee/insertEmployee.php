<?php
	include'../../ConnectDB/connect.php';
	include'../../ConnectDB/Sanitize.php';
	
	$json = $_POST['UserID'];
    $pushURI = clean($_POST['PushURI']);
    $Device = clean($_POST['Device']);

    //$pushURI = "malmike21";
	////$EmpFN = "UCC-PF-212";
	//$json = '{"data":[{"Serial":"236","EmpFn":"UCC-PF-212","FirstName":"Peter","LastName":"Mukuru"}]}';
	////$json = '{"data":[{"Serial":"1","EmpFn":"UCC-PF-114","FirstName":"Abdul","LastName":"Musoke"}]}';

    echo $json."<br>";
	$elements = json_decode($json, true);
	
	foreach($elements['data'] as $item)
	{
		$Serial = $item['Serial'];
		$EmpFn = $item['EmpFn'];
		$FirstName = $item['FirstName'];
		$LastName = $item['LastName'];
		
        echo $Serial."<br>";
        echo $EmpFn."<br>";
        echo $FirstName."<br>";
        echo $LastName."<br>";

        $retrieve="SELECT `Serial` FROM `registeredemployees` WHERE `EmpFn`='$EmpFn'";
        $row1=$dbo->prepare($retrieve);
        $row1->execute();
          
        $result=$row1->fetchAll(PDO::FETCH_ASSOC);

        if(count($result)==1)
        {
            if($Device == "W") $uriupdate="UPDATE `registeredemployees` SET `WPushURI`='$pushURI' WHERE `Serial`='$Serial'";
            if($Device == "WP") $uriupdate="UPDATE `registeredemployees` SET `WPPushURI`='$pushURI' WHERE `Serial`='$Serial'";
            $row2=$dbo->prepare($uriupdate);
            $row2->execute();

        }
        else
        {
		    if($Device == "W") $sql = "INSERT INTO `registeredemployees`(`Serial`, `EmpFn`, `FirstName`, `LastName`, `WPushURI`) VALUES 
		    ('$Serial','$EmpFn','$FirstName','$LastName', '$pushURI')";
            if($Device == "WP") $sql = "INSERT INTO `registeredemployees`(`Serial`, `EmpFn`, `FirstName`, `LastName`, `WPPushURI`) VALUES 
		    ('$Serial','$EmpFn','$FirstName','$LastName', '$pushURI')";
		
		    $row=$dbo->prepare($sql);
		    $row->execute();
	    }
	  } 
?>