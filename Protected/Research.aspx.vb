Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess

Partial Class Research
    Inherits System.Web.UI.Page
    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Profile.ActiveQuestion = 0
        Profile.LastPage = "Research.aspx"
    End Sub
   
End Class
