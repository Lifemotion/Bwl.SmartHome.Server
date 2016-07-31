<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComputerControlApp
    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.setObjects = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(0, 270)
        Me.logWriter.Size = New System.Drawing.Size(670, 221)
        '
        'setObjects
        '
        Me.setObjects.Enabled = True
        Me.setObjects.Interval = 3000
        '
        'ComputerControlApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 494)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "ComputerControlApp"
        Me.Text = "ComputerControlApp"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents setObjects As Timer
End Class
