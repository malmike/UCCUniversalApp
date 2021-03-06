<?php
    class WPNTypesEnum
    {       
        const Toast = 'wns/toast';
        const Badge = 'wns/badge';
        const Tile  = 'wns/tile';
        const Raw   = 'wns/raw';
    }                         

    class WPNResponse
    {
        public $message = '';
        public $error = false;
        public $httpCode = '';

        function __construct($message, $httpCode, $error = false)
        {
            $this->message = $message;
            $this->httpCode = $httpCode;
            $this->error = $error;
        }
    }
?>



<?php
    class WPNRAW
    {            
        private $access_token = '';
        private $sid = '';
        private $secret = '';

        function __construct($sid, $secret)
        {
            $this->sid = $sid;
            $this->secret = $secret;
        }

        private function get_access_token()
        {
            if($this->access_token != '')
            {
                return;
            }

            $str = "grant_type=client_credentials&client_id=$this->sid&client_secret=$this->secret&scope=notify.windows.com";
            $url = "https://login.live.com/accesstoken.srf";

            $ch = curl_init($url);
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/x-www-form-urlencoded'));
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$str");
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            curl_close($ch);                       

            $output = json_decode($output);

            if(isset($output->error))
            {
                throw new Exception($output->error_description);
            }else{
                $output->access_token;
            }

            $this->access_token = $output->access_token;
        }

        public function build_raw_xml($json)
        {
             return $json;
        }

        public function post_raw($uri, $xml_data, $type = WPNTypesEnum::Raw, $rawTag = '')
        {

            if($this->access_token == '')
            {
                $this->get_access_token();
            }

            $headers = array('Content-Type: application/octet-stream', "Content-Length: " . strlen($xml_data), "X-WNS-Type: $type", "Authorization: Bearer $this->access_token");
            if($rawTag != '')
            {
                array_push($headers, "X-WNS-Tag: $rawTag");
            }

            $ch = curl_init($uri);
            # Raws: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868263.aspx
            # http://msdn.microsoft.com/en-us/library/windows/apps/hh465435.aspx
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$xml_data");
            curl_setopt($ch, CURLOPT_VERBOSE, 1);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            $response = curl_getinfo( $ch );
            curl_close($ch);

            $code = $response['http_code'];
            if($code == 200)
            {
                return new WPNResponse('Successfully sent message', $code);
            }
            else if($code == 401)
            {
                $this->access_token = '';
                return $this->post_raw($uri, $xml_data, $type, $rawTag);
            }
            else if($code == 410 || $code == 404)
            {
                return new WPNResponse('Expired or invalid URI', $code, true);
            }
            else
            {
                return new WPNResponse('Unknown error while sending message', $code, true);
            }
        }
    }
?>


