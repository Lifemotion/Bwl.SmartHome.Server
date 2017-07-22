Imports System.Windows.Forms
Imports Bwl.Framework
Imports Microsoft.Win32

Public Class SmartHomeClientBase
    Inherits FormAppBase

    Protected _client As New SmartHomeClient(_storage, _logger)
    Protected _autostartSetting As New BooleanSetting(_storage, "Autostart", False)

    Private Sub ComputerControlApp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Hide()
        End If
        If _autostartSetting.Value Then
            Dim rkey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run")
            rkey.SetValue("SmartHome ComputerControl", Application.ExecutablePath)
        End If

    End Sub

    Private Sub niTray_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles niTray.MouseDoubleClick
        Me.Hide()
        Me.Show()
    End Sub

    Private Sub bLocalDebug_Click(sender As Object, e As EventArgs) Handles bLocalDebug.Click
        _client.StartLocalDebug()
    End Sub

End Class