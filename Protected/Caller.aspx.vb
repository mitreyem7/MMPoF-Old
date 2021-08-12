Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess
Partial Class Caller
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Profile.ActiveQuestion = 0
        GridView2.Enabled = True
        'If Profile.Job <> "Phone" And Profile.Job <> "Phone2" Then
        '    GridView2.Enabled = False
        'End If

        If Not IsPostBack Then
            Profile.LastPage = "Caller.aspx"
        End If
        Dim dsQuestions As New DataSet
        dsQuestions = GetDS("SELECT * FROM [Question] WHERE ClosedOn is null ORDER BY QuestionNumber", meyerdb)
        gvQuestions.DataSource = dsQuestions.Tables(0)
        gvQuestions.DataBind()

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") > 9 Then
                e.Row.DataItem("QuestionNumber") = e.Row.DataItem("QuestionNumber") - 9
            End If
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 1 Or DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 6 Then
                e.Row.BackColor = System.Drawing.Color.LightBlue
            End If
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 2 Or DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 7 Then
                e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow
            End If
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 3 Or DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 8 Then
                e.Row.BackColor = System.Drawing.Color.LightGray
            End If
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 4 Or DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 9 Then
                e.Row.BackColor = System.Drawing.Color.Snow
            End If
            If DataBinder.Eval(e.Row.DataItem, "QuestionNumber") = 5 Then
                e.Row.BackColor = System.Drawing.Color.LightCyan
            End If
            If DataBinder.Eval(e.Row.DataItem, "SureNess") = 1 Then
                e.Row.Font.Size = "14"
                e.Row.Font.Bold = "True"
            End If
            If DataBinder.Eval(e.Row.DataItem, "SureNess") = 3 Then
                e.Row.Font.Italic = "True"
            End If
        End If
    End Sub
    

   

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim id As String = e.CommandArgument
        Dim CommandButton As Button = e.CommandSource
        If e.CommandName = "Wrong" Then
            execSQL("UPDATE [Answer] SET IsCorrect=0, IsSubmitted=1, IsPartial=0 WHERE AnswerUniqueID = '" + id + "'", meyerdb)
            CommandButton.BackColor = Drawing.Color.Gray
        End If
        If e.CommandName = "Partial" And CommandButton.BackColor <> Drawing.Color.Gray Then
            execSQL("UPDATE [Answer] SET Answer = 'PARTIAL: ' + Answer, IsCorrect=0, IsSubmitted=1, IsPartial=1, SubmittedOn='" + DateTime.Now + "' WHERE AnswerUniqueID = '" + id + "'", meyerdb)
            CommandButton.BackColor = Drawing.Color.Gray
        End If
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Page.MaintainScrollPositionOnPostBack = True
        Dim dsQuestions As New DataSet
        dsQuestions = GetDS("SELECT * FROM [Question] WHERE ClosedOn is null ORDER BY QuestionNumber", meyerdb)
        gvQuestions.DataSource = dsQuestions.Tables(0)
        gvQuestions.DataBind()
        GridView2.DataBind()
        GridView2.Enabled = True
        'If Profile.Job <> "Phone" And Profile.Job <> "Phone2" Then
        '    GridView2.Enabled = False
        'End If
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

            If User.IsInRole("OffSiteUser") = True Then
                e.Row.Cells.Item(0).Text = ""
            End If

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

End Class