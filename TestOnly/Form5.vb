Public Class Form5
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form6 As New Form6
        form6.Show
        Hide
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form7 As New Form7()
        form7.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim form8 As New Form8()
        form8.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form1 As New Form9
        Form9.Show()
        Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ' Show confirmation message box
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?",
                                               "Logout Confirmation",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question)

        ' If user clicks Yes, proceed with logout
        If result = DialogResult.Yes Then
            ' Close current form
            Me.Hide()
            ' Show login form
            Dim form4 As New Form4()  ' Replace Form1 with your actual login form name
            form4.Show()
        End If
        ' If user clicks No, nothing happens and they stay on current form
    End Sub
End Class