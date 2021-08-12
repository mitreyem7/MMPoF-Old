Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DataAccess

Partial Class EditQuestion
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim Qid As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Qid = Request.QueryString("id")
        If Not IsPostBack Then
            'If getString("SELECT COUNT(*) FROM [User] WHERE UserName = '" + Membership.GetUser.UserName + "' AND IsDeleted = 0", meyerdb) = "0" Then
            'Response.Redirect("../Login.aspx")
            'End If
            If getString("SELECT CreatedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "" Then
                Label4.Text = ""
                execSQL("UPDATE [Question] Set CreatedBy = '" + Membership.GetUser.UserName + "' WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                Label4.Text = "Question currently being edited by " & getString("SELECT CreatedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
                table1.Visible = "false"
            End If
            Profile.Hour = getString("SELECT Hour FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Label3.Text = CInt(getString("SELECT QuestionNumber FROM [Question] WHERE UniqueID = '" + Qid.ToString + "'", meyerdb))
            LabelQuestion.Text = getString("SELECT Question FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            LabelPointValue.Text = getString("SELECT PointValue FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            LabelAnswer.Text = getString("SELECT CorrectAnswer FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            LabelClosedBy.Text = getString("SELECT ClosedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            LabelPhonePerson.Text = getString("SELECT PhonePerson FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            HourBox.Text = Profile.Hour
            QuestionBox.Text = LabelQuestion.Text
            PointValueBox.Text = LabelPointValue.Text
            tbCorrectAnswer.Text = LabelAnswer.Text
            DropDownList1.Items.Add(New ListItem(" ", ""))
            DropDownList1.SelectedValue = LabelClosedBy.Text.ToLower
            TextBoxPhone.Text = LabelPhonePerson.Text
            CheckBoxClosed.Checked = True
            If getString("SELECT ClosedOn FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "" Then
                CheckBoxClosed.Checked = False
            End If
            If getString("SELECT IsCorrect FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "1" Then
                CheckBoxCorrect.Checked = True
            End If
            If getString("SELECT ClosedOn FROM Question WHERE UniqueID = '" + Qid.ToString + "'", meyerdb).Length > 0 Then
                LabelClosed.Text = "Closed"
                If getString("SELECT IsCorrect FROM Question WHERE UniqueID = '" + Qid.ToString + "'", meyerdb) = "1" Then
                    LabelCorrect.Text = "Correct"
                Else
                    LabelCorrect.Text = "Wrong"
                End If
            Else
                LabelClosed.Text = "Open"
            End If
        End If
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Qid = CInt(Request.QueryString("id"))
        If Label4.Text = "" Then

        Else
            execSQL("UPDATE [Question] Set CreatedBy = '' WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Response.Redirect("EditQuestion.aspx?id=" & Qid.ToString)
        End If
        If PointValueBox.Text.Length > 0 Then
            Try
                If (CInt(PointValueBox.Text.Trim) < 0 Or CInt(PointValueBox.Text.Trim) > 500) Then
                    Label2.Text = "Integer Between 0 and 500"
                    Return
                End If
            Catch ex As Exception
                Label2.Text = "Integer Between 0 and 500"
                Return
            End Try
        End If
        If QuestionBox.Text.Trim = "" Then
            QuestionBox.Text = "blank question"
        End If
        QuestionBox.Text = QuestionBox.Text.Replace("''", "'")
        QuestionBox.Text = QuestionBox.Text.Replace("'", "''")
        tbCorrectAnswer.Text = tbCorrectAnswer.Text.Replace("''", "'")
        tbCorrectAnswer.Text = tbCorrectAnswer.Text.Replace("'", "''")
        TextBoxPhone.Text = TextBoxPhone.Text.Replace("''", "'")
        TextBoxPhone.Text = TextBoxPhone.Text.Replace("'", "''")
        If tbCorrectAnswer.Text Is Nothing Then
            tbCorrectAnswer.Text = ""
        End If
        Label2.Text = ""
        If (LabelQuestion.Text = getString("SELECT Question FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) And LabelPointValue.Text = getString("SELECT PointValue FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) And LabelAnswer.Text = getString("SELECT CorrectAnswer FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) And LabelClosedBy.Text = getString("SELECT ClosedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) And LabelPhonePerson.Text = getString("SELECT PhonePerson FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)) Then
            execSQL("UPDATE [Question] SET Question = '" + QuestionBox.Text.Trim + "', PointValue = '" + PointValueBox.Text.Trim + "', PhonePerson = '" + TextBoxPhone.Text.Trim + "', ClosedBy = '" + DropDownList1.SelectedValue.ToString + "', CorrectAnswer = '" + tbCorrectAnswer.Text.Trim + "', CreatedBy = '', CreatedOn = '' WHERE  UniqueID='" + Qid.ToString + "'", meyerdb)
            If CheckBoxClosed.Checked = True Then
                If getString("SELECT ClosedOn FROM Question WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "" Then
                    execSQL("UPDATE Question SET ClosedOn = '" + DateTime.Now.ToString + "' WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
                End If
            Else
                execSQL("UPDATE [Question] SET ClosedOn = Null WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
            End If
            If CheckBoxCorrect.Checked = True Then
                execSQL("UPDATE [Question] SET IsCorrect = 1 WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
                If getString("SELECT ClosedOn FROM Question WHERE UniqueID = '" + Qid.ToString + "'", meyerdb) = "" Then
                    execSQL("UPDATE [Question] SET ClosedOn = '" + DateTime.Now.ToString + "' WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
                End If
            Else
                execSQL("UPDATE [Question] SET IsCorrect = 0 WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
            End If
            'If FileUpload1.HasFile Then
            '    Dim DataPath As String = "/SoundClips/Audio_" + HourBox.Text + "_" + Label3.Text + Path.GetExtension(FileUpload1.FileName)
            '    Dim appPath As String = Server.MapPath("../") + DataPath
            '    FileUpload1.SaveAs(appPath)
            '    execSQL("UPDATE [Question] SET SoundRecording = '" + DataPath + "' WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
            'End If
            Response.Redirect("../" & Profile.LastPage)
        Else
            Label10.Visible = True
            If LabelQuestion.Text <> getString("SELECT Question FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) Then
                LabelQuestion.ForeColor = Drawing.Color.Red
                LabelQuestion.Text = getString("SELECT Question FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                LabelQuestion.ForeColor = Drawing.Color.LimeGreen
            End If
            If LabelPointValue.Text <> getString("SELECT PointValue FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) Then
                LabelPointValue.ForeColor = Drawing.Color.Red
                LabelPointValue.Text = getString("SELECT PointValue FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                LabelPointValue.ForeColor = Drawing.Color.LimeGreen
            End If
            If LabelAnswer.Text <> getString("SELECT CorrectAnswer FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) Then
                LabelAnswer.ForeColor = Drawing.Color.Red
                LabelAnswer.Text = getString("SELECT CorrectAnswer FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                LabelAnswer.ForeColor = Drawing.Color.LimeGreen
            End If
            If LabelClosedBy.Text <> getString("SELECT ClosedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) Then
                LabelClosedBy.ForeColor = Drawing.Color.Red
                LabelClosedBy.Text = getString("SELECT ClosedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                LabelClosedBy.ForeColor = Drawing.Color.LimeGreen
            End If
            If LabelPhonePerson.Text <> getString("SELECT PhonePerson FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) Then
                LabelPhonePerson.ForeColor = Drawing.Color.Red
                LabelPhonePerson.Text = getString("SELECT PhonePerson FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            Else
                LabelPhonePerson.ForeColor = Drawing.Color.LimeGreen
            End If
            If getString("SELECT ClosedOn FROM Question WHERE UniqueID = '" + Qid.ToString + "'", meyerdb).Length > 0 Then
                LabelClosed.Text = "Closed"
                If getString("SELECT IsCorrect FROM Question WHERE UniqueID = '" + Qid.ToString + "'", meyerdb) = "1" Then
                    LabelCorrect.Text = "Correct"
                Else
                    LabelCorrect.Text = "Wrong"
                End If
            Else
                LabelClosed.Text = "Open"
            End If
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Qid = CInt(Request.QueryString("id"))
        If getString("SELECT CreatedBy FROM [Question] WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = Membership.GetUser.UserName Then
            execSQL("UPDATE [Question] Set CreatedBy = '', CreatedOn = '' WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
        End If
        Response.Redirect("../" & Profile.LastPage)
    End Sub

End Class
