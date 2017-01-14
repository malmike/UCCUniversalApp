<?php
    include "pushNotificationClass.php";
     
    function pushNotification($WPushURI, $WPPushURI, $employeeName, $checkID, $location, $checkType)
	{
        $arr = array('EmpName' => $employeeName, 'CheckID' => $checkID, 'CheckType' => $checkType, 'Location' => $location, 'Approval' => 'Pending');
        $main = array('data'=>$arr);
	    $json = json_encode($main);

        /*Windows phone app sid and secret*/
        $WPsid = 'ms-app://s-1-15-2-3452075587-2753200591-1356912711-3214445738-1100062652-302268477-3672084874';
        $WPsecret = 'jKGfvHv5qjEnXQ9RKLcPv1tREIv8u9J5';

        /*Windows store app sid and secret*/
        $Wsid = 'ms-app://s-1-15-2-1827275922-1334945956-3050260112-1164630092-3263312247-1439865370-2047268081';
        $Wsecret = 'R0SYhF0LcGcwsJEJcIwpbiAbZkB0sg0e';

        /*Windows Phone toast and raw notifications*/
        if($WPPushURI != '-')
        {
            $wpToast = new WPNTOAST($WPsid, $WPsecret);
            $wpToast->post_toast($WPPushURI, $wpToast->build_toast_xml($employeeName, $location, $checkID), WPNTypesEnum::Toast, "");
      
            $wpRaw = new WPNRAW($WPsid, $WPsecret);
            $wpRaw->post_raw($WPPushURI, $wpRaw->build_raw_xml($json), WPNTypesEnum::Raw, "");
        }

        /*Windows store toast and raw notifications*/
        if($WPushURI != '-')
        {
            $wToast = new WPNTOAST($Wsid, $Wsecret);
            $wToast->post_toast($WPushURI, $wToast->build_toast_xml($employeeName, $location, $checkID), WPNTypesEnum::Toast, "");
      
            $wRaw = new WPNRAW($Wsid, $Wsecret);
            $wRaw->post_raw($WPushURI, $wRaw->build_raw_xml($json), WPNTypesEnum::Raw, "");
        }



  //      echo "</br> 10.". $pushURI ."</br>";
		//echo "</br> 20". $checkID ."</br>";
  //      echo "</br> 30". $employeeName ."</br>";
		//echo "</br> 40". $location ."</br>";
		//echo "</br> 50". $checkType ."</br>";
  //      echo "</br> 60". $wpRaw->build_raw_xml($send) ."<br>";
  //      echo "</br> 70". $wpToast->build_toast_xml($employeeName, $location, $checkID) ."</br>";
  //      echo "</br> 70". $json ."</br></br></br></br></br></br></br>";
          
          echo "<label>REQUEST SENT</label>";
 
    }

?>




































<?php
	/*function pushNotification($pushURI, $employeeName, $checkID, $location, $checkType)
	//{
		//$pushURI = "http://s.notify.live.net/u/1/db3/H2QAAAB6ZOn67HEp_gXuz6XPxEXvq7PX8rlRvXO7YLs3TCRdk664tdfkmLBdgx-m0yud7DCfHFNQh_NYpeHXmqezRWTZxzPYs3ZN_3SbAwGuNTysuM_0Vfxz64EU3v60jZpVQHU/d2luZG93c3Bob25lZGVmYXVsdA/0pqGa5A7DkiEcxeWg4Asvw/k5NaOc2_-HB_uP-4UBRcR7p30XE";
		 // Create the toast message
		
		
		$toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" .
//					"<wp:Notification xmlns:wp=\"WPNotification\">" .
//					   "<wp:Toast>" .
//						   "<wp:Text1>{$employeeName}</wp:Text1>" . 
//						   "<wp:Text2>{$checkType}</wp:Text2>" .
//                           "<wp:Param>/SupervisorPage.xaml?" . 
//                                "employeeName={$employeeName}&amp;" . 
//                                "checkID={$checkID}&amp;" . 
//                                "location={$location}&amp;" . 
//                                "checkType={$checkType}" . 
//                           "</wp:Param>" .
//					    "</wp:Toast>" .
//					"</wp:Notification>";


////<toast>
////    <visual>
////        <binding template="ToastText04">
////            <text id="1">headlineText</text>
////            <text id="2">bodyText1</text>
////            <text id="3">bodyText2</text>
////        </binding>
////    </visual>
////</toast>


/////SupervisorPage.xaml?employeeName=."$employeeName".&employeeID=."$employeeID".&location=."$location."&reason=."$reason."&checkType=."$checkType."
//		// Create request to send
//		$r = curl_init();
//		curl_setopt($r, CURLOPT_URL, $pushURI);
//		curl_setopt($r, CURLOPT_RETURNTRANSFER, 1);
//		curl_setopt($r, CURLOPT_POST, true);
//		curl_setopt($r, CURLOPT_HEADER, true); 

//		// add headers
//		$httpHeaders=array('Content-type: text/xml; charset=utf-8', 'X-WindowsPhone-Target: toast',
//						'Accept: application/*', 'X-NotificationClass: 2','Content-Length:'.strlen($toastMessage));
//		curl_setopt($r, CURLOPT_HTTPHEADER, $httpHeaders);

//		// add message
//		curl_setopt($r, CURLOPT_POSTFIELDS, $toastMessage);

//		// execute request
//		$output = curl_exec($r);
//		curl_close($r);
//		echo "</br>".$pushURI."</br>";
//		echo $checkID."</br>";
//        echo $employeeName."</br>";
//		echo $location."</br>";
//		echo $checkType."</br>";
//      
//	}*/
?>


