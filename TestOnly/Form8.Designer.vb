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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form8))
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        DataGridView1 = New DataGridView()
        columnName = New DataGridViewTextBoxColumn()
        Surname = New DataGridViewTextBoxColumn()
        TimeIn = New DataGridViewTextBoxColumn()
        DateToday = New DataGridViewTextBoxColumn()
        Section = New DataGridViewTextBoxColumn()
        Button2 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(179, 175)
        Label1.Name = "Label1"
        Label1.Size = New Size(222, 31)
        Label1.TabIndex = 0
        Label1.Text = "Today's Attendance"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Font = New Font("Microsoft Sans Serif", 9.0F)
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"All Classes", "Class A", "Class B", "Class C"})
        ComboBox1.Location = New Point(193, 226)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(151, 26)
        ComboBox1.TabIndex = 1
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {columnName, Surname, TimeIn, DateToday, Section})
        DataGridView1.Location = New Point(232, 272)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(504, 188)
        DataGridView1.TabIndex = 2
        ' 
        ' columnName
        ' 
        columnName.HeaderText = "First Name"
        columnName.MinimumWidth = 6
        columnName.Name = "columnName"
        columnName.Width = 109
        ' 
        ' Surname
        ' 
        Surname.HeaderText = "Surname"
        Surname.MinimumWidth = 6
        Surname.Name = "Surname"
        Surname.Width = 90
        ' 
        ' TimeIn
        ' 
        TimeIn.HeaderText = "Time-in"
        TimeIn.MinimumWidth = 6
        TimeIn.Name = "TimeIn"
        TimeIn.Width = 90
        ' 
        ' DateToday
        ' 
        DateToday.HeaderText = "Date"
        DateToday.MinimumWidth = 6
        DateToday.Name = "DateToday"
        DateToday.Width = 90
        ' 
        ' Section
        ' 
        Section.HeaderText = "Class"
        Section.MinimumWidth = 6
        Section.Name = "Section"
        Section.Width = 72
        ' 
        ' Button2
        ' 
        Button2.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___6_
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.Cursor = Cursors.Hand
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = Color.DarkBlue
        Button2.Location = New Point(679, 493)
        Button2.Name = "Button2"
        Button2.Size = New Size(167, 51)
        Button2.TabIndex = 12
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Form8
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___5_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Controls.Add(ComboBox1)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form8"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Monitor Attendance"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents columnName As DataGridViewTextBoxColumn
    Friend WithEvents Surname As DataGridViewTextBoxColumn
    Friend WithEvents TimeIn As DataGridViewTextBoxColumn
    Friend WithEvents DateToday As DataGridViewTextBoxColumn
    Friend WithEvents Section As DataGridViewTextBoxColumn
End Class