Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Sheets.v4
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports Google.Apis.Util.Store
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class Form1
    Private WithEvents syncTimer As System.Windows.Forms.Timer
    Private ReadOnly pendingDataPath As String = Path.Combine(Application.StartupPath, "pending_uploads.csv")
    Private isSyncing As Boolean = False
    Private lastConnectionStatus As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTimer()
        CheckAndSyncPendingData()
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

    Private Sub NetworkAvailabilityChanged(sender As Object, e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
        If e.IsAvailable Then
            CheckAndSyncPendingData()
        End If
    End Sub

    Private Async Sub CheckAndSyncPendingData()
        If isSyncing OrElse Not File.Exists(pendingDataPath) Then Return

        Try
            isSyncing = True
            Dim currentConnectionStatus = Await IsInternetAvailableAsync()

            ' Check if connection status changed from offline to online
            If currentConnectionStatus AndAlso Not lastConnectionStatus Then
                SyncPendingData()
            End If

            lastConnectionStatus = currentConnectionStatus
        Finally
            isSyncing = False
        End Try
    End Sub

    Private Async Sub AddData_Click(sender As Object, e As EventArgs) Handles AddData.Click
        If Not ValidateInputs() Then Return

        Dim name As String = TBox_Name.Text
        Dim surname As String = TBox_Surname.Text
        Dim age As String = TBox_Age.Text
        Dim registrationTime As String = DateTime.Now.ToString("hh:mm tt")
        Dim registrationDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        Try
            AddDataToExcel(name, surname, age, registrationTime, registrationDate)

            If Await IsInternetAvailableAsync() Then
                Try
                    AddDataToGoogleSheets(name, surname, age, registrationTime, registrationDate)
                    ShowStatusMessage("Data saved online successfully!", MessageBoxIcon.Information)
                Catch ex As Exception
                    HandleOfflineSave(name, surname, age, registrationTime, registrationDate)
                End Try
            Else
                HandleOfflineSave(name, surname, age, registrationTime, registrationDate)
            End If

            ClearInputFields()
        Catch ex As Exception
            ShowStatusMessage($"Error: {ex.Message}", MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(TBox_Name.Text) OrElse
           String.IsNullOrWhiteSpace(TBox_Surname.Text) OrElse
           String.IsNullOrWhiteSpace(TBox_Age.Text) Then
            ShowStatusMessage("Please fill in all fields.", MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub HandleOfflineSave(name As String, surname As String, age As String,
                                registrationTime As String, registrationDate As String)
        SavePendingOnlineData(name, surname, age, registrationTime, registrationDate)
        ShowStatusMessage("Data saved locally. Will sync when online.", MessageBoxIcon.Information)
    End Sub

    Private Sub ShowStatusMessage(message As String, icon As MessageBoxIcon)
        MessageBox.Show(message, "Status", MessageBoxButtons.OK, icon)
    End Sub

    Private Sub SavePendingOnlineData(name As String, surname As String, age As String,
                                    registrationTime As String, registrationDate As String)
        Try
            Dim dataLine As String = $"{name},{surname},{age},{registrationTime},{registrationDate}"
            File.AppendAllText(pendingDataPath, dataLine & Environment.NewLine)
        Catch ex As Exception
            ShowStatusMessage($"Failed to save pending data: {ex.Message}", MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SyncPendingData()
        If Not File.Exists(pendingDataPath) Then Return

        Try
            Dim pendingLines = File.ReadAllLines(pendingDataPath).ToList()
            If pendingLines.Count = 0 Then Return

            Dim failedUploads As New List(Of String)
            Dim syncedCount As Integer = 0

            For Each line In pendingLines
                Try
                    Dim fields = line.Split(","c)
                    If fields.Length = 5 Then
                        AddDataToGoogleSheets(fields(0), fields(1), fields(2), fields(3), fields(4))
                        syncedCount += 1
                    End If
                Catch ex As Exception
                    failedUploads.Add(line)
                End Try
            Next

            UpdatePendingFile(failedUploads)
            If syncedCount > 0 Then
                ShowStatusMessage($"Synced {syncedCount} records to Google Sheets.", MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ShowStatusMessage($"Sync error: {ex.Message}", MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdatePendingFile(failedUploads As List(Of String))
        If failedUploads.Count = 0 Then
            File.Delete(pendingDataPath)
        Else
            File.WriteAllLines(pendingDataPath, failedUploads)
        End If
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

    Private Sub syncTimer_Tick(sender As Object, e As EventArgs) Handles syncTimer.Tick
        CheckAndSyncPendingData()
    End Sub

    Private Sub ClearInputFields()
        TBox_Name.Clear()
        TBox_Surname.Clear()
        TBox_Age.Clear()
    End Sub

    Private Sub AddDataToGoogleSheets(name As String, surname As String, age As String, registrationTime As String, registrationDate As String)
        Try
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

            ' Get the current data from the sheet
            Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest = service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "data-received!A:E")
            Dim getResponse As ValueRange = getRequest.Execute()
            Dim values As IList(Of IList(Of Object)) = getResponse.Values

            ' Calculate the next row to insert data
            Dim nextRow As Integer = If(values IsNot Nothing, values.Count + 1, 1)

            ' Check if the last entry's date is different from the current date to avoid empty rows
            If values IsNot Nothing AndAlso values.Count > 0 Then
                Dim lastRowDate As String = If(values(values.Count - 1).Count >= 5, values(values.Count - 1)(4).ToString(), "")

                If lastRowDate <> registrationDate Then
                    ' Add a new row for the new date
                    nextRow += 1
                End If
            End If

            Dim range As String = $"data-received!A{nextRow}:E{nextRow}"

            ' Prepare data to insert
            Dim valueRange As New ValueRange() With {
            .Values = New List(Of IList(Of Object)) From {New List(Of Object) From {name, surname, age, registrationTime, registrationDate}}
        }

            Dim updateRequest As SpreadsheetsResource.ValuesResource.UpdateRequest = service.Spreadsheets.Values.Update(valueRange, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()

        Catch ex As Exception
            Throw New Exception($"Google Sheets API error: {ex.Message}")
        End Try
    End Sub


    Private Sub AddDataToExcel(name As String, surname As String, age As String, registrationTime As String, registrationDate As String)
        Try
            Dim excelApp As New Excel.Application()
            Dim workbook As Excel.Workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            Dim worksheet As Excel.Worksheet = CType(workbook.Sheets(1), Excel.Worksheet)

            ' Find the last used row in the sheet
            Dim lastRow As Integer = worksheet.UsedRange.Rows.Count
            Dim lastEntryDate As Date

            ' Check if there is a last entry and if it has a value in the date column
            If lastRow > 1 AndAlso worksheet.Cells(lastRow, 5).Value IsNot Nothing Then
                lastEntryDate = Convert.ToDateTime(worksheet.Cells(lastRow, 5).Value)
            End If

            ' Check if the last entry date is different from the current registration date
            If lastRow > 1 AndAlso lastEntryDate.Date <> Convert.ToDateTime(registrationDate).Date Then
                ' Add an empty row to separate data from the previous day
                lastRow += 1
            End If

            ' Move to the next row for new data entry
            lastRow += 1

            ' Add the data to the appropriate row
            worksheet.Cells(lastRow, 1).Value = name
            worksheet.Cells(lastRow, 2).Value = surname
            worksheet.Cells(lastRow, 3).Value = age
            worksheet.Cells(lastRow, 4).Value = registrationTime
            worksheet.Cells(lastRow, 5).Value = registrationDate

            ' Save and close the workbook
            workbook.Save()
            workbook.Close()
            excelApp.Quit()

            ' Release Excel objects from memory
            ReleaseObject(worksheet)
            ReleaseObject(workbook)
            ReleaseObject(excelApp)

        Catch ex As Exception
            Throw New Exception($"Excel operation error: {ex.Message}")
        End Try
    End Sub

    ' Helper function to release COM objects
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



    Private Sub SaveDataToAccess(name As String, surname As String, age As String)
        Try
            Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\quiam\Documents\BiometricAttendance.accdb;Persist Security Info=False;"

            Using connection As New OleDbConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO [UserTable] ([FirstName], [LastName], [UserAge]) VALUES (?, ?, ?)"

                Using command As New OleDbCommand(query, connection)
                    command.Parameters.AddWithValue("?", name)
                    command.Parameters.AddWithValue("?", surname)
                    command.Parameters.AddWithValue("?", Convert.ToInt32(age))

                    command.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"MS Access operation error: {ex.Message}")
        End Try
    End Sub

    Private Sub UpdateData_Click(sender As Object, e As EventArgs) Handles UpdateData.Click
        Dim form As New Form2()
        form.ShowDialog()
    End Sub

End Class

