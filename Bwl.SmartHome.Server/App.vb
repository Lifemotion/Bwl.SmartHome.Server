Imports Bwl.Framework

Module App
    Private _app As New AppBase
    Private _service As New SmartHomeService(_app.RootStorage, _app.RootLogger, _app.DataFolder, _app.AutoUI)
    Private WithEvents _debugFormButton As New AutoButton(_app.AutoUI, "DebugFormButton")

    Public Sub Main()
        Dim startThread As New Threading.Thread(Sub() Start())
        startThread.Start()
        Application.EnableVisualStyles()
        Application.Run(AutoUIForm.Create(_app))
    End Sub

    Public Sub Start()
        Threading.Thread.Sleep(1000)
        _service.Start()
    End Sub

    Private Sub _debugFormButton_Click(source As AutoButton) Handles _debugFormButton.Click
        Dim form As New SmartHomeDebugger
        form.SmartHome = _service.SmartHome
        form.Show()
    End Sub
End Module
