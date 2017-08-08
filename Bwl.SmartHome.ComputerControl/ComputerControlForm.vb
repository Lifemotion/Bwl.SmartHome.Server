Imports Bwl.Framework

Public Class ComputerControlForm
    Inherits SmartHomeClientBase
    Private _guid As New StringSetting(_storage, "ComputerObjectGuid", GuidTool.GuidToString)
    Private _computerObjectScheme As New SmartObjectScheme

    Private _muteState As New SmartStateScheme("mute", SmartStateType.actionOnOff, "Отключить звук")
    Private _blackscreenState As New SmartStateScheme("blackscreen", SmartStateType.actionButton, "Отключить экран")
    Private _shutdownState As New SmartStateScheme("shutdown", SmartStateType.actionButton, "Завершение работы")
    Private _paramtersState As New SmartStateScheme("parameters", SmartStateType.actionString, "Параметры")
    Private _stateState As New SmartStateScheme("state", SmartStateType.stateString, "Состояние")
    Private _activityState As New SmartStateScheme("activity", SmartStateType.stateYesNo, "Активность")

    Private _keyMon As New Keymon(_client, AppBase.DataFolder + "\rules.txt")

    Private Sub ComputerControlApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _computerObjectScheme.ClassID = "BwlComputerControlApp"
        _computerObjectScheme.DefaultCaption = "Компьютер " + My.Computer.Name
        _computerObjectScheme.DefaultCategory = SmartObjectCategory.generic
        _computerObjectScheme.DefaultGroups = {"Компьютеры"}
        _computerObjectScheme.States.Add(_muteState)
        _computerObjectScheme.States.Add(_blackscreenState)
        _computerObjectScheme.States.Add(_shutdownState)
        _computerObjectScheme.States.Add(_paramtersState)
        _computerObjectScheme.States.Add(_stateState)
        _computerObjectScheme.States.Add(_activityState)
        AddHandler _client.SmartHome.Objects.StateChanged, AddressOf StateChangedHandler
        AddHandler _client.SendObjectsSchemesTimer, AddressOf SendObjectsTimerHandler
        _client.SendObjectsTimerHandler()
        Text += " " + Application.ProductVersion
        niTray.Text = "BWL SH: ComputerControl"
        niTray.Icon = Me.Icon
#If Not DEBUG Then
        Dim invisible As New Threading.Thread(Sub() Me.Invoke(Sub() Hide()))
        invisible.Start()
#End If
    End Sub

    Private Sub SendObjectsTimerHandler()
        Try
            _client.SmartHome.Objects.SetScheme(_guid, _computerObjectScheme)
            _client.SmartHome.Objects.SetValue(_guid, _stateState.ID, "Time: " + Now.ToString, ChangedBy.device)
            _client.SmartHome.Objects.SetValue(_guid, _activityState.ID, "yes", ChangedBy.device)

        Catch ex As Exception
            _logger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub StateChangedHandler(objGuid As String, stateId As String, lastValue As String, currentValue As String, changedBy As ChangedBy)
        If objGuid = _guid And changedBy = ChangedBy.user Or changedBy = ChangedBy.script Then
            If stateId = _muteState.ID Then
                If currentValue = "on" Then
                    Shell("nircmdc mutesysvolume 1")
                Else
                    Shell("nircmdc mutesysvolume 0")
                End If
            End If

            If stateId = _blackscreenState.ID Then
                If currentValue = "1" Then
                    Shell("nircmdc monitor off")
                    _client.SmartHome.Objects.SetValue(objGuid, stateId, "0", ChangedBy.device)
                End If
            End If

            If stateId = _shutdownState.ID Then
                If currentValue = "1" Then
                    Shell("shutdown /s /t 60")
                    _client.SmartHome.Objects.SetValue(objGuid, stateId, "0", ChangedBy.device)
                End If
            End If
        End If
    End Sub

End Class