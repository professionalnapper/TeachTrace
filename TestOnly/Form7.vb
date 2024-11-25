Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form7
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim form5 As New Form5
        form5.Show()
        Hide()  'Hides current form
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize ComboBoxes
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

        ' Configure DataGridView properties
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True

        ' Load initial data
        UpdateDataGridView()
    End Sub

    Public Sub UpdateDataGridView()
        Try
            DataGridView1.Rows.Clear()
            ' Read data from Excel file
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            Dim worksheet As Excel.Worksheet = workbook.Sheets(1)
            Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

            ' Read and display data
            For i As Integer = 2 To lastRow ' Assuming row 1 has headers
                Dim name As String = worksheet.Cells(i, 1).Value?.ToString()
                Dim surname As String = worksheet.Cells(i, 2).Value?.ToString()
                Dim age As String = worksheet.Cells(i, 3).Value?.ToString()

                ' Format the time value with AM/PM
                Dim regTime As String = ""
                Dim timeValue = worksheet.Cells(i, 4).Value
                If timeValue IsNot Nothing Then
                    If IsNumeric(timeValue) Then
                        ' Convert Excel time (decimal) to DateTime
                        Dim dateTime As DateTime = DateTime.FromOADate(Convert.ToDouble(timeValue))
                        ' Format as time with AM/PM
                        regTime = dateTime.ToString("hh:mm:ss tt")
                    Else
                        ' If it's already a string, try to parse it
                        Dim parsedTime As DateTime
                        If DateTime.TryParse(timeValue.ToString(), parsedTime) Then
                            regTime = parsedTime.ToString("hh:mm:ss tt")
                        Else
                            regTime = timeValue.ToString()
                        End If
                    End If
                End If

                ' Format the date value (date only)
                Dim regDate As String = ""
                Dim dateValue = worksheet.Cells(i, 5).Value
                If dateValue IsNot Nothing Then
                    Dim parsedDate As DateTime
                    If DateTime.TryParse(dateValue.ToString(), parsedDate) Then
                        regDate = parsedDate.ToString("MM/dd/yyyy")
                    Else
                        regDate = dateValue.ToString()
                    End If
                End If

                If Not String.IsNullOrEmpty(name) Then
                    DataGridView1.Rows.Add(name, surname, age, regTime, regDate)
                End If
            Next

            ' Set the format for the time column (assuming it's column index 3)
            DataGridView1.Columns(3).DefaultCellStyle.Format = "hh:mm:ss tt"
            ' Set the format for the date column (assuming it's column index 4)
            DataGridView1.Columns(4).DefaultCellStyle.Format = "MM/dd/yyyy"

            ' Clean up Excel objects
            workbook.Close()
            excelApp.Quit()
            ReleaseObject(worksheet)
            ReleaseObject(workbook)
            ReleaseObject(excelApp)
        Catch ex As Exception
            MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            If obj IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            End If
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Add your filtering logic here for ComboBox1 if needed
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Add your filtering logic here for ComboBox2 if needed
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub
End Class