Imports System.Data
Imports System.Data.SqlClient
Partial Class Dump
    Inherits System.Web.UI.Page
     Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TriviaQuestions2007.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As New System.IO.StringWriter()
        Dim htw As New System.Web.UI.HtmlTextWriter(sw)
        GridView1.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
End Class
