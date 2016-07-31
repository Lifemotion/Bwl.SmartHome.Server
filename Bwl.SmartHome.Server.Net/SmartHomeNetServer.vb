Imports System.Text
Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging

Public Class SmartHomeNetServer
    Private _logger As Logger
    Private _storage As SettingsStorage
    Private _port As Integer = 3060
    Private _storageServer As SettingsServer
    Private _loggerServer As LogsServer
    Private WithEvents _server As New NetServer

    Private _smartHome As SmartHome
    Private _smartHomeObjectsServer As RemoteCollectionServer(Of SmartObject)

    Public Sub New(storage As SettingsStorage, logger As Logger, smartHome As SmartHome)
        _smartHome = smartHome
        _logger = logger.CreateChildLogger("NetServer")
        _storage = storage.CreateChildStorage("NetServer")
        _storageServer = New SettingsServer(storage, _server, "BwlSmartHome")
        _loggerServer = New LogsServer(logger, _server, "BwlSmartHome")

        _smartHomeObjectsServer = New RemoteCollectionServer(Of SmartObject)("smartobject", _server, _logger, _smartHome.Objects)
        AddHandler _smartHomeObjectsServer.ObjectUpdated, Sub(sender As Object, obj As SmartObject) obj.LastUpdated = Now
    End Sub

    Public Sub Start()
        _server.StartServer(_port, False)
        _logger.AddMessage("Сетевой сервер запущен, порт " + _port.ToString)
    End Sub

    Private Sub _server_ReceivedMessage(message As NetMessage, client As ConnectedClient) Handles _server.ReceivedMessage
        Try
            Select Case message.Part(0).ToLower()
                Case "debug"
                    Select Case message.Part(1)
                        Case "getstates"
                            Dim text = GlobalStates.ToString()
                            client.SendMessage(New NetMessage("S", "debug", "states", text))
                            _logger.AddDebug("ReceivedMessage/debug/getstates")
                    End Select
                Case "testrequest"
                    client.SendMessage(New NetMessage("S", "testanswer"))
                'Case "smartobjects"
                '    _lastRequest = Now
                '    Select Case message.Part(1)
                '        Case "get-all"
                '            Dim objs = Serializer.SaveObjectToJsonString(_smartHome.Objects)
                '            Dim msg As New NetMessage("P", "smartobjects", "objects") : msg.Part(2) = objs
                '            client.SendMessage(msg)
                '        Case "get-id"
                '            Dim id = message.Part(2)
                '            Dim obj = ""
                '            For Each so In _smartHome.Objects
                '                If so.ObjectConfig.Guid = id Then obj = Serializer.SaveObjectToJsonString(_smartHome.Objects(id))
                '            Next
                '            Dim msg As New NetMessage("P", "smartobjects", "object")
                '            client.SendMessage(msg)
                '        Case "set-id"
                '            Dim objString = message.Part(2)
                '            Try
                '                Dim obj = Serializer.LoadObjectFromJsonString(Of SmartObject)(objString)
                '                obj.LastUpdated = Now
                '                Dim found As SmartObject = Nothing
                '                For Each so In _smartHome.Objects
                '                    If so.ObjectConfig.Guid = obj.ObjectConfig.Guid Then
                '                        found = so
                '                    End If
                '                Next
                '                If found IsNot Nothing Then _smartHome.Objects.Remove(found)
                '                _smartHome.Objects.Add(obj)
                '            Catch ex As Exception
                '            End Try
                '    End Select
                Case "attributes"
                    Select Case message.Part(1)
                        Case "getorlanattributes"
                            client.SendMessage(New NetMessage("P", "attributes", "orlanattributes"))
                            _logger.AddDebug("ReceivedMessage/attributes/getorlanattributes")
                    End Select
            End Select
        Catch ex As Exception
            _logger.AddWarning("NetReceived: " + ex.Message)
        End Try
    End Sub
End Class
