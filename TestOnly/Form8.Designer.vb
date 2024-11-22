<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form8
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
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        DataGridView1 = New DataGridView()
        StudentName = New DataGridViewTextBoxColumn()
        ClassType = New DataGridViewTextBoxColumn()
        TimeIn = New DataGridViewTextBoxColumn()
        TimeOut = New DataGridViewTextBoxColumn()
        Button2 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(48, 32)
        Label1.Name = "Label1"
        Label1.Size = New Size(368, 31)
        Label1.TabIndex = 0
        Label1.Text = "Today's Attendance (11/23/2024)"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"All Classes", "Class A", "Class B", "Class C"})
        ComboBox1.Location = New Point(62, 83)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 28)
        ComboBox1.TabIndex = 1
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {StudentName, ClassType, TimeIn, TimeOut})
        DataGridView1.Location = New Point(62, 149)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(653, 188)
        DataGridView1.TabIndex = 2
        ' 
        ' StudentName
        ' 
        StudentName.HeaderText = "Student Name"
        StudentName.MinimumWidth = 6
        StudentName.Name = "StudentName"
        StudentName.Width = 150
        ' 
        ' ClassType
        ' 
        ClassType.HeaderText = "Class"
        ClassType.MinimumWidth = 6
        ClassType.Name = "ClassType"
        ClassType.Width = 150
        ' 
        ' TimeIn
        ' 
        TimeIn.HeaderText = "Time-in"
        TimeIn.MinimumWidth = 6
        TimeIn.Name = "TimeIn"
        TimeIn.Width = 150
        ' 
        ' TimeOut
        ' 
        TimeOut.HeaderText = "Time-out"
        TimeOut.MinimumWidth = 6
        TimeOut.Name = "TimeOut"
        TimeOut.Width = 150
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(543, 371)
        Button2.Name = "Button2"
        Button2.Size = New Size(172, 37)
        Button2.TabIndex = 12
        Button2.Text = "Back to Dashboard"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Form8
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Controls.Add(ComboBox1)
        Controls.Add(Label1)
        Text = "Form8"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents StudentName As DataGridViewTextBoxColumn
    Friend WithEvents ClassType As DataGridViewTextBoxColumn
    Friend WithEvents TimeIn As DataGridViewTextBoxColumn
    Friend WithEvents TimeOut As DataGridViewTextBoxColumn
    Friend WithEvents Button2 As Button
End Class