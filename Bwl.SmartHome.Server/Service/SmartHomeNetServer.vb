Imports Bwl.Framework
Imports Bwl.Network.ClientServer
Imports Bwl.SmartHome

Public Class SmartHomeNetServer
    Private _logger As Logger
    Private WithEvents _transport As MessageTransport

    Private _smartHome As SmartHome

    Public Sub New(storage As SettingsStorage, logger As Logger, smartHome As SmartHome)
        _transport = New MessageTransport(storage.CreateChildStorage("Transport"), logger.CreateChildLogger("Logger"), "NetServer", "localhost:3210", "SmartHomeServer", "", "SmartHomeServer", False)
        _smartHome = smartHome
        _logger = logger
        AddHandler _smartHome.Objects.StateChanged, AddressOf ObjectStateChangedHandler
    End Sub

    Private Sub ObjectStateChangedHandler(objGuid As String, stateId As String, lastValue As String, currentValue As String, changedBy As ChangedBy)
        Dim response As New NetMessage("S", "ObjectStateChanged", objGuid, stateId, lastValue, currentValue, changedBy.ToString)
        Try
            _transport.SendMessage(response)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Start()
        _transport.OpenAndRegister()
    End Sub

    Private Sub _server_ReceivedMessage(request As NetMessage) Handles _transport.ReceivedMessage
        Try
            Select Case request.Part(0).ToLower
                Case "SmartObjects".ToLower
                    Select Case request.Part(1).ToLower
                        Case "GetObjects".ToLower
                            Dim filter = request.Part(2)
                            Dim objs = _smartHome.Objects.GetObjects(filter)
                            Dim response As New NetMessage(request, "GetObjectsResult")
                            For i = 0 To objs.Length - 1
                                Dim serialized = Serializer.SaveObjectToJsonBytes(objs(i))
                                response.PartBytes(1 + i) = serialized
                            Next
                            _transport.SendMessage(response)
                        Case "GetObject".ToLower
                            Dim id = request.Part(2)
                            Dim obj As SmartObject = _smartHome.Objects.GetObject(id)
                            Dim response As New NetMessage(request, "GetObjectResult", "Ok")
                            Dim serialized = Serializer.SaveObjectToJsonBytes(obj)
                            response.PartBytes(2) = serialized
                            _transport.SendMessage(response)
                        Case "SetObjectScheme".ToLower
                            Dim id = request.Part(2)
                            Dim scheme = Serializer.LoadObjectFromJsonBytes(Of SmartObjectScheme)(request.PartBytes(3))
                            _smartHome.Objects.SetScheme(id, scheme)
                            Dim response As New NetMessage(request, "SetObjectSchemeResult", "Ok")
                            _transport.SendMessage(response)
                            _logger.AddDebug("Updated Object Scheme: " + id)
                        Case "SetObjectUserConfig".ToLower
                            Dim id = request.Part(2)
                            Dim config = Serializer.LoadObjectFromJsonBytes(Of SmartObjectUserConfig)(request.PartBytes(3))
                            _smartHome.Objects.SetUserConfig(id, config)
                            Dim response As New NetMessage(request, "SetObjectUserConfigResult", "Ok")
                            _transport.SendMessage(response)
                            _logger.AddMessage("Updated Object Config: " + id)
                        Case "GetStateValue".ToLower
                            Dim id = request.Part(2)
                            Dim stateId = request.Part(3)
                            Dim response As New NetMessage(request, "GetStateValueResult", _smartHome.Objects.GetValue(id, stateId))
                            _transport.SendMessage(response)
                        Case "SetStateValue".ToLower
                            Dim id = request.Part(2)
                            Dim stateId = request.Part(3)
                            Dim value = request.Part(4)
                            Dim changedBy As ChangedBy = ChangedBy.unknown
                            Select Case request.Part(5)
                                Case "device" : changedBy = ChangedBy.device
                                Case "script" : changedBy = ChangedBy.script
                                Case "user" : changedBy = ChangedBy.user
                            End Select
                            _smartHome.Objects.SetValue(id, stateId, value, changedBy)
                            Dim response As New NetMessage(request, "SetStateValueResult", "Ok")
                            _transport.SendMessage(response)
                    End Select
            End Select
        Catch ex As Exception
            _logger.AddWarning("Net Received Handler Error: " + ex.Message)
        End Try
    End Sub

    Private Sub _transport_RegisterClientRequest(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String) Handles _transport.RegisterClientRequest
        allowRegister = True
    End Sub
End Class
