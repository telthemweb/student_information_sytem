Imports System.Data.OleDb
Public Class Fees
    Private fees As New dbConnection
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
   
    Public Sub loadFees()
        ' RUN QUERY
        fees.ExecQuery("SELECT * FROM FeesTable ORDER BY ID ASC")
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(fees.Exception) Then MsgBox(fees.Exception) : Exit Sub
        feesGrid.DataSource = fees.dt
    End Sub
    'Register student
    Private Sub Register()
        'add parameter fro DBConn cLass
        fees.AddParam("@idno", txtReg.Text)
        fees.AddParam("@lname", txtfname.Text)
        fees.AddParam("@fname", txtlname.Text)
        fees.AddParam("@St_class", cboclass.Text)
        fees.AddParam("@fees_paid", txtFeesPd.Text)
        fees.AddParam("@amount_left", txtFeesLeft.Text)
        fees.AddParam("@datePaid", dtpPaid.Text)
        fees.AddParam("@sch_term", cboTerm.Text)
        fees.AddParam("@isFullyPaid", cmbisFullyPaid.Text)

        'execute command

        fees.ExecQuery("INSERT INTO FeesTable([idno],[lname],[fname], [St_class], [fees_paid],[amount_left],[datePaid], [sch_term],[isFullyPaid]) VALUES(@idno,@lname,@fname,@St_class,@fees_paid,@amount_left,@datePaid,@sch_term,@isFullyPaid)")

        ' REPORT & ABORT ON ERRORS
        If NotEmpty(fees.Exception) Then MsgBox(fees.Exception) : Exit Sub

        MsgBox("New Fees  Paid Successfully", MsgBoxStyle.Information)
        loadFees()
        txtReg.ResetText()
        txtfname.Clear()
        txtlname.Clear()
        txtFeesLeft.Clear()
        txtFeesPd.Clear()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Register()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        txtFeesLeft.Text = Val(1600) - txtFeesPd.Text
    End Sub

    Private Sub txtReg_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtReg.TextChanged
      
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM studentdetails WHERE idno ='" & txtReg.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            Dim arrimage() As Byte = Nothing
            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtReg.Text = DbReader("idno")
                txtlname.Text = DbReader("lname")
                txtfname.Text = DbReader("fname")
                cboclass.Text = DbReader("St_class")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Fees_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadFees()
    End Sub
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        conn.Open()
        Try
            dbcmd = New OleDbCommand("SELECT * FROM FeesTable WHERE idno ='" & txtSearch.Text & "'", conn)
            Dim DbReader As OleDbDataReader
            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtReg.Text = DbReader("idno")
                txtlname.Text = DbReader("lname")
                txtfname.Text = DbReader("fname")
                cboclass.Text = DbReader("St_class")
                txtFeesPd.Text = DbReader("fees_paid")
                txtFeesLeft.Text = DbReader("amount_left")
                dtpPaid.Text = DbReader("datePaid")
                cboTerm.Text = DbReader("sch_term")
                cmbisFullyPaid.Text = DbReader("isFullyPaid")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub  
End Class