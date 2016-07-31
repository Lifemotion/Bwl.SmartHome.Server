Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging

Public Class RemoteCollectionServer(Of T As IRemoteObject)
    Private _netServer As NetServer
    Private _collection As ICollection(Of T)
    Private _id As String
    Private _logger As Logger

    Public Event ObjectUpdated(sender As Object, obj As T)

    Public Sub New(id As String, netServer As NetServer, logger As Logger, collection As IEnumerable(Of T))
        _id = id
        _netServer = netServer
        _collection = collection
        _logger = logger
        AddHandler netServer.ReceivedMessage, AddressOf ReceivedMessageHandler
    End Sub

    Private Sub ReceivedMessageHandler(message As NetMessage, client As ConnectedClient)
        Dim ident = "remote-collection-" + _id
        Try
            If message.Part(0).ToLower() = ident Then
                Select Case message.Part(1)
                    Case "get-all"
                        Dim objs = Serializer.SaveObjectToJsonString(_collection)
                        Dim msg As New NetMessage("P", ident + "-objects") : msg.Part(1) = objs
                        client.SendMessage(msg)
                    Case "get-id"
                        Dim id = message.Part(2)
                        Dim obj = ""
                        For Each so In _collection
                            If so.ID = id Then obj = Serializer.SaveObjectToJsonString(so)
                        Next
                        Dim msg As New NetMessage("P", ident + "-object", obj)
                        client.SendMessage(msg)
                    Case "set-id"
                        Dim objString = message.Part(2)
                        Try
                            Dim obj = Serializer.LoadObjectFromJsonString(Of T)(objString)
                            'TODO: many objs
                            Dim found As T
                            For Each so In _collection
                                If so.ID = obj.ID Then
                                    found = so
                                End If
                            Next
                            If found IsNot Nothing Then _collection.Remove(found)
                            _collection.Add(obj)
                            Dim msg As New NetMessage("P", ident + "-set-ok")
                            client.SendMessage(msg)
                            RaiseEvent ObjectUpdated(Me, obj)
                        Catch ex As Exception
                            _logger.AddWarning(ident + ": " + ex.Message)
                        End Try
                End Select
            End If
        Catch ex As Exception
            _logger.AddWarning(Ident + ": " + ex.Message)
        End Try
    End Sub
End Class
