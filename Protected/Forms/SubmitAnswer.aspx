<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubmitAnswer.aspx.vb" Inherits="SubmitAnswer" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Submit Answer</title>
    <link rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setFocus()
        {
        document.getElementById("tbAnswer").focus()
        }
    </script>
</head>
<body onload="setFocus()">
    <form id="form1" runat="server">
    <div align=center>
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Submit Answer"></asp:Label>&nbsp;
        <br />
        <br />
        <asp:Label ID="lbQuestion" runat="server" Font-Italic="True" Font-Size="Large"></asp:Label>&nbsp;
            <br />
            <br />
            <asp:TextBox ID="tbAnswer" runat="server" Height="70px" Width="300px" TextMode="MultiLine"></asp:TextBox>
        <br /><br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
            AutoPostBack="True">
                <asp:ListItem Value="1">1 - This is the answer!</asp:ListItem>
                <asp:ListItem Value="2">2 - Researched guess</asp:ListItem>
                <asp:ListItem Value="3">3 -  Just fishing</asp:ListItem>
                <asp:ListItem Value="4">Note</asp:ListItem>
            </asp:RadioButtonList>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Cancel" />
    </div>
    </form>
</body>
</html>
