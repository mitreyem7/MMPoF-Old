<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dump.aspx.vb" Inherits="Dump" StylesheetTheme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>50 Hour Question Compiler</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align=center>
        <asp:Button ID="Button1" runat="server" Text="Save as Excel Spreadsheet" /><br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Hour" HeaderText="Hour" SortExpression="Hour" />
                <asp:BoundField DataField="QuestionNumber" HeaderText="QuestionNumber" SortExpression="QuestionNumber" />
                <asp:BoundField DataField="Question" HeaderText="Question" SortExpression="Question" />
                <asp:BoundField DataField="PointValue" HeaderText="PointValue" SortExpression="PointValue" />
                <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                <asp:BoundField DataField="CorrectAnswer" HeaderText="CorrectAnswer" SortExpression="CorrectAnswer" />
                <asp:BoundField DataField="ClosedBy" HeaderText="AnsweredBy" SortExpression="ClosedBy" />
                <asp:BoundField DataField="ClosedOn" HeaderText="ClosedOn" SortExpression="ClosedOn" />
                <asp:BoundField DataField="IsCorrect" HeaderText="IsCorrect" SortExpression="IsCorrect" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM [Question] ORDER BY [Hour], [QuestionNumber]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
