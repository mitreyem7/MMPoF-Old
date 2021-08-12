Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient


Public Class DataAccess
   
    Shared Function GetDS(ByVal SQL As String, ByVal Connection As SqlConnection) As DataSet
        Dim ds As New DataSet
        Dim cmd As SqlCommand = New SqlCommand(SQL, Connection)
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        da.Fill(ds)
        Return ds
    End Function

    Shared Function execSQL(ByVal SQL As String, ByVal Connection As SqlConnection) As Boolean
        Try
            Dim ds As New DataSet
            Dim cmd As SqlCommand = New SqlCommand(SQL, Connection)
            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            da.Fill(ds)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Shared Function LogUserActivity(ByVal UserName As String) As Boolean
    '    Dim meyerdb As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("MeyerOnFireConnectionString").ConnectionString)
    '    Return execSQL("Update [user] set lastlogin='" + DateTime.Now.ToString + "' where username='" + UserName + "'", meyerdb)
    'End Function

    Shared Function getString(ByVal SQL As String, ByVal Connection As SqlConnection) As String
        Dim ds As New DataSet
        Dim cmd As SqlCommand = New SqlCommand(SQL, Connection)
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        da.Fill(ds)
        Try
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return CStr(ds.Tables(0).Rows(0).Item(0))
                End If
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Shared Function updateDS(ByVal SQL As String, ByVal DS As DataSet, ByVal Connection As SqlConnection) As DataSet

        Try
            Dim da As New SqlDataAdapter()
            da.SelectCommand = New SqlCommand(SQL, Connection)
            Dim cb As SqlCommandBuilder = New SqlCommandBuilder(da)
            Connection.Open()
            da.Update(DS)
            Connection.Close()
        Catch ex As Exception

        End Try
        Return DS


    End Function
    Shared Function InsertDS(ByVal TableName As String, ByVal DS As DataSet, ByVal Connection As SqlConnection) As Boolean

        Dim rowData As DataRow
        Dim SQL As String
        Dim BaseSQL As String
        Dim colColumn As DataColumn
        Dim Sucess As Boolean = True

        BaseSQL = "Insert into " + TableName + " ("
        For Each colColumn In DS.Tables(0).Columns
            BaseSQL += "[" + colColumn.ToString + "],"
        Next
        BaseSQL = Mid(BaseSQL, 1, BaseSQL.Length - 1) + ") VALUES ('"


        For Each rowData In DS.Tables(0).Rows
            SQL = BaseSQL
            For Each colColumn In DS.Tables(0).Columns
                If IsDBNull(rowData.Item(colColumn.ToString)) Then
                    SQL += "','"
                Else
                    SQL += CStr(rowData.Item(colColumn.ToString)) + "','"
                End If
            Next
            SQL = Mid(SQL, 1, SQL.Length - 2) + ")"

            'SQL = SQL.Replace("User", "[User]")

            If Not execSQL(SQL, Connection) Then
                Sucess = False
            End If

        Next
        Return Sucess

    End Function
End Class
