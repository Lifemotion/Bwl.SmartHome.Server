Imports Bwl.Framework

Public Class SmartHomeService
    Private _logger As Logger
    Private _storage As SettingsStorage
    Private _dataFolder As String
    Private _smartHome As New SmartHome
    Private _netServer As SmartHomeNetServer

    Public Sub New(storage As SettingsStorage, logger As Logger, dataFolder As String)
        Me._storage = storage
        Me._logger = logger
        Me._dataFolder = dataFolder

        _netServer = New SmartHomeNetServer(storage, logger, _smartHome)
    End Sub

    Public Sub Start()
        _netServer.Start()
    End Sub

    Public ReadOnly Property SmartHome As SmartHome
        Get
            Return _smartHome
        End Get
    End Property
End Class
