<?php
	function clean($str)
	{
		$str = @trim($str);
		
		if(get_magic_quotes_gpc())
		{
			$str = stripslashes($str);
		}
		return $str;	
	}
	
	function normalClean($str)
	{
		$str = @trim($str);
		
		if(get_magic_quotes_gpc())
		{
			$str = stripslashes($str);
		}
		return $str;
	}
?>