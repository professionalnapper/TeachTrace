<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form6
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
        ComboBox1 = New ComboBox()
        Label3 = New Label()
        ComboBox2 = New ComboBox()
        DataGridView1 = New DataGridView()
        ID = New DataGridViewTextBoxColumn()
        StudentName = New DataGridViewTextBoxColumn()
        ClassType = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(31, 29)
        Label1.Name = "Label1"
        Label1.Size = New Size(325, 41)
        Label1.TabIndex = 0
        Label1.Text = "View Student Records"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(31, 89)
        Label2.Name = "Label2"
        Label2.Size = New Size(98, 31)
        Label2.TabIndex = 1
        Label2.Text = "Sort by:"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Name", "Class"})
        ComboBox1.Location = New Point(135, 92)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 28)
        ComboBox1.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(410, 94)
        Label3.Name = "Label3"
        Label3.Size = New Size(167, 31)
        Label3.TabIndex = 3
        Label3.Text = "Filter by Class:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(594, 95)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 28)
        ComboBox2.TabIndex = 4
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {ID, StudentName, ClassType})
        DataGridView1.Location = New Point(66, 172)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(653, 195)
        DataGridView1.TabIndex = 5
        ' 
        ' ID
        ' 
        ID.HeaderText = "ID"
        ID.MinimumWidth = 6
        ID.Name = "ID"
        ID.Width = 200
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
        ' Button1
        ' 
        Button1.Location = New Point(573, 392)
        Button1.Name = "Button1"
        Button1.Size = New Size(172, 37)
        Button1.TabIndex = 6
        Button1.Text = "Back to Dashboard"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Form6
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Controls.Add(ComboBox2)
        Controls.Add(Label3)
        Controls.Add(ComboBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Text = "Form6"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents StudentName As DataGridViewTextBoxColumn
    Friend WithEvents ClassType As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
End Class