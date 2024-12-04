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
        UserID = New DataGridViewTextBoxColumn()
        NameColumn = New DataGridViewTextBoxColumn()
        SurnameColumn = New DataGridViewTextBoxColumn()
        AgeColumn = New DataGridViewTextBoxColumn()
        DateInColumn = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label3 = New Label()
        ComboBox1 = New ComboBox()
        Label4 = New Label()
        Button1 = New Button()
        ComboBox2 = New ComboBox()
        Label1 = New Label()
        TextBox2 = New TextBox()
        Button2 = New Button()
        Button3 = New Button()
        TextBox3 = New TextBox()
        Label5 = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {UserID, NameColumn, SurnameColumn, AgeColumn, DateInColumn})
        DataGridView1.Location = New Point(179, 386)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(653, 149)
        DataGridView1.TabIndex = 0
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
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Lavender
        Label2.Font = New Font("Microsoft Sans Serif", 10.2F)
        Label2.Location = New Point(158, 170)
        Label2.Name = "Label2"
        Label2.Size = New Size(67, 20)
        Label2.TabIndex = 2
        Label2.Text = "User ID"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(382, 202)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(194, 35)
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
        ComboBox1.Location = New Point(158, 277)
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
        Button1.Location = New Point(681, 191)
        Button1.Name = "Button1"
        Button1.Size = New Size(160, 48)
        Button1.TabIndex = 7
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Font = New Font("Microsoft Sans Serif", 9F)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"All", "Class A", "Class B", "Class C"})
        ComboBox2.Location = New Point(681, 341)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(151, 26)
        ComboBox2.TabIndex = 8
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Lavender
        Label1.Font = New Font("Microsoft Sans Serif", 10.2F)
        Label1.Location = New Point(382, 170)
        Label1.Name = "Label1"
        Label1.Size = New Size(92, 20)
        Label1.TabIndex = 9
        Label1.Text = "First Name"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(382, 277)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(194, 35)
        TextBox2.TabIndex = 10
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(681, 275)
        Button2.Name = "Button2"
        Button2.Size = New Size(125, 29)
        Button2.TabIndex = 11
        Button2.Text = "Update Details"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(473, 339)
        Button3.Name = "Button3"
        Button3.Size = New Size(169, 29)
        Button3.TabIndex = 12
        Button3.Text = "Delete Student Record"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(158, 202)
        TextBox3.Multiline = True
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(194, 35)
        TextBox3.TabIndex = 14
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Lavender
        Label5.Font = New Font("Microsoft Sans Serif", 10.2F)
        Label5.Location = New Point(382, 254)
        Label5.Name = "Label5"
        Label5.Size = New Size(76, 20)
        Label5.TabIndex = 13
        Label5.Text = "Surname"
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___4_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(TextBox3)
        Controls.Add(Label5)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(TextBox2)
        Controls.Add(Label1)
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
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents UserID As DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents SurnameColumn As DataGridViewTextBoxColumn
    Friend WithEvents AgeColumn As DataGridViewTextBoxColumn
    Friend WithEvents DateInColumn As DataGridViewTextBoxColumn
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
End Class