<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblName = New Label()
        Surname = New Label()
        Age = New Label()
        TBox_Name = New TextBox()
        TBox_Surname = New TextBox()
        TBox_Age = New TextBox()
        AddData = New Button()
        UpdateData = New Button()
        SuspendLayout()
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Location = New Point(121, 93)
        lblName.Name = "lblName"
        lblName.Size = New Size(49, 20)
        lblName.TabIndex = 0
        lblName.Text = "Name"
        ' 
        ' Surname
        ' 
        Surname.AutoSize = True
        Surname.Location = New Point(121, 145)
        Surname.Name = "Surname"
        Surname.Size = New Size(67, 20)
        Surname.TabIndex = 1
        Surname.Text = "Surname"
        ' 
        ' Age
        ' 
        Age.AutoSize = True
        Age.Location = New Point(121, 192)
        Age.Name = "Age"
        Age.Size = New Size(36, 20)
        Age.TabIndex = 2
        Age.Text = "Age"
        ' 
        ' TBox_Name
        ' 
        TBox_Name.Location = New Point(279, 91)
        TBox_Name.Name = "TBox_Name"
        TBox_Name.Size = New Size(211, 27)
        TBox_Name.TabIndex = 3
        ' 
        ' TBox_Surname
        ' 
        TBox_Surname.Location = New Point(286, 152)
        TBox_Surname.Name = "TBox_Surname"
        TBox_Surname.Size = New Size(125, 27)
        TBox_Surname.TabIndex = 4
        ' 
        ' TBox_Age
        ' 
        TBox_Age.Location = New Point(279, 206)
        TBox_Age.Name = "TBox_Age"
        TBox_Age.Size = New Size(125, 27)
        TBox_Age.TabIndex = 5
        ' 
        ' AddData
        ' 
        AddData.Location = New Point(140, 308)
        AddData.Name = "AddData"
        AddData.Size = New Size(94, 29)
        AddData.TabIndex = 6
        AddData.Text = "Add Data"
        AddData.UseVisualStyleBackColor = True
        ' 
        ' UpdateData
        ' 
        UpdateData.Location = New Point(414, 303)
        UpdateData.Name = "UpdateData"
        UpdateData.Size = New Size(187, 29)
        UpdateData.TabIndex = 7
        UpdateData.Text = "Proceed to Dashboard"
        UpdateData.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(UpdateData)
        Controls.Add(AddData)
        Controls.Add(TBox_Age)
        Controls.Add(TBox_Surname)
        Controls.Add(TBox_Name)
        Controls.Add(Age)
        Controls.Add(Surname)
        Controls.Add(lblName)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblName As Label
    Friend WithEvents Surname As Label
    Friend WithEvents Age As Label
    Friend WithEvents TBox_Name As TextBox
    Friend WithEvents TBox_Surname As TextBox
    Friend WithEvents TBox_Age As TextBox
    Friend WithEvents AddData As Button
    Friend WithEvents UpdateData As Button

End Class
