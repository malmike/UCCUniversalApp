<?php
    include'../../ConnectDB/connect.php';
    include'../../ConnectDB/Sanitize.php';
	
    $UserID = clean($_POST['UserID']);
    $PushURI = $_POST['PushURI']; 
    $Device = clean($_POST['Device']);

    updatePushURI($UserID,$PushURI,$dbo,$Device);   

    function updatePushURI($Serial,$PushURI,$dbo,$Device)
    {
         if($Device == "W") $uriupdate="UPDATE `registeredemployees` SET `WPushURI`='$PushURI' WHERE `Serial`='$Serial'";
         if($Device == "WP") $uriupdate="UPDATE `registeredemployees` SET `WPPushURI`='$PushURI' WHERE `Serial`='$Serial'";
         $row2=$dbo->prepare($uriupdate);
         $row2->execute();

     }


?>


