Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)


    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Label5.Text = ""
    '    If tbUserName.Text.Length > 0 Then
    '        If tbPassword.Text.Length > 0 Then
    '            If getString("Select count(*) from [user] where password='" + tbPassword.Text.Trim + "' and username='" + tbUserName.Text.Trim + "'", meyerdb) = "1" Then
    '                If String.Compare(tbUserName.Text.Trim, getString("Select UserName from [user] where password='" + tbPassword.Text.Trim + "' and username='" + tbUserName.Text.Trim + "'", meyerdb), False) = 0 Then
    '                    Membership.GetUser.UserName = tbUserName.Text.Trim
    '                    Dim Permission As Integer
    '                    Dim strIPAddress As String = Request.ServerVariables("REMOTE_ADDR")
    '                    If ip2num(strIPAddress) = ip2num("96.24.84.152") Or ip2num(strIPAddress) = ip2num("96.24.90.58") Then
    '                        Permission = 1
    '                    Else
    '                        Permission = 0
    '                    End If
    '                    If tbUserName.Text.Trim = "mitreyem" Or tbUserName.Text.Trim = "JoE" Then
    '                        Permission = 2
    '                    End If
    '                    execSQL("UPDATE [User] SET IsDeleted = 0, Permission = '" + Permission.ToString + "', Role = '' WHERE UserName = '" + Membership.GetUser.UserName + "'", meyerdb)
    '                    Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
    '                    Dim currenttime As DateTime = DateTime.Now
    '                    Profile.Hour = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
    '                    If Profile.Hour < 1 Or Profile.Hour > 50 Then
    '                        Profile.Hour = 1
    '                    End If
    '                    Response.Redirect("Statistics.aspx")
    '                Else
    '                    Label3.Text = "Not Recognized"
    '                End If
    '                Else
    '                    Label3.Text = "Invalid Login"
    '                End If
    '        Else
    '            Label3.Text = "Please enter a password"
    '        End If
    '    Else
    '        Label3.Text = "Please enter a username"
    '    End If



    'End Sub

    Public Function ip2num(ByVal ip As String) As Integer
        Try
            Dim i, N As Integer
            Dim a As String()
            a = Split(ip, ".")
            N = CDbl(0)
            For i = 0 To 3
                N = N * 256 + CInt(a(i))
            Next
            ip2num = N
            Return ip2num
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        'Old School
        'Label3.Text = ""
        'If tbNewName.Text.Length > 0 Then
        '    If getString("Select count(*) from [user] where UserName='" + tbNewName.Text.Trim + "'", meyerdb) = "0" Then
        '        If getString("Select count(*) from [user] where password='" + tbTeam.Text.Trim + "' and username='Team'", meyerdb) = "1" Then
        '            If tbNewPassword.Text.Length > 0 Then
        '                If tbNewPassword.Text.Trim = tbNewPassword2.Text.Trim Then
        '                    Membership.GetUser.UserName = tbNewName.Text.Trim
        '                    Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
        '                    Dim currenttime As DateTime = DateTime.Now
        '                    Profile.Hour = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
        '                    If Profile.Hour < 1 Or Profile.Hour > 50 Then
        '                        Profile.Hour = 1
        '                    End If
        '                    execSQL("INSERT [User] (UserName, Password, Created, IsDeleted, Permission, email, phone, location, role) Values ('" + tbNewName.Text.Trim + "', '" + tbNewPassword.Text.Trim + "', '" + DateTime.Now.ToString + "', 0, 1, '" + tbEmail.Text + "', '" + tbPhone.Text + "', '" + tbName.Text + "', '')", meyerdb)
        '                    Response.Redirect("Statistics.aspx")
        '                Else
        '                    Label5.Text = "Passwords do not match"
        '                End If
        '            Else
        '                Label5.Text = "Please enter a password"
        '            End If
        '        Else
        '            Label5.Text = "Call for password"
        '        End If
        '    Else
        '        Label5.Text = "Username already taken"
        '    End If
        'Else
        '    Label5.Text = "Please enter a username"
        'End If




        If tbNewName.Text.Length > 2 Then
            If Membership.GetUser(tbNewName.Text.Trim) Is Nothing Then
                If tbTeam.Text.Trim = ConfigurationManager.AppSettings("TeamPassword") Then
                    If tbNewPassword.Text.Trim.Length >= Membership.MinRequiredPasswordLength Then
                        If tbName.Text.Trim.Length > 7 Then
                            If tbNewPassword.Text.Trim = tbNewPassword2.Text.Trim Then
                                Dim newUser As System.Web.Security.MembershipUser = Membership.CreateUser(tbNewName.Text.Trim, tbNewPassword.Text.Trim, tbEmail.Text.Trim)
                                Membership.ValidateUser(tbNewName.Text.Trim, tbNewPassword.Text.Trim)
                                Roles.AddUserToRole(tbNewName.Text.Trim, "OffSiteUser")
                                Dim strIPAddress As String = Request.ServerVariables("REMOTE_ADDR")
                                If ip2num(strIPAddress) = ip2num("96.24.84.152") Or ip2num(strIPAddress) = ip2num("96.24.90.58") Or ip2num(strIPAddress) = ip2num("96.24.111.202") Or ip2num(strIPAddress) = ip2num("96.24.103.235") Then
                                    Roles.RemoveUserFromRole(tbNewName.Text.Trim, "OffSiteUser")
                                    Roles.AddUserToRole(tbNewName.Text.Trim, "OnSiteUser")
                                End If
                                Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
                                Dim currenttime As DateTime = DateTime.Now
                                Dim newUserProfile As ProfileCommon
                                newUserProfile = Profile.GetProfile(tbNewName.Text.Trim)
                                If DateDiff(DateInterval.Hour, oldtime, currenttime) + 1 < 1 Or DateDiff(DateInterval.Hour, oldtime, currenttime) + 1 > 50 Then
                                    newUserProfile.Hour = 1
                                Else
                                    newUserProfile.Hour = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
                                End If
                                newUserProfile.Phone = tbPhone.Text.Trim
                                newUserProfile.Name = tbName.Text.Trim
                                newUserProfile.Job = ""
                                newUserProfile.Save()
                                FormsAuthentication.SetAuthCookie(tbNewName.Text.Trim, True)
                                FormsAuthentication.RedirectFromLoginPage(tbNewName.Text.Trim, True)
                            Else
                                Label5.Text = "Passwords do not match"
                            End If
                        Else
                            Label5.Text = "Last name?"
                        End If
                    Else
                        Label5.Text = "Please enter a password of at least 6 characters"
                    End If
                Else
                    Label5.Text = "Call 320-309-9171 for password"
                End If
            Else
                Label5.Text = "Username already taken"
            End If
        Else
            Label5.Text = "Please enter a username"
        End If
        tbNewPassword.Focus()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Login1.Focus()
    End Sub


    Protected Sub Login1_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles Login1.LoggedIn
        If Not Roles.IsUserInRole(Login1.UserName.Trim, "Owner") Then
            If Not Roles.IsUserInRole(Login1.UserName.Trim, "OffSiteUser") Then
                Roles.AddUserToRole(Login1.UserName.Trim, "OffSiteUser")
            End If
            Dim strIPAddress As String = Request.ServerVariables("REMOTE_ADDR")
            If ip2num(strIPAddress) = ip2num("96.24.84.152") Or ip2num(strIPAddress) = ip2num("64.83.226.153") Or ip2num(strIPAddress) = ip2num("96.24.111.202") Or ip2num(strIPAddress) = ip2num("96.24.103.235") Then
                If Not Roles.IsUserInRole(Login1.UserName.Trim, "OnSiteUser") Then
                    Roles.AddUserToRole(Login1.UserName.Trim, "OnSiteUser")
                End If
                Roles.RemoveUserFromRole(Login1.UserName.Trim, "OffSiteUser")
            End If
        End If
        Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
        Dim currenttime As DateTime = DateTime.Now
        Dim newUserProfile As ProfileCommon
        newUserProfile = Profile.GetProfile(Login1.UserName.Trim)
        If DateDiff(DateInterval.Hour, oldtime, currenttime) + 1 < 1 Or DateDiff(DateInterval.Hour, oldtime, currenttime) + 1 > 50 Then
            newUserProfile.Hour = 1
        Else
            newUserProfile.Hour = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
        End If
        newUserProfile.Job = ""
        newUserProfile.ActiveQuestion = 0
        newUserProfile.Save()
    End Sub
End Class
