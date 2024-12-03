Public Class Form4
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if both TextBoxes are empty
        If String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Please fill in all fields", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        ' Check if only ID is empty
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Focus()
            Return
        End If

        ' Check if the password is correct
        If TextBox2.Text = "Nem00yp" Then
            ' Create an instance of Form9
            Dim form9 As New Form9()
            ' Show Form1
            form9.Show()
            ' Hide the current form
            Me.Hide()
        Else
            ' Show error message if password is incorrect
            MessageBox.Show("Incorrect login code. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Clear the password textbox
            TextBox2.Clear()
            ' Set focus back to the password textbox
            TextBox2.Focus()
        End If
    End Sub
End Class