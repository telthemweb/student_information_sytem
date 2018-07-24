Imports System.Data.OleDb
Public Class Login

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Application.Exit()
    End Sub

    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
        Dim cmd As OleDbCommand = New OleDbCommand( _
                   "SELECT * FROM tbUser WHERE Username = '" & _
                   txtUser.Text & "' AND [Password] = '" & txtPass.Text & "' AND  userType ='" & cmbUserTy.Text & "' ", con)
        Dim user As String = ""
        Dim pass As String = ""
        Dim rolep As String = ""
        Dim fulname As String = ""

        con.Open()
        Dim sdr As OleDbDataReader = cmd.ExecuteReader()

        If (sdr.Read() = True) Then
            user = sdr("Username")
            pass = sdr("Password")
            rolep = sdr("userType")
            fulname = sdr("Fullname")

            If rolep = "Bursar" Then
                MsgBox("Welcome to Bursar Page", MsgBoxStyle.Information)
                HomePage.btnLog.Text = (fulname)
                HomePage.Show()
                Me.Hide()
                IsUserLoggedIn = True
            ElseIf rolep = "Headmaster" Then
                MsgBox("Welcome to headmaster Home Page Head", MsgBoxStyle.Information)
                HeadmasterHome.btnLog.Text = (fulname)
                HeadmasterHome.Show()
                Me.Hide()
                IsUserLoggedIn = True
            ElseIf rolep = "Teacher" Then
                MsgBox("Welcome to Teacher Home Page", MsgBoxStyle.Information)
                TeacherPage.btnLog.Text = (fulname)
                TeacherPage.Show()
                Me.Hide()
                IsUserLoggedIn = True

            ElseIf rolep = "Student" Then
                MsgBox("You login successfully", MsgBoxStyle.Information)
                StudentResults.btnLog.Text = (user)
                StudentResults.txtName.Text = (fulname)
                StudentResults.Show()
                Me.Hide()
                IsUserLoggedIn = True
            Else
                MsgBox("Error", MsgBoxStyle.Critical)
            End If

        Else

            MsgBox("Invalid username or password!", MsgBoxStyle.Critical)
        End If


        txtPass.Text = ""
        txtUser.Text = ""




    End Sub
End Class
