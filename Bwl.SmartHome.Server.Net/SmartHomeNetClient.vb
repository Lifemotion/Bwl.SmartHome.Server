Imports System.IO
Imports System.Text
Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging
Public Class SmartHomeNetClient

    Private Class LogWriter
        Implements ILogWriter
        Private _logger As Logger

        Public Sub New(logger As Logger)
            _logger = logger
        End Sub

        Public Sub CategoryListChanged() Implements ILogWriter.CategoryListChanged

        End Sub

        Public Sub ConnectedToLogger(logger As Logger) Implements ILogWriter.ConnectedToLogger

        End Sub

        Public Property LogEnabled As Boolean Implements ILogWriter.LogEnabled

        Public Sub WriteEvent(datetime As Date, path() As String, type As LogEventType, text As String, ParamArray params() As Object) Implements ILogWriter.WriteEvent
            _logger.Add(type, "[ ] " + text, params)
        End Sub
    End Class

    Event ObjectsReceived(objs As List(Of SmartObject))

    Public Sub Disconnect()
        Try
            _ConnectedAddress = ""
            _client.Disconnect()
            _lastAddressString = ""
        Catch
        End Try
    End Sub

    Public ReadOnly Property ConnectedAddress As String = ""

    Event StatesReceived(text As String)

    Private _logger As Logger
    Private _storage As SettingsStorage
    Private WithEvents _client As New NetClient
    Private WithEvents _storageClient As SettingsClient
    Private _loggerClient As LogsClient

    Private _serverName As StringSetting
    Private _serverPort As Integer = 3060
    Private _lastAddressString As String = ""

    Public Sub New(rootStorage As SettingsStorage, rootLogger As Logger, useRemoteSettingsLogs As Boolean)
        _logger = rootLogger.CreateChildLogger("NetClient")
        _storage = rootStorage.CreateChildStorage("NetClient")
        If useRemoteSettingsLogs Then
            _storageClient = New SettingsClient(_client, "BwlSmartHome")
            _loggerClient = New LogsClient(_client, "BwlSmartHome")
        Else
            _storageClient = New SettingsClient(_client, "BwlSmartHome")
            _loggerClient = New LogsClient(_client, "BwlSmartHome")
        End If
        _serverName = New StringSetting(_storage, "ServerName", "127.0.0.1")
        _loggerClient.ConnectWriter(New LogWriter(_logger.CreateChildLogger("RemoteService")))
    End Sub


    Public Sub Connect(addressString As String)
        Try
            _client.IgnoreNotConnected = True
            _client.Connect(addressString, 3060)
            _ConnectedAddress = addressString
            _logger.AddMessage("Подключено к серверу " + _serverName.Value + " " + _serverPort.ToString)
            _lastAddressString = addressString
        Catch ex As Exception
            _logger.AddMessage(ex.Message)
        End Try
        If _client.IsConnected Then _ConnectedAddress = addressString
    End Sub

    Public Sub Connect()
        Try
            _client.IgnoreNotConnected = True
            _client.Connect(_serverName, _serverPort)
            _logger.AddMessage("Подключено к серверу " + _serverName.Value + " " + _serverPort.ToString)
        Catch ex As Exception
            _logger.AddMessage(ex.Message)
        End Try
        If _client.IsConnected Then _ConnectedAddress = _serverName
    End Sub

    Public ReadOnly Property Connected As Boolean
        Get
            Return _client.IsConnected
        End Get
    End Property

    Public ReadOnly Property RemoteSettings As SettingsClient
        Get
            Return _storageClient
        End Get
    End Property

    Public Sub RequestRegions()
        _client.SendMessage(New NetMessage("P", "regionlib", "getregions")) 'Время запроса нужно для того, чтобы ни одна форма не отобразила "запрошенное ранее"
        _logger.AddDebug("RequestRegions/regionlib/getregions")
    End Sub

    Public Sub SendRegion(id As String)
        '    Dim regionBytes = RegionStorage.GetRegionAsBytes(id)
        Dim msg As New NetMessage("P", "regionlib", "setregion") : msg.PartBytes(2) = Encoding.UTF8.GetBytes(id) ' : msg.PartBytes(3) = regionBytes
        _client.SendMessage(msg)
        _logger.AddDebug("SendRegion/regionlib/setregion")
    End Sub

    Public Sub RequestAdminFrame()
        _client.SendMessage(New NetMessage("P", "adminvideo", "getframe"))
        _logger.AddDebug("RequestAdminFrame/adminvideo/getframe")
    End Sub

    Public Sub RequestOrlanEventFrame(frameId As String)
        Dim msg = New NetMessage("P", "orlanlogic", "getorlaneventframe") : msg.PartBytes(2) = Encoding.UTF8.GetBytes(frameId)
        _client.SendMessage(msg)
        _logger.AddDebug("RequestOrlanEventFrame/orlanlogic/getorlaneventframe")
    End Sub


    Public Sub RequestDebugStates()
        Dim msg = New NetMessage("P", "debug", "getstates")
        _client.SendMessage(msg)
        _logger.AddDebug("RequestDebugStates/debug/getstates")
    End Sub

    Public Sub ClearEventsStorage()
        _client.SendMessage(New NetMessage("P", "orlanlogic", "clearorlanevents"))
        _logger.AddDebug("ClearEventsStorage/orlanlogic/clearorlanevents")
    End Sub

    Private Sub _client_ReceivedMessage(message As NetMessage) Handles _client.ReceivedMessage
        Try
            Select Case message.Part(0).ToLower
                Case "regionlib"
                    Select Case message.Part(1)
                        Case "regions"
                            Dim regionsBytes = message.PartBytes(2)
                            '_regionStorage.LoadFromBytes(regionsBytes)
                            'RaiseEvent RegionsReceived(_regionStorage)
                            '_logger.AddDebug("ReceivedMessage/regionlib/regions")
                    End Select
                Case "debug"
                    Select Case message.Part(1)
                        Case "states"
                            Dim states = message.Part(2)
                            RaiseEvent StatesReceived(states)
                            _logger.AddDebug("ReceivedMessage/debug/states")
                    End Select
            End Select
        Catch ex As Exception
            _logger.AddWarning("NetReceived: " + ex.Message)
        End Try
    End Sub

End Class
