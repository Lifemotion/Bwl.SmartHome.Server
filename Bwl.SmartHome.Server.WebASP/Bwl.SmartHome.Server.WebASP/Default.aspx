<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Bwl.SmartHome.Server.WebASP._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #smartObjects
        {
            width:400px;
            left:20%;
            display:block;
        }
        .SmartObject
        {
            border: 1px solid black;
        }
        .SmartObjectState
        {
            display:block;
        }
        .SmartObjectStateLabel
        {
            display:inline-block;
            width:50%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="smartObjects" runat="server">

    </div>
    </form>
</body>
</html>
