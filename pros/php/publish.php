<?php

require("phpMQTT.php");
require("config.php");
$message = "Hello CloudAMQP MQTT!";

//MQTT client id to use for the device. "" will generate a client id automatically

$mqtt = new bluerhinos\phpMQTT($host, $port, "ClientID".rand());
if ($mqtt->connect(true,NULL,$username,$password)) 
{
  $mqtt->publish("testing1",$message, 0);
  $mqtt->close();
}
else
{
  echo "Fail or time out
";
}

?>