Imports Bwl.Framework
Imports Bwl.Network.ClientServer

Public Class SmartHomeNetServer
    Private _logger As Logger
    Private WithEvents _transport As MessageTransport

    Private _smartHome As SmartHome

    Public Sub New(storage As SettingsStorage, logger As Logger, smartHome As SmartHome)
        _transport = New MessageTransport(storage.CreateChildStorage("Transport"), logger.CreateChildLogger("Logger"), "NetServer", "localhost:3210", "SmartHomeServer", "", "SmartHomeServer", False)
        _smartHome = smartHome
        _logger = logger
    End Sub

    Public Sub Start()
        _transport.OpenAndRegister()
    End Sub

    Private Sub _server_ReceivedMessage(request As NetMessage) Handles _transport.ReceivedMessage
        Try
            Select Case request.Part(0).ToLower
                Case "SmartObjects".ToLower
                    Select Case request.Part(1).ToLower
                        Case "GetObjectList".ToLower
                            Dim response As New NetMessage(request, "GetObjectListResult", _smartHome.Objects.GetObjectList)
                            _transport.SendMessage(response)
                        Case "GetObject".ToLower
                            Dim id = request.Part(2)
                            Dim obj As SmartObject = _smartHome.Objects.GetObject(id)
                            Dim serialized = Serializer.SaveObjectToJsonBytes(obj)
                            Dim response As New NetMessage(request, "GetObjectResult", "Ok")
                            response.PartBytes(2) = serialized
                            _transport.SendMessage(response)
                        Case "SetObject".ToLower
                            Dim mask = request.Part(2)
                            Dim obj = Serializer.LoadObjectFromJsonBytes(Of SmartObject)(request.PartBytes(3))
                            _smartHome.Objects.SetObject(obj, mask)
                            Dim response As New NetMessage(request, "SetObjectResult", "Ok")
                            _transport.SendMessage(response)
                            _logger.AddDebug("Updated Object: " + obj.Guid + "_" + obj.Config.Caption)
                        Case "GetStateValue".ToLower
                            Dim id = request.Part(2)
                            Dim stateId = request.Part(3)
                            Dim response As New NetMessage(request, "GetStateValueResult", _smartHome.Objects.GetStateValue(id, stateId))
                            _transport.SendMessage(response)
                        Case "SetStateValue".ToLower
                            Dim id = request.Part(2)
                            Dim stateId = request.Part(3)
                            Dim value = request.Part(4)
                            _smartHome.Objects.SetStateValue(id, stateId, value)
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
