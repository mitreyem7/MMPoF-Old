<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Phone.aspx.vb" Inherits="Phone" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Called In Answer</title>
    <link rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setFocus()
        {
        document.getElementById("tbPhonePerson").focus()
        }
    </script>
</head>
<body onload="setFocus()">
    <form id="form1" runat="server">
    <div><center>
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Call In An Answer"></asp:Label><br />
        &nbsp;</center>
        <center>
            <asp:Label ID="LabelHour" runat="server" Font-Size="Medium"></asp:Label>
            &nbsp;
            <asp:Label ID="LabelQuestionNumber" runat="server" Font-Size="Large" ForeColor="Blue"></asp:Label><br />
        &nbsp;
        <asp:TextBox ID="tbAnswer" runat="server" TextMode="MultiLine" Height="75px" Width="250px"></asp:TextBox>&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Answer"></asp:Label>&nbsp;
        </center>
        <center>
        <asp:TextBox ID="tbPointValue" runat="server" Height="22px" Width="63px"></asp:TextBox>&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Point Value"></asp:Label></center>
        <center>
            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" DataMember="DefaultView"
                DataSourceID="SqlDataSource1" DataTextField="LoweredUserName" DataValueField="LoweredUserName">
            </asp:DropDownList>&nbsp; Found By<br />
            <asp:TextBox ID="tbPhonePerson" runat="server" Height="25px" Width="140px"></asp:TextBox>&nbsp;&nbsp;<asp:Label
                ID="Label3" runat="server" Text="Phone Person"></asp:Label><br />
        <br />
    
    
        <asp:Button ID="Button1" runat="server" Height="45px" Text="Submit" Width="75px"/>
        &nbsp; &nbsp;
        <asp:Button ID="Button2" runat="server" Text="Cancel" />
        <br />
        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></center>
        <center>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT [LoweredUserName] FROM [aspnet_Users]"></asp:SqlDataSource>
            &nbsp;</center>
    </div></form>
</body>
</html>
