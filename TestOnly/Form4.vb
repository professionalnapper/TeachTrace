Public Class Form4
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Create an instance of Form5
        Dim form5 As New Form5()

        ' Show Form5
        form5.Show()

        ' Optional: Close the current form (Form4) if needed
        Me.Hide()
    End Sub
End Class
