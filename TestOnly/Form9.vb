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
Imports System.Diagnostics
Imports System.Linq
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO
Imports System.IO.Ports
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports Google.Apis.Util.Store
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Runtime.InteropServices

Public Class Form9
    Private WithEvents syncTimer As System.Windows.Forms.Timer
    Private ReadOnly pendingDataPath As String = Path.Combine(Application.StartupPath, "pending_uploads.csv")
    Private isSyncing As Boolean = False
    Private lastConnectionStatus As Boolean = False

    'Private WithEvents TBox_UserId As TextBox


    ' Serial Port additions
    ' Private WithEvents serialPort1 As New System.IO.Ports.SerialPort(components)

    Private WithEvents serialPort1 As System.IO.Ports.SerialPort
    Private statusLabel As Label
    Private buffer As String = ""
    Private isSerialConnected As Boolean = False

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize status label first
        statusLabel = New Label()
        statusLabel.Name = "lblSerialStatus"
        statusLabel.AutoSize = True
        statusLabel.Location = New Point(10, Me.Height - 50)
        Me.Controls.Add(statusLabel)

        ' Initialize SerialPort
        serialPort1 = New System.IO.Ports.SerialPort()

        ' Rest of initialization
        InitializeNetworkMonitoring()
        InitializeTimer()
        InitializeSerialPort()
        CheckAndSyncPendingData()
        UpdateSerialStatus()
    End Sub


    Private Sub InitializeSerialPort()
        Try
            ' Configure serial port without checking ports
            With serialPort1
                .BaudRate = 9600
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
                .ReadTimeout = 500
                .WriteTimeout = 500
                .DtrEnable = True
                .RtsEnable = True
            End With

            isSerialConnected = False
            UpdateSerialStatus()
        Catch ex As Exception
            Debug.WriteLine($"Error initializing serial port: {ex.Message}")
            isSerialConnected = False
            UpdateSerialStatus()
        End Try
    End Sub

    Private Sub InitializeNetworkMonitoring()
        ' Add handler for network changes
        AddHandler System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged,
        AddressOf NetworkAvailabilityChanged
    End Sub


    Private Sub UpdateSerialStatus()
        If statusLabel IsNot Nothing Then
            If isSerialConnected Then
                statusLabel.Text = $"Connected to {serialPort1.PortName}"
                statusLabel.ForeColor = System.Drawing.Color.Green
            Else
                statusLabel.Text = "Scanner Not Connected"
                statusLabel.ForeColor = System.Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub serialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles serialPort1.DataReceived
        Try
            ' Read the incoming data
            Dim incoming As String = serialPort1.ReadLine()

            ' Use Invoke to safely update UI from a different thread
            Me.Invoke(Sub()
                          ProcessSerialData(incoming)
                      End Sub)
        Catch ex As Exception
            Debug.WriteLine($"Error reading serial data: {ex.Message}")
        End Try
    End Sub

    Private Function HasAttendedToday(userId As String) As Boolean
        Try
            ' Check Excel first (Sheet3)
            Dim excelApp As Excel.Application = Nothing
            Dim workbook As Excel.Workbook = Nothing
            Dim attendanceSheet As Excel.Worksheet = Nothing

            Try
                excelApp = New Excel.Application()
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                attendanceSheet = workbook.Sheets(3)  ' Attendance sheet

                Dim lastRow As Integer = attendanceSheet.Cells(attendanceSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
                Dim today As String = DateTime.Now.ToString("MM/dd/yyyy")

                ' Check each row for the same user ID and today's date
                For i As Integer = 2 To lastRow
                    If CStr(attendanceSheet.Cells(i, 1).Value) = userId AndAlso
                   CStr(attendanceSheet.Cells(i, 5).Value) = today Then
                        Return True
                    End If
                Next

                Return False

            Finally
                If attendanceSheet IsNot Nothing Then ReleaseObject(attendanceSheet)
                If workbook IsNot Nothing Then
                    workbook.Close(False)
                    ReleaseObject(workbook)
                End If
                If excelApp IsNot Nothing Then
                    excelApp.Quit()
                    ReleaseObject(excelApp)
                End If
            End Try

        Catch ex As Exception
            MessageBox.Show($"Error checking attendance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub ProcessSerialData(data As String)
        Try
            ' Handle attendance data with duplicate check
            If data.StartsWith("MSGBOX") Then
                Dim parts = data.Split(","c)
                Dim id As String = ""
                Dim status As String = ""

                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 Then
                        Select Case keyValue(0).Trim().ToUpper()
                            Case "ID"
                                id = keyValue(1).Trim()
                            Case "STATUS"
                                status = keyValue(1).Trim()
                        End Select
                    End If
                Next

                ' Show status message and handle user lookup
                If status.ToUpper() = "FOUND" Then
                    Dim details = GetUserDetails(id)
                    If details.firstName <> "Unknown" Then
                        ' Check for duplicate attendance
                        If HasAttendedToday(id) Then
                            MessageBox.Show($"Attendance for {details.firstName} {details.lastName} was already recorded today.",
                                          "Duplicate Attendance",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning)
                            ' Send response back to Arduino
                            If serialPort1 IsNot Nothing AndAlso serialPort1.IsOpen Then
                                serialPort1.WriteLine("DUPLICATE")
                            End If
                        Else
                            ' Save attendance record
                            SaveAttendanceRecord(id, details.firstName, details.lastName)
                            MessageBox.Show($"Attendance recorded for {details.firstName} {details.lastName}",
                                          "Success",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show($"ID {id} not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                ElseIf status.ToUpper() = "NOTFOUND" Then
                    MessageBox.Show("ID not found", "Verification Result", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Return
            End If
            If data.StartsWith("REGISTER") Then
                Dim parts = data.Split(","c)
                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 Then
                        Select Case keyValue(0).Trim().ToUpper()
                            Case "ID"
                                Tbox_UserId.Text = keyValue(1).Trim()
                            Case "NAME"
                                TBox_Name.Text = keyValue(1).Trim()
                            Case "SURNAME"
                                TBox_Surname.Text = keyValue(1).Trim()
                        End Select
                    End If
                Next
                MessageBox.Show("Fingerprint registered successfully. Please modify details before adding.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            'Attendance
            If data.StartsWith("MSGBOX") Then
                Dim parts = data.Split(","c)
                Dim id As String = ""
                Dim status As String = ""

                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 Then
                        Select Case keyValue(0).Trim().ToUpper()
                            Case "ID"
                                id = keyValue(1).Trim()
                            Case "STATUS"
                                status = keyValue(1).Trim()
                        End Select
                    End If
                Next

                ' Show status message and handle user lookup
                If status.ToUpper() = "FOUND" Then
                    Dim details = GetUserDetails(id)
                    If details.firstName <> "Unknown" Then
                        ' Save attendance record
                        SaveAttendanceRecord(id, details.firstName, details.lastName)
                        MessageBox.Show($"Attendance recorded for {details.firstName} {details.lastName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show($"ID {id} not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                ElseIf status.ToUpper() = "NOTFOUND" Then
                    MessageBox.Show("ID not found", "Verification Result", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Return
            End If

            ' Handle attendance data
            If data.StartsWith("ATTENDANCE") Then
                Dim parts = data.Split(","c)
                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 AndAlso keyValue(0).Trim().ToUpper() = "ID" Then
                        Dim userId = keyValue(1).Trim()

                        ' Update Form8 if it's open
                        For Each form In Application.OpenForms
                            If TypeOf form Is Form8 Then
                                DirectCast(form, Form8).ProcessAttendance(userId)
                                Exit For
                            End If
                        Next

                        Return
                    End If
                Next
            End If

            ' Handle regular data
            If data.Contains(",") AndAlso Not data.StartsWith("MSGBOX") AndAlso Not data.StartsWith("ATTENDANCE") AndAlso Not data.StartsWith("REGISTER") Then
                Dim parts = data.Split(","c)
                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 Then
                        Select Case keyValue(0).Trim().ToUpper()
                            Case "ID"
                                Tbox_UserId.Text = keyValue(1).Trim()
                            Case "NAME"
                                TBox_Name.Text = keyValue(1).Trim()
                            Case "SURNAME"
                                TBox_Surname.Text = keyValue(1).Trim()
                        End Select
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show($"Error processing data: {ex.Message}", "Data Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetUserDetails(userId As String) As (firstName As String, lastName As String, className As String)
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim usersSheet As Excel.Worksheet = Nothing

        Try
            excelApp = New Excel.Application
            Dim filePath As String = "C:\Users\quiam\Downloads\Example.xlsx"
            workbook = excelApp.Workbooks.Open(filePath)
            usersSheet = workbook.Sheets(2)  ' Sheet2 contains user details

            Dim lastRow As Integer = usersSheet.Cells(usersSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

            For i As Integer = 2 To lastRow
                If CStr(usersSheet.Cells(i, 1).Value) = userId Then
                    Return (
                    CStr(usersSheet.Cells(i, 2).Value),  ' FirstName
                    CStr(usersSheet.Cells(i, 3).Value),  ' LastName
                    CStr(usersSheet.Cells(i, 4).Value)   ' Class
                )
                End If
            Next

            Return ("Unknown", "Unknown", "Unknown")

        Finally
            If usersSheet IsNot Nothing Then ReleaseObject(usersSheet)
            If workbook IsNot Nothing Then
                workbook.Close(False)
                ReleaseObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                ReleaseObject(excelApp)
            End If
        End Try
    End Function
    Private Function GetUserName(userId As String) As String
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim usersSheet As Excel.Worksheet = Nothing

        Try
            excelApp = New Excel.Application
            Dim filePath As String = "C:\Users\quiam\Downloads\Example.xlsx"
            workbook = excelApp.Workbooks.Open(filePath)
            usersSheet = workbook.Sheets(2)

            Dim lastRow As Integer = usersSheet.Cells(usersSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

            For i As Integer = 2 To lastRow
                If CStr(usersSheet.Cells(i, 1).Value) = userId Then
                    Dim firstName As String = CStr(usersSheet.Cells(i, 2).Value)
                    Dim lastName As String = CStr(usersSheet.Cells(i, 3).Value)
                    Return $"{firstName} {lastName}"
                End If
            Next

            Return "Unknown User"
        Finally
            If usersSheet IsNot Nothing Then ReleaseObject(usersSheet)
            If workbook IsNot Nothing Then
                workbook.Close(False)
                ReleaseObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                ReleaseObject(excelApp)
            End If
        End Try
    End Function

    ' Function to save attendance record
    Private Sub SaveAttendanceRecord(userId As String, name As String, surname As String)
        Try
            ' Use 12-hour format (hh:mm tt) where tt represents AM/PM
            Dim currentTime As String = DateTime.Now.ToString("hh:mm tt")

            ' Save to Google Sheets
            SaveAttendanceToGoogleSheets(userId, name, surname, currentTime)

            ' Save to Excel with 12-hour time format
            SaveAttendanceToExcel(userId, name, surname, DateTime.Now.ToString("yyyy-MM-dd"), currentTime)
        Catch ex As Exception
            MessageBox.Show($"Error saving attendance: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveAttendanceToGoogleSheets(userId As String, firstName As String, surname As String, currentTime As String)
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

            ' Get the current data from the attendance sheet
            Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest = service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "attendance!A:F")
            Dim getResponse As ValueRange = getRequest.Execute()
            Dim values As IList(Of IList(Of Object)) = getResponse.Values

            ' Find the next row
            Dim nextRow As Integer = If(values IsNot Nothing, values.Count + 1, 1)

            ' Set the range for the new row
            Dim range As String = $"attendance!A{nextRow}:F{nextRow}"

            ' Get student's class
            Dim studentClass As String = GetStudentClass(userId)

            ' Current date in MM/dd/yyyy format
            Dim currentDate As String = DateTime.Now.ToString("MM/dd/yyyy")

            ' Prepare data to insert with all fields
            Dim valueRange As New ValueRange() With {
            .Values = New List(Of IList(Of Object)) From {
                New List(Of Object) From {
                    userId,          ' Column A: UserID
                    firstName,       ' Column B: First Name
                    surname,         ' Column C: Surname
                    currentTime,     ' Column D: Time-in (now in 12-hour format)
                    currentDate,     ' Column E: Date
                    studentClass     ' Column F: Class
                }
            }
        }

            ' Update the sheet
            Dim updateRequest As SpreadsheetsResource.ValuesResource.UpdateRequest = service.Spreadsheets.Values.Update(valueRange, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            Dim response = updateRequest.Execute()

        Catch ex As Exception
            Throw New Exception($"Google Sheets API error: {ex.Message}")
        End Try
    End Sub

    ' Helper function to get student's class from Excel database
    Private Function GetStudentClass(userId As String) As String
        Try
            Dim excelApp As New Excel.Application
            Dim workbook As Excel.Workbook = Nothing
            Dim studentsSheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                studentsSheet = workbook.Sheets(2) ' Student database sheet

                ' Get last row
                Dim lastRow As Integer = studentsSheet.Cells(studentsSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

                ' Search for student
                For i As Integer = 2 To lastRow
                    If CStr(studentsSheet.Cells(i, 1).Value) = userId Then
                        Return CStr(studentsSheet.Cells(i, 4).Value) ' Assuming class is in column 4
                    End If
                Next

                Return "" ' Return empty string if student not found

            Finally
                If studentsSheet IsNot Nothing Then Marshal.ReleaseComObject(studentsSheet)
                If workbook IsNot Nothing Then
                    workbook.Close(False)
                    Marshal.ReleaseComObject(workbook)
                End If
                If excelApp IsNot Nothing Then
                    excelApp.Quit()
                    Marshal.ReleaseComObject(excelApp)
                End If
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        Catch ex As Exception
            MessageBox.Show($"Error getting student class: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Private Sub SaveAttendanceToExcel(userId As String, name As String, surname As String, currentDate As String, currentTime As String)
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim worksheet As Excel.Worksheet = Nothing
        Dim studentDetails = GetUserDetails(userId)

        Try
            excelApp = New Excel.Application()
            workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            worksheet = DirectCast(workbook.Sheets(3), Excel.Worksheet)

            Dim lastRow As Integer = worksheet.Cells(worksheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
            lastRow += 1

            worksheet.Cells(lastRow, 1).Value = userId
            worksheet.Cells(lastRow, 2).Value = studentDetails.firstName  ' From Sheet2
            worksheet.Cells(lastRow, 3).Value = studentDetails.lastName   ' From Sheet2
            worksheet.Cells(lastRow, 4).Value = currentTime
            worksheet.Cells(lastRow, 5).Value = currentDate
            worksheet.Cells(lastRow, 6).Value = studentDetails.className  ' Class from Sheet2

            workbook.Save()

        Finally
            If worksheet IsNot Nothing Then ReleaseObject(worksheet)
            If workbook IsNot Nothing Then
                workbook.Close(True)
                ReleaseObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                ReleaseObject(excelApp)
            End If
        End Try
    End Sub

    Private Sub SaveAttendanceToAccess(userId As String, currentDate As String, currentTime As String)
        Try
            Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\quiam\Documents\BiometricAttendance.accdb;Persist Security Info=False;"

            Using connection As New OleDbConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO [AttendanceTable] ([UserID], [AttendanceDate], [AttendanceTime]) VALUES (?, ?, ?)"

                Using command As New OleDbCommand(query, connection)
                    command.Parameters.AddWithValue("?", userId)
                    command.Parameters.AddWithValue("?", Convert.ToDateTime(currentDate))
                    command.Parameters.AddWithValue("?", Convert.ToDateTime(currentTime))

                    command.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"MS Access operation error: {ex.Message}")
        End Try
    End Sub

    Private Sub InitializeTimer()
        syncTimer = New System.Windows.Forms.Timer With {
            .Interval = 1000, ' Check every second
            .Enabled = True
        }
    End Sub

    Private Async Sub NetworkAvailabilityChanged(sender As Object, e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
        If e.IsAvailable Then
            Try
                ' Check if there's actually internet connectivity
                If Await IsInternetAvailableAsync() Then
                    SyncPendingData()
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error during network sync: {ex.Message}")
            End Try
        End If
    End Sub

    Private Async Sub CheckAndSyncPendingData()
        If isSyncing OrElse Not File.Exists(pendingDataPath) Then Return

        Try
            isSyncing = True
            Dim currentConnectionStatus = Await IsInternetAvailableAsync()

            If currentConnectionStatus Then
                SyncPendingData()
            End If

            lastConnectionStatus = currentConnectionStatus
        Finally
            isSyncing = False
        End Try
    End Sub

    Private Async Sub AddData_Click(sender As Object, e As EventArgs) Handles AddData.Click
        If Not ValidateInputs() Then Return

        Dim userId As String = Tbox_UserId.Text
        Dim name As String = TBox_Name.Text
        Dim surname As String = TBox_Surname.Text
        Dim age As String = TBox_Class.Text
        Dim registrationDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        Try
            AddDataToExcel(userId, name, surname, age, registrationDate)

            For Each form In Application.OpenForms
                If TypeOf form Is Form6 Then
                    DirectCast(form, Form6).UpdateDataGridView()
                    Exit For
                End If
            Next

            For Each form In Application.OpenForms
                If TypeOf form Is Form7 Then
                    DirectCast(form, Form7).UpdateDataGridView()
                    Exit For
                End If
            Next

            If Await IsInternetAvailableAsync() Then
                Try
                    AddDataToGoogleSheets(userId, name, surname, age, registrationDate)
                    ' Remove this message since we'll show it after all operations complete
                Catch ex As Exception
                    HandleOfflineSave(userId, name, surname, age, registrationDate)
                End Try
            Else
                HandleOfflineSave(userId, name, surname, age, registrationDate)
            End If

            ShowStatusMessage("Data saved successfully!", MessageBoxIcon.Information)  ' Single message for all operations
            ClearInputFields()
        Catch ex As Exception
            ShowStatusMessage($"Error: {ex.Message}", MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(Tbox_UserId.Text) OrElse
       String.IsNullOrWhiteSpace(TBox_Name.Text) OrElse
       String.IsNullOrWhiteSpace(TBox_Surname.Text) OrElse
       String.IsNullOrWhiteSpace(TBox_Class.Text) Then
            ShowStatusMessage("Please fill in all fields.", MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub HandleOfflineSave(userId As String, name As String, surname As String, age As String, registrationDate As String)
        SavePendingOnlineData(userId, name, surname, age, registrationDate)
        ShowStatusMessage("Data saved locally. Will sync when online.", MessageBoxIcon.Information)
    End Sub

    Private Sub ShowStatusMessage(message As String, icon As MessageBoxIcon)
        MessageBox.Show(message, "Status", MessageBoxButtons.OK, icon)
    End Sub

    Private Sub SavePendingOnlineData(userId As String, name As String, surname As String, age As String, registrationDate As String)
        Try
            Dim dataLine As String = $"{userId}, {name},{surname},{age},{registrationDate}"
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
                    If fields.Length = 5 Then ' Name, Surname, Age, Date
                        AddDataToGoogleSheets(fields(0), fields(1), fields(2), fields(3), fields(4))
                        syncedCount += 1
                        Threading.Thread.Sleep(100) ' Small delay between uploads
                    End If
                Catch ex As Exception
                    failedUploads.Add(line)
                    Debug.WriteLine($"Failed to sync line: {line}, Error: {ex.Message}")
                End Try
            Next

            UpdatePendingFile(failedUploads)
            If syncedCount > 0 Then
                ' Only show sync message if it's not part of an immediate save operation
                If Not isSyncing Then
                    ShowStatusMessage($"Synced {syncedCount} records to Google Sheets.", MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine($"Sync error: {ex.Message}")
            ' Only show error if it's not part of an immediate save operation
            If Not isSyncing Then
                ShowStatusMessage($"Sync error: {ex.Message}", MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub UpdatePendingFile(failedUploads As List(Of String))
        Try
            If failedUploads.Count = 0 Then
                ' If no failed uploads, delete the pending file
                If File.Exists(pendingDataPath) Then
                    File.Delete(pendingDataPath)
                End If
            Else
                ' Write any failed uploads back to the file
                File.WriteAllLines(pendingDataPath, failedUploads)
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error updating pending file: {ex.Message}")
        End Try
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

    Private Async Sub syncTimer_Tick(sender As Object, e As EventArgs) Handles syncTimer.Tick
        If Await IsInternetAvailableAsync() Then
            CheckAndSyncPendingData()
        End If
    End Sub

    Private Sub ClearInputFields()
        Tbox_UserId.Clear()
        TBox_Name.Clear()
        TBox_Surname.Clear()
        TBox_Class.Clear()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Close serial port if open
        If serialPort1 IsNot Nothing AndAlso serialPort1.IsOpen Then
            serialPort1.Close()
        End If
        MyBase.OnFormClosing(e)
    End Sub

    ' Method to send commands to Arduino if needed
    Public Sub SendCommand(command As String)
        Try
            If serialPort1 IsNot Nothing AndAlso serialPort1.IsOpen Then
                serialPort1.WriteLine(command)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error sending command: {ex.Message}", "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add a method to reconnect serial port
    Public Sub ReconnectSerialPort()
        Try
            If serialPort1.IsOpen Then
                serialPort1.Close()
            End If
            InitializeSerialPort()
        Catch ex As Exception
            MessageBox.Show($"Error reconnecting: {ex.Message}", "Reconnection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddDataToGoogleSheets(userId As String, name As String, surname As String, age As String, registrationDate As String)
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

            ' Find the next row without checking dates
            Dim nextRow As Integer = If(values IsNot Nothing, values.Count + 1, 1)

            ' Set the range for the new row
            Dim range As String = $"data-received!A{nextRow}:E{nextRow}"

            ' Prepare data to insert
            Dim valueRange As New ValueRange() With {
            .Values = New List(Of IList(Of Object)) From {New List(Of Object) From {userId, name, surname, age, registrationDate}}
        }

            ' Update the sheet
            Dim updateRequest As SpreadsheetsResource.ValuesResource.UpdateRequest = service.Spreadsheets.Values.Update(valueRange, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()

        Catch ex As Exception
            Throw New Exception($"Google Sheets API error: {ex.Message}")
        End Try
    End Sub


    Private Sub AddDataToExcel(userId As String, name As String, surname As String, age As String, registrationDate As String)
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim worksheet As Excel.Worksheet = Nothing

        Try
            ' Create Excel application instance
            excelApp = New Excel.Application()

            ' Define the file path - consider making this configurable
            Dim filePath As String = "C:\Users\quiam\Downloads\Example.xlsx"

            ' Check if file exists
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException("Excel file not found at: " & filePath)
            End If

            ' Open workbook
            workbook = excelApp.Workbooks.Open(filePath)

            ' Verify workbook has sheets
            If workbook.Sheets.Count < 2 Then
                Throw New Exception("Excel file does not have enough sheets. Expected at least 2 sheets.")
            End If

            ' Get the second worksheet
            worksheet = DirectCast(workbook.Sheets(2), Excel.Worksheet)

            ' Find the last used row in the sheet
            Dim lastRow As Integer = worksheet.Cells(worksheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

            ' Add the data to the next row
            lastRow += 1

            ' Add the data
            worksheet.Cells(lastRow, 1).Value = userId
            worksheet.Cells(lastRow, 2).Value = name
            worksheet.Cells(lastRow, 3).Value = surname
            worksheet.Cells(lastRow, 4).Value = age
            worksheet.Cells(lastRow, 5).Value = registrationDate

            ' Save changes
            workbook.Save()

        Catch ex As Exception
            Throw New Exception($"Excel operation error: {ex.Message}")
        Finally
            ' Clean up Excel objects
            If worksheet IsNot Nothing Then
                ReleaseObject(worksheet)
            End If
            If workbook IsNot Nothing Then
                workbook.Close(False)
                ReleaseObject(workbook)
            End If
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                ReleaseObject(excelApp)
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
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
        'Dim form As New Form5()
        'Form.ShowDialog()
        'Me.Hide()

        Dim form5 As New Form5
        form5.Show()
        Hide()
    End Sub

    Private Sub TestConnection_Click(sender As Object, e As EventArgs) Handles TestConnection.Click
        Try
            ' Close existing connection if any
            If serialPort1 IsNot Nothing AndAlso serialPort1.IsOpen Then
                serialPort1.Close()
                Threading.Thread.Sleep(1000)  ' Wait before reopening
                isSerialConnected = False
                UpdateSerialStatus()
            End If
            ' Get available ports
            Dim ports As String() = SerialPort.GetPortNames()
            If ports.Length = 0 Then
                MessageBox.Show("No serial ports found.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ' Show port selection dialog
            Using portDialog As New Form
                portDialog.Text = "Select COM Port"
                portDialog.Size = New Size(300, 200)
                portDialog.StartPosition = FormStartPosition.CenterParent
                ' Add instructions label
                Dim label As New Label
                label.Text = "Select the Arduino COM port:"
                label.AutoSize = True
                label.Location = New Point(10, 20)
                portDialog.Controls.Add(label)
                ' Add port selection combo box
                Dim combo As New ComboBox
                combo.Location = New Point(10, 50)
                combo.Size = New Size(260, 25)
                combo.DropDownStyle = ComboBoxStyle.DropDownList
                combo.Items.AddRange(ports)
                combo.SelectedIndex = 0
                portDialog.Controls.Add(combo)
                ' Add connection button
                Dim btn As New Button
                btn.Text = "Connect"
                btn.Location = New Point(90, 100)
                btn.Size = New Size(120, 30)
                AddHandler btn.Click, Sub()
                                          Try
                                              serialPort1.PortName = combo.SelectedItem.ToString()
                                              TestSerialConnection()
                                              MessageBox.Show("Connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                              portDialog.DialogResult = DialogResult.OK
                                          Catch ex As Exception
                                              MessageBox.Show($"Connection failed: {ex.Message}" & vbCrLf & vbCrLf &
                                                  "Please check:" & vbCrLf &
                                                  "1. Arduino is properly connected" & vbCrLf &
                                                  "2. Correct COM port is selected" & vbCrLf &
                                                  "3. No other program is using the port",
                                                  "Connection Error",
                                                  MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error)
                                          End Try
                                      End Sub
                portDialog.Controls.Add(btn)
                If portDialog.ShowDialog() = DialogResult.OK Then
                    InitializeTimer()
                    CheckAndSyncPendingData()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            isSerialConnected = False
            UpdateSerialStatus()
        End Try
    End Sub

    Private Sub TestSerialConnection()
        Try
            ' Close port if already open
            If serialPort1.IsOpen Then
                serialPort1.Close()
                Threading.Thread.Sleep(1000)  ' Wait before reopening
            End If

            ' Configure port
            With serialPort1
                .BaudRate = 9600
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .ReadTimeout = 3000
                .WriteTimeout = 3000
                .ReceivedBytesThreshold = 1
                .ReadBufferSize = 4096
                .WriteBufferSize = 4096
            End With

            ' Open port
            serialPort1.Open()
            Threading.Thread.Sleep(2000)  ' Wait for connection to stabilize

            ' Clear any existing data
            serialPort1.DiscardInBuffer()
            serialPort1.DiscardOutBuffer()

            ' Send test command
            serialPort1.WriteLine("TEST")

            ' Wait for response
            Dim startTime As DateTime = DateTime.Now
            While (DateTime.Now - startTime).TotalSeconds < 3
                If serialPort1.BytesToRead > 0 Then
                    Dim response As String = serialPort1.ReadLine().Trim()
                    If response = "OK" Then
                        isSerialConnected = True
                        UpdateSerialStatus()
                        Return
                    End If
                End If
                Threading.Thread.Sleep(100)
                Application.DoEvents()
            End While

            Throw New Exception("No response received from device")

        Catch ex As Exception
            If serialPort1 IsNot Nothing AndAlso serialPort1.IsOpen Then
                serialPort1.Close()
            End If
            MessageBox.Show("Error details: " & ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class