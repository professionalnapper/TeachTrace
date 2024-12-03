Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configure DataGridView properties
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.Font = New Font("Poppins", 10, FontStyle.Regular)

        ' Initialize DataGridView columns
        InitializeDataGridViewColumns()

        ' Initialize ComboBoxes
        InitializeComboBoxes()

        ' Load initial data
        UpdateDataGridView()
    End Sub

    Private Sub InitializeComboBoxes()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Sort by")
        ComboBox1.Items.AddRange(New String() {"Name", "Class"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Filter by Class")
        ComboBox2.Items.AddRange(New String() {"All", "Class A", "Class B", "Class C"})
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.SelectedIndex = 0
    End Sub

    Private Sub InitializeDataGridViewColumns()
        DataGridView1.Columns.Clear()

        ' Add columns to match the design
        With DataGridView1.Columns
            .Add("User ID", "User ID")
            .Add("Name", "Name")
            .Add("Surname", "Surname")
            .Add("Class", "Class")
        End With

        ' Configure column properties
        For Each col As DataGridViewColumn In DataGridView1.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next

        ' Set specific column formats
        'DataGridView1.Columns("Date").DefaultCellStyle.Format = "MM/dd/yyyy"
    End Sub

    Public Sub UpdateDataGridView()
        Try
            ' Ensure columns are initialized
            If DataGridView1.Columns.Count = 0 Then
                InitializeDataGridViewColumns()
            End If

            DataGridView1.Rows.Clear()

            ' Read data from Excel file
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = Nothing
            Dim worksheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                worksheet = workbook.Sheets(2)
                Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

                ' Read and display data
                For i As Integer = 2 To lastRow ' Assuming row 1 has headers
                    Dim userId As String = Convert.ToString(worksheet.Cells(i, 1).Value)
                    Dim name As String = Convert.ToString(worksheet.Cells(i, 2).Value)
                    Dim surname As String = Convert.ToString(worksheet.Cells(i, 3).Value)
                    Dim class_value As String = Convert.ToString(worksheet.Cells(i, 4).Value)
                    Dim dateValue = worksheet.Cells(i, 5).Value

                    ' Handle the date value
                    Dim formattedDate As String = ""
                    If dateValue IsNot Nothing Then
                        Try
                            If TypeOf dateValue Is Double Then
                                ' Convert Excel serial date to DateTime
                                formattedDate = DateTime.FromOADate(Convert.ToDouble(dateValue)).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is DateTime Then
                                formattedDate = Convert.ToDateTime(dateValue).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is String Then
                                Dim parsedDate As DateTime
                                If DateTime.TryParse(dateValue.ToString(), parsedDate) Then
                                    formattedDate = parsedDate.ToString("MM/dd/yyyy")
                                Else
                                    formattedDate = dateValue.ToString()
                                End If
                            End If
                        Catch ex As Exception
                            formattedDate = dateValue.ToString()
                        End Try
                    End If

                    ' Only add row if name exists
                    If Not String.IsNullOrEmpty(name) Then
                        DataGridView1.Rows.Add(userId, name, surname, class_value, formattedDate)
                    End If
                Next

            Finally
                ' Clean up Excel objects
                If workbook IsNot Nothing Then
                    workbook.Close(False)
                    ReleaseObject(workbook)
                End If
                If worksheet IsNot Nothing Then
                    ReleaseObject(worksheet)
                End If
                excelApp.Quit()
                ReleaseObject(excelApp)
            End Try

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Implement sorting logic here
        If ComboBox1.SelectedIndex > 0 Then ' If not "Sort by"
            Dim sortColumn As String = ComboBox1.SelectedItem.ToString()
            SortDataGridView(sortColumn)
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedItem.ToString() = "All" Then
            UpdateDataGridView() ' Show all records
        ElseIf ComboBox2.SelectedIndex > 0 Then ' If not "Filter by Class"
            Dim filterClass As String = ComboBox2.SelectedItem.ToString()
            FilterDataGridView(filterClass)
        End If
    End Sub

    Private Sub SortDataGridView(columnName As String)
        Try
            ' Sort the DataGridView based on the selected column
            If columnName = "Name" Then
                DataGridView1.Sort(DataGridView1.Columns("Name"), System.ComponentModel.ListSortDirection.Ascending)
            ElseIf columnName = "Class" Then
                DataGridView1.Sort(DataGridView1.Columns("Class"), System.ComponentModel.ListSortDirection.Ascending)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error sorting data: {ex.Message}", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterDataGridView(classFilter As String)
        Try
            ' Clear existing rows
            DataGridView1.Rows.Clear()

            ' Read data from Excel file
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = Nothing
            Dim worksheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                worksheet = workbook.Sheets(2)
                Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

                ' Read and filter data
                For i As Integer = 2 To lastRow ' Assuming row 1 has headers
                    Dim userId As String = Convert.ToString(worksheet.Cells(i, 1).Value)
                    Dim name As String = Convert.ToString(worksheet.Cells(i, 2).Value)
                    Dim surname As String = Convert.ToString(worksheet.Cells(i, 3).Value)
                    Dim class_value As String = Convert.ToString(worksheet.Cells(i, 4).Value)
                    Dim dateValue = worksheet.Cells(i, 5).Value

                    ' Process date value
                    Dim formattedDate As String = ""
                    If dateValue IsNot Nothing Then
                        Try
                            If TypeOf dateValue Is Double Then
                                formattedDate = DateTime.FromOADate(Convert.ToDouble(dateValue)).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is DateTime Then
                                formattedDate = Convert.ToDateTime(dateValue).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is String Then
                                Dim parsedDate As DateTime
                                If DateTime.TryParse(dateValue.ToString(), parsedDate) Then
                                    formattedDate = parsedDate.ToString("MM/dd/yyyy")
                                Else
                                    formattedDate = dateValue.ToString()
                                End If
                            End If
                        Catch ex As Exception
                            formattedDate = dateValue.ToString()
                        End Try
                    End If

                    ' Only add row if name exists and (if All is selected OR class matches filter)
                    If Not String.IsNullOrEmpty(name) Then
                        If classFilter = "All" OrElse
                       class_value.Trim().Equals(classFilter.Replace("Class ", ""), StringComparison.OrdinalIgnoreCase) Then
                            DataGridView1.Rows.Add(userId, name, surname, class_value, formattedDate)
                        End If
                    End If
                Next

            Finally
                ' Clean up Excel objects
                If workbook IsNot Nothing Then
                    workbook.Close(False)
                    ReleaseObject(workbook)
                End If
                If worksheet IsNot Nothing Then
                    ReleaseObject(worksheet)
                End If
                excelApp.Quit()
                ReleaseObject(excelApp)
            End Try

        Catch ex As Exception
            MessageBox.Show($"Error filtering data: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class