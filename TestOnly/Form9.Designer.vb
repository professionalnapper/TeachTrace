﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form9
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form9))
        TestConnection = New Button()
        TBox_Class = New TextBox()
        ClassType = New Label()
        UpdateData = New Button()
        AddData = New Button()
        TBox_Surname = New TextBox()
        TBox_Name = New TextBox()
        Surname = New Label()
        lblName = New Label()
        SuspendLayout()
        ' 
        ' TestConnection
        ' 
        TestConnection.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___467_x_64_px___1_
        TestConnection.BackgroundImageLayout = ImageLayout.Zoom
        TestConnection.Cursor = Cursors.Hand
        TestConnection.FlatStyle = FlatStyle.Flat
        TestConnection.Location = New Point(293, 465)
        TestConnection.Name = "TestConnection"
        TestConnection.Size = New Size(249, 35)
        TestConnection.TabIndex = 21
        TestConnection.UseVisualStyleBackColor = True
        ' 
        ' TBox_Class
        ' 
        TBox_Class.Font = New Font("Poppins", 11F)
        TBox_Class.Location = New Point(365, 314)
        TBox_Class.Name = "TBox_Class"
        TBox_Class.Size = New Size(454, 35)
        TBox_Class.TabIndex = 20
        ' 
        ' ClassType
        ' 
        ClassType.AutoSize = True
        ClassType.BackColor = Color.FromArgb(CByte(239), CByte(239), CByte(252))
        ClassType.Font = New Font("Poppins", 11F)
        ClassType.Location = New Point(207, 314)
        ClassType.Name = "ClassType"
        ClassType.Size = New Size(68, 34)
        ClassType.TabIndex = 19
        ClassType.Text = "Class"
        ' 
        ' UpdateData
        ' 
        UpdateData.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___467_x_64_px___2_
        UpdateData.BackgroundImageLayout = ImageLayout.Zoom
        UpdateData.Cursor = Cursors.Hand
        UpdateData.FlatStyle = FlatStyle.Flat
        UpdateData.Location = New Point(560, 465)
        UpdateData.Name = "UpdateData"
        UpdateData.Size = New Size(249, 35)
        UpdateData.TabIndex = 18
        UpdateData.UseVisualStyleBackColor = True
        ' 
        ' AddData
        ' 
        AddData.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___516_x_43_px_
        AddData.BackgroundImageLayout = ImageLayout.Zoom
        AddData.Cursor = Cursors.Hand
        AddData.FlatStyle = FlatStyle.Flat
        AddData.ForeColor = Color.DarkBlue
        AddData.Location = New Point(293, 402)
        AddData.Name = "AddData"
        AddData.Size = New Size(516, 43)
        AddData.TabIndex = 17
        AddData.UseVisualStyleBackColor = True
        ' 
        ' TBox_Surname
        ' 
        TBox_Surname.Font = New Font("Poppins", 11F)
        TBox_Surname.Location = New Point(365, 255)
        TBox_Surname.Multiline = True
        TBox_Surname.Name = "TBox_Surname"
        TBox_Surname.Size = New Size(454, 35)
        TBox_Surname.TabIndex = 15
        ' 
        ' TBox_Name
        ' 
        TBox_Name.Font = New Font("Poppins", 11F)
        TBox_Name.Location = New Point(365, 200)
        TBox_Name.Multiline = True
        TBox_Name.Name = "TBox_Name"
        TBox_Name.Size = New Size(454, 35)
        TBox_Name.TabIndex = 14
        ' 
        ' Surname
        ' 
        Surname.AutoSize = True
        Surname.BackColor = Color.FromArgb(CByte(239), CByte(239), CByte(252))
        Surname.Font = New Font("Poppins", 11F)
        Surname.Location = New Point(207, 256)
        Surname.Name = "Surname"
        Surname.Size = New Size(102, 34)
        Surname.TabIndex = 12
        Surname.Text = "Surname"
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.BackColor = Color.FromArgb(CByte(239), CByte(239), CByte(252))
        lblName.Font = New Font("Poppins", 11F)
        lblName.Location = New Point(207, 199)
        lblName.Name = "lblName"
        lblName.Size = New Size(117, 34)
        lblName.TabIndex = 11
        lblName.Text = "First Name"
        ' 
        ' Form9
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___6_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(TestConnection)
        Controls.Add(TBox_Class)
        Controls.Add(ClassType)
        Controls.Add(UpdateData)
        Controls.Add(AddData)
        Controls.Add(TBox_Surname)
        Controls.Add(TBox_Name)
        Controls.Add(Surname)
        Controls.Add(lblName)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form9"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Registration"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TestConnection As Button
    Friend WithEvents TBox_Class As TextBox
    Friend WithEvents ClassType As Label
    Friend WithEvents UpdateData As Button
    Friend WithEvents AddData As Button
    Friend WithEvents TBox_Surname As TextBox
    Friend WithEvents TBox_Name As TextBox
    Friend WithEvents Surname As Label
    Friend WithEvents lblName As Label
End Class
