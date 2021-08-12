Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class Individual
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim Qid As Integer



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Qid = Request.QueryString("id")

        If getString("SELECT ClosedOn FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) <> "" Then
            Response.Redirect("../Questions.aspx")
        End If
        If Not IsPostBack Then
            Profile.LastPage = "Forms/Individual.aspx?ID=" & Qid.ToString

            Dim ds1 As New DataSet
            ds1 = GetDS("SELECT [Question], [QuestionNumber], [PointValue], [UniqueID], [Hour], [CreatedOn], [SoundRecording] FROM [Question] WHERE ([UniqueID] = '" + Qid.ToString + "')", meyerdb)
            gvQuestions.DataSource = ds1
            gvQuestions.DataBind()
            mpControl.MovieURL = "http://audio.mmpof.com/" + ds1.Tables(0).Rows(0).Item("SoundRecording").ToString()

            Profile.ActiveQuestion = ds1.Tables(0).Rows(0).Item("QuestionNumber").ToString()

            Dim ds2 As New DataSet
            ds2 = GetDS("SELECT A.Answer, A.QuestionUniqID, A.IsCorrect, A.SureNess, A.SubmittedBy, A.SubmittedOn, A.AnswerUniqueID, A.IsPartial, A.IsSubmitted, Q.QuestionNumber, Q.Hour FROM Answer AS A INNER JOIN Question AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=0) ORDER BY A.SureNess, A.SubmittedOn", meyerdb)
            GridView2.DataSource = ds2
            GridView2.DataBind()

            Dim ds3 As New DataSet
            ds3 = GetDS("SELECT A.Answer, A.QuestionUniqID, A.IsCorrect, A.AnswerUniqueID, A.IsSubmitted, Q.QuestionNumber, Q.Hour FROM Answer AS A INNER JOIN Question AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=1) AND (A.IsCorrect=0) AND (A.IsPartial=0) ORDER BY A.Answer", meyerdb)
            GridView3.DataSource = ds3
            GridView3.DataBind()

            SqlDataSource1.SelectCommand = "SELECT A.Answer AS Answerr, A.SubmittedOn AS Submit, A.AnswerUniqueID AS AnswerUniqueID FROM [Answer] AS A INNER JOIN [Question] AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=1) AND (A.IsCorrect=0) AND (A.IsPartial=1) ORDER BY A.Sureness, A.SubmittedOn DESC"

        End If

    End Sub
    
    Protected Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DateDiff(DateInterval.Second, DataBinder.Eval(e.Row.DataItem, "Submit"), DateTime.Now) < 60 Then
                e.Row.ForeColor = System.Drawing.Color.Red
            End If
        End If
    End Sub


    
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Page.MaintainScrollPositionOnPostBack = True
        Dim ds1 As New DataSet
        ds1 = GetDS("SELECT [Question], [QuestionNumber], [PointValue], [UniqueID], [Hour], [CreatedOn], [SoundRecording] FROM [Question] WHERE ([UniqueID] = '" + Qid.ToString + "')", meyerdb)
        gvQuestions.DataSource = ds1
        gvQuestions.DataBind()
       
        Dim ds2 As New DataSet
        ds2 = GetDS("SELECT A.Answer, A.QuestionUniqID, A.IsCorrect, A.SureNess, A.SubmittedBy, A.SubmittedOn, A.AnswerUniqueID, A.IsPartial, A.IsSubmitted, Q.QuestionNumber, Q.Hour FROM Answer AS A INNER JOIN Question AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=0) ORDER BY A.SureNess, A.SubmittedOn", meyerdb)
        GridView2.DataSource = ds2
        GridView2.DataBind()

        Dim ds3 As New DataSet
        ds3 = GetDS("SELECT A.Answer, A.QuestionUniqID, A.IsCorrect, A.AnswerUniqueID, A.IsSubmitted, Q.QuestionNumber, Q.Hour FROM Answer AS A INNER JOIN Question AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=1) AND (A.IsCorrect=0) AND (A.IsPartial=0) ORDER BY A.Answer", meyerdb)
        GridView3.DataSource = ds3
        GridView3.DataBind()

        SqlDataSource1.SelectCommand = "SELECT A.Answer AS Answerr, A.SubmittedOn AS Submit, A.AnswerUniqueID AS AnswerUniqueID FROM [Answer] AS A INNER JOIN [Question] AS Q ON A.QuestionUniqID = Q.UniqueID WHERE ([UniqueID] = '" + Qid.ToString + "') AND (A.IsSubmitted=1) AND (A.IsCorrect=0) AND (A.IsPartial=1) ORDER BY A.Sureness, A.SubmittedOn DESC"

    End Sub

    Protected Sub gvQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuestions.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Attributes.Add("Title", "Edit")
            e.Row.Cells(3).Attributes.Add("Title", "Answer")
        End If

    End Sub
    Protected Sub gvQuestions_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvQuestions.RowCommand
        Select Case e.CommandName
            Case "EditQuestion"
                Response.Redirect("EditQuestion.aspx?ID=" + gvQuestions.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("UniqueID").ToString())
            Case "AnswerQuestion"
                Response.Redirect("SubmitAnswer.aspx?ID=" + gvQuestions.DataKeys(Convert.ToInt32(e.CommandArgument)).Values("UniqueID").ToString())
        End Select
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Qid = Request.QueryString("id")
        If RadioButtonList1.SelectedValue < 1 Or RadioButtonList1.SelectedValue > 4 Then
            RadioButtonList1.SelectedValue = 3
        End If
        If tbAnswer.Text.Length > 0 Then
            tbAnswer.Text = tbAnswer.Text.Replace("'", "''")
            tbAnswer.Text = tbAnswer.Text.Trim
            If (getString("SELECT Count(*) FROM [Answer] WHERE Answer = '" + tbAnswer.Text + "' AND QuestionUniqID = '" + Qid.ToString + "'", meyerdb) = "0") Or (LabelAlreadySubmit.Text = "Already submitted") Then
                If RadioButtonList1.SelectedValue < 4 And RadioButtonList1.SelectedValue > 0 Then
                    execSQL("INSERT [Answer] (Answer, SureNess, SubmittedOn, SubmittedBy, QuestionUniqID, IsCorrect, IsPartial, IsSubmitted) Values ('" + tbAnswer.Text + "', '" + RadioButtonList1.SelectedValue.ToString + "', '" + DateTime.Now.ToString + "', '" + Membership.GetUser.UserName + "', '" + Qid.ToString + "', 0, 0, 0)", meyerdb)
                Else
                    Dim Append As String = tbAnswer.Text & " - " & Membership.GetUser.UserName
                    execSQL("INSERT [Answer] (Answer, SureNess, SubmittedOn, SubmittedBy, QuestionUniqID, IsCorrect, IsPartial, IsSubmitted) Values ('" + Append + "', '" + RadioButtonList1.SelectedValue.ToString + "', '" + DateTime.Now.ToString + "', '" + Membership.GetUser.UserName + "', '" + Qid.ToString + "', 0, 1, 1)", meyerdb)
                End If
                LabelAlreadySubmit.Text = ""
            Else
                LabelAlreadySubmit.Text = "Already submitted"
                Return
            End If
        Else
            RadioButtonList1.SelectedIndex = Nothing
            LabelAlreadySubmit.Text = ""
            Return
        End If
        Response.Redirect("~/Protected/" & Profile.LastPage)
    End Sub

End Class
