<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="_Default" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MMPOF Login</title>
    <link rel="stylesheet" type="text/css" />
</head>
<body > 
    <form id="form1" runat="server">
    <div>
        <center>
        <asp:Label ID="Label4" runat="server" Font-Size="XX-Large" Text="Meyer Meyer Pants on Fire" ForeColor="OrangeRed"></asp:Label>&nbsp;</center>
        <center>
            <br />
            <asp:Image ID="Image1" runat="server" AlternateText="MMPoF" ImageUrl="~/pants.jpg" />&nbsp;</center>
        <center>
        <br />
            <asp:Login ID="Login1" runat="server"
                FailureText="Your login attempt was not successful. Call 320-309-9171 if you need assistance." 
                DestinationPageUrl="~/Protected/Statistics.aspx" TabIndex="1" 
                DisplayRememberMe="False">
            </asp:Login>
            <br />
            This site works best in Internet Explorer.&nbsp; Other users may not have media 
            functionality.<br />
            <%--<asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
                NavigateUrl="~/wmpfirefoxplugin.zip">Firefox users - Use of this site 
            requires the Windows Media Player plugin - Click this link to Download</asp:HyperLink>
            <br />--%>
        <br />
        To add a new user to the database, enter information below.</center>
        <center>
        If you do not know the Team Password, you can call 320-309-9171 to obtain it.</center>
        <center>
            <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Desired Username"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbNewName" runat="server" TabIndex="2"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Password"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbNewPassword" runat="server" EnableTheming="True" 
                        TextMode="Password" TabIndex="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Retype Password"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbNewPassword2" runat="server" TextMode="Password" 
                        TabIndex="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Real Name"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbName" runat="server" TabIndex="5"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="E-Mail Address"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbEmail" runat="server" TabIndex="6"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Phone Number During the Contest"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbPhone" runat="server" TabIndex="7"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Team Password"></asp:Label></td>
                <td style="width: 53px">
                    <asp:TextBox ID="tbTeam" runat="server" TextMode="Password" TabIndex="8"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 26px">
                    <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label></td>
                <td style="width: 53px; height: 26px;">
                    <asp:Button ID="Button2" runat="server" Text="Register" /></td>
            </tr>
        </table>
            Version 3.14.17</center>
        <center>
            ©2009 Tim Fox-Meyer and Pat Huesers</center>
    </div>
    </form>
</body>
</html>
