Imports System.Text
Imports System.Timers
Imports Bwl.Framework
Imports Bwl.Network.ClientServer
Public Class SmartHomeClient
    Public Property SmartHome As New SmartHome
    Public Event SendObjectsSchemesTimer()

    Private _sendObjectsTimer As New System.Timers.Timer
    Private _logger As Logger
    Private WithEvents _client As MessageTransport

    Public Sub New(rootStorage As SettingsStorage, rootLogger As Logger)
        Dim rnd As New Random
        _client = New MessageTransport(rootStorage.CreateChildStorage("Transport"), rootLogger.CreateChildLogger("Transport"), "NetClient", "localhost:3210", "SmartHomeClient" + rnd.Next.ToString, "SmartHomeServer", "SmartHomeServer", True)
        _logger = rootLogger
        SmartHome.Objects = New RemoteSmartObjects(_client)
        _sendObjectsTimer.Interval = 30000
        _sendObjectsTimer.AutoReset = True
        _sendObjectsTimer.Start()
        AddHandler _sendObjectsTimer.Elapsed, AddressOf SendObjectsTimerHandler
        Try
            _client.OpenAndRegister()
            'SendObjectsTimerHandler()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SendObjectsTimerHandler()
        If _client IsNot Nothing AndAlso _client.IsConnected Then
            RaiseEvent SendObjectsSchemesTimer()
        End If
    End Sub

    Public ReadOnly Property Transport As MessageTransport
        Get
            Return _client
        End Get
    End Property
End Class
