Imports Bwl.Network.ClientServer

Public Class RemoteSmartObjects
    Implements ISmartObjectsCollection

    Private WithEvents _client As MessageTransport
    Public Event CollectionChanged() Implements ISmartObjectsCollection.CollectionChanged
    Public Event StateChanged(objGuid As String, stateId As String, lastValue As String, currentValue As String, changedBy As ChangedBy) Implements ISmartObjectsCollection.StateChanged

    Public Sub New(client As MessageTransport)
        _client = client
    End Sub

    Public Function GetObject(guid As String) As SmartObject Implements ISmartObjectsCollection.GetObject
        Dim msg As New NetMessage("S", "SmartObjects", "GetObject", guid)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "GetObjectResult")
        If result.Part(1) <> "Ok" Then Throw New Exception("Result <> ok")
        Dim obj = Serializer.LoadObjectFromJsonBytes(Of SmartObject)(result.PartBytes(2))
        Return obj
    End Function

    Public Function GetObjects(filters As String) As SmartObject() Implements ISmartObjectsCollection.GetObjects
        Dim msg As New NetMessage("S", "SmartObjects", "GetObjects", filters)
        msg.ToID = _client.TargetSetting.Value
        Dim list As New List(Of SmartObject)
        Dim result = _client.SendMessageWaitAnswer(msg, "GetObjectsResult")
        If result IsNot Nothing Then
            For i = 1 To result.Count - 1
                Try
                    Dim obj = Serializer.LoadObjectFromJsonBytes(Of SmartObject)(result.PartBytes(i))
                    list.Add(obj)
                Catch ex As Exception
                End Try
            Next
        End If
        Return list.ToArray
    End Function

    Public Sub SetScheme(objGuid As String, scheme As SmartObjectScheme) Implements ISmartObjectsCollection.SetScheme
        Dim serialized = Serializer.SaveObjectToJsonBytes(scheme)
        Dim msg As New NetMessage("S", "SmartObjects", "SetObjectScheme", objGuid)
        msg.PartBytes(3) = serialized
        msg.ToID = _client.TargetSetting.Value
        _client.SendMessage(msg)
    End Sub

    Public Sub SetUserConfig(objGuid As String, config As SmartObjectUserConfig) Implements ISmartObjectsCollection.SetUserConfig
        Dim serialized = Serializer.SaveObjectToJsonBytes(config)
        Dim msg As New NetMessage("S", "SmartObjects", "SetObjectUserConfig", objGuid)
        msg.PartBytes(3) = serialized
        msg.ToID = _client.TargetSetting.Value
        _client.SendMessage(msg)
    End Sub

    Public Function GetValue(objGuid As String, stateId As String) As String Implements ISmartObjectsCollection.GetValue
        Dim msg As New NetMessage("S", "SmartObjects", "GetStateValue", objGuid, stateId)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "GetStateValueResult")
        Return result.Part(1)
    End Function

    Public Sub SetValue(objGuid As String, stateId As String, value As String, changedBy As ChangedBy) Implements ISmartObjectsCollection.SetValue
        Dim msg As New NetMessage("S", "SmartObjects", "SetStateValue", objGuid, stateId, value, changedBy.ToString)
        msg.ToID = _client.TargetSetting.Value
        Dim result = _client.SendMessageWaitAnswer(msg, "SetStateValueResult")
        If result.Part(1) <> "Ok" Then Throw New Exception("Result <> ok")
    End Sub

    Private Sub _client_ReceivedMessage(message As NetMessage) Handles _client.ReceivedMessage
        Select Case message.Part(0)
            Case "ObjectStateChanged"
                Dim changed As ChangedBy = ChangedBy.unknown
                Select Case message.Part(5)
                    Case "user" : changed = ChangedBy.user
                    Case "script" : changed = ChangedBy.script
                    Case "device" : changed = ChangedBy.device
                End Select
                Dim thread As New Threading.Thread(Sub()
                                                       RaiseEvent StateChanged(message.Part(1), message.Part(2), message.Part(3), message.Part(4), changed)
                                                   End Sub)
                thread.Start()
        End Select
    End Sub
End Class
