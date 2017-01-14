<?php
	function supervisorPage($dbo)
	{
        $id = $_SESSION['SESS_EMPLOYEE_ID'];
        
	    ////Check whether the query was successful or not

        $sql="SELECT FirstName, LastName, `CheckType`, `Approval`, `Location`, `Reason`, (emplogs.CheckID)CID FROM `employeelogs` 
            INNER JOIN (Select FirstName, LastName, CheckID FROM employeelogs 
            INNER JOIN employees on employeelogs.EmpFn = employees.EmpFn Where Approval='Pending') AS emplogs 
            on employeelogs.CheckID=emplogs.CheckID where SupervisorEmpFn ='" . $id . "'";
        $row=$dbo->prepare($sql);
        $row->execute();
        $result=$row->fetchAll(PDO::FETCH_ASSOC);
        if(count($result)<=0)
        {
            echo "<br><br><label>No request at the moment!!!!</label>";
        }
        else
        {
            echo "<br><br>TABLE OF REQUESTS FOR APPROVAL";
?>
            <form role="form" method="post" action="?connect=approval"> 
                <table class="table table-striped">
                <thead>
                    <tr>
                    <th>Select</th>
                    <th>EmployeeName</th>
                    <th>CheckType</th>
                    <th>Location</th>
                    <th>Reason</th>
                    </tr>
                </thead>
                <tbody>   
        
                        <?php
                        foreach($result as $perrow)
                        {
                        ?>
                            <tr>
                                <td><input type="checkbox" name="approvetype[]" value="<?php echo($perrow['CID'])?>"></td>
                                <td><?php echo($perrow['FirstName']) ?> <?php echo($perrow['LastName']) ?></td>
                                <td><?php echo($perrow['CheckType']) ?></td>
                                <td><?php echo($perrow['Location']) ?></td>
                                <td><?php echo($perrow['Reason']) ?></td> 
                         
                            </tr>
                        <?php 
                            }
                        ?>
                    </tbody>
                </table>  
                </div>
                <br>
            <button type="submit" id="accept" name="accept" class="btn btn-success btn-block">ACCEPT</button> 
            <button type="submit" id="deny" name="deny" class="btn btn-danger btn-block">DENY</button> 
            </form>      
            <div style="color: rgb(139, 50, 223); font-style:italic;">Site live updating still under development. Please be sure to refresh every so often for new pending requests</div>
<?php
        }
    }
?>