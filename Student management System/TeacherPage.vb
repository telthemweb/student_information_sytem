Public Class TeacherPage

    Private Sub FeesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FeesToolStripMenuItem.Click

    End Sub

    Private Sub btnLog_Click(sender As System.Object, e As System.EventArgs) Handles btnLog.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = TimeOfDay
        Timer1.Start()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        TeacherMarkUploader.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Resultslist.Show()
    End Sub

    Private Sub AddSubjectToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddSubjectToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class