<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SmartHomeDebugger
    Inherits System.Windows.Forms.Form

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gbSelected = New System.Windows.Forms.GroupBox()
        Me.lbStates = New System.Windows.Forms.ListBox()
        Me.bUpdateSelectedObject = New System.Windows.Forms.Button()
        Me.bObjectsUpdate = New System.Windows.Forms.Button()
        Me.lbObjects = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.gbSelected.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.bUpdateSelectedObject)
        Me.GroupBox1.Controls.Add(Me.gbSelected)
        Me.GroupBox1.Controls.Add(Me.lbObjects)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(356, 473)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Objects"
        '
        'gbSelected
        '
        Me.gbSelected.Controls.Add(Me.Label2)
        Me.gbSelected.Controls.Add(Me.lbStates)
        Me.gbSelected.Location = New System.Drawing.Point(6, 252)
        Me.gbSelected.Name = "gbSelected"
        Me.gbSelected.Size = New System.Drawing.Size(344, 212)
        Me.gbSelected.TabIndex = 2
        Me.gbSelected.TabStop = False
        Me.gbSelected.Text = "Selected Object"
        '
        'lbStates
        '
        Me.lbStates.FormattingEnabled = True
        Me.lbStates.Location = New System.Drawing.Point(6, 19)
        Me.lbStates.Name = "lbStates"
        Me.lbStates.Size = New System.Drawing.Size(328, 160)
        Me.lbStates.TabIndex = 1
        '
        'bUpdateSelectedObject
        '
        Me.bUpdateSelectedObject.Location = New System.Drawing.Point(272, 245)
        Me.bUpdateSelectedObject.Name = "bUpdateSelectedObject"
        Me.bUpdateSelectedObject.Size = New System.Drawing.Size(75, 23)
        Me.bUpdateSelectedObject.TabIndex = 1
        Me.bUpdateSelectedObject.Text = "Update"
        Me.bUpdateSelectedObject.UseVisualStyleBackColor = True
        '
        'bObjectsUpdate
        '
        Me.bObjectsUpdate.Location = New System.Drawing.Point(284, 8)
        Me.bObjectsUpdate.Name = "bObjectsUpdate"
        Me.bObjectsUpdate.Size = New System.Drawing.Size(75, 23)
        Me.bObjectsUpdate.TabIndex = 1
        Me.bObjectsUpdate.Text = "Update"
        Me.bObjectsUpdate.UseVisualStyleBackColor = True
        '
        'lbObjects
        '
        Me.lbObjects.FormattingEnabled = True
        Me.lbObjects.Location = New System.Drawing.Point(6, 19)
        Me.lbObjects.Name = "lbObjects"
        Me.lbObjects.Size = New System.Drawing.Size(344, 199)
        Me.lbObjects.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 224)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Double click to select object"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Double click to change"
        '
        'tRefresh
        '
        Me.tRefresh.Enabled = True
        Me.tRefresh.Interval = 2000
        '
        'SmartHomeDebugger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 491)
        Me.Controls.Add(Me.bObjectsUpdate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SmartHomeDebugger"
        Me.Text = "SmartHomeDebugger"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSelected.ResumeLayout(False)
        Me.gbSelected.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents gbSelected As Windows.Forms.GroupBox
    Friend WithEvents lbStates As Windows.Forms.ListBox
    Friend WithEvents bObjectsUpdate As Windows.Forms.Button
    Friend WithEvents lbObjects As Windows.Forms.ListBox
    Friend WithEvents bUpdateSelectedObject As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tRefresh As Windows.Forms.Timer
End Class
