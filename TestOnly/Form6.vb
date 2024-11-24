﻿Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configure DataGridView properties
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True

        ' Initialize ComboBoxes
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
                Dim regTime As String = worksheet.Cells(i, 4).Value?.ToString()
                Dim regDate As String = worksheet.Cells(i, 5).Value?.ToString()

                If Not String.IsNullOrEmpty(name) Then
                    DataGridView1.Rows.Add(name, surname, age, regTime, regDate)
                End If
            Next

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form5 As New Form5()
        form5.Show()
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Add sorting logic here if needed
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ' Add filtering logic here if needed
    End Sub
End Class