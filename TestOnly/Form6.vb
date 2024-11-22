Public Class Form6
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()  'Hides current form
    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Sort by")
        ComboBox1.Items.AddRange(New String() {"Name", "Class"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Filter by Class")
        ComboBox2.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.SelectedIndex = 0
    End Sub
End Class