Imports Bwl.Framework

Public Class SmartHomeService
    Private _logger As Logger
    Private _storage As SettingsStorage
    Private _dataFolder As String
    Private _ui As IAutoUI
    Private _smartHome As New SmartHome
    Private _netServer As SmartHomeNetServer

    Public Sub New(storage As SettingsStorage, logger As Logger, dataFolder As String, ui As IAutoUI)
        _storage = storage
        _logger = logger
        _dataFolder = dataFolder
        _ui = ui
        Dim form As New AutoFormDescriptor(_ui, "SmartHomeForm") With {.Text = "SmartHome Server", .LoggerExtended = False, .FormHeight = 400}
        _smartHome.Objects = New SmartObjectsCollection(IO.Path.Combine(dataFolder, "objects"))
        _netServer = New SmartHomeNetServer(storage, logger, _smartHome)
    End Sub

    Public Sub Start()
        _logger.AddMessage("Starting...")
        _netServer.Start()
    End Sub

    Public ReadOnly Property SmartHome As SmartHome
        Get
            Return _smartHome
        End Get
    End Property
End Class
