Imports System.Data.OleDb
Public Class FeesStructure
    Private stdnt As New dbConnection
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    Public Sub loadFess()
        ' RUN QUERY
        stdnt.ExecQuery("SELECT * FROM fStructure ORDER BY SN0 ASC")
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub
        StudentGrid.DataSource = stdnt.dt
    End Sub

    Private Sub FeesStructure_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadFess()
    End Sub
End Class