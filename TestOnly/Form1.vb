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
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports Google.Apis.Util.Store
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Form initialization logic if needed
    End Sub

    ' Button click event to add data
    Private Sub AddData_Click(sender As Object, e As EventArgs) Handles AddData.Click
        Dim name As String = TBox_Name.Text
        Dim surname As String = TBox_Surname.Text
        Dim age As String = TBox_Age.Text
        Dim registrationTime As String = DateTime.Now.ToString("hh:mm tt")
        Dim registrationDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        Try
            AddDataToGoogleSheets(name, surname, age, registrationTime, registrationDate)
            AddDataToExcel(name, surname, age, registrationTime, registrationDate)

            ' Optionally, show success message
            MessageBox.Show("Data added successfully!")

            ' Clear TextBoxes after adding data
            TBox_Name.Clear()
            TBox_Surname.Clear()
            TBox_Age.Clear()

        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Dim form As New Form2()
        form.ShowDialog()
    End Sub

End Class

