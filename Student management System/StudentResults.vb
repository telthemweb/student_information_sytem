Imports System.Data.OleDb
Public Class StudentResults
    Private stdnt As New dbConnection
    Dim Myconnection As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
  
    Private Sub lblTime_Click(sender As System.Object, e As System.EventArgs) Handles lblTime.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = TimeOfDay
        Timer1.Start()
    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles btnLoad.Click
        dbdt = New DataTable
        Try
            Sql = "SELECT school_yr, subject_id,subjectName, class, mark,Grade,Decision FROM Results WHERE idno ='" & Login.txtUser.Text & "'"

            conn.ConnectionString = Myconnection
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            dbda.SelectCommand = dbcmd
            dbda.Fill(dbdt)
            DispayRes.DataSource = dbdt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub StudentResults_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        conn.Open()
        Try
            dbcmd = New OleDbCommand("SELECT * FROM studentdetails WHERE idno ='" & Login.txtUser.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtClass.Text = DbReader("St_class")
                txtGender.Text = DbReader("sex")
                lblcity.Text = DbReader("city")
                lbladdress.Text = DbReader("address")
                lblnationality.Text = DbReader("nationality")
                lblreligion.Text = DbReader("religion")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()


        ''RefreshResultsGrd()
        lblRegn.Text = Login.txtUser.Text
        btnLog.Text = Login.txtUser.Text
    End Sub



    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Login.Show()
        Me.Close()
    End Sub

    Private Sub StudentResults_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT [amount_left],[fees_paid],[isFullyPaid] FROM FeesTable WHERE idno ='" & btnLog.Text & "'", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                lblfeesPaid.Text = DbReader("fees_paid")
                lblLeft.Text = DbReader("amount_left")
                lblFees1.Text = DbReader("isFullyPaid")

            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

        If lblFees1.Text = "NO" Then
            btnLoad.Visible = False
            BtnPrint.Visible = False
            lblMessage.Visible = True
            DispayRes.Visible = False
            lblMessage.Text = "You can't view your results because You have an outstanding balance of " & "   " & "$" & lblLeft.Text
        Else
            btnLoad.Visible = True
            BtnPrint.Visible = True
            lblMessage.Visible = False
            DispayRes.Visible = True
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        FeesStructure.Show()
    End Sub
End Class