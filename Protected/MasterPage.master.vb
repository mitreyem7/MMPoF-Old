Imports System.Data
Imports DataAccess
Imports System.Data.SqlClient


Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)

    Structure UserInfo
        Dim Count As Integer
        Dim UserList As String
    End Structure
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.MaintainScrollPositionOnPostBack = True
        RoleDisplay()
        LabelUser.Text = Membership.GetUser.UserName
        If Membership.GetUser.IsOnline() = False Then
            Profile.Job = ""
            Profile.ActiveQuestion = 0
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack Then
            Membership.GetUser()
            'delete jobs and activequestion for offline users
        End If
        SetHyperLinks()
        CountMembers()
        

        ' End If
    End Sub
    Function RoleDisplay() As Boolean


        'Dim users As System.Web.Security.MembershipUserCollection
        'users = Membership.GetAllUsers()

        'Dim strResearcher As String = ""
        'Dim intResearcher As Integer
        'Dim bolFoundRecord1 As Boolean = False
        'Dim bolFoundRecord2 As Boolean = False
        'Dim bolFoundAudio1 As Boolean = False
        'Dim bolFoundPhone1 As Boolean = False
        'Dim bolFoundPhone2 As Boolean = False

        'For Each singleuser As System.Web.Security.MembershipUser In users
        '    If singleuser.IsOnline = True Then
        '        Dim singleprofile As ProfileCommon = Profile.GetProfile(singleuser.UserName)
        '        If singleprofile.Job = "Record" Then
        '            btnRecorderOne.Text = "Rec #1 - " + singleuser.UserName
        '            bolFoundRecord1 = True
        '        End If
        '        If singleprofile.Job = "Record2" Then
        '            btnRecorderTwo.Text = "Rec #2 - " + singleuser.UserName
        '            bolFoundRecord2 = True
        '        End If
        '        If singleprofile.Job = "Phone" Then
        '            btnCallerOne.Text = "Caller #1 - " + singleuser.UserName
        '            bolFoundPhone1 = True
        '        End If
        '        If singleprofile.Job = "Phone2" Then
        '            btnCallerTwo.Text = "Caller #2 - " + singleuser.UserName
        '            bolFoundPhone2 = True
        '        End If
        '        If singleprofile.Job = "" Then
        '            intResearcher += 1
        '            strResearcher += singleuser.UserName + vbCrLf
        '        End If
        '    End If
        'Next

        'If intResearcher > 0 Then
        '    If intResearcher = 1 Then
        '        btnResearcher.Text = "Researcher - " + strResearcher.Replace(vbCrLf, "")
        '    Else
        '        btnResearcher.Text = "Researchers - " + intResearcher.ToString
        '        btnResearcher.ToolTip = strResearcher
        '    End If
        'Else
        '    btnResearcher.Text = "Reseacher - None!"
        'End If

        'If Not bolFoundRecord1 Then
        '    btnRecorderOne.BackColor = Drawing.Color.Red
        '    btnRecorderOne.Text = "Record #1"
        'Else
        '    btnRecorderOne.BackColor = Drawing.Color.LightGray
        'End If

        'If Not bolFoundRecord2 Then
        '    btnRecorderTwo.BackColor = Drawing.Color.Red
        '    btnRecorderTwo.Text = "Record #2"
        'Else
        '    btnRecorderTwo.BackColor = Drawing.Color.LightGray
        'End If

        'If Not bolFoundPhone1 Then
        '    btnCallerOne.BackColor = Drawing.Color.Red
        '    btnCallerOne.Text = "Caller #1"
        'Else
        '    btnCallerOne.BackColor = Drawing.Color.LightGray
        'End If

        'If Not bolFoundPhone2 Then
        '    btnCallerTwo.BackColor = Drawing.Color.Red
        '    btnCallerTwo.Text = "Caller #2"
        'Else
        '    btnCallerTwo.BackColor = Drawing.Color.LightGray
        'End If

        'If My.User.IsInRole("OffSiteUser") = True Then
        '    btnCallerOne.Enabled = False
        '    btnCallerTwo.Enabled = False
        '    btnRecorderOne.Enabled = False
        '    btnRecorderTwo.Enabled = False
        'End If
        Return True
    End Function

    Function CountMembers() As Boolean
        Dim ContestHour As Integer
        Dim LoggedIn As Integer = 0
        Dim oldtime As DateTime = ConfigurationManager.AppSettings("StartTime")
        Dim currenttime As DateTime = DateTime.Now
        ContestHour = DateDiff(DateInterval.Hour, oldtime, currenttime) + 1
        Dim users As System.Web.Security.MembershipUserCollection
        users = Membership.GetAllUsers()
        Try
            If DateTime.Now.Minute > 30 And ContestHour > 0 And ContestHour < 51 Then
                If getString("select count(Hour) from LoggedIn WHERE [Hour] = " + ContestHour.ToString + " and Year = 2009", meyerdb) = "0" Then

                    For Each singleuser As System.Web.Security.MembershipUser In users
                        If DateDiff(DateInterval.Minute, singleuser.LastActivityDate, currenttime) < 20 Then
                            LoggedIn += 1
                        End If

                        Dim singleprofile As ProfileCommon = Profile.GetProfile(singleuser.UserName)
                        If singleprofile.Job = "" And singleuser.IsOnline = True Then
                            singleprofile.HoursResearching += 1
                        End If
                    Next
                    'execSQL("INSERT [LoggedIn] (HowMany, [Hour], Year) Values ('" + LoggedIn.ToString + "', '" + ContestHour.ToString + "', 2009)", meyerdb)
                End If
            End If
        Catch ex As Exception

        End Try

        For Each singleuser As System.Web.Security.MembershipUser In users
            If singleuser.IsOnline = False Then
                Dim singleprofile2 As ProfileCommon = Profile.GetProfile(singleuser.UserName)
                singleprofile2.Job = ""
                singleprofile2.ActiveQuestion = 0
            End If
        Next
    End Function


    

    Protected Sub LoginStatus1_LoggingOut(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles LoginStatus1.LoggingOut
        Profile.Job = ""
        Profile.ActiveQuestion = 0
        Profile.Save()
    End Sub

   
    Function SetJob(ByVal JobName As String) As Boolean

        Dim users As System.Web.Security.MembershipUserCollection
        users = Membership.GetAllUsers()
        If JobName <> "" Then
            For Each singleuser As System.Web.Security.MembershipUser In users
                Dim singleprofile As ProfileCommon = Profile.GetProfile(singleuser.UserName)
                If singleprofile.Job = JobName Then
                    singleprofile.Job = ""
                End If
            Next
        End If
       

        Profile.Job = JobName
        Profile.Save()
        RoleDisplay()
    End Function

    Function SetHyperLinks() As Boolean
        Dim dtQuestions As DataTable = GetDS("Select * from question where CorrectAnswer = '' order by Hour,questionnumber asc", meyerdb).Tables(0)
        hlQuestion1.Visible = False
        hlQuestion2.Visible = False
        hlQuestion3.Visible = False
        hlQuestion4.Visible = False
        hlQuestion5.Visible = False
        hlQuestion6.Visible = False

        hlQuestion1.BackColor = Drawing.Color.Silver
        hlQuestion2.BackColor = Drawing.Color.Silver
        hlQuestion3.BackColor = Drawing.Color.Silver
        hlQuestion4.BackColor = Drawing.Color.Silver
        hlQuestion5.BackColor = Drawing.Color.Silver
        hlQuestion6.BackColor = Drawing.Color.Silver


        If dtQuestions.Rows.Count > 0 Then
            Dim uInfo As UserInfo = ActiveUserCount(dtQuestions.Rows(0).Item("QuestionNumber"))
            hlQuestion1.Text = "Q" + dtQuestions.Rows(0).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
            hlQuestion1.ToolTip = uInfo.UserList
            hlQuestion1.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(0).Item("UniqueID").ToString
            hlQuestion1.Visible = True
            If uInfo.Count < 3 Then
                hlQuestion1.BackColor = Drawing.Color.Red
                hlQuestion1.BorderColor = Drawing.Color.Red
            End If

            If dtQuestions.Rows.Count > 1 Then
                uInfo = ActiveUserCount(dtQuestions.Rows(1).Item("QuestionNumber"))
                hlQuestion2.Text = "Q" + dtQuestions.Rows(1).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
                hlQuestion2.ToolTip = uInfo.UserList
                hlQuestion2.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(1).Item("UniqueID").ToString
                hlQuestion2.Visible = True
                If uInfo.Count < 3 Then
                    hlQuestion2.BackColor = Drawing.Color.Red
                    hlQuestion2.BorderColor = Drawing.Color.Red
                End If

                If dtQuestions.Rows.Count > 2 Then
                    uInfo = ActiveUserCount(dtQuestions.Rows(2).Item("QuestionNumber"))
                    hlQuestion3.Text = "Q" + dtQuestions.Rows(2).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
                    hlQuestion3.ToolTip = uInfo.UserList
                    hlQuestion3.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(2).Item("UniqueID").ToString
                    hlQuestion3.Visible = True
                    If uInfo.Count < 3 Then
                        hlQuestion3.BackColor = Drawing.Color.Red
                        hlQuestion3.BorderColor = Drawing.Color.Red
                    End If

                    If dtQuestions.Rows.Count > 3 Then
                        uInfo = ActiveUserCount(dtQuestions.Rows(3).Item("QuestionNumber"))
                        hlQuestion4.Text = "Q" + dtQuestions.Rows(3).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
                        hlQuestion4.ToolTip = uInfo.UserList
                        hlQuestion4.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(3).Item("UniqueID").ToString
                        hlQuestion4.Visible = True
                        If uInfo.Count < 3 Then
                            hlQuestion4.BackColor = Drawing.Color.Red
                            hlQuestion4.BorderColor = Drawing.Color.Red
                        End If

                        If dtQuestions.Rows.Count > 4 Then
                            uInfo = ActiveUserCount(dtQuestions.Rows(4).Item("QuestionNumber"))
                            hlQuestion5.Text = "Q" + dtQuestions.Rows(4).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
                            hlQuestion5.ToolTip = uInfo.UserList
                            hlQuestion5.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(4).Item("UniqueID").ToString
                            hlQuestion5.Visible = True
                            If uInfo.Count < 3 Then
                                hlQuestion5.BackColor = Drawing.Color.Red
                                hlQuestion5.BorderColor = Drawing.Color.Red
                            End If

                            If dtQuestions.Rows.Count > 5 Then
                                uInfo = ActiveUserCount(dtQuestions.Rows(5).Item("QuestionNumber"))
                                hlQuestion6.Text = "Q" + dtQuestions.Rows(5).Item("QuestionNumber").ToString + " (" + uInfo.Count.ToString + ")"
                                hlQuestion6.ToolTip = uInfo.UserList
                                hlQuestion6.NavigateUrl = "~/Protected/Forms/Individual.aspx?ID=" + dtQuestions.Rows(5).Item("UniqueID").ToString
                                hlQuestion6.Visible = True
                                If uInfo.Count < 3 Then
                                    hlQuestion6.BackColor = Drawing.Color.Red
                                    hlQuestion6.BorderColor = Drawing.Color.Red
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Function
    Function ActiveUserCount(ByVal QuestionNumber As Integer) As UserInfo
        Dim users As System.Web.Security.MembershipUserCollection
        users = Membership.GetAllUsers()
        Dim uInfo As New UserInfo
        uInfo.UserList = ""

        For Each singleuser As System.Web.Security.MembershipUser In users
            Dim singleprofile As ProfileCommon = Profile.GetProfile(singleuser.UserName)
            If singleprofile.ActiveQuestion = QuestionNumber Then
                uInfo.Count += 1
                uInfo.UserList += singleuser.UserName + vbCrLf
            End If
        Next

        If uInfo.Count = 0 Then
            uInfo.UserList = "No Current Users!"
        End If
        Return uInfo
    End Function
   

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Page.MaintainScrollPositionOnPostBack = True
        RoleDisplay()
        CountMembers()
        SetHyperLinks()
    End Sub

End Class

