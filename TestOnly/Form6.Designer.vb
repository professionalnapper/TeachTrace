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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form6))
        Label1 = New Label()
        Label2 = New Label()
        ComboBox1 = New ComboBox()
        Label3 = New Label()
        ComboBox2 = New ComboBox()
        DataGridView1 = New DataGridView()
        NameColumn = New DataGridViewTextBoxColumn()
        SurnameColumn = New DataGridViewTextBoxColumn()
        AgeColumn = New DataGridViewTextBoxColumn()
        TimeInColumn = New DataGridViewTextBoxColumn()
        DateInColumn = New DataGridViewTextBoxColumn()
        ClassType = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(80, 58)
        Label1.Name = "Label1"
        Label1.Size = New Size(325, 41)
        Label1.TabIndex = 0
        Label1.Text = "View Student Records"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Poppins", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(158, 185)
        Label2.Name = "Label2"
        Label2.Size = New Size(96, 36)
        Label2.TabIndex = 1
        Label2.Text = "Sort by:"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Font = New Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Name", "Class"})
        ComboBox1.Location = New Point(270, 185)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 34)
        ComboBox1.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Poppins", 12F, FontStyle.Bold)
        Label3.Location = New Point(521, 184)
        Label3.Name = "Label3"
        Label3.Size = New Size(170, 36)
        Label3.TabIndex = 3
        Label3.Text = "Filter by Class:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Font = New Font("Poppins", 9F)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(721, 188)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 34)
        ComboBox2.TabIndex = 4
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {NameColumn, SurnameColumn, AgeColumn, TimeInColumn, DateInColumn, ClassType})
        DataGridView1.GridColor = Color.DarkBlue
        DataGridView1.Location = New Point(158, 257)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(727, 195)
        DataGridView1.TabIndex = 5
        ' 
        ' NameColumn
        ' 
        NameColumn.HeaderText = "Name"
        NameColumn.MinimumWidth = 6
        NameColumn.Name = "NameColumn"
        NameColumn.Width = 125
        ' 
        ' SurnameColumn
        ' 
        SurnameColumn.HeaderText = "Surname"
        SurnameColumn.MinimumWidth = 6
        SurnameColumn.Name = "SurnameColumn"
        SurnameColumn.Width = 125
        ' 
        ' AgeColumn
        ' 
        AgeColumn.HeaderText = "Age"
        AgeColumn.MinimumWidth = 6
        AgeColumn.Name = "AgeColumn"
        AgeColumn.Width = 50
        ' 
        ' TimeInColumn
        ' 
        TimeInColumn.HeaderText = "Time-in"
        TimeInColumn.MinimumWidth = 6
        TimeInColumn.Name = "TimeInColumn"
        TimeInColumn.Width = 125
        ' 
        ' DateInColumn
        ' 
        DateInColumn.HeaderText = "Date"
        DateInColumn.MinimumWidth = 6
        DateInColumn.Name = "DateInColumn"
        DateInColumn.Width = 125
        ' 
        ' ClassType
        ' 
        ClassType.HeaderText = "Class"
        ClassType.MinimumWidth = 6
        ClassType.Name = "ClassType"
        ClassType.Width = 125
        ' 
        ' Button1
        ' 
        Button1.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___6_
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Cursor = Cursors.Hand
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = Color.DarkBlue
        Button1.Location = New Point(692, 480)
        Button1.Name = "Button1"
        Button1.Size = New Size(193, 60)
        Button1.TabIndex = 6
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Form6
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___3_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Controls.Add(ComboBox2)
        Controls.Add(Label3)
        Controls.Add(ComboBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form6"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Student Records"
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
    Friend WithEvents Button1 As Button
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents SurnameColumn As DataGridViewTextBoxColumn
    Friend WithEvents AgeColumn As DataGridViewTextBoxColumn
    Friend WithEvents TimeInColumn As DataGridViewTextBoxColumn
    Friend WithEvents DateInColumn As DataGridViewTextBoxColumn
    Friend WithEvents ClassType As DataGridViewTextBoxColumn
End Class