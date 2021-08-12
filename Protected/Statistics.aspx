<%@ Page Language="VB" MasterPageFile="~/Protected/MasterPage.master" AutoEventWireup="false" CodeFile="Statistics.aspx.vb" Inherits="Statistics" StylesheetTheme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <table align="center" width="100%">
            <tr>
                 <td valign="top" width="100%" style="border:0 px">
                    <table width="100%">
                        <tr>
                            <td align="center" style="font-size:25px">
                                <asp:Image ID="Image2" runat="server" BorderColor="Black" BorderWidth="1px" ImageUrl="~/pants.jpg" Height="50px" Width="40px"/>
                                Meyer Meyer Pants on Fire
                                <asp:Image ID="Image3" runat="server" BorderColor="Black" BorderWidth="1px" ImageUrl="~/pants.jpg" Height="50px" Width="40px"/>
                        </td></tr>
                        <tr>
                            <td style="border: 0px">
                                <br /><div align="center">
                                    <a href="TriviaQuestions2009.xls" style="font-size: 20px">
                                    TriviaQuestions2009.xls</a><br />
                                <asp:GridView ID="GridView3" runat="server" CellPadding="4" GridLines="None" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <br />
                            <table cellpadding="5">
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="dgStats" runat="server" CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                            <AlternatingRowStyle BackColor="Snow" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                    <td valign="top">
                                        <asp:GridView ID="GridView4" runat="server" CellPadding="4" 
                                            AutoGenerateColumns="False" Visible="False">
                                            <Columns>
                                                <asp:BoundField DataField="Hour" HeaderText="Hour" SortExpression="Hour" />
                                                <asp:BoundField DataField="HowMany" HeaderText="HowMany" SortExpression="HowMany" />
                                            </Columns>
                                            <AlternatingRowStyle BackColor="Snow" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table cellpadding="10">
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" CellPadding="4" GridLines="Horizontal" RowStyle-HorizontalAlign="Center">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                    <td align="center" valign="top">
                                        <br />
                                        <br />
                                        <asp:Button ID="Button4" runat="server" Text="Delete EVERYTHING!" Visible="False" /><br /><br />
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                                    <td>
                                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" GridLines="Horizontal" Visible="False" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                            <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#284775" />
                                        </asp:GridView></td>
                                </tr>
                            </table>
                            ©2009 Tim Fox-Meyer and Pat Huesers </div> 
                        </td>
                    </tr>
                </table>
                          
                        
                </td>
                
            </tr>
        </table>

</asp:Content >

