<?php
    include'androidPushNotification.php';

    function sendAndroidPN($checktime, $checktype, $loc, $reason, $messageAuto, $reg_id)
    {
        $allDetails = $checktime."~~~".$checktype."~~~".$loc."~~~".$reason;					
	    $message = $messageAuto."~~~".$allDetails;
        $message_passed = array("Notice" => $message); 

        $gcm_response = SendGcmNotification($reg_id, $message_passed, $gcm_api_key_passed, $dry_run = false);
    }   
    
?>

