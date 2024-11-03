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


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' Button click event to add data
    Private Sub AddData_Click(sender As Object, e As EventArgs) Handles AddData.Click
        ' Example data input from TextBoxes
        Dim name As String = TBox_Name.Text
        Dim surname As String = TBox_Surname.Text
        Dim age As String = TBox_Age.Text

        AddDataToGoogleSheets(name, surname, age)

        ' Add data to DataGridView in Form2
        'form2.AddDataToGridView(name, surname, age)

        ' Add data to Excel
        AddDataToExcel(name, surname, age)

        ' Clear TextBoxes after adding data
        TBox_Name.Clear()
        TBox_Surname.Clear()
        TBox_Age.Clear()

        Try
            Dim Scopes As String() = {SheetsService.Scope.Spreadsheets}
            Dim service As New SheetsService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    New ClientSecrets() With {
                        .ClientId = "1089286082984-b7gqp3181vfmgd40p8slhtgfci9g5dlm.apps.googleusercontent.com",
                        .ClientSecret = "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"
                    }, Scopes, "user", CancellationToken.None, New FileDataStore("MyAppsToken")).Result,
                .ApplicationName = "Google Sheets API .NET Quickstart"
            })

            Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest = service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "data-received!A:C")
            Dim getResponse As ValueRange = getRequest.Execute()
            Dim values As IList(Of IList(Of Object)) = getResponse.Values
            Dim Range As String = $"data-received!A{values.Count + 1}:C{values.Count + 1}"

            Dim ValueRange As New ValueRange() With {
                .Values = New List(Of IList(Of Object)) From {New List(Of Object) From {TBox_Name.Text, TBox_Surname.Text, TBox_Age.Text}}
            }

            Dim updateRequest As SpreadsheetsResource.ValuesResource.UpdateRequest = service.Spreadsheets.Values.Update(ValueRange, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", Range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()

            ' Save data to MS Access
            SaveDataToAccess(TBox_Name.Text, TBox_Surname.Text, TBox_Age.Text)

            ' Optionally, show success message
            MessageBox.Show("Data added successfully!")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddDataToGoogleSheets(name As String, surname As String, age As String)
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

            Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest = service.Spreadsheets.Values.Get("1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", "data-received!A:C")
            Dim getResponse As ValueRange = getRequest.Execute()
            Dim values As IList(Of IList(Of Object)) = getResponse.Values
            Dim range As String = $"data-received!A{values.Count + 1}:C{values.Count + 1}"

            Dim valueRange As New ValueRange()
            valueRange.Values = New List(Of IList(Of Object)) From {New List(Of Object) From {name, surname, age}}
            Dim updateRequest As SpreadsheetsResource.ValuesResource.UpdateRequest = service.Spreadsheets.Values.Update(valueRange, "1K07Vv21Gqy6u9o3N155TufAwpkEEw64YTXZd_tnOjKI", range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()

            MessageBox.Show("Data added to Google Sheets successfully!")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveDataToAccess(name As String, surname As String, age As String)
        ' Connection string to your MS Access database
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
    End Sub

    ' Method to add data to Excel
    Private Sub AddDataToExcel(name As String, surname As String, age As String)
        Dim excelApp As New Excel.Application()
        ' Use a new test file path
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Open("C:\Users\quiam\Downloads\Example.xlsx")
        Dim worksheet As Excel.Worksheet = CType(workbook.Sheets(1), Excel.Worksheet)

        ' Find the next empty row
        Dim row As Integer = worksheet.UsedRange.Rows.Count + 1

        ' Add data to cells
        worksheet.Cells(row, 1) = name
        worksheet.Cells(row, 2) = surname
        worksheet.Cells(row, 3) = age

        ' Save and close the workbook
        workbook.Save()
        workbook.Close()
        excelApp.Quit()
    End Sub

    Private Sub UpdateData_Click(sender As Object, e As EventArgs) Handles UpdateData.Click
        Dim form As New Form2()
        form.ShowDialog()
    End Sub
End Class
