Imports System.Data.OleDb
Public Class EnrollStudent
    Private cb As New dbConnection
    Dim imgpath As String
    Dim arrImage() As Byte
    Private Function NotEmpty(text As String) As Boolean
        Return Not String.IsNullOrEmpty(text)
    End Function
 
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        EnrollList.ShowDialog()
    End Sub
    'for generating RegNumber.
    Private Sub EnrollStudent_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\QueenElzer.accdb")
        Str = "select * from studentdetails"
        dbcmd = New OleDbCommand(Str, con)
        dbda.SelectCommand = dbcmd
        dbda.Fill(ds, "studentdetails")


        Dim LastNo As Integer
        LastNo = ds.Tables("studentdetails").Rows.Count + 1
        If LastNo >= 0 Then
            txtReg.Text = "Qn189006" & LastNo
        Else
            txtReg.Text = "Qn" & 0
        End If
    End Sub
    'Enroll student
    Private Sub EnrollStudent()
        Dim mstream As New System.IO.MemoryStream
        picUp.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        arrImage = mstream.GetBuffer
        Dim filesize As UInt32
        filesize = mstream.Length

        mstream.Close()

        'add parameter fro DBConn cLass
        cb.AddParam("@idno", txtReg.Text)
        cb.AddParam("@lname", txtlname.Text)
        cb.AddParam("@fname", txtfname.Text)
        cb.AddParam("@nationality", txtNation.Text)
        cb.AddParam("@city", txtCity.Text)
        cb.AddParam("@address", txtAdress.Text)
        cb.AddParam("@religion", txtreligion.Text)
        cb.AddParam("@sex", cbosex.Text)
        cb.AddParam("@pbirth", txtbplace.Text)
        cb.AddParam("@class", cbostatus.Text)
        cb.AddParam("@birthday", dtpDOB.Text)
        cb.AddParam("@dateAdded", dtpAdded.Text)
        cb.AddParam("@upload", arrImage)
        cb.AddParam("@SchYear", cbos_sy.Text)
        cb.AddParam("@FatherName", txtFan.Text)
        cb.AddParam("@occupation", txtOccupy.Text)

        'execute command

        cb.ExecQuery("INSERT INTO studentdetails([idno],[lname],[fname], [nationality], [city],[address],[religion], [sex],[pbirth],[St_class],[birthday],[dateAdded],[upload],[SchYear],[FatherName],[occupation]) VALUES(@idno,@lname,@fname,@nationality,@city,@address,@religion,@sex,@pbirth,@class,@birthday,@dateAdded,@upload,@SchYear,@FatherName,@occupation)")



        ' REPORT & ABORT ON ERRORS
        If NotEmpty(cb.Exception) Then MsgBox(cb.Exception) : Exit Sub

        ''cb.ExecQuery("INSERT INTO FeesTable([idno],[lname],[fname], [St_class], [fees_paid],[amount_left],[datePaid], [sch_term]) VALUES(@idno,@lname,@fname,@St_class,@feesPaid,@amount_left,@datePaid,@sch_term)")
        MsgBox("New studentdetails Added Successfully", MsgBoxStyle.Information)
        txtReg.ResetText()
        txtlname.Clear()
        txtfname.Clear()
        txtNation.Clear()
        txtbplace.Clear()
    End Sub
    

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        EnrollStudent()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
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

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        txtlname.Clear()
        txtfname.Clear()
        txtNation.Clear()
        txtbplace.Clear()
    End Sub

End Class