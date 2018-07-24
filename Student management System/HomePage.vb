Public Class HomePage

    Private Sub btnLog_Click(sender As System.Object, e As System.EventArgs) Handles btnLog.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub AddUsersToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        CreateAccount.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Fees.Show()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        arrears.Show()
    End Sub

    Private Sub AddSubjectToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddSubjectToolStripMenuItem.Click
        FeesStructure.ShowDialog()
    End Sub

    Private Sub HomePage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        EnrollStudent.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        EnrollList.ShowDialog()
    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddUserToolStripMenuItem.Click
        CreateAccount.ShowDialog()
    End Sub
End Class