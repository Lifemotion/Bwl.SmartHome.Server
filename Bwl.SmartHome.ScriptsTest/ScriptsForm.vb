Imports Bwl.Framework

Public Class ScriptsForm
    Inherits SmartHomeClientBase

    Private Sub ComputerControlApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler _client.SmartHome.Objects.StateChanged, AddressOf StateChangedHandler
        Text += " " + Application.ProductVersion
        niTray.Text = "BWL SH: Scripts"
        niTray.Icon = Me.Icon
#If Not DEBUG Then
        Dim invisible As New Threading.Thread(Sub() Me.Invoke(Sub() Hide()))
        invisible.Start()
#End If
    End Sub

    Private Sub SendObjectsTimerHandler()
        Try
            '     _client.SmartHome.Objects.SetScheme(_guid, _computerObjectScheme)
        Catch ex As Exception
            _logger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub StateChangedHandler(objGuid As String, stateId As String, lastValue As String, currentValue As String, changedBy As ChangedBy)
        If objGuid = "da72bbb9-7167-49c9-9949-6f35b9bf1140" Then
            If stateId = "SwitchAll" And currentValue = "off" Then
                Dim thread As New Threading.Thread(Sub()
                                                       Try
                                                           _client.SmartHome.Objects.SetValue("daa32c18-e0e1-4bf0-ab09-25f1e0753f1f", "RGB1Switch", "on", ChangedBy.script)
                                                       Catch ex As Exception
                                                       End Try
                                                       Threading.Thread.Sleep(10000)
                                                       Try
                                                           _client.SmartHome.Objects.SetValue("daa32c18-e0e1-4bf0-ab09-25f1e0753f1f", "RGB1Switch", "off", ChangedBy.script)
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub)
                thread.Start()
            End If
        End If
    End Sub

End Class