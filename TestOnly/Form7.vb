Imports Excel = Microsoft.Office.Interop.Excel
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Util.Store
Imports System.Threading
Imports System.IO

Public Class Form7
    Private pendingDeletionsPath As String = Path.Combine(Application.StartupPath, "pending_deletions.csv")
    Private WithEvents syncTimer As System.Windows.Forms.Timer
    Private isSyncing As Boolean = False

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox3.Enabled = False
        ' Initialize ComboBoxes
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Select a class")
        ComboBox1.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Filter by Class")
        ComboBox2.Items.AddRange(New String() {"All", "Class A", "Class B", "Class C"})
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.SelectedIndex = 0

        ' Initialize DataGridView
        InitializeDataGridViewColumns()

        ' Initialize network monitoring and timer
        InitializeTimer()
        InitializeNetworkMonitoring()

        ' Load data
        LoadData()
    End Sub

    Private Sub InitializeTimer()
        syncTimer = New System.Windows.Forms.Timer With {
            .Interval = 1000, ' Check every second
            .Enabled = True
        }
    End Sub

    Private Sub InitializeNetworkMonitoring()
        AddHandler System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged,
            AddressOf NetworkAvailabilityChanged
    End Sub

    Private Async Function IsInternetAvailableAsync() As Task(Of Boolean)
        Try
            Using client = New Net.Http.HttpClient()
                client.Timeout = TimeSpan.FromSeconds(2)
                Dim response = Await client.GetAsync("http://www.google.com")
                Return response.IsSuccessStatusCode
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Async Sub NetworkAvailabilityChanged(sender As Object, e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
        If e.IsAvailable Then
            Try
                If Await IsInternetAvailableAsync() Then
                    SyncPendingDeletions()
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error during network sync: {ex.Message}")
            End Try
        End If
    End Sub

    Private Async Sub syncTimer_Tick(sender As Object, e As EventArgs) Handles syncTimer.Tick
        If Await IsInternetAvailableAsync() Then
            SyncPendingDeletions()
        End If
    End Sub

    Private Sub SyncPendingDeletions()
        If isSyncing OrElse Not File.Exists(pendingDeletionsPath) Then Return

        Try
            isSyncing = True
            Dim pendingLines = File.ReadAllLines(pendingDeletionsPath).ToList()
            If pendingLines.Count = 0 Then Return

            Dim failedDeletions As New List(Of String)
            Dim syncedCount As Integer = 0

            For Each line In pendingLines
                Try
                    Dim fields = line.Split(","c)
                    If fields.Length = 2 Then
                        DeleteFromGoogleSheets(fields(0), fields(1))
                        syncedCount += 1
                        Threading.Thread.Sleep(100) ' Small delay between deletions
                    End If
                Catch ex As Exception
                    failedDeletions.Add(line)
                    Debug.WriteLine($"Failed to sync deletion: {line}, Error: {ex.Message}")
                End Try
            Next

            ' Update the pending deletions file
            If failedDeletions.Count = 0 Then
                File.Delete(pendingDeletionsPath)
            Else
                File.WriteAllLines(pendingDeletionsPath, failedDeletions)
            End If

            If syncedCount > 0 Then
                MessageBox.Show($"Synced {syncedCount} pending deletions to Google Sheets.", "Sync Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Debug.WriteLine($"Error syncing deletions: {ex.Message}")
        Finally
            isSyncing = False
        End Try
    End Sub

    Private Sub InitializeDataGridViewColumns()
        ' Configure DataGridView properties
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.Font = New Font("Poppins", 10, FontStyle.Regular)
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Clear and add columns
        DataGridView1.Columns.Clear()
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

        ' Set date column format
        'DataGridView1.Columns("Date").DefaultCellStyle.Format = "MM/dd/yyyy"
    End Sub

    Public Sub LoadData()
        Try
            DataGridView1.Rows.Clear()
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = Nothing
            Dim worksheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                worksheet = workbook.Sheets(2)
                Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

                For i As Integer = 2 To lastRow
                    Dim name As String = Convert.ToString(worksheet.Cells(i, 1).Value)
                    Dim surname As String = Convert.ToString(worksheet.Cells(i, 2).Value)
                    Dim class_value As String = Convert.ToString(worksheet.Cells(i, 3).Value)

                    ' Handle date
                    Dim dateValue = worksheet.Cells(i, 4).Value
                    Dim formattedDate As String = ""
                    If dateValue IsNot Nothing Then
                        Try
                            If TypeOf dateValue Is Double Then
                                formattedDate = DateTime.FromOADate(CDbl(dateValue)).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is DateTime Then
                                formattedDate = Convert.ToDateTime(dateValue).ToString("MM/dd/yyyy")
                            Else
                                formattedDate = dateValue.ToString()
                            End If
                        Catch
                            formattedDate = dateValue.ToString()
                        End Try
                    End If

                    ' Only add row if name exists
                    If Not String.IsNullOrEmpty(name) Then
                        DataGridView1.Rows.Add(name, surname, class_value, formattedDate)
                    End If
                Next

            Finally
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
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterDataGridView(classFilter As String)
        Try
            DataGridView1.Rows.Clear()
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = Nothing
            Dim worksheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                worksheet = workbook.Sheets(2)
                Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

                For i As Integer = 2 To lastRow
                    Dim name As String = Convert.ToString(worksheet.Cells(i, 1).Value)
                    Dim surname As String = Convert.ToString(worksheet.Cells(i, 2).Value)
                    Dim class_value As String = Convert.ToString(worksheet.Cells(i, 3).Value)
                    Dim dateValue = worksheet.Cells(i, 4).Value

                    ' Process date value
                    Dim formattedDate As String = ""
                    If dateValue IsNot Nothing Then
                        Try
                            If TypeOf dateValue Is Double Then
                                formattedDate = DateTime.FromOADate(CDbl(dateValue)).ToString("MM/dd/yyyy")
                            ElseIf TypeOf dateValue Is DateTime Then
                                formattedDate = Convert.ToDateTime(dateValue).ToString("MM/dd/yyyy")
                            Else
                                formattedDate = dateValue.ToString()
                            End If
                        Catch
                            formattedDate = dateValue.ToString()
                        End Try
                    End If

                    ' Only add row if name exists and class matches filter
                    If Not String.IsNullOrEmpty(name) Then
                        If classFilter = "All" OrElse
                           class_value.Trim().Equals(classFilter.Replace("Class ", ""), StringComparison.OrdinalIgnoreCase) Then
                            DataGridView1.Rows.Add(name, surname, class_value, formattedDate)
                        End If
                    End If
                Next

            Finally
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Check if a row is selected
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the values from selected row
            Dim selectedName As String = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
            Dim selectedSurname As String = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()

            ' Ask for confirmation
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete the record for {selectedName} {selectedSurname}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Delete from Excel first
                DeleteFromExcel(selectedName, selectedSurname)

                Try
                    DeleteFromGoogleSheets(selectedName, selectedSurname)
                    MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ' Save for later deletion
                    SavePendingDeletion(selectedName, selectedSurname)
                    MessageBox.Show("Record deleted locally. Will delete from online storage when internet is restored.", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

                ' Refresh the DataGridView
                LoadData()

                ' Clear the textboxes and reset combobox
                TextBox1.Clear()
                TextBox2.Clear()
                ComboBox1.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show($"Error deleting record: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteFromExcel(name As String, surname As String)
        Dim excelApp As New Excel.Application()
        Dim workbook As Excel.Workbook = Nothing
        Dim worksheet As Excel.Worksheet = Nothing

        Try
            workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            worksheet = workbook.Sheets(2)
            Dim lastRow As Integer = worksheet.UsedRange.Rows.Count

            ' Find and delete the matching row
            For i As Integer = 2 To lastRow
                Dim currentName As String = Convert.ToString(worksheet.Cells(i, 1).Value)
                Dim currentSurname As String = Convert.ToString(worksheet.Cells(i, 2).Value)

                If currentName = name AndAlso currentSurname = surname Then
                    worksheet.Rows(i).Delete()
                    workbook.Save()
                    Exit For
                End If
            Next

        Finally
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
    End Sub

    Private Sub DeleteFromGoogleSheets(name As String, surname As String)
        Try
            ' Initialize Google Sheets service
            Dim Scopes As String() = {SheetsService.Scope.Spreadsheets}
            Dim service As New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(
                New ClientSecrets With {
                    .ClientId = "1089286082984-b7gqp3181vfmgd40p8slhtgfci9g5dlm.apps.googleusercontent.com",
                    .ClientSecret = "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"
                },
                Scopes, "user", CancellationToken.None, New FileDataStore("MyAppsToken")).Result,
            .ApplicationName = "Google Sheets API .NET Quickstart"
        })

            ' Get the current data
            Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest =
            service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "data-received!A:E")
            Dim getResponse As ValueRange = getRequest.Execute()
            Dim values As IList(Of IList(Of Object)) = getResponse.Values

            ' Find the row to delete
            Dim rowIndex As Integer = -1
            For i As Integer = 0 To values.Count - 1
                If values(i).Count >= 2 AndAlso
               values(i)(0).ToString() = name AndAlso
               values(i)(1).ToString() = surname Then
                    rowIndex = i + 1  ' +1 because Google Sheets is 1-based
                    Exit For
                End If
            Next

            If rowIndex > 0 Then
                ' Clear the row's contents
                Dim range As String = $"data-received!A{rowIndex}:E{rowIndex}"
                Dim clearRequest As New ClearValuesRequest()
                service.Spreadsheets.Values.Clear(clearRequest, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range).Execute()
            End If

        Catch ex As Exception
            Throw New Exception($"Google Sheets delete error: {ex.Message}")
        End Try
    End Sub

    Private Sub SavePendingDeletion(name As String, surname As String)
        Try
            Dim deletionLine As String = $"{name},{surname}"
            File.AppendAllText(pendingDeletionsPath, deletionLine & Environment.NewLine)
        Catch ex As Exception
            Debug.WriteLine($"Error saving pending deletion: {ex.Message}")
        End Try
    End Sub

    ' Event handlers and utility methods
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub

    Public Sub UpdateDataGridView()
        LoadData()
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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedItem.ToString() = "All" Then
            LoadData() ' Show all records
        ElseIf ComboBox2.SelectedIndex > 0 Then ' If not "Filter by Class"
            Dim filterClass As String = ComboBox2.SelectedItem.ToString()
            FilterDataGridView(filterClass)
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            ' Check if a valid row is selected (not header, not empty)
            If e.RowIndex >= 0 AndAlso DataGridView1.Rows(e.RowIndex).Cells(1).Value IsNot Nothing Then
                ' Get values from selected row using column indices
                Dim selectedName As String = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString()
                Dim selectedSurname As String = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString()

                ' Populate the textboxes
                TextBox1.Text = selectedName
                TextBox2.Text = selectedSurname
            End If
        Catch ex As Exception
            MessageBox.Show($"Error selecting record: {ex.Message}", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
    Try
        If DataGridView1.CurrentRow IsNot Nothing Then
                ' Get values from selected row using column indices
                Dim selectedUserId As String = DataGridView1.CurrentRow.Cells(0).Value.ToString()
                Dim selectedName As String = DataGridView1.CurrentRow.Cells(1).Value.ToString()
                Dim selectedSurname As String = DataGridView1.CurrentRow.Cells(2).Value.ToString()
                Dim selectedClass As String = DataGridView1.CurrentRow.Cells(3).Value.ToString()

                ' Populate the TextBoxes and ComboBox
                TextBox1.Text = selectedName
                TextBox2.Text = selectedSurname
                TextBox3.Text = selectedUserId

                ' Set the class in ComboBox1
                Dim classValue As String = $"Class {selectedClass}"
            Dim classIndex As Integer = ComboBox1.Items.IndexOf(classValue)
            If classIndex >= 0 Then
                ComboBox1.SelectedIndex = classIndex
            End If
        End If
    Catch ex As Exception
        MessageBox.Show($"Error selecting record: {ex.Message}", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Validate inputs
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
           ComboBox1.SelectedIndex <= 0 Then
                MessageBox.Show("Please fill in all fields and select a class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Check if a row is selected
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the new values
            Dim newName = TextBox1.Text.Trim
            Dim newSurname = TextBox2.Text.Trim
            Dim newClass = ComboBox1.SelectedItem.ToString.Replace("Class ", "")

            ' Get the original values for matching
            Dim originalUserId = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
            Dim originalName = DataGridView1.SelectedRows(0).Cells(1).Value.ToString
            Dim originalSurname = DataGridView1.SelectedRows(0).Cells(2).Value.ToString

            ' Update Excel
            UpdateExcel(originalUserId, originalName, originalSurname, newName, newSurname, newClass)

            ' Update Google Sheets
            UpdateGoogleSheets(originalUserId, originalName, originalSurname, newName, newSurname, newClass)

            MessageBox.Show("Student details updated successfully in both Excel and Google Sheets!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the DataGridView
            LoadData()
        Catch ex As Exception
            MessageBox.Show($"Error updating student details: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateExcel(originalUserId As String, originalName As String, originalSurname As String, newName As String, newSurname As String, newClass As String)
        Dim excelApp As New Excel.Application()
        Dim workbook As Excel.Workbook = Nothing
        Dim studentsSheet As Excel.Worksheet = Nothing
        Dim attendanceSheet As Excel.Worksheet = Nothing

        Try
            workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            studentsSheet = workbook.Sheets(2)  ' Student details sheet
            attendanceSheet = workbook.Sheets(3) ' Attendance sheet

            ' Update Sheet2 (Student Details)
            Dim lastRowSheet2 As Integer = studentsSheet.UsedRange.Rows.Count
            For i As Integer = 2 To lastRowSheet2
                Dim currentUserId As String = Convert.ToString(studentsSheet.Cells(i, 1).Value)
                Dim currentName As String = Convert.ToString(studentsSheet.Cells(i, 2).Value)
                Dim currentSurname As String = Convert.ToString(studentsSheet.Cells(i, 3).Value)

                If currentUserId = originalUserId AndAlso currentName = originalName AndAlso currentSurname = originalSurname Then
                    studentsSheet.Cells(i, 2).Value = newName
                    studentsSheet.Cells(i, 3).Value = newSurname
                    studentsSheet.Cells(i, 4).Value = newClass
                    Exit For
                End If
            Next

            ' Update Sheet3 (Attendance Records)
            Dim lastRowSheet3 As Integer = attendanceSheet.UsedRange.Rows.Count
            For i As Integer = 2 To lastRowSheet3
                Dim currentUserId As String = Convert.ToString(attendanceSheet.Cells(i, 1).Value)
                If currentUserId = originalUserId Then
                    ' Update name, surname, and class in attendance record
                    attendanceSheet.Cells(i, 2).Value = newName     ' First Name column
                    attendanceSheet.Cells(i, 3).Value = newSurname  ' Surname column
                    attendanceSheet.Cells(i, 6).Value = newClass    ' Class column
                End If
            Next

            ' Save the changes
            workbook.Save()

            ' Update Form8 if it's open
            For Each form In Application.OpenForms
                If TypeOf form Is Form8 Then
                    DirectCast(form, Form8).UpdateDataGridView()
                    Exit For
                End If
            Next

        Finally
            If studentsSheet IsNot Nothing Then ReleaseObject(studentsSheet)
            If attendanceSheet IsNot Nothing Then ReleaseObject(attendanceSheet)
            If workbook IsNot Nothing Then
                workbook.Close(False)
                ReleaseObject(workbook)
            End If
            excelApp.Quit()
            ReleaseObject(excelApp)
        End Try
    End Sub

    Private Sub UpdateGoogleSheets(originalUserId As String, originalName As String, originalSurname As String, newName As String, newSurname As String, newClass As String)
        Try
            ' Initialize Google Sheets service
            Dim Scopes As String() = {SheetsService.Scope.Spreadsheets}
            Dim service As New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(
                New ClientSecrets With {
                    .ClientId = "1089286082984-b7gqp3181vfmgd40p8slhtgfci9g5dlm.apps.googleusercontent.com",
                    .ClientSecret = "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"
                },
                Scopes, "user", CancellationToken.None, New FileDataStore("MyAppsToken")).Result,
            .ApplicationName = "Google Sheets API .NET Quickstart"
        })

            ' Update student details in data-received sheet
            UpdateGoogleSheetsStudentDetails(service, originalUserId, originalName, originalSurname, newName, newSurname, newClass)

            ' Update attendance records in attendance sheet
            UpdateGoogleSheetsAttendance(service, originalUserId, newName, newSurname, newClass)

        Catch ex As Exception
            Throw New Exception($"Google Sheets update error: {ex.Message}")
        End Try
    End Sub

    Private Sub UpdateGoogleSheetsStudentDetails(service As SheetsService, originalUserId As String, originalName As String,
                                           originalSurname As String, newName As String, newSurname As String, newClass As String)
        ' Get the current data from student details sheet
        Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest =
            service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "data-received!A:E")
        Dim getResponse As ValueRange = getRequest.Execute()
        Dim values As IList(Of IList(Of Object)) = getResponse.Values

        ' Find and update student details
        Dim rowIndex As Integer = -1
        For i As Integer = 0 To values.Count - 1
            If values(i).Count >= 3 AndAlso
               values(i)(0).ToString() = originalUserId AndAlso
               values(i)(1).ToString() = originalName AndAlso
               values(i)(2).ToString() = originalSurname Then
                rowIndex = i + 1
                Exit For
            End If
        Next

        If rowIndex > 0 Then
            Dim range As String = $"data-received!A{rowIndex}:E{rowIndex}"
            Dim updateData As New ValueRange() With {
                .Values = New List(Of IList(Of Object)) From {
                    New List(Of Object) From {originalUserId, newName, newSurname, newClass}
                }
            }

            Dim updateRequest = service.Spreadsheets.Values.Update(updateData,
                "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()
        End If
    End Sub

    Private Sub UpdateGoogleSheetsAttendance(service As SheetsService, userId As String, newName As String,
                                           newSurname As String, newClass As String)
        ' Get the current data from attendance sheet
        Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest =
            service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "attendance!A:F")
        Dim getResponse As ValueRange = getRequest.Execute()
        Dim values As IList(Of IList(Of Object)) = getResponse.Values

        ' Find and update all attendance records for this user
        For i As Integer = 0 To values.Count - 1
            If values(i).Count >= 1 AndAlso values(i)(0).ToString() = userId Then
                Dim rowIndex As Integer = i + 1
                Dim range As String = $"attendance!A{rowIndex}:F{rowIndex}"

                ' Preserve existing time and date
                Dim timeIn As String = If(values(i).Count >= 4, values(i)(3).ToString(), "")
                Dim date_ As String = If(values(i).Count >= 5, values(i)(4).ToString(), "")

                Dim updateData As New ValueRange() With {
                    .Values = New List(Of IList(Of Object)) From {
                        New List(Of Object) From {userId, newName, newSurname, timeIn, date_, newClass}
                    }
                }

                Dim updateRequest = service.Spreadsheets.Values.Update(updateData,
                    "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
                updateRequest.Execute()
            End If
        Next
    End Sub

End Class