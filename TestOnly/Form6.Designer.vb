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
        Label2 = New Label()
        ComboBox1 = New ComboBox()
        Label3 = New Label()
        ComboBox2 = New ComboBox()
        DataGridView1 = New DataGridView()
        Button1 = New Button()
        UserID = New DataGridViewTextBoxColumn()
        NameColumn = New DataGridViewTextBoxColumn()
        SurnameColumn = New DataGridViewTextBoxColumn()
        AgeColumn = New DataGridViewTextBoxColumn()
        DateInColumn = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Lavender
        Label2.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(158, 185)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 25)
        Label2.TabIndex = 1
        Label2.Text = "Sort by:"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Name", "Class"})
        ComboBox1.Location = New Point(270, 185)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 26)
        ComboBox1.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Lavender
        Label3.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
        Label3.Location = New Point(521, 184)
        Label3.Name = "Label3"
        Label3.Size = New Size(157, 25)
        Label3.TabIndex = 3
        Label3.Text = "Filter by Class:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Font = New Font("Microsoft Sans Serif", 9F)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"All", "Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(721, 188)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 26)
        ComboBox2.TabIndex = 4
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {UserID, NameColumn, SurnameColumn, AgeColumn, DateInColumn})
        DataGridView1.GridColor = Color.DarkBlue
        DataGridView1.Location = New Point(197, 251)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(653, 195)
        DataGridView1.TabIndex = 5
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
        ' UserID
        ' 
        UserID.HeaderText = "User ID"
        UserID.MinimumWidth = 6
        UserID.Name = "UserID"
        UserID.Width = 120
        ' 
        ' NameColumn
        ' 
        NameColumn.HeaderText = "Name"
        NameColumn.MinimumWidth = 6
        NameColumn.Name = "NameColumn"
        NameColumn.Width = 120
        ' 
        ' SurnameColumn
        ' 
        SurnameColumn.HeaderText = "Surname"
        SurnameColumn.MinimumWidth = 6
        SurnameColumn.Name = "SurnameColumn"
        SurnameColumn.Width = 120
        ' 
        ' AgeColumn
        ' 
        AgeColumn.HeaderText = "Class"
        AgeColumn.MinimumWidth = 6
        AgeColumn.Name = "AgeColumn"
        AgeColumn.Width = 120
        ' 
        ' DateInColumn
        ' 
        DateInColumn.HeaderText = "Date"
        DateInColumn.MinimumWidth = 6
        DateInColumn.Name = "DateInColumn"
        DateInColumn.Width = 120
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
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form6"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Student Records"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents UserID As DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents SurnameColumn As DataGridViewTextBoxColumn
    Friend WithEvents AgeColumn As DataGridViewTextBoxColumn
    Friend WithEvents DateInColumn As DataGridViewTextBoxColumn
End Class