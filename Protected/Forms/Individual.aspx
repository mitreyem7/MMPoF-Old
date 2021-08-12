<%@ Page Language="VB" MasterPageFile="~/Protected/MasterPage.master"  AutoEventWireup="false" CodeFile="Individual.aspx.vb" Inherits="Individual" StylesheetTheme="SkinFile" %>
<%@ Register assembly="Media-Player-ASP.NET-Control" namespace="Media_Player_ASP.NET_Control" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <table align="center" width="100%">
            <tr>
                 <td valign="top" width="100%" style="border:0 px">
                    <table width="100%">
                        <tr>
                            <td align="center" style="font-size:25px">
                                Meyer Meyer Pants on Fire</td>
                        </tr>
                        <tr>
                            <td style="border: 0px">
                            <table>
                                <tr>
                                    <td style="border: 0px; height: 225px;">
                                        <br />
                    <cc1:Media_Player_Control ID="mpControl" runat="server" 
                                        Height="65px" Volume="100" Width="325px" />
                <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="SoundRecording,UniqueID" SkinID="Tahoma11">
        <Columns>
            <asp:BoundField  DataField="Hour" HeaderText="Hour"  >
            </asp:BoundField >
            <asp:BoundField DataField="QuestionNumber" HeaderText="Q #" />
            <asp:ButtonField ButtonType="Image" CommandName="EditQuestion" 
                ImageUrl="~/Protected/Images/Edit.gif">
                <ControlStyle BorderStyle="None" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="AnswerQuestion" 
                ImageUrl="~/Protected/Images/Answer.gif" />
            <asp:BoundField  DataField="Question" HeaderText="Question" ReadOnly="True" >
            </asp:BoundField >
            <asp:BoundField DataField="PointValue" HeaderText="Points" />
        </Columns>
    </asp:GridView>
                                        &nbsp;<br />
                                        Answer<br />
            <table>
                <tr>
                    <td><asp:TextBox ID="tbAnswer" runat="server" Height="100px" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
                    <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="1">1 - This is the answer!</asp:ListItem>
                        <asp:ListItem Value="2">2 - Researched guess</asp:ListItem>
                        <asp:ListItem Value="3">3 -  Just fishing</asp:ListItem>
                        <asp:ListItem Value="4">Note</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td><asp:Label ID="LabelAlreadySubmit" runat="server" ForeColor="Red"></asp:Label></td>
                    <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></td>
                </tr>
            </table>
            
                                        </td></tr>
                                <tr><td style="border: 0px">
                                    <table style="width: 100%" cellpadding="4">
                                        <tr>
                                            <td valign="top" width="45%">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" GridLines="Horizontal" HorizontalAlign="Left" 
                                                    EmptyDataText="No Answers Submitted Yet!" SkinID="Tahoma11">
                                                    <Columns>
                                                        <asp:BoundField DataField="Answer" DataFormatString="{0}" HeaderText="Answer" ReadOnly="True" />
                                                        <asp:BoundField DataField="SubmittedBy" DataFormatString="{0}" HeaderText="Submitted By" ReadOnly="True" />
                                                        <asp:BoundField DataField="SureNess" DataFormatString="{0}" HeaderText="Sureness" ReadOnly="True" />
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                </asp:GridView>
                                            </td>
                                            <td valign="top" width="25%">
                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" DataKeyNames="AnswerUniqueID" GridLines="Horizontal" 
                                                    HorizontalAlign="Center" SkinID="Tahoma11">
                                                    <Columns>
                                                        <asp:BoundField DataField="Answer" HeaderText="Wrong Answers" SortExpression="Answer" />
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                </asp:GridView>&nbsp;
                                            </td>
                                            <td valign="top" width="30%">
                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                                                    AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" 
                                                    AllowSorting = "True" CellPadding="4" DataKeyNames="AnswerUniqueID" 
                                                    GridLines="Horizontal" HorizontalAlign="Right" DataSourceID = "SQLDataSource1" 
                                                    SkinID="Tahoma11">
                                                    <Columns>
                                                        <asp:BoundField DataField="Answerr" HeaderText="Notes and Partials" SortExpression="Answerr" HtmlEncode="False" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#FFFF80" BorderColor="#FF8000" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    </td></tr>
                                
                                <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="Timer1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="GridView2" />
                                                <telerik:AjaxUpdatedControl ControlID="GridView3" />
                                                <telerik:AjaxUpdatedControl ControlID="GridView4" />
                                                <telerik:AjaxUpdatedControl ControlID="gvQuestions" />
                                                <telerik:AjaxUpdatedControl ControlID="HyperLink1" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManagerProxy>
                                <asp:Timer ID="Timer1" runat="server" Interval="10000">
                                </asp:Timer>
                                
                            </table></td>
                        </tr>
                    </table>
                    </td>
                
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MeyerOnFireConnectionString %>"
            OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Answer] SET [Answer] = @Answerr WHERE [AnswerUniqueID] = @original_AnswerUniqueID" 
            DeleteCommand="DELETE FROM Answer WHERE [AnswerUniqueID] = @original_AnswerUniqueID">
            <UpdateParameters>
                <asp:Parameter Name="Answerr" Type="String" />
                <asp:Parameter Name="original_Answerr" Type="String" />
                <asp:Parameter Name="original_Submit" Type="String" />
                <asp:Parameter Name="original_AnswerUniqueID" Type="Int16" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="AnswerUniqueID" Type="Int16" />
            </DeleteParameters>
        </asp:SqlDataSource>
                                        
</asp:Content>
