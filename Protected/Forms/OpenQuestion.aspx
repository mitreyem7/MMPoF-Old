<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OpenQuestion.aspx.vb" Inherits="OpenQuestion" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Open Question</title>
    <link rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setFocus()
        {
        document.getElementById("QuestionBox").focus()
        }
    </script>
</head>
<body onload="setFocus()">
    <form id="form1" runat="server">
    <div><center>
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Open"></asp:Label>&nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            Hour&nbsp;
        <asp:Label ID="HourBox" runat="server"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;Question #<asp:Label ID="QuestionNumberBox" runat="server" Font-Size="X-Large" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;</center>
        <center>
            <br />
        Point Value &nbsp<asp:TextBox ID="PointValueBox" runat="server" Width="55px" Height="20px"></asp:TextBox>&nbsp;
         &nbsp; &nbsp;:&nbsp; </center>
        <center>
        <br />
            &nbsp;
        <asp:TextBox ID="QuestionBox" runat="server" Height="95px" Width="390px" TextMode="MultiLine"></asp:TextBox><br />
            Question</center>
        <center>
        <asp:Button ID="Button1" runat="server" Text="Open!" Height="40px" Width="75px" CssClass="fire" />
        &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:Button ID="Button2" runat="server" Text="Cancel" Height="21px" Width="53px" CssClass="fire" /></center>
        <center>
            <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>&nbsp;</center>
    </div>
    </form>
</body>
</html>
