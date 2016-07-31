<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class App

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
        Me.startTimer = New System.Windows.Forms.Timer(Me.components)
        Me._stateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.statesTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(2, 314)
        Me.logWriter.Size = New System.Drawing.Size(827, 285)
        '
        'startTimer
        '
        Me.startTimer.Enabled = True
        Me.startTimer.Interval = 1000
        '
        '_stateTimer
        '
        Me._stateTimer.Enabled = True
        Me._stateTimer.Interval = 1000
        '
        'statesTextBox
        '
        Me.statesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.statesTextBox.Location = New System.Drawing.Point(0, 27)
        Me.statesTextBox.Multiline = True
        Me.statesTextBox.Name = "statesTextBox"
        Me.statesTextBox.Size = New System.Drawing.Size(370, 284)
        Me.statesTextBox.TabIndex = 3
        '
        'App
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 600)
        Me.Controls.Add(Me.statesTextBox)
        Me.Name = "App"
        Me.Text = "Bwl SmartHome Server"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.statesTextBox, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents startTimer As Timer
    Friend WithEvents _stateTimer As Timer
    Friend WithEvents statesTextBox As TextBox
End Class
