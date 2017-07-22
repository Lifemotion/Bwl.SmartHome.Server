<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SmartHomeClientBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SmartHomeClientBase))
        Me.niTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.bLocalDebug = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.ExtendedView = False
        Me.logWriter.Location = New System.Drawing.Point(2, 80)
        Me.logWriter.Size = New System.Drawing.Size(514, 233)
        '
        'niTray
        '
        Me.niTray.Icon = CType(resources.GetObject("niTray.Icon"), System.Drawing.Icon)
        Me.niTray.Text = "SmartHome"
        Me.niTray.Visible = True
        '
        'bLocalDebug
        '
        Me.bLocalDebug.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bLocalDebug.Location = New System.Drawing.Point(393, 27)
        Me.bLocalDebug.Name = "bLocalDebug"
        Me.bLocalDebug.Size = New System.Drawing.Size(112, 23)
        Me.bLocalDebug.TabIndex = 3
        Me.bLocalDebug.Text = "Local Debug"
        Me.bLocalDebug.UseVisualStyleBackColor = True
        '
        'SmartHomeClientBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 314)
        Me.Controls.Add(Me.bLocalDebug)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "SmartHomeClientBase"
        Me.Text = "SmartHomeClientBase"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.bLocalDebug, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents niTray As Windows.Forms.NotifyIcon
    Public WithEvents bLocalDebug As Windows.Forms.Button
End Class
