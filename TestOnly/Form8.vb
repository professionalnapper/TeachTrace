Public Class Form8
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Filter by Class")
        ComboBox1.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        DataGridView1.Font = New Font("Poppins", 10, FontStyle.Regular)
    End Sub
End Class