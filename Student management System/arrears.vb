Imports System.Data.OleDb
Public Class arrears
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
    'Update FeeTable
    Private Sub UpDateArrears()
        'add parameter fro DBConn cLass
        fees.AddParam("@idno", txtReg.Text)
        fees.AddParam("@lname", txtfname.Text)
        fees.AddParam("@fname", txtlname.Text)
        fees.AddParam("@St_class", cboclass.Text)
        fees.AddParam("@fees_paid", txtBalance.Text)
        fees.AddParam("@amount_left", txtFeesLeft.Text)
        fees.AddParam("@datePaid", dtpPaid.Text)
        fees.AddParam("@sch_term", cboTerm.Text)
        fees.AddParam("@isFullyPaid", cmbisFullyPaid.Text)

        'execute command
        fees.ExecQuery("UPDATE  FeesTable SET [idno]=@idno,[lname]=@lname,[fname]=@fname, [St_class]=@St_class, [fees_paid]=@fees_paid,[amount_left]=@amount_left,[datePaid]=@datePaid, [sch_term]=@sch_term,[isFullyPaid]=@isFullyPaid WHERE ID =" & Me.Text)

        ' REPORT & ABORT ON ERRORS
        If NotEmpty(fees.Exception) Then MsgBox(fees.Exception) : Exit Sub

        MsgBox("Arrears cleared Successfully", MsgBoxStyle.Information)
        loadFees()
        txtReg.ResetText()
        txtfname.Clear()
        txtlname.Clear()
        txtFeesLeft.Clear()
        txtBalance.Clear()
    End Sub
    Private Sub feesGrid_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles feesGrid.CellClick
        Me.Text = feesGrid.CurrentRow.Cells(0).Value.ToString
        txtReg.Text = feesGrid.CurrentRow.Cells(1).Value.ToString
        txtlname.Text = feesGrid.CurrentRow.Cells(2).Value.ToString
        txtfname.Text = feesGrid.CurrentRow.Cells(3).Value.ToString
        cboclass.Text = feesGrid.CurrentRow.Cells(4).Value.ToString
        txtBalance.Text = feesGrid.CurrentRow.Cells(5).Value.ToString
        txtFeesLeft.Text = feesGrid.CurrentRow.Cells(6).Value.ToString
        dtpPaid.Text = feesGrid.CurrentRow.Cells(7).Value.ToString
        cboTerm.Text = feesGrid.CurrentRow.Cells(8).Value.ToString
        cmbisFullyPaid.Text = feesGrid.CurrentRow.Cells(9).Value.ToString
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        txtBalance.Text = Val(txtFeesLeft.Text) + txtBalance.Text
        txtFeesLeft.Clear()
    End Sub

    Private Sub arrears_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadFees()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        txtFeesLeft.Text = Val(1600) - txtBalance.Text
    End Sub



    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        UpDateArrears()
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
                txtBalance.Text = DbReader("fees_paid")
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