<%@ Page Language="VB" MasterPageFile="~/Protected/MasterPage.master" AutoEventWireup="false" CodeFile="Caller.aspx.vb" Inherits="Caller" StyleSheetTheme="SkinFile" %>
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
                                    <td style="border: 0px">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="4" DataSourceID="SqlDataSource2" 
                                            onrowcommand="GridView2_RowCommand"  GridLines="Horizontal" 
                                            HorizontalAlign="Left" EmptyDataText="Keep Fishing!" SkinID="Tahoma11">
                                        <Columns>
                                            <asp:BoundField DataField="Hour" HeaderText="Hour" SortExpression="Hour" />
                                            <asp:BoundField DataField="QuestionNumber" DataFormatString="{0}" HeaderText="#" ReadOnly="True" >
                                            <ControlStyle Font-Size="X-Large" />
                                            <ItemStyle Font-Size="X-Large" /></asp:BoundField>
                                            <asp:BoundField DataField="Answer" DataFormatString="{0}" HeaderText="Answer" ReadOnly="True" />
                                            <asp:BoundField DataField="SubmittedBy" DataFormatString="{0}" HeaderText="Submitted By" ReadOnly="True" />
                                            <asp:BoundField DataField="SureNess" DataFormatString="{0}" HeaderText="Sureness" ReadOnly="True" />
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="Wrong" runat="server" CausesValidation="false" CommandName="Wrong" CommandArgument='<%# Eval("AnswerUniqueID") %>' Text="Wrong" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Size="X-Small" /></asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="Partial" runat="server" CausesValidation="false" CommandName="Partial" CommandArgument='<%# Eval("AnswerUniqueID") %>' Text="Partial" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Size="X-Small" /></asp:TemplateField>
                                            <asp:HyperLinkField DataNavigateUrlFields="AnswerUniqueID" DataNavigateUrlFormatString="Forms/Phone.aspx?ID={0}" Text="Correct!" Target="_blank" />
                                        </Columns>
                                    </asp:GridView></td></tr>
                                <tr>
                                <td style="border: 0px">
                                    <br />
                                    <br /><br />
                                </td>
                                </tr>
                                <tr>
                                    <td style="border: 0px">
                                    
                                    <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" DataKeyNames="SoundRecording,UniqueID" SkinID="Tahoma11">
                                        <Columns>
                                            <asp:BoundField DataField="QuestionNumber" HeaderText="Q #" />
                                            <asp:ButtonField ButtonType="Image" CommandName="EditQuestion" ImageUrl="~/Protected/Images/Edit.gif">
                                                <ControlStyle BorderStyle="None" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="IndividualQuestion" ImageUrl="~/Protected/Images/Record.gif" />
                                            <asp:ButtonField ButtonType="Image" CommandName="AnswerQuestion" ImageUrl="~/Protected/Images/Answer.gif" />
                                            <asp:BoundField  DataField="Question" HeaderText="Question" ReadOnly="True" >
                                            </asp:BoundField >
                                            <asp:BoundField DataField="PointValue" HeaderText="Points" />
                                        </Columns>
                                    </asp:GridView>
                                    </td></tr>
                                
                            </table></td>
                        </tr>
                    </table>
                    </td>

            </tr>
        </table>
    
    
    
    
            <asp:Timer ID="Timer1" runat="server" Interval="7000">
                </asp:Timer>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT A.Answer, A.QuestionUniqID, A.IsCorrect, A.SureNess, A.SubmittedBy, A.SubmittedOn, A.AnswerUniqueID, A.IsPartial, A.IsSubmitted, Q.QuestionNumber, Q.Hour FROM Answer AS A INNER JOIN Question AS Q ON A.QuestionUniqID = Q.UniqueID WHERE (Q.ClosedOn IS NULL) AND (A.IsSubmitted=0) ORDER BY Q.Hour, Q.QuestionNumber, A.SureNess, A.SubmittedOn">
            </asp:SqlDataSource>
           
            <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="gvQuestions">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="mpControlTop" />
                                <telerik:AjaxUpdatedControl ControlID="mpControlBottom" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                         <telerik:AjaxSetting AjaxControlID="GridView2">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="ddlQuestions" />
                                <telerik:AjaxUpdatedControl ControlID="gvQuestions" />
                            </UpdatedControls>
                            </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="Timer1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="gvQuestions" />
                                <telerik:AjaxUpdatedControl ControlID="GridView2" />
                                <telerik:AjaxUpdatedControl ControlID="PlayPageClosed" />
                                <telerik:AjaxUpdatedControl ControlID="PlayPageNew" />
                                <telerik:AjaxUpdatedControl ControlID="ddlQuestions" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                
    </telerik:RadAjaxManagerProxy>
</asp:Content>
