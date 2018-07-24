Public Class HeadmasterHome

    Private Sub btnLog_Click(sender As System.Object, e As System.EventArgs) Handles btnLog.Click
        Login.Show()
        Me.Hide()
    End Sub



    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = TimeOfDay
        Timer1.Start()
    End Sub

   

   

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        EnrollList.ShowDialog()
    End Sub


    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub UploadResultsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UploadResultsToolStripMenuItem.Click
        HeadmasterViewRes.ShowDialog()
    End Sub

  

    Private Sub ViewListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewListToolStripMenuItem.Click
        EnrollList.ShowDialog()
    End Sub

    Private Sub FeesStructureToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FeesStructureToolStripMenuItem.Click
        FeesStructure.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        HeadmasterViewRes.ShowDialog()
    End Sub
End Class