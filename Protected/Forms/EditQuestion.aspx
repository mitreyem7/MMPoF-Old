<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditQuestion.aspx.vb" Inherits="EditQuestion" ValidateRequest="false" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Question</title>
    <link rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Label ID="Label4" runat="server" Font-Size="XX-Large" ForeColor="Red"></asp:Label><br />
        <table runat="server" id="table1" style="width: 100%" border="1" bordercolor="black">
            <tr align=center>
                <td valign="middle">
            Hour&nbsp;
            <asp:Label ID="HourBox" runat="server"></asp:Label>
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;Question #<asp:Label ID="Label3" runat="server"></asp:Label></td>
                <td width="100">
                </td>
                <td width="45%">
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Edit" ForeColor="OrangeRed"></asp:Label></td>
            </tr>
            <tr align=center>
                <td>
        <asp:Label ID="LabelQuestion" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
                    Question</td>
                <td width="45%">
            <asp:TextBox ID="QuestionBox" runat="server" Height="80px" TextMode="MultiLine" Width="301px"></asp:TextBox></td>
            </tr>
            <tr align=center>
                <td>
                    <asp:Label ID="LabelPointValue" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
                    Point Value</td>
                <td width="45%">
                   
            <asp:TextBox ID="PointValueBox" runat="server" Height="20px" Width="55px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr align=center>
                <td>
                    <asp:Label ID="LabelClosed" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label>
                    &nbsp; &nbsp; &nbsp;<asp:Label ID="LabelCorrect" runat="server" Font-Size="Large"
                        ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
                </td>
                <td width="45%">
        
            <asp:CheckBox ID="CheckBoxClosed" runat="server" Text="Closed" />&nbsp;
        
            &nbsp;&nbsp;
                    &nbsp;&nbsp; 
                    <asp:CheckBox ID="CheckBoxCorrect" runat="server" Text="Correct" /></td>
            </tr>
            <tr align=center>
                <td>
        <asp:Label ID="LabelAnswer" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
                    &nbsp;Correct Answer</td>
                <td width="45%">
        
            <asp:TextBox ID="tbCorrectAnswer" runat="server" Height="50px" TextMode="MultiLine" Width="301px"></asp:TextBox></td>
            </tr>
            <tr align=center>
                <td>
                    <asp:Label ID="LabelClosedBy" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
                    Correct Answer<br />
                    Submitted By</td>
                <td width="45%">
        
            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" DataMember="DefaultView"
                DataSourceID="SqlDataSource1" DataTextField="LoweredUserName" DataValueField="LoweredUserName">
            </asp:DropDownList></td>
            </tr>
            <tr align=center>
                <td>
        <asp:Label ID="LabelPhonePerson" runat="server" Font-Size="Large" ForeColor="LimeGreen"></asp:Label></td>
                <td width="100">
            Phone Person</td>
                <td width="45%">
        
            <asp:TextBox ID="TextBoxPhone" runat="server" Height="20px" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
            <td colspan=3 align=center>
            &nbsp;<br />
                    <asp:Label ID="Label10" runat="server" Font-Size="X-Large" ForeColor="Red" Text="The Question Has Changed!"
                        Visible="False"></asp:Label>
            </td>
            </tr>
        </table>
        <br />
            <asp:Button ID="Button1" runat="server" Height="40px" Text="Edit!" Width="75px" CssClass="fire" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="Button2"
                runat="server" Height="21px" Text="Cancel" Width="56px" /><br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT [LoweredUserName] FROM [aspnet_Users]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
