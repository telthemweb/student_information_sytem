Imports System.Data.OleDb
Public Class TeacherMarkUploader
    Private dbstdnt As New dbConnection
    Dim imgpath As String
    Dim arrImage() As Byte
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = TimeOfDay
        Timer1.Start()
    End Sub

    Private Sub TeacherMarkUploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dbstdnt.ExecQuery("SELECT * FROM tbsubject ORDER BY subject_id ASC")
        If NotEmpty(dbstdnt.Exception) Then MsgBox(dbstdnt.Exception) : Exit Sub


        ' TotalAmount = TotalAmount + txtAmount.Value

        ' CLEAR COMBOBOX
        cboCode.Items.Clear()

        ' FILL COMBOBOX
        For Each R As DataRow In dbstdnt.dt.Rows
            cboCode.Items.Add(R("subject_id"))
        Next

        ' DISPLAY FIRS NAME FOUND
        If dbstdnt.RecordCount > 0 Then cboCode.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim mark As Integer = txtMark.Text
        Dim grad As String
        If mark >= 70 And mark <= 100 Then
            grad = "A"
            txtgrad.Text = grad
        ElseIf mark >= 60 And mark <= 69 Then
            grad = "B"
            txtgrad.Text = grad
        ElseIf mark >= 50 And mark <= 59 Then
            grad = "C"
            txtgrad.Text = grad
        ElseIf mark >= 45 And mark <= 49 Then
            grad = "D"
            txtgrad.Text = grad
        ElseIf mark >= 35 And mark <= 44 Then
            grad = "E"
            txtgrad.Text = grad
        Else
            grad = "U"
            txtgrad.Text = grad
        End If
    End Sub

    Private Sub cboCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCode.SelectedIndexChanged
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM tbsubject WHERE subject_id ='" & cboCode.Text & "'", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtSub.Text = DbReader("subject_s")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txtReg_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtReg.KeyPress
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM studentdetails WHERE idno ='" & txtReg.Text & "'", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtName.Text = DbReader("fname")
                txtSuame.Text = DbReader("lname")
                cboClas.Text = DbReader("St_class")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub
    Private Sub AddMark()
        'add parameter fro DBConn cLass
        dbstdnt.AddParam("@idno", txtReg.Text)
        dbstdnt.AddParam("@lname", txtSuame.Text)
        dbstdnt.AddParam("@fname", txtName.Text)
        dbstdnt.AddParam("@subject_id", cboCode.Text)
        dbstdnt.AddParam("@subjectName", txtSub.Text)
        dbstdnt.AddParam("@class", cboClas.Text)
        dbstdnt.AddParam("@mark", txtMark.Text)
        dbstdnt.AddParam("@Grade", txtgrad.Text)
        dbstdnt.AddParam("@Teacher", lblTeacher.Text)
        dbstdnt.AddParam("@school_yr", cboYr.Text)

        'execute command

        dbstdnt.ExecQuery("INSERT INTO Results([idno],[lname],[fname],[subject_id],[subjectName], [class], [mark],[Grade], [Teacher],[school_yr]) VALUES(@idno,@lname,@fname,@subject_id,@subjectName,@class,@mark,@Grade,@Teacher,@school_yr)")

        ' REPORT & ABORT ON ERRORS
        If NotEmpty(dbstdnt.Exception) Then MsgBox(dbstdnt.Exception) : Exit Sub

        MsgBox("New studentdetails Added Successfully", MsgBoxStyle.Information)
        txtReg.ResetText()
        txtSuame.Clear()
        txtName.Clear()
        txtMark.Clear()
        txtgrad.Clear()
    End Sub

    Private Sub TeacherMarkUploader_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        lblTeacher.Text = TeacherPage.btnLog.Text
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        AddMark()
    End Sub

    Private Sub txtReg_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtReg.TextChanged

    End Sub
End Class