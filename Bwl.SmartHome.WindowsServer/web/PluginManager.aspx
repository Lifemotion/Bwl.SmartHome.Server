<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PluginManager.aspx.vb" Inherits="Bwl.SmartHome.Server.WebASP.PluginManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Plugin Manager</title>
    <style>
        body {
            
        }

        .body_style {
            width: 1000px;
            margin: 0 auto;
            border:2px solid #dedede;
            padding:20px;
        }

        .body_style .plugin_upload_form{
            display:block;
            margin-top:20px;
            width:350px;
            height: 167px;
        }
        table {
            border-color: #001437;
            text-align:center;
            background-color: #ffffff;
            border-collapse: collapse;
            width:100%;
        }

            table th {
                background-color: #004180;
                color: #FFF;
                text-align: center;
                 border:2px solid #001437;
            }

            table td {
                padding-left: 25px;
                padding-right: 25px;
                padding-top: 5px;
                padding-bottom: 5px;
                border:1px solid #001437;
                border-width: 2px;
                white-space: nowrap;
                font-style: Serif;
            }
        #submit {
            width: 343px;
            margin-left: 1px;
            height: 30px;
        }
    </style>
</head>
<body>
    <div class="body_style">
        <asp:Table ID="PluginsTable" runat="server"> 
            <asp:TableRow>
                <asp:TableHeaderCell>Plugin</asp:TableHeaderCell>
                <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date</asp:TableHeaderCell>
                <asp:TableHeaderCell>Action</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table> 
        <div class="plugin_upload_form">
            <form runat="server">
                <fieldset>
                <legend>Upload new plugin</legend>
                <asp:TextBox ID="newPluginDescription" runat="server" TextMode="MultiLine" Rows="4" Width="335px" ></asp:TextBox><br />
                <asp:FileUpload ID="pluginFile"  runat="server" Width="347px"/>  <br />         
                <asp:Button runat="server" ID="btnUpload" OnClick="btnUploadClick" Text="Upload" />
                </fieldset>    
            </form>
        </div>
    </div>


</body>
</html>
