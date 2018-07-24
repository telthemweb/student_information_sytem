Imports System.Data.OleDb
Public Class Resultslist
    Private stdnt As New dbConnection
    Dim Myconnection As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    Public Sub RefreshResultsGrd()
        ' RUN QUERY
        stdnt.ExecQuery("SELECT [idno],[lname],[fname],[subject_id],[subjectName], [class], [mark],[Grade],[Decision],[school_yr] FROM Results WHERE Teacher ='" & TeacherMarkUploader.lblTeacher.Text & "'")
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub
        ResultsGrd.DataSource = stdnt.dt
    End Sub
    Private Sub Resultslist_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RefreshResultsGrd()
        btnLog.Text = TeacherPage.btnLog.Text
        lblTeacher.Text = TeacherPage.btnLog.Text
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnLoad.Click
        dbdt = New DataTable
        Try
            Sql = "SELECT [idno],[lname],[fname],[subject_id],[subjectName], [class], [mark],[Grade],[Decision],[school_yr] FROM Results WHERE Teacher ='" & btnLog.Text & "'"

            conn.ConnectionString = Myconnection
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            dbda.SelectCommand = dbcmd
            dbda.Fill(dbdt)
            ResultsGrd.DataSource = dbdt



        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub


    Private Sub btnLog_Click(sender As System.Object, e As System.EventArgs) Handles btnLog.Click
        Me.Close()
    End Sub

    Private Sub ResultsGrd_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ResultsGrd.CellClick
        txtReg.Text = ResultsGrd.CurrentRow.Cells(0).Value.ToString
        txtName.Text = ResultsGrd.CurrentRow.Cells(1).Value.ToString
        txtSuame.Text = ResultsGrd.CurrentRow.Cells(2).Value.ToString
        cboCode.Text = ResultsGrd.CurrentRow.Cells(3).Value.ToString
        txtSub.Text = ResultsGrd.CurrentRow.Cells(4).Value.ToString
        cboClas.Text = ResultsGrd.CurrentRow.Cells(5).Value.ToString
        txtMark.Text = ResultsGrd.CurrentRow.Cells(6).Value.ToString
        txtgrad.Text = ResultsGrd.CurrentRow.Cells(7).Value.ToString
        txtDecision.Text = ResultsGrd.CurrentRow.Cells(8).Value.ToString
        cboYr.Text = ResultsGrd.CurrentRow.Cells(9).Value.ToString
    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim sqlquery As String = "UPDATE Results SET [lname]='" & txtName.Text & "', [fname]= '" & txtName.Text & "', [subject_id] = '" & cboCode.Text & "',[subjectName]= '" & txtSub.Text & "', [class]= '" & cboClas.Text & "',[mark]= '" & txtMark.Text & "',[Grade]= '" & txtgrad.Text & "',[Decision]= '" & txtDecision.Text & "',[Teacher]= '" & lblTeacher.Text & "',[school_yr]= '" & cboYr.Text & "' WHERE idno =" & txtReg.Text
            Dim sqlcommand As New OleDbCommand

            conn.Open()
            With sqlcommand
                .CommandText = sqlquery
                .Connection = conn
                .ExecuteNonQuery()
            End With
            MsgBox("User use Successfully Updated.")
            RefreshResultsGrd()
            txtDecision.Clear()
            txtgrad.Clear()
            txtMark.Clear()
            txtName.Clear()

        Catch ex As Exception
            MsgBox(ex.Message)

            conn.Close()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        conn.Open()
        Try
            dbcmd = New OleDbCommand("SELECT * FROM Results WHERE idno ='" & txtSearch.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtReg.Text = DbReader("idno")
                txtName.Text = DbReader("fname")
                txtSuame.Text = DbReader("lname")
                cboCode.Text = DbReader("subject_id")
                txtSub.Text = DbReader("subjectName")
                cboClas.Text = DbReader("class")
                txtMark.Text = DbReader("mark")
                cboYr.Text = DbReader("school_yr")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub
End Class