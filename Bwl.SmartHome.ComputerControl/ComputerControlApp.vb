Imports Bwl.Framework

Public Class ComputerControlApp
    Inherits FormAppBase
    Private _client As New SmartHomeClient(_storage, _logger)
    Private _guid As New StringSetting(_storage, "ComputerObjectGuid", GuidTool.GuidToString)
    Private _computerObject As SmartObject
    Dim muteState As New SmartState
    Dim blackscreenState As New SmartState
    Dim shutdownState As New SmartState

    Private Sub ComputerControlApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _computerObject = New SmartObject(_guid.Value)
        _computerObject.Config.Caption = "Компьютер " + _guid.Value
        _computerObject.Config.Category = SmartObjectCategory.generic
        _computerObject.Config.Groups = {"Компьютеры"}
        muteState.ID = "mute"
        muteState.Caption = "Отключить звук"
        muteState.Type = SmartStateType.actionOnOff
        muteState.Value = "off"
        blackscreenState.ID = "blackscreen"
        blackscreenState.Caption = "Отключить экран"
        blackscreenState.Type = SmartStateType.actionButton
        blackscreenState.Value = "off"
        shutdownState.ID = "shutdown"
        shutdownState.Caption = "Выключить ПК"
        shutdownState.Type = SmartStateType.actionButton
        shutdownState.Value = ""
        _computerObject.States.Add(muteState)
        _computerObject.States.Add(blackscreenState)
        _computerObject.States.Add(shutdownState)
    End Sub

    Private Sub setObjects_Tick(sender As Object, e As EventArgs) Handles setObjects.Tick
        If _client.Transport.IsConnected AndAlso _computerObject IsNot Nothing Then
            _client.SmartHome.Objects.SetObject(_computerObject, SmartObjectSetMask.configOnlyReplaceEmpty Or SmartObjectSetMask.statesOnlyReplaceEmpty)

            _computerObject = _client.SmartHome.Objects.GetObject(_computerObject.Guid)
            Dim changed As Boolean = False
            For Each state In _computerObject.States
                If state.ID = "mute" Then
                    If state.ValueChanged = True Then
                        state.ValueChanged = False
                        changed = True
                        If state.Value = "on" Then
                            Shell("nircmdc mutesysvolume 1")
                        Else
                            Shell("nircmdc mutesysvolume 0")
                        End If
                    End If
                End If
                If state.ID = "blackscreen" Then
                    If state.ValueChanged = True Then
                        changed = True
                        If state.Value = "1" Then
                            Shell("nircmdc monitor off")
                            state.Value = ""
                        End If
                        state.ValueChanged = False
                    End If
                End If
                If state.ID = "shutdown" Then
                    If state.ValueChanged = True Then
                        changed = True
                        If state.Value = "1" Then
                            Shell("shutdown -t 60")
                            state.Value = ""
                        End If
                        state.ValueChanged = False
                    End If
                End If
            Next

            If changed Then
                _client.SmartHome.Objects.SetObject(_computerObject, SmartObjectSetMask.statesAll)
            End If
        End If
    End Sub
End Class
