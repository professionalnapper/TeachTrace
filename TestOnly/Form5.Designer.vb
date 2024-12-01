<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form5
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form5))
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___4_
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Cursor = Cursors.Hand
        Button1.FlatStyle = FlatStyle.Flat
        Button1.ForeColor = Color.DarkBlue
        Button1.Location = New Point(243, 236)
        Button1.Name = "Button1"
        Button1.Size = New Size(238, 73)
        Button1.TabIndex = 1
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___2_
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.Cursor = Cursors.Hand
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = Color.DarkBlue
        Button2.Location = New Point(243, 352)
        Button2.Name = "Button2"
        Button2.Size = New Size(238, 73)
        Button2.TabIndex = 2
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___3_
        Button3.BackgroundImageLayout = ImageLayout.Zoom
        Button3.Cursor = Cursors.Hand
        Button3.FlatStyle = FlatStyle.Flat
        Button3.ForeColor = Color.DarkBlue
        Button3.Location = New Point(540, 236)
        Button3.Name = "Button3"
        Button3.Size = New Size(238, 73)
        Button3.TabIndex = 3
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___8_
        Button4.BackgroundImageLayout = ImageLayout.Zoom
        Button4.Cursor = Cursors.Hand
        Button4.FlatStyle = FlatStyle.Flat
        Button4.ForeColor = Color.DarkBlue
        Button4.Location = New Point(540, 352)
        Button4.Name = "Button4"
        Button4.Size = New Size(238, 73)
        Button4.TabIndex = 4
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.BackgroundImage = My.Resources.Resources.Log_In__316_x_219_px___9_
        Button5.BackgroundImageLayout = ImageLayout.Zoom
        Button5.Cursor = Cursors.Hand
        Button5.FlatStyle = FlatStyle.Flat
        Button5.ForeColor = Color.DarkBlue
        Button5.Location = New Point(384, 462)
        Button5.Name = "Button5"
        Button5.Size = New Size(238, 73)
        Button5.TabIndex = 5
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Form5
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Log_In__1134_x_696_px___1_
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(1030, 637)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form5"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dashboard"
        ResumeLayout(False)
    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
End Class
