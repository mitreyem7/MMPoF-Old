<%@ Page Language="VB"   EnableEventValidation="false" MasterPageFile="~/Protected/MasterPage.master" AutoEventWireup="false" CodeFile="Questions.aspx.vb" Inherits="Protected_Questions3" title="MMPOF Home" StyleSheetTheme="SkinFile" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Media-Player-ASP.NET-Control" namespace="Media_Player_ASP.NET_Control" tagprefix="cc1" %>
<%@ Register Assembly="MakeNoise" Namespace="MakeNoise" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Hour"></asp:Label>
                <asp:DropDownList ID="ddlHour" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Open Question"></asp:Label>
                <asp:DropDownList ID="ddlQuestions" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="lblWarning" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <table style="width: 100%;">
        <tr>
            <td>
                    <cc1:Media_Player_Control ID="mpControlTop" runat="server" 
                                        Height="65px" Volume="100" Width="600px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="SoundRecording,UniqueID" SkinID="Tahoma11">
        <Columns>
            <asp:BoundField DataField="QuestionNumber" HeaderText="Q #" />
            <asp:ButtonField ButtonType="Image" CommandName="EditQuestion" 
                ImageUrl="~/Protected/Images/Edit.gif">
                <ControlStyle BorderStyle="None" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="IndividualQuestion" 
                ImageUrl="~/Protected/Images/Record.gif" />
            <asp:ButtonField ButtonType="Image" CommandName="AnswerQuestion" 
                ImageUrl="~/Protected/Images/Answer.gif" />
                <%--<asp:HyperLinkField DataNavigateUrlFields="UniqueID" DataNavigateUrlFormatString="Forms/EditQuestion.aspx?ID={0}" Text="{Edit}" />
                <asp:HyperLinkField DataNavigateUrlFields="UniqueID" DataNavigateUrlFormatString="Forms/Individual.aspx?ID={0}" Text="{Indiv}" />
                <asp:HyperLinkField DataNavigateUrlFields="UniqueID" DataNavigateUrlFormatString="Forms/SubmitAnswer.aspx?ID={0}" Text="{Answer}" />--%>
            <asp:BoundField  DataField="Question" HeaderText="Question" ReadOnly="True" >
            </asp:BoundField >
            <asp:BoundField  DataField="CorrectAnswer" HeaderText="Correct Answer" ReadOnly="True" SortExpression="CorrectAnswer"  >
            </asp:BoundField >
            <asp:BoundField  DataField="ClosedBy" HeaderText="Found By" ReadOnly="True"  >
            </asp:BoundField >
            <asp:BoundField DataField="PointValue" HeaderText="Points" />
        </Columns>
    </asp:GridView>
            </td>
        </tr>
        </table>
                <asp:Timer ID="Timer1" runat="server" Interval="15000">
                </asp:Timer>
                
        <cc2:PlayPageSound ID="PlayPageClosed" runat="server" />
        <cc2:PlayPageSound ID="PlayPageNew" runat="server" />
                
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="ddlHour">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="ddlQuestions" />
                                <telerik:AjaxUpdatedControl ControlID="gvQuestions" />
                                <telerik:AjaxUpdatedControl ControlID="mpControlTop" />
                                <telerik:AjaxUpdatedControl ControlID="mpControlBottom" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="gvQuestions">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="mpControlTop" />
                                <telerik:AjaxUpdatedControl ControlID="mpControlBottom" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                        <telerik:AjaxSetting AjaxControlID="Timer1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="gvQuestions" />
                                <telerik:AjaxUpdatedControl ControlID="PlayPageClosed" />
                                <telerik:AjaxUpdatedControl ControlID="PlayPageNew" />
                                <telerik:AjaxUpdatedControl ControlID="ddlQuestions" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                
    </telerik:RadAjaxManagerProxy>

</asp:Content>

