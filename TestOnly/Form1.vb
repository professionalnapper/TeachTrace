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

Public Class Form1
    Private WithEvents syncTimer As System.Windows.Forms.Timer
    Private ReadOnly pendingDataPath As String = Path.Combine(Application.StartupPath, "pending_uploads.csv")
    Private isSyncing As Boolean = False
    Private lastConnectionStatus As Boolean = False

    ' Serial Port additions
    ' Private WithEvents serialPort1 As New System.IO.Ports.SerialPort(components)

    Private WithEvents serialPort1 As System.IO.Ports.SerialPort
    Private statusLabel As Label
    Private buffer As String = ""
    Private isSerialConnected As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize status label first
        statusLabel = New Label()
        statusLabel.Name = "lblSerialStatus"
        statusLabel.AutoSize = True
        statusLabel.Location = New Point(10, Me.Height - 50)
        Me.Controls.Add(statusLabel)

        ' Initialize SerialPort
        serialPort1 = New System.IO.Ports.SerialPort()

        ' Rest of initialization
        InitializeTimer()
        InitializeSerialPort()
        CheckAndSyncPendingData()
        UpdateSerialStatus()
    End Sub


    Private Sub InitializeSerialPort()
        Try
            ' Get available ports
            Dim ports As String() = SerialPort.GetPortNames()
            If ports.Length = 0 Then
                MessageBox.Show("No serial ports found.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Configure serial port
            With serialPort1
                .PortName = ports(0) ' Default to first available port
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

            serialPort1.Open()
            isSerialConnected = True
            UpdateSerialStatus()
        Catch ex As Exception
            MessageBox.Show($"Error initializing serial port: {ex.Message}", "Serial Port Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            isSerialConnected = False
            UpdateSerialStatus()
        End Try
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

    Private Sub ProcessSerialData(data As String)
        Try
            ' Expected format: "ID:123,NAME:John,SURNAME:Doe,AGE:25"
            If data.Contains(",") Then
                Dim parts = data.Split(","c)
                For Each part In parts
                    Dim keyValue = part.Split(":"c)
                    If keyValue.Length = 2 Then
                        Select Case keyValue(0).Trim().ToUpper()
                            Case "NAME"
                                TBox_Name.Text = keyValue(1).Trim()
                            Case "SURNAME"
                                TBox_Surname.Text = keyValue(1).Trim()
                            Case "AGE"
                                TBox_Age.Text = keyValue(1).Trim()
                        End Select
                    End If
                Next

                ' Optionally auto-save the data
                If ValidateInputs() Then
                    AddData_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show($"Error processing data: {ex.Message}", "Data Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
End Class

