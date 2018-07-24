Imports System.Data.OleDb
Public Class CreateAccount
    Private dbCon As New dbConnection
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    'Create user account
    Private Sub AddUserAccount()
        'add parameter fro DBConn cLass
        dbCon.AddParam("@Username", txtUser.Text)
        dbCon.AddParam("@Password", txtPass.Text)
        dbCon.AddParam("@userType", cboUsertype.Text)
        dbCon.AddParam("@Fullname", txtFul.Text)




        'execute command
        dbCon.ExecQuery("INSERT INTO tbUser([Username],[Password],[userType], [Fullname]) VALUES(@Username,@Password,@userType,@Fullname)")

        ' REPORT & ABORT ON ERRORS
        If NotEmpty(dbCon.Exception) Then MsgBox(dbCon.Exception) : Exit Sub

        MsgBox("New Account created Successfully", MsgBoxStyle.Information)
        txtUser.ResetText()
        txtPass.Clear()
        cboUsertype.ResetText()
        txtFul.Clear()
        txtrType.Clear()

        
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        AddUserAccount()
    End Sub

    Private Sub CreateAccount_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class