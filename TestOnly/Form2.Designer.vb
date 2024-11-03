<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        DataGridView1 = New DataGridView()
        name = New DataGridViewTextBoxColumn()
        surname = New DataGridViewTextBoxColumn()
        age = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {name, surname, age})
        DataGridView1.Location = New Point(156, 72)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(502, 188)
        DataGridView1.TabIndex = 0
        ' 
        ' name
        ' 
        name.HeaderText = "Name"
        name.MinimumWidth = 6
        name.Name = "name"
        name.Width = 125
        ' 
        ' surname
        ' 
        surname.HeaderText = "Surname"
        surname.MinimumWidth = 6
        surname.Name = "surname"
        surname.Width = 125
        ' 
        ' age
        ' 
        age.HeaderText = "Age"
        age.MinimumWidth = 6
        age.Name = "age"
        age.Width = 125
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(DataGridView1)
        'Name = "Form2"
        'Text = "Form2"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents name As DataGridViewTextBoxColumn
    Friend WithEvents surname As DataGridViewTextBoxColumn
    Friend WithEvents age As DataGridViewTextBoxColumn
End Class
