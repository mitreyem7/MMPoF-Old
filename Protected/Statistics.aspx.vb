Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class Statistics
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    Dim OpenHour As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Profile.ActiveQuestion = 0
            Profile.LastPage = "Statistics.aspx"
            Dim IndivOkay As Integer
            Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
            Dim currenttime As DateTime = DateTime.Now
            IndivOkay = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
            If IndivOkay < 1 Then
                IndivOkay = 1
            End If
            If IndivOkay > 50 Then
                IndivOkay = 50
            End If
            Dim dsStats As New DataSet
            dsStats = GetDS("select [Hour],(select sum(pointvalue) from question where [hour]=q.[hour] and iscorrect=1) as [Points Earned], sum(pointvalue) as [Total Points for Hour], (select sum(pointvalue) from question where [hour]=q.[hour] and iscorrect=1)*100/sum(pointvalue) as [Percent for Hour], (select sum(pointvalue) from question where [hour]<=q.[hour] and iscorrect=1) as [Total Points Earned] from question Q WHERE [Hour] <= '" + IndivOkay.ToString + "' group by [hour] order by [hour] DESC", meyerdb)
            dgStats.DataSource = dsStats.Tables(0)
            dgStats.DataBind()
            Dim dsTotal As New DataSet
            dsTotal = GetDS("select (select sum(pointvalue) from question where iscorrect=1) as [Total Points Earned], sum(pointvalue) as [Total Points], (select sum(pointvalue) from question where iscorrect=1)*100/sum(pointvalue) as [Percent Correct] from question", meyerdb)
            GridView3.DataSource = dsTotal.Tables(0)
            GridView3.DataBind()
            Dim dsUsers As New DataSet
            dsUsers = GetDS("SELECT U.[UserName],SUM(Q.PointValue) as Points FROM [aspnet_Users] U inner join [Question] Q on U.[UserName]=Q.[ClosedBy] where IsCorrect = 1 group by U.UserName order by Points desc", meyerdb)
            GridView2.DataSource = dsUsers.Tables(0)
            GridView2.DataBind()
            Dim dsMembers As New DataTable()
            dsMembers.Columns.Add(New DataColumn("Username", GetType(String)))
            dsMembers.Columns.Add(New DataColumn("Name", GetType(String)))
            dsMembers.Columns.Add(New DataColumn("Email", GetType(String)))
            dsMembers.Columns.Add(New DataColumn("Phone", GetType(String)))
            dsMembers.Columns.Add(New DataColumn("LastActivity", GetType(DateTime)))
            Dim users As System.Web.Security.MembershipUserCollection
            users = Membership.GetAllUsers()
            For Each singleuser As System.Web.Security.MembershipUser In users
                Dim singleprofile As ProfileCommon = Profile.GetProfile(singleuser.UserName)
                Dim dr As DataRow = dsMembers.NewRow()
                dr("Username") = singleuser.UserName
                dr("Name") = singleprofile.Name
                dr("Email") = singleuser.Email
                dr("Phone") = singleprofile.Phone
                dr("LastActivity") = singleuser.LastActivityDate
                dsMembers.Rows.Add(dr)
            Next
            GridView1.DataSource = dsMembers
            GridView1.DataBind()
            Dim dsLogged As New DataSet
            dsLogged = GetDS("SELECT [HowMany], [Hour] FROM [LoggedIn] WHERE [Year] = '2009' AND [Hour] <= '" + IndivOkay.ToString + "' ORDER BY [Hour] DESC", meyerdb)
            GridView4.DataSource = dsLogged.Tables(0)
            GridView4.DataBind()
            If IndivOkay > 49 Then
                GridView2.Visible = True
            End If
            Button4.Visible = User.IsInRole("Owner")
        End If
    End Sub


    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Label1.Text <> "ARE YOU SURE???" Then
            Label1.Text = "ARE YOU SURE???"
        Else
            If execSQL("DELETE FROM Question", meyerdb) And execSQL("Delete from LoggedIn", meyerdb) And execSQL("Delete from Answer", meyerdb) Then
                Label1.Text = "Success"
            Else
                Label1.Text = "Failure"
            End If
        End If
    End Sub
    
   
End Class
