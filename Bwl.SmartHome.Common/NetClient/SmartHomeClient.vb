Imports System.Text
Imports Bwl.Framework
Imports Bwl.Network.ClientServer
Public Class SmartHomeClient
    Public Property SmartHome As New SmartHome

    Private _logger As Logger
    Private WithEvents _client As MessageTransport

    Public Sub New(rootStorage As SettingsStorage, rootLogger As Logger)
        Dim rnd As New Random
        _client = New MessageTransport(rootStorage.CreateChildStorage("Transport"), rootLogger.CreateChildLogger("Transport"), "NetClient", "localhost:3210", "SmartHomeClient" + rnd.Next.ToString, "SmartHomeServer", "SmartHomeServer", True)
        _logger = rootLogger
        SmartHome.Objects = New RemoteSmartObjects(_client)
    End Sub

    Public ReadOnly Property Transport As MessageTransport
        Get
            Return _client
        End Get
    End Property
End Class