<?php
    class WPNTOAST
    {            
        private $access_token = '';
        private $sid = '';
        private $secret = '';

        function __construct($sid, $secret)
        {
            $this->sid = $sid;
            $this->secret = $secret;
        }

        private function get_access_token()
        {
            if($this->access_token != '')
            {
                return;
            }

            $str = "grant_type=client_credentials&client_id=$this->sid&client_secret=$this->secret&scope=notify.windows.com";
            $url = "https://login.live.com/accesstoken.srf";

            $ch = curl_init($url);
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/x-www-form-urlencoded'));
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$str");
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            curl_close($ch);                       

            $output = json_decode($output);

            if(isset($output->error))
            {
                throw new Exception($output->error_description);
            }else{
                $output->access_token;
            }

            $this->access_token = $output->access_token;
        }

        public function build_toast_xml($title, $text2, $text3)
        {
            return '<?xml version="1.0" encoding="utf-16"?>'.
                            '<toast launch="{&quot;type&quot;:&quot;toast&quot;:&quot;param1&quot;:&quot;'.$title.'&quot;:&quot;param2&quot;:&quot;'.$text3.'&quot;}">'.
                               '<visual lang="en-US">'.
                                    '<binding template="ToastText04">'.
                                        '<text id="1">'.$title.'</text>'.
                                        '<text id="2">'.$text2.'</text>'.
                                        '<text id="3">'.$text3.'</text>'.
                                    '</binding>'.
                                '</visual>'.
                            '</toast>';
        }

        public function post_toast($uri, $xml_data, $type = WPNTypesEnum::Toast, $toastTag = '')
        {

            if($this->access_token == '')
            {
                $this->get_access_token();
            }

            $headers = array('Content-Type: text/xml', "Content-Length: " . strlen($xml_data), "X-WNS-Type: $type", "Authorization: Bearer $this->access_token");
            if($toastTag != '')
            {
                array_push($headers, "X-WNS-Tag: $toastTag");
            }

            $ch = curl_init($uri);
            # Toasts: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868263.aspx
            # http://msdn.microsoft.com/en-us/library/windows/apps/hh465435.aspx
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$xml_data");
            curl_setopt($ch, CURLOPT_VERBOSE, 1);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            $response = curl_getinfo( $ch );
            curl_close($ch);

            $code = $response['http_code'];
            if($code == 200)
            {
                return new WPNResponse('Successfully sent message', $code);
            }
            else if($code == 401)
            {
                $this->access_token = '';
                return $this->post_toast($uri, $xml_data, $type, $toastTag);
            }
            else if($code == 410 || $code == 404)
            {
                return new WPNResponse('Expired or invalid URI', $code, true);
            }
            else
            {
                return new WPNResponse('Unknown error while sending message', $code, true);
            }
        }
    }
?>



<?php
    class WPNTILE
    {            
        private $access_token = '';
        private $sid = '';
        private $secret = '';

        function __construct($sid, $secret)
        {
            $this->sid = $sid;
            $this->secret = $secret;
        }

        private function get_access_token()
        {
            if($this->access_token != '')
            {
                return;
            }

            $str = "grant_type=client_credentials&client_id=$this->sid&client_secret=$this->secret&scope=notify.windows.com";
            $url = "https://login.live.com/accesstoken.srf";

            $ch = curl_init($url);
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/x-www-form-urlencoded'));
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$str");
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            curl_close($ch);                       

            $output = json_decode($output);

            if(isset($output->error))
            {
                throw new Exception($output->error_description);
            }

            $this->access_token = $output->access_token;
        }

        public function build_tile_xml($title, $img)
        {
            return '<?xml version="1.0" encoding="utf-16"?>'.
            '<tile>'.
              '<visual lang="en-US">'.
                '<binding template="TileWideImageAndText01">'.
                  '<image id="1" src="'.$img.'"/>'.
                  '<text id="1">'.$title.'</text>'.
                '</binding>'.
              '</visual>'.
            '</tile>';
        }

        public function post_tile($uri, $xml_data, $type = WPNTypesEnum::Tile, $tileTag = '')
        {
            if($this->access_token == '')
            {
                $this->get_access_token();
            }

            $headers = array('Content-Type: text/xml', "Content-Length: " . strlen($xml_data), "X-WNS-Type: $type", "Authorization: Bearer $this->access_token");
            if($tileTag != '')
            {
                array_push($headers, "X-WNS-Tag: $tileTag");
            }

            $ch = curl_init($uri);
            # Tiles: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868263.aspx
            # http://msdn.microsoft.com/en-us/library/windows/apps/hh465435.aspx
            curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
            curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
            curl_setopt($ch, CURLOPT_POSTFIELDS, "$xml_data");
            curl_setopt($ch, CURLOPT_VERBOSE, 1);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            $output = curl_exec($ch);
            $response = curl_getinfo( $ch );
            curl_close($ch);

            $code = $response['http_code'];
            if($code == 200)
            {
                return new WPNResponse('Successfully sent message', $code);
            }
            else if($code == 401)
            {
                $this->access_token = '';
                return $this->post_tile($uri, $xml_data, $type, $tileTag);
            }
            else if($code == 410 || $code == 404)
            {
                return new WPNResponse('Expired or invalid URI', $code, true);
            }
            else
            {
                return new WPNResponse('Unknown error while sending message', $code, true);
            }
        }
    }
?>