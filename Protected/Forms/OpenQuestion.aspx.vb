Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DataAccess


Partial Class OpenQuestion
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim Qid As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Qid = Request.QueryString("ID")
        If Not IsPostBack Then
            'If getString("SELECT COUNT(*) FROM [User] WHERE UserName = '" + Membership.GetUser.UserName + "' AND IsDeleted = 0", meyerdb) = "0" Then
            'Response.Redirect("../Login.aspx")
            'End If
            If getString("SELECT IsOpen FROM Question WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "1" Then
                Response.Redirect("../Questions.aspx")
            Else
                execSQL("UPDATE [Question] SET Question = 'Opening by " + Membership.GetUser.UserName + "', CreatedOn = '" + DateTime.Now.ToString + "', CreatedBy = '" + Membership.GetUser.UserName + "', IsOpen=1, PointValue=0 WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            End If
            QuestionNumberBox.Text = getString("SELECT [QuestionNumber] FROM Question WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
            HourBox.Text = getString("SELECT [Hour] FROM Question WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Qid = Request.QueryString("ID")
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
        Else
            PointValueBox.Text = "0"
        End If
        If QuestionBox.Text.Trim = "" Then
            QuestionBox.Text = "blank question"
        End If
        QuestionBox.Text = QuestionBox.Text.Replace("'", "''")
        execSQL("UPDATE Question SET Question = '" + QuestionBox.Text.Trim + "', PointValue = '" + PointValueBox.Text.Trim + "', CreatedOn = '', CreatedBy = '' WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
        'If FileUpload1.HasFile Then
        '    Dim DataPath As String = "/SoundClips/Audio_" + HourBox.Text + "_" + QuestionNumberBox.Text + Path.GetExtension(FileUpload1.FileName)
        '    Dim appPath As String = Server.MapPath("../") + DataPath
        '    FileUpload1.SaveAs(appPath)
        '    execSQL("UPDATE [Question] SET SoundRecording = '" + DataPath + "' WHERE UniqueID = '" + Qid.ToString + "'", meyerdb)
        'End If

        Response.Redirect("../Questions.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Qid = Request.QueryString("ID")
        If getString("SELECT Question FROM Question WHERE UniqueID='" + Qid.ToString + "'", meyerdb) = "Opening by " & Membership.GetUser.UserName Then
            execSQL("UPDATE Question SET Question = 'ABORTED', PointValue='0', CreatedOn = '', CreatedBy = '' WHERE UniqueID='" + Qid.ToString + "'", meyerdb)
        End If
        Response.Redirect("../Questions.aspx")
    End Sub
    
End Class
