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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form7))
        DataGridView1 = New DataGridView()
        NameColumn = New DataGridViewTextBoxColumn()
        SurnameColumn = New DataGridViewTextBoxColumn()
        AgeColumn = New DataGridViewTextBoxColumn()
        TimeInColumn = New DataGridViewTextBoxColumn()
        DateInColumn = New DataGridViewTextBoxColumn()
        ClassType = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label3 = New Label()
        ComboBox1 = New ComboBox()
        Label4 = New Label()
        Button1 = New Button()
        ComboBox2 = New ComboBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {NameColumn, SurnameColumn, AgeColumn, TimeInColumn, DateInColumn, ClassType})
        DataGridView1.Location = New Point(158, 390)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(728, 149)
        DataGridView1.TabIndex = 0
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
        TimeInColumn.HeaderText = "Time-In"
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
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Lavender
        Label2.Font = New Font("Microsoft Sans Serif", 10.2F)
        Label2.Location = New Point(158, 170)
        Label2.Name = "Label2"
        Label2.Size = New Size(92, 20)
        Label2.TabIndex = 2
        Label2.Text = "First Name"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(159, 202)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(290, 35)
        TextBox1.TabIndex = 3
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Lavender
        Label3.Font = New Font("Microsoft Sans Serif", 10.2F)
        Label3.Location = New Point(158, 254)
        Label3.Name = "Label3"
        Label3.Size = New Size(52, 20)
        Label3.TabIndex = 4
        Label3.Text = "Class"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Font = New Font("Microsoft Sans Serif", 9F)
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Select a Class", "Class A", "Class B", "Class C"})
        ComboBox1.Location = New Point(165, 284)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 26)
        ComboBox1.TabIndex = 5
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Lavender
        Label4.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(165, 328)
        Label4.Name = "Label4"
        Label4.Size = New Size(263, 41)
        Label4.TabIndex = 6
        Label4.Text = "Existing Students"
        ' 
        ' Button1
        ' 
        Button1.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___6_
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Cursor = Cursors.Hand
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = Color.DarkBlue
        Button1.Location = New Point(693, 213)
        Button1.Name = "Button1"
        Button1.Size = New Size(160, 48)
        Button1.TabIndex = 7
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Font = New Font("Microsoft Sans Serif", 9F)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Filter by Class", "Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(681, 341)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 26)
        ComboBox2.TabIndex = 8
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___4_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(ComboBox2)
        Controls.Add(Button1)
        Controls.Add(Label4)
        Controls.Add(ComboBox1)
        Controls.Add(Label3)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(DataGridView1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form7"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Modify Student Record"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents SurnameColumn As DataGridViewTextBoxColumn
    Friend WithEvents AgeColumn As DataGridViewTextBoxColumn
    Friend WithEvents TimeInColumn As DataGridViewTextBoxColumn
    Friend WithEvents DateInColumn As DataGridViewTextBoxColumn
    Friend WithEvents ClassType As DataGridViewTextBoxColumn
End Class