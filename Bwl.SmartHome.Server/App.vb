Imports Bwl.Framework

Public Class App
    Inherits FormAppBase
    Private _service As New SmartHomeService(_storage, _logger, AppBase.DataFolder)

    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub startTimer_Tick(sender As Object, e As EventArgs) Handles startTimer.Tick
        startTimer.Enabled = False
        _logger.AddMessage("Starting...")
        _service.Start()
    End Sub

    Private Sub _stateTimer_Tick(sender As Object, e As EventArgs) Handles _stateTimer.Tick
        GlobalStates.SetState(_service.SmartHome, "SmartObjects", _service.SmartHome.Objects.Count.ToString, "", 10)

        Dim text = GlobalStates.ToString
        statesTextBox.Text = text
        statesTextBox.Refresh()
    End Sub
End Class
