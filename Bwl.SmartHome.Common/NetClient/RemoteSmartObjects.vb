Imports Bwl.Network.ClientServer

Public Class RemoteSmartObjects
    Implements ISmartObjects

    Private WithEvents _client As MessageTransport

    Public Sub New(client As MessageTransport)
        _client = client
    End Sub

    Public ReadOnly Property Path As String = "" Implements ISmartObjects.Path

    Public Sub SetObject(obj As SmartObject, mask As SmartObjectSetMask) Implements ISmartObjects.SetObject
        Dim serialized = Serializer.SaveObjectToJsonBytes(obj)
        Dim msg As New NetMessage("S", "SmartObjects", "SetObject", CInt(mask).ToString)
        msg.PartBytes(3) = serialized
        msg.ToID = _client.TargetSetting.Value
        _client.SendMessage(msg)
    End Sub

    Public Function GetObject(guid As String) As SmartObject Implements ISmartObjects.GetObject
        Dim msg As New NetMessage("S", "SmartObjects", "GetObject", guid)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "GetObjectResult")
        If result.Part(1) <> "Ok" Then Throw New Exception("Result <> ok")
        Dim obj = Serializer.LoadObjectFromJsonBytes(Of SmartObject)(result.PartBytes(2))
        Return obj
    End Function

    Public Function GetObjectList() As String Implements ISmartObjects.GetObjectList
        Dim msg As New NetMessage("S", "SmartObjects", "GetObjectList")
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "GetObjectListResult")
        '    If result.Part(1) <> "OK" Then Throw New Exception("Result <> ok")
        Dim list = result.Part(1)
        Return list
    End Function

    Public Function GetStateValue(objGuid As String, stateId As String) As String Implements ISmartObjects.GetStateValue
        Dim msg As New NetMessage("S", "SmartObjects", "SetStateValue", objGuid, stateId)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "GetStateValueResult")
        Return result.Part(1)
    End Function

    Public Sub SetStateValue(objGuid As String, stateId As String, value As String) Implements ISmartObjects.SetStateValue
        Dim msg As New NetMessage("S", "SmartObjects", "SetStateValue", objGuid, stateId, value)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "SetStateValueResult")
        If result.Part(1) <> "Ok" Then Throw New Exception("Result <> ok")
    End Sub
End Class
