<?php
    function updateApproval($dbo)
    {
        $values = $_POST['approvetype'];
        
        if(isset($_POST['accept'])) $approval="ALLOWED";
        if(isset($_POST['deny'])) $approval="DENIED";

        foreach($values as $x)
        {
           $qry = "UPDATE `employeelogs` SET `Approval` = '{$approval}' WHERE `CheckID`= {$x}";
           $row = $dbo->prepare($qry);
           $row->execute(); 
        }

        header("Location:?action=supervisor");
    }
?>

