Imports System.Data.OleDb
Public Class HeadmasterViewRes
    Private stdnt As New dbConnection
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    Public Sub RefreshResultsGrd()
        ' RUN QUERY 
        stdnt.ExecQuery("SELECT * FROM Results ORDER BY ID ASC")
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub
        headGrid.DataSource = stdnt.dt
    End Sub
   
    Private Sub UpdateResults()
        'add parameter fro DBConn cLass
        stdnt.AddParam("@idno", txtReg.Text)
        stdnt.AddParam("@lname", txtSuame.Text)
        stdnt.AddParam("@fname", txtName.Text)
        stdnt.AddParam("@subject_id", cboCode.Text)
        stdnt.AddParam("@subjectName", txtSub.Text)
        stdnt.AddParam("@class", cboClas.Text)
        stdnt.AddParam("@mark", txtMark.Text)
        stdnt.AddParam("@Grade", txtgrad.Text)
        stdnt.AddParam("@Decision", txtdecision.Text)
        stdnt.AddParam("@Teacher", lblTeacher.Text)
        stdnt.AddParam("@school_yr", cboYr.Text)

        'execute command

        '' stdnt.ExecQuery("INSERT INTO Results([idno],[lname],[fname],[subject_id],[subjectName], [class], [mark],[Grade], [Decision],[Teacher],[school_yr]) VALUES(@idno,@lname,@fname,@subject_id,@subjectName,@class,@mark,@Grade,@Decision,@Teacher,@school_yr)")
        stdnt.ExecQuery("UPDATE Results SET [idno]=@idno ,[lname]=@lname, [fname] = @fname, [subject_id] = @subject_id, [subjectName] = @subjectName , [class] = @class , [mark] = @mark,[Grade] = @Grade , [Decision] = @Decision , [Teacher] = @Teacher, [school_yr] = @school_yr WHERE ID = " & Me.Text)
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub

        MsgBox("Updated Successfully", MsgBoxStyle.Information)
        txtReg.Clear()
        txtSuame.Clear()
        txtName.Clear()
        txtdecision.Clear()
        txtgrad.Clear()
        cboClas.ResetText()
        cboCode.ResetText()
        cboYr.ResetText()
    End Sub
    Private Sub HeadmasterViewRes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RefreshResultsGrd()
    End Sub


    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtReg.Clear()
        txtSuame.Clear()
        txtName.Clear()
        txtdecision.Clear()
        txtgrad.Clear()
        cboClas.ResetText()
        cboCode.ResetText()
        cboYr.ResetText()

    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        UpdateResults()
        RefreshResultsGrd()
    End Sub

   
    Private Sub headGrid_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles headGrid.CellClick
        Me.Text = headGrid.CurrentRow.Cells(0).Value.ToString
        txtReg.Text = headGrid.CurrentRow.Cells(1).Value.ToString
        txtSuame.Text = headGrid.CurrentRow.Cells(2).Value.ToString
        txtName.Text = headGrid.CurrentRow.Cells(3).Value.ToString
        cboCode.Text = headGrid.CurrentRow.Cells(4).Value.ToString
        txtSub.Text = headGrid.CurrentRow.Cells(5).Value.ToString
        cboClas.Text = headGrid.CurrentRow.Cells(6).Value.ToString
        txtMark.Text = headGrid.CurrentRow.Cells(7).Value.ToString
        txtgrad.Text = headGrid.CurrentRow.Cells(8).Value.ToString
        txtdecision.Text = headGrid.CurrentRow.Cells(9).Value.ToString
        lblTeacher.Text = headGrid.CurrentRow.Cells(10).Value.ToString
        cboYr.Text = headGrid.CurrentRow.Cells(11).Value.ToString
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        conn.Open()
        Try
            dbcmd = New OleDbCommand("SELECT * FROM Results WHERE idno ='" & txtSearch.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtReg.Text = DbReader("idno")
                txtSuame.Text = DbReader("lname")
                txtName.Text = DbReader("fname")
                cboCode.Text = DbReader("subject_id")
                txtSub.Text = DbReader("subjectName")
                cboClas.Text = DbReader("class")
                txtMark.Text = DbReader("mark")
                txtgrad.Text = DbReader("Grade")
                txtdecision.Text = DbReader("Decision")
                lblTeacher.Text = DbReader("Teacher")
                cboYr.Text = DbReader("school_yr")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub
End Class