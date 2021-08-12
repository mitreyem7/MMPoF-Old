Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class Phone
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim Answerid, Questionid As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Response.Write("<script language=""JavaScript"">window.opener='x';</script>")
            Button2.Attributes.Add("onClick", "window.open('','_parent','');window.close();")
            Answerid = Request.QueryString("id")
            Questionid = CInt(getString("SELECT QuestionUniqID FROM [Answer] WHERE AnswerUniqueID='" + Answerid.ToString + "'", meyerdb))
            tbPointValue.Text = getString("SELECT PointValue FROM [Question] WHERE UniqueID='" + Questionid.ToString + "'", meyerdb)
            tbAnswer.Text = getString("SELECT Answer FROM [Answer] WHERE AnswerUniqueID='" + Answerid.ToString + "'", meyerdb)
            LabelHour.Text = "Hour: " & getString("SELECT Hour FROM [Question] WHERE UniqueID='" + Questionid.ToString + "'", meyerdb)
            LabelQuestionNumber.Text = "Question # " & getString("SELECT QuestionNumber FROM [Question] WHERE UniqueID='" + Questionid.ToString + "'", meyerdb)
            Try
                DropDownList1.SelectedValue = getString("SELECT SubmittedBy FROM [Answer] WHERE AnswerUniqueID = '" + Answerid.ToString + "'", meyerdb).ToLower
            Catch ex As Exception
                DropDownList1.SelectedValue = "team"
            End Try
        End If
    End Sub

   

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Answerid = Request.QueryString("id")
        Questionid = CInt(getString("SELECT QuestionUniqID FROM [Answer] WHERE AnswerUniqueID='" + Answerid.ToString + "'", meyerdb))
        If tbPointValue.Text.Length > 0 Then
            Try
                If (CInt(tbPointValue.Text.Trim) < 0 Or CInt(tbPointValue.Text.Trim) > 500) Then
                    Label2.Text = "Integer Between 0 and 500"
                    Return
                End If
            Catch ex As Exception
                Label2.Text = "Integer Between 0 and 500"
                Return
            End Try
        End If
        tbAnswer.Text = tbAnswer.Text.Replace("'", "''")
        tbPhonePerson.Text = tbPhonePerson.Text.Replace("'", "''")
        execSQL("UPDATE [Answer] SET IsCorrect = 1, IsSubmitted = 1 WHERE AnswerUniqueID='" + Answerid.ToString + "'", meyerdb)
        execSQL("UPDATE [Question] SET CorrectAnswer = '" + tbAnswer.Text.Trim + "', IsCorrect = 1, PointValue = '" + tbPointValue.Text.Trim + "', PhonePerson = '" + tbPhonePerson.Text.Trim + "', ClosedOn = '" + DateTime.Now.ToString + "', ClosedBy = '" + DropDownList1.SelectedValue.ToString + "' WHERE UniqueID = '" + Questionid.ToString + "'", meyerdb)
        Response.Write("<script language=""JavaScript"">window.opener.location.reload();</script>")
        Response.Write("<script language=""JavaScript"">window.open('','_parent','');</script>")
        Response.Write("<script language=""JavaScript"">window.close();</script>")
    End Sub

End Class
