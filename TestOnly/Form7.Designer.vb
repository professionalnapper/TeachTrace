<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form7
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
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label3 = New Label()
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        Label4 = New Label()
        Label5 = New Label()
        ComboBox2 = New ComboBox()
        DataGridView1 = New DataGridView()
        StudentName = New DataGridViewTextBoxColumn()
        ClassType = New DataGridViewTextBoxColumn()
        Edit = New DataGridViewButtonColumn()
        Button2 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(28, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(202, 31)
        Label1.TabIndex = 0
        Label1.Text = "Add New Student"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(37, 81)
        Label2.Name = "Label2"
        Label2.Size = New Size(137, 28)
        Label2.TabIndex = 1
        Label2.Text = "Student Name"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(41, 123)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(328, 50)
        TextBox1.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(37, 185)
        Label3.Name = "Label3"
        Label3.Size = New Size(55, 28)
        Label3.TabIndex = 3
        Label3.Text = "Class"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Select a class", "Class A", "Class B", "Class C"})
        ComboBox1.Location = New Point(41, 229)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 28)
        ComboBox1.TabIndex = 4
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(271, 228)
        Button1.Name = "Button1"
        Button1.Size = New Size(144, 29)
        Button1.TabIndex = 5
        Button1.Text = "Add Student"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(28, 290)
        Label4.Name = "Label4"
        Label4.Size = New Size(200, 31)
        Label4.TabIndex = 6
        Label4.Text = "Existing Students"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(455, 293)
        Label5.Name = "Label5"
        Label5.Size = New Size(135, 28)
        Label5.TabIndex = 7
        Label5.Text = "Filter by Class:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"All Classes", "Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(612, 296)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 28)
        ComboBox2.TabIndex = 8
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {StudentName, ClassType, Edit})
        DataGridView1.Location = New Point(52, 339)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(653, 99)
        DataGridView1.TabIndex = 10
        ' 
        ' StudentName
        ' 
        StudentName.HeaderText = "Name"
        StudentName.MinimumWidth = 6
        StudentName.Name = "StudentName"
        StudentName.Width = 200
        ' 
        ' ClassType
        ' 
        ClassType.HeaderText = "Class"
        ClassType.MinimumWidth = 6
        ClassType.Name = "ClassType"
        ClassType.Width = 200
        ' 
        ' Edit
        ' 
        Edit.HeaderText = "Edit"
        Edit.MinimumWidth = 6
        Edit.Name = "Edit"
        Edit.Width = 200
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(612, 21)
        Button2.Name = "Button2"
        Button2.Size = New Size(172, 37)
        Button2.TabIndex = 11
        Button2.Text = "Back to Dashboard"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Controls.Add(ComboBox2)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Label3)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Text = "Form7"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents StudentName As DataGridViewTextBoxColumn
    Friend WithEvents ClassType As DataGridViewTextBoxColumn
    Friend WithEvents Edit As DataGridViewButtonColumn
    Friend WithEvents Button2 As Button
End Class