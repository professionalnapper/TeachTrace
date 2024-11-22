Public Class Form7
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()  'Hides current form
    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Select a class")
        ComboBox1.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Filter by Class")
        ComboBox2.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.SelectedIndex = 0
    End Sub
End Class