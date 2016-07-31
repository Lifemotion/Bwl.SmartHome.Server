Imports Bwl.Framework

Module App
    Private _app As New AppBase
    Private _service As New SmartHomeService(_app.RootStorage, _app.RootLogger, _app.DataFolder, _app.AutoUI)

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
End Module
