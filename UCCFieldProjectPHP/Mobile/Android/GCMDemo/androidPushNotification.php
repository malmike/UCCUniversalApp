<?php
    /** Method to send Notification to GCM Server */
    function SendGcmNotification($registatoin_ids_from_table, $message, $gcm_api_key, $dry_run = false) 
    {
        // Set POST variables
        $url = 'https://android.googleapis.com/gcm/send';                       
        $fields = array('registration_ids' => $registatoin_ids_from_table,'data' => $message,'dry_run' => $dry_run);
        $headers = array('Authorization: key=' . $gcm_api_key, 'Content-Type: application/json');

        //print_r($headers);
        // Open connection
        if (!class_exists('curl_init')) 
        {
            $ch = curl_init();
        } 
        else 
        {
            echo "Class Doesnot Exist";
            exit();
        }

        // Set the url, number of POST vars, POST data
        curl_setopt($ch, CURLOPT_URL, $url);
        curl_setopt($ch, CURLOPT_POST, true);
        curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

        // Disabling SSL Certificate support temporarly
        curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
        curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($fields));

        // Execute post
        $result = curl_exec($ch);
        if ($result === FALSE) 
        {            
            die('Curl failed: ' . curl_error($ch));
            return false;
        } 
        else 
        {
            if($dry_run == false) 
            {
                //PRINT EASY TO READ REPSONSE
                echo '<br/><br/><h1>Results [showing devices sent to and response message]</h1><br/><br/>';
                echo str_ireplace("{", "<br/><br/>{", $result);
            }
            else 
            {
                //return json_decode($result, true);
                return str_ireplace("{", "<br/><br/>{", $result);
            }            
        }

        // Close connection
        curl_close($ch);
    }
?>

