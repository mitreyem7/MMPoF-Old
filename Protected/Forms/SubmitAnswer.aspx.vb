Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class SubmitAnswer
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim Questionid As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Questionid = Request.QueryString("id")
            lbQuestion.Text = getString("SELECT Question FROM [Question] WHERE UniqueID='" + Questionid.ToString + "'", meyerdb)
        End If
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If tbAnswer.Text.Length > 0 Then
            Questionid = Request.QueryString("id")
            tbAnswer.Text = tbAnswer.Text.Replace("'", "''")
            If (getString("SELECT Count(*) FROM [Answer] WHERE Answer = '" + tbAnswer.Text.Trim + "' AND QuestionUniqID = '" + Questionid.ToString + "' AND IsSubmitted = 1", meyerdb) = "0") Then
                If RadioButtonList1.SelectedValue < 4 Then
                    execSQL("INSERT [Answer] (Answer, SureNess, SubmittedOn, SubmittedBy, QuestionUniqID, IsCorrect, IsPartial, IsSubmitted) Values ('" + tbAnswer.Text.Trim + "', '" + RadioButtonList1.SelectedValue.ToString + "', '" + DateTime.Now.ToString + "', '" + Membership.GetUser.UserName + "', '" + Questionid.ToString + "', 0, 0, 0)", meyerdb)
                Else
                    Dim Append As String = tbAnswer.Text.Trim & " - " & Membership.GetUser.UserName
                    execSQL("INSERT [Answer] (Answer, SureNess, SubmittedOn, SubmittedBy, QuestionUniqID, IsCorrect, IsPartial, IsSubmitted) Values ('" + Append + "', '" + RadioButtonList1.SelectedValue.ToString + "', '" + DateTime.Now.ToString + "', '" + Membership.GetUser.UserName + "', '" + Questionid.ToString + "', 0, 1, 1)", meyerdb)
                End If
            Else
                tbAnswer.Text = "Already submitted"
                tbAnswer.ForeColor = Drawing.Color.Red
                Return
            End If
        Else
            RadioButtonList1.SelectedIndex = Nothing
            Return
        End If
        Response.Redirect("~/Protected/" & Profile.LastPage)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("~/Protected/" & Profile.LastPage)
    End Sub
End Class
