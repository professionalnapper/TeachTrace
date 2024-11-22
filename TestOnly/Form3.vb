Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.IO

Public Class Form3
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupForm()
    End Sub

    Private Sub SetupForm()
        ' Form settings
        Me.Text = "Biometric Enrollment"
        Me.Size = New Size(600, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White

        ' Main title
        Dim titleLabel As New Label With {
            .Text = "Biometric Enrollment",
            .Font = New Font("Segoe UI", 20, FontStyle.Bold),
            .Location = New Point(20, 20),
            .AutoSize = True
        }

        ' Subtitle
        Dim subtitleLabel As New Label With {
            .Text = "Enroll New Student",
            .Font = New Font("Segoe UI", 16),
            .Location = New Point(20, 70),
            .AutoSize = True
        }

        ' Student ID
        Dim idLabel As New Label With {
            .Text = "Student ID",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 120),
            .AutoSize = True
        }

        Dim idTextBox As New TextBox With {
            .Location = New Point(20, 145),
            .Size = New Size(540, 30),
            .Font = New Font("Segoe UI", 10),
            .PlaceholderText = "Enter student ID"
        }

        ' Student Name
        Dim nameLabel As New Label With {
            .Text = "Student Name",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 185),
            .AutoSize = True
        }

        Dim nameTextBox As New TextBox With {
            .Location = New Point(20, 210),
            .Size = New Size(540, 30),
            .Font = New Font("Segoe UI", 10),
            .PlaceholderText = "Enter student name"
        }

        ' Class
        Dim classLabel As New Label With {
            .Text = "Class",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 250),
            .AutoSize = True
        }

        Dim classComboBox As New ComboBox With {
            .Location = New Point(20, 275),
            .Size = New Size(540, 30),
            .Font = New Font("Segoe UI", 10),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }
        classComboBox.Items.AddRange(New String() {"Select a class", "Class A", "Class B", "Class C"})
        classComboBox.SelectedIndex = 0

        ' Fingerprint section
        Dim fingerprintLabel As New Label With {
            .Text = "Fingerprint",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 315),
            .AutoSize = True
        }

        Dim scanButton As New Button With {
            .Text = "  Scan Fingerprint",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 340),
            .Size = New Size(150, 35),
            .BackColor = Color.FromArgb(28, 57, 147),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .TextAlign = ContentAlignment.MiddleRight,
            .TextImageRelation = TextImageRelation.ImageBeforeText,
            .Image = New Bitmap(New Bitmap(Path.Combine(Application.StartupPath, "fingerprint.png")), 16, 16)
        }
        scanButton.FlatAppearance.BorderSize = 0

        ' Enroll button
        Dim enrollButton As New Button With {
            .Text = "Enroll Student",
            .Font = New Font("Segoe UI", 10),
            .Location = New Point(20, 395),
            .Size = New Size(540, 40),
            .BackColor = Color.FromArgb(28, 57, 147),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        enrollButton.FlatAppearance.BorderSize = 0

        ' Add controls to form
        Me.Controls.AddRange(New Control() {
            titleLabel,
            subtitleLabel,
            idLabel,
            idTextBox,
            nameLabel,
            nameTextBox,
            classLabel,
            classComboBox,
            fingerprintLabel,
            scanButton,
            enrollButton
        })

        ' Event handlers
        AddHandler scanButton.Click, AddressOf ScanButton_Click
        AddHandler enrollButton.Click, AddressOf EnrollButton_Click
    End Sub

    Private Sub ScanButton_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Fingerprint scanning initiated...", "Scan Fingerprint")
    End Sub

    Private Sub EnrollButton_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Student enrollment initiated...", "Enroll Student")
    End Sub
End Class