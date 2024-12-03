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

Public Class Form8
    Private titleLabel As New Label
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add dynamic date label
        Dim titleLabel As New Label
        Label1.Text = $"Today's Attendance ({DateTime.Now:MM/dd/yyyy})"
        titleLabel.Location = New Point(183, 210)
        titleLabel.AutoSize = True
        Me.Controls.Add(titleLabel)

        ' Rest of your existing Form8_Load code
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Filter by Class")
        ComboBox1.Items.AddRange(New String() {"Class A", "Class B", "Class C"})
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = 0

        DataGridView1.ColumnCount = 5
        DataGridView1.Columns(0).HeaderText = "First Name"
        DataGridView1.Columns(1).HeaderText = "Surname"
        DataGridView1.Columns(2).HeaderText = "Time-in"
        DataGridView1.Columns(3).HeaderText = "Date"
        DataGridView1.Columns(4).HeaderText = "Class"
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False

        UpdateDataGridView()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        UpdateDataGridView()
    End Sub

    Private Function HasAttendanceToday(userId As String) As Boolean
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim attendanceSheet As Excel.Worksheet = Nothing

        Try
            excelApp = New Excel.Application
            workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            attendanceSheet = workbook.Sheets(3)  ' Attendance sheet

            ' Get today's date
            Dim today As DateTime = DateTime.Today
            Dim todayStr As String = today.ToString("MM/dd/yyyy")

            ' Get last row with data
            Dim lastRow As Integer = attendanceSheet.Cells(attendanceSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

            ' Read all data at once for better performance
            Dim dataRange As Excel.Range = attendanceSheet.Range(attendanceSheet.Cells(2, 1), attendanceSheet.Cells(lastRow, 5))
            Dim attendanceData As Object(,) = DirectCast(dataRange.Value, Object(,))

            ' Check each row
            For i As Integer = 1 To lastRow - 1  ' Adjust for array base 0
                Dim storedId As String = If(attendanceData(i, 1) IsNot Nothing, attendanceData(i, 1).ToString(), "")
                Dim storedDate As String = If(attendanceData(i, 5) IsNot Nothing, attendanceData(i, 5).ToString(), "")

                If storedId = userId AndAlso storedDate = todayStr Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            MessageBox.Show("Error checking attendance: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    End Function

    Private Function HasAttendedToday(userId As String) As Boolean
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim attendanceSheet As Excel.Worksheet = Nothing

        Try
            excelApp = New Excel.Application()
            workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
            attendanceSheet = workbook.Sheets(3) ' Attendance sheet

            Dim lastRow As Integer = attendanceSheet.Cells(attendanceSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
            Dim today As String = DateTime.Now.ToString("MM/dd/yyyy")

            ' Check each row for same user ID and today's date
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
    End Function

    Public Sub ProcessAttendance(userId As String)
        Try
            ' First check if student has already attended today
            If HasAttendedToday(userId) Then
                MessageBox.Show("This student has already recorded attendance today.",
                          "Duplicate Attendance", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' If no duplicate, proceed with normal attendance process
            Dim excelApp As New Excel.Application
            Dim workbook As Excel.Workbook = Nothing
            Dim studentsSheet As Excel.Worksheet = Nothing
            Dim attendanceSheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                studentsSheet = workbook.Sheets(2)
                attendanceSheet = workbook.Sheets(3)

                ' Get student details
                Dim lastRow As Integer = studentsSheet.Cells(studentsSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
                Dim found As Boolean = False
                Dim firstName As String = ""
                Dim lastName As String = ""
                Dim studentClass As String = ""

                For i As Integer = 2 To lastRow
                    If CStr(studentsSheet.Cells(i, 1).Value) = userId Then
                        firstName = CStr(studentsSheet.Cells(i, 2).Value)
                        lastName = CStr(studentsSheet.Cells(i, 3).Value)
                        studentClass = CStr(studentsSheet.Cells(i, 4).Value)
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    MessageBox.Show("Student ID not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Record attendance
                Dim attendanceLastRow As Integer = attendanceSheet.Cells(attendanceSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
                Dim currentTime As String = DateTime.Now.ToString("hh:mm tt")
                Dim currentDate As String = DateTime.Now.ToString("MM/dd/yyyy")

                attendanceSheet.Cells(attendanceLastRow + 1, 1).Value = userId
                attendanceSheet.Cells(attendanceLastRow + 1, 2).Value = firstName
                attendanceSheet.Cells(attendanceLastRow + 1, 3).Value = lastName
                attendanceSheet.Cells(attendanceLastRow + 1, 4).Value = currentTime
                attendanceSheet.Cells(attendanceLastRow + 1, 5).Value = currentDate
                attendanceSheet.Cells(attendanceLastRow + 1, 6).Value = studentClass

                workbook.Save()

                ' Update DataGridView if class filter matches
                If ComboBox1.SelectedIndex = 0 OrElse studentClass = ComboBox1.SelectedItem.ToString() Then
                    DataGridView1.Rows.Add(firstName, lastName, currentTime, currentDate, studentClass)
                    If DataGridView1.Rows.Count > 0 Then
                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1
                    End If
                End If

            Finally
                If studentsSheet IsNot Nothing Then ReleaseObject(studentsSheet)
                If attendanceSheet IsNot Nothing Then ReleaseObject(attendanceSheet)
                If workbook IsNot Nothing Then
                    workbook.Close(True)
                    ReleaseObject(workbook)
                End If
                If excelApp IsNot Nothing Then
                    excelApp.Quit()
                    ReleaseObject(excelApp)
                End If
            End Try

        Catch ex As Exception
            MessageBox.Show($"Error recording attendance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function FormatTimeIn(timeValue As String) As String
        Try
            If Double.TryParse(timeValue, Nothing) Then
                Dim dateValue As DateTime = DateTime.FromOADate(Double.Parse(timeValue))
                Return dateValue.ToString("hh:mm tt")  ' 12-hour format with AM/PM
            ElseIf DateTime.TryParse(timeValue, Nothing) Then
                Return DateTime.Parse(timeValue).ToString("hh:mm tt")
            Else
                Return timeValue
            End If
        Catch
            Return timeValue
        End Try
    End Function
    Public Sub UpdateDataGridView()
        Try
            Dim excelApp As New Excel.Application
            Dim workbook As Excel.Workbook = Nothing
            Dim attendanceSheet As Excel.Worksheet = Nothing

            Try
                workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
                attendanceSheet = workbook.Sheets(3)  ' Attendance sheet is Sheet3

                ' Get the last row
                Dim lastRow As Integer = attendanceSheet.Cells(attendanceSheet.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row

                ' Clear existing data
                DataGridView1.Rows.Clear()

                ' Read and display attendance records
                For i As Integer = 2 To lastRow ' Start from row 2 to skip header
                    Dim firstName = CStr(attendanceSheet.Cells(i, 2).Value)  ' Column B: First Name
                    Dim lastName = CStr(attendanceSheet.Cells(i, 3).Value)   ' Column C: Last Name
                    Dim timeIn = CStr(attendanceSheet.Cells(i, 4).Value)    ' Column D: Time-in
                    Dim date_ = CStr(attendanceSheet.Cells(i, 5).Value)     ' Column E: Date
                    Dim class_ = CStr(attendanceSheet.Cells(i, 6).Value)    ' Column F: Class

                    ' Apply class filter if selected
                    If ComboBox1.SelectedIndex = 0 OrElse class_ = ComboBox1.SelectedItem.ToString() Then
                        DataGridView1.Rows.Add(firstName, lastName, FormatTimeIn(timeIn), date_, class_)
                    End If
                Next

                ' Auto-size columns for better display
                DataGridView1.AutoResizeColumns()

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
            MessageBox.Show("Error updating attendance data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReleaseObject(obj As Object)
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
End Class