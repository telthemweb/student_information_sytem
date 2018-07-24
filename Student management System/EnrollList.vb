Imports System.Data.OleDb
Public Class EnrollList
    Private stdnt As New dbConnection
    Dim imgpath As String
    Dim arrImage() As Byte
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPicUp.Click
        Try
            Dim OFD As FileDialog = New OpenFileDialog()

            OFD.Filter = "Image File (*.jpg;*.bmp;*.gif)| *.jpg;*.bmp;*.gif"

            If OFD.ShowDialog() = DialogResult.OK Then
                imgpath = OFD.FileName
                picUp.ImageLocation = imgpath

            End If

            OFD = Nothing

        Catch ex As Exception
            MsgBox(ex.Message.ToString())

        End Try
    End Sub
    Private Sub StudentGrid_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles StudentGrid.CellClick
        Dim arrimage() As Byte = Nothing

        Me.Text = StudentGrid.CurrentRow.Cells(0).Value.ToString
            txtReg.Text = StudentGrid.CurrentRow.Cells(1).Value.ToString
            txtlname.Text = StudentGrid.CurrentRow.Cells(2).Value.ToString
            txtfname.Text = StudentGrid.CurrentRow.Cells(3).Value.ToString
            txtNation.Text = StudentGrid.CurrentRow.Cells(4).Value.ToString
            txtCity.Text = StudentGrid.CurrentRow.Cells(5).Value.ToString
            txtAdress.Text = StudentGrid.CurrentRow.Cells(6).Value.ToString
            txtreligion.Text = StudentGrid.CurrentRow.Cells(7).Value.ToString
            cbosex.Text = StudentGrid.CurrentRow.Cells(8).Value.ToString
            txtbplace.Text = StudentGrid.CurrentRow.Cells(9).Value.ToString
            cbostatus.Text = StudentGrid.CurrentRow.Cells(10).Value.ToString
            dtpDOB.Text = StudentGrid.CurrentRow.Cells(11).Value.ToString
        dtpAdded.Text = StudentGrid.CurrentRow.Cells(12).Value.ToString
        cbos_sy.Text = StudentGrid.CurrentRow.Cells(14).Value.ToString
        txtFan.Text = StudentGrid.CurrentRow.Cells(15).Value.ToString
        txtOccupy.Text = StudentGrid.CurrentRow.Cells(16).Value.ToString



        If Not IsDBNull(arrimage) Then
            arrimage = StudentGrid.CurrentRow.Cells(13).Value
        End If

        Dim mstream As New System.IO.MemoryStream(arrimage)
        picUp.Image = Image.FromStream(mstream)

    End Sub
    Public Sub loadStudents()
        ' RUN QUERY
        stdnt.ExecQuery("SELECT * FROM studentdetails ORDER BY userid ASC")
        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub
        StudentGrid.DataSource = stdnt.dt
    End Sub
    Public Sub UpdateList()
        Dim mstream As New System.IO.MemoryStream
        picUp.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        arrImage = mstream.GetBuffer
        Dim filesize As UInt32
        filesize = mstream.Length

        mstream.Close()

        'add parameter fro DBConn cLass
        stdnt.AddParam("@idno", txtReg.Text)
        stdnt.AddParam("@lname", txtlname.Text)
        stdnt.AddParam("@fname", txtfname.Text)
        stdnt.AddParam("@nationality", txtNation.Text)
        stdnt.AddParam("@city", txtCity.Text)
        stdnt.AddParam("@address", txtAdress.Text)
        stdnt.AddParam("@religion", txtreligion.Text)
        stdnt.AddParam("@sex", cbosex.Text)
        stdnt.AddParam("@pbirth", txtbplace.Text)
        stdnt.AddParam("@class", cbostatus.Text)
        stdnt.AddParam("@birthday", dtpDOB.Text)
        stdnt.AddParam("@dateAdded", dtpAdded.Text)
        stdnt.AddParam("@upload", arrImage)
        stdnt.AddParam("@SchYear", cbos_sy.Text)
        stdnt.AddParam("@FatherName", txtFan.Text)
        stdnt.AddParam("@occupation", txtOccupy.Text)

        stdnt.ExecQuery("UPDATE studentdetails SET [idno]=@idno ,[lname]=@lname, [fname] = @fname, [nationality] = @nationality, [city] = @city , [address] = @address , [religion] = @religion, [sex] = @sex , [pbirth] = @pbirth, [St_class] = @class, [birthday] = @birthday, [dateAdded] = @dateAdded ,[upload] = @upload, [SchYear] = @SchYear,[FatherName]=@FatherName,[occupation]=@ WHERE userid = " & Me.Text)

        ' REPORT & ABORT ON ERRORS
        If NotEmpty(stdnt.Exception) Then MsgBox(stdnt.Exception) : Exit Sub

        MsgBox("New studentdetails Added Successfully", MsgBoxStyle.Information)
        txtReg.ResetText()
        txtlname.Clear()
        txtfname.Clear()
        txtNation.Clear()
        txtbplace.Clear()
        txtFan.Clear()
        txtOccupy.Clear()
    End Sub
    Private Sub EnrollList_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        loadStudents()
    End Sub
    Private Sub btnUp_Click(sender As System.Object, e As System.EventArgs) Handles btnUp.Click
        UpdateList()
        loadStudents()
    End Sub



    Private Sub btnDel_Click(sender As System.Object, e As System.EventArgs) Handles btnDel.Click
        Try
            Sql = "DELETE * FROM studentdetails  WHERE userid =" & Me.Text
            conn.Open()
            With dbcmd
                .CommandText = Sql
                .Connection = conn
            End With

            result = dbcmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("New student record has been deleted!")
                loadStudents()
                conn.Close()

                txtReg.ResetText()
                txtlname.Clear()
                txtfname.Clear()
                txtNation.Clear()
                txtbplace.Clear()
                txtCity.Clear()
                txtAdress.Clear()
                dtpAdded.ResetText()
                txtNation.Clear()
                txtreligion.Clear()
            Else
                MsgBox("Nov record has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        conn.Open()

        Try
            dbcmd = New OleDbCommand("SELECT * FROM studentdetails WHERE idno ='" & txtSearch.Text & "'", conn)
            Dim DbReader As OleDbDataReader

            DbReader = dbcmd.ExecuteReader
            While DbReader.Read
                txtReg.Text = DbReader("idno")
                txtlname.Text = DbReader("lname")
                txtfname.Text = DbReader("fname")
                txtNation.Text = DbReader("nationality")
                txtCity.Text = DbReader("city")
                txtAdress.Text = DbReader("address")
                txtreligion.Text = DbReader("religion")
                cbosex.Text = DbReader("sex")
                txtbplace.Text = DbReader("pbirth")
                cbostatus.Text = DbReader("St_class")
                dtpDOB.Text = DbReader("birthday")
                dtpAdded.Text = DbReader("dateAdded")
                picUp.Text = DbReader("dateAdded")
                cbos_sy.Text = DbReader("SchYear")
                txtFan.Text = DbReader("FatherName")
                txtOccupy.Text = DbReader("occupation")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub StudentGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles StudentGrid.CellContentClick

    End Sub
End Class