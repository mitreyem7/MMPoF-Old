Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess
Partial Class Protected_Questions3
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim OpenHour, OpenQuestionNumber As Integer
    Dim statQuestions(19) As QuestionsStatus
    Structure QuestionsStatus
        Public IsNew As Boolean
        Public IsOpen As Boolean
    End Structure
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Profile.ActiveQuestion = 0
        Me.Page.Title = "MMPOF Home"
        If Not IsPostBack Then
            'Default all values
            For Each rowStats As QuestionsStatus In statQuestions
                rowStats.IsNew = True
                rowStats.IsOpen = False
            Next

            For intHour As Integer = 1 To 50
                ddlHour.Items.Add(intHour)
            Next

            Profile.LastPage = "Questions.aspx"
            OpenHour = Profile.Hour
            ddlHour.SelectedValue = Profile.Hour
            Dim dsQuestions As New DataSet
            dsQuestions = GetDS("SELECT * FROM [Question] WHERE Hour = '" + OpenHour.ToString + "' ORDER BY QuestionNumber", meyerdb)
            gvQuestions.DataSource = dsQuestions.Tables(0)
            gvQuestions.DataBind()
            Load_ddlQuestions(dsQuestions)
            Session("statsArray") = LoadStatsArray(dsQuestions, statQuestions)
            ddlQuestions.Visible = True
            Label2.Visible = True
            'If Profile.Job <> "Record" And Profile.Job <> "Record2" Then
            '    ddlQuestions.Visible = False
            '    Label2.Visible = False
            'End If
        End If
    End Sub

    Protected Sub ddlHour_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHour.SelectedIndexChanged
        Profile.Hour = ddlHour.SelectedValue.ToString
        OpenHour = Profile.Hour
        Dim dsQuestions As New DataSet
        dsQuestions = GetDS("SELECT * FROM [Question] WHERE [Hour] = '" + OpenHour.ToString + "' ORDER BY QuestionNumber", meyerdb)
        gvQuestions.DataSource = dsQuestions.Tables(0)
        gvQuestions.DataBind()
        Load_ddlQuestions(dsQuestions)
        Session("statsArray") = LoadStatsArray(dsQuestions, statQuestions)
    End Sub

    Protected Sub ddlQuestions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQuestions.SelectedIndexChanged
        If ddlQuestions.SelectedValue.ToString <> "All Questions Opened" And ddlQuestions.SelectedValue.ToString <> "Please Select" Then


            If getString("select count(questionnumber) from Question WHERE [Hour] = " + Profile.Hour.ToString + " and QuestionNumber=" + ddlQuestions.SelectedValue.ToString, meyerdb) = "0" Then
                Response.Redirect("Forms/OpenQuestion.aspx?ID=" + getString("INSERT [Question] (QuestionNumber, IsOpen, [Hour], IsCorrect, CorrectAnswer) Values ('" + ddlQuestions.SelectedValue.ToString + "', 0, '" + Profile.Hour.ToString + "', 0, '') Select @@IDENTITY", meyerdb))
            Else
                lblWarning.Text = "Question " + ddlQuestions.SelectedValue.ToString + " is already opened!"
                Dim dsQuestions As New DataSet
                dsQuestions = GetDS("SELECT * FROM [Question] WHERE [Hour] = '" + OpenHour.ToString + "' ORDER BY QuestionNumber", meyerdb)
                gvQuestions.DataSource = dsQuestions.Tables(0)
                gvQuestions.DataBind()
                Load_ddlQuestions(dsQuestions)
            End If
        End If
    End Sub
    Function Load_ddlQuestions(ByVal dsQuestions As DataSet) As Boolean
        ddlQuestions.Items.Clear()
        ddlQuestions.Items.Add("Please Select")
        For intQuestion = 1 To 18
            Dim bolFound As Boolean = False
            For Each rowQuestion As DataRow In dsQuestions.Tables(0).Rows

                If rowQuestion.Item("QuestionNumber") = intQuestion Then
                    bolFound = True
                End If
            Next
            If bolFound = False Then
                ddlQuestions.Items.Add(intQuestion)
            End If
        Next
        If ddlQuestions.Items.Count = 1 Then
            ddlQuestions.Items.Clear()
            ddlQuestions.Items.Add("All Questions Opened")
        End If
    End Function

    Protected Sub gvQuestions_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvQuestions.RowCommand
        Select Case e.CommandName
            Case "EditQuestion"
                Response.Redirect("Forms/EditQuestion.aspx?ID=" + gvQuestions.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("UniqueID").ToString())
            Case "IndividualQuestion"
                Response.Redirect("Forms/Individual.aspx?ID=" + gvQuestions.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("UniqueID").ToString())
            Case "AnswerQuestion"
                Response.Redirect("Forms/SubmitAnswer.aspx?ID=" + gvQuestions.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("UniqueID").ToString())
        End Select
    End Sub
    Protected Sub gvQuestions_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvQuestions.SelectedIndexChanging

        mpControlTop.MovieURL = "http://audio.mmpof.com/" + gvQuestions.DataKeys(Convert.ToInt32(e.NewSelectedIndex)).Values("SoundRecording").ToString()

    End Sub

    Protected Sub gvQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuestions.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim isOpen As Integer
            Dim isCorrect As Integer
            If DataBinder.Eval(e.Row.DataItem, "ClosedOn").ToString.Length > 0 Then
                isOpen = 0
            Else
                isOpen = 1
            End If
            isCorrect = DataBinder.Eval(e.Row.DataItem, "isCorrect")
            If isOpen = 1 Then
                e.Row.BackColor = System.Drawing.Color.Snow
            Else
                e.Row.Cells(3).Controls.RemoveAt(0)
                If isCorrect = 0 Then
                    e.Row.BackColor = System.Drawing.Color.LightSalmon
                Else
                    e.Row.BackColor = System.Drawing.Color.LightGreen
                End If
            End If

            'If User.IsInRole("OffSiteUser") = True Then
            ' e.Row.Cells.Item(0).Text = ""
            'End If

            e.Row.Cells(1).Attributes.Add("Title", "Edit")
            e.Row.Cells(2).Attributes.Add("Title", "Research This One")
            e.Row.Cells(3).Attributes.Add("Title", "Answer")

            For intIndex As Integer = 0 To e.Row.Cells.Count - 1
                If intIndex < 1 Or intIndex > 3 Then
                    e.Row.Cells(intIndex).Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.gvQuestions, "Select$" + e.Row.RowIndex.ToString)
                End If
            Next
        End If

    End Sub
    Protected Overloads Overrides Sub Render(ByVal writer As HtmlTextWriter)
        For i As Integer = 0 To Me.gvQuestions.Rows.Count - 1
            ClientScript.RegisterForEventValidation(Me.gvQuestions.UniqueID, "Select$" + i.ToString)
        Next

        MyBase.Render(writer)

    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Me.Page.MaintainScrollPositionOnPostBack = True
        statQuestions = Session("statsArray")
        Dim intSelectedIndex As Integer = gvQuestions.SelectedIndex
        Dim dsQuestions As New DataSet
        dsQuestions = GetDS("SELECT * FROM [Question] WHERE [Hour] = '" + Profile.Hour.ToString + "' ORDER BY QuestionNumber", meyerdb)
        gvQuestions.DataSource = dsQuestions.Tables(0)
        gvQuestions.DataBind()
        Load_ddlQuestions(dsQuestions)
        If gvQuestions.Rows.Count >= intSelectedIndex Then
            gvQuestions.SelectedIndex = intSelectedIndex
        End If

        Dim strNewQuestions() As String = Split(CheckStatsArrayForNewQuestions(dsQuestions, statQuestions), "|")
        Dim strClosedQuestions() As String = Split(CheckStatsArrayForClosedQuestions(dsQuestions, statQuestions), "|")
        Session("statsArray") = statQuestions


        If strNewQuestions.Length > 0 Then
            PlayPageNew.SoundFile = "http://www.mmpof.com/Protected/" + strNewQuestions(0)
        End If

        If strClosedQuestions.Length > 0 Then
            PlayPageClosed.SoundFile = "http://www.mmpof.com/Protected/" + strClosedQuestions(0)
        End If

        ddlQuestions.Visible = True
        Label2.Visible = True
        'If Profile.Job <> "Record" And Profile.Job <> "Record2" Then
        '    ddlQuestions.Visible = False
        '    Label2.Visible = False
        'End If

    End Sub
    Function LoadStatsArray(ByVal dsQuestions As DataSet, ByVal statsQuestions As QuestionsStatus()) As QuestionsStatus()

        For Each rowQuestion As DataRow In dsQuestions.Tables(0).Rows
            statsQuestions(rowQuestion.Item("QuestionNumber")).IsOpen = rowQuestion.Item("IsOpen")
            statsQuestions(rowQuestion.Item("QuestionNumber")).IsNew = False
        Next
        Return statsQuestions
    End Function
    Function CheckStatsArrayForNewQuestions(ByVal dsQuestions As DataSet, ByVal statsQuestions As QuestionsStatus()) As String
        Dim rtnString As String = ""
        For Each rowQuestion As DataRow In dsQuestions.Tables(0).Rows
            If statsQuestions(rowQuestion.Item("QuestionNumber")).IsNew = True Then
                rtnString += "Question" + rowQuestion.Item("QuestionNumber").ToString + "New.wav|"
                statsQuestions(rowQuestion.Item("QuestionNumber")).IsNew = False
            End If
        Next
        Return rtnString.Trim("|")
    End Function
    Function CheckStatsArrayForClosedQuestions(ByVal dsQuestions As DataSet, ByVal statsQuestions As QuestionsStatus()) As String
        Dim rtnString As String = ""
        For Each rowQuestion As DataRow In dsQuestions.Tables(0).Rows
            If statsQuestions(rowQuestion.Item("QuestionNumber")).IsOpen = True Then
                If rowQuestion.Item("IsOpen") = False Then
                    rtnString += "Question" + rowQuestion.Item("QuestionNumber").ToString + "Closed.wav|"
                    statsQuestions(rowQuestion.Item("QuestionNumber")).IsOpen = False
                End If
            End If
        Next
        Return rtnString.Trim("|")
    End Function
End Class
