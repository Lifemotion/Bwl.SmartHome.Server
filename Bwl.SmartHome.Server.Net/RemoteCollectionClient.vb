Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging

    Public Class RemoteCollectionClient(Of T As IRemoteObject)
    Private _netClient As NetClient
    Private _id As String
    Private _logger As Logger

    Public Event ObjectUpdated(sender As Object, obj As T)
    Private _ident As String


    Public Sub New(id As String, netClient As NetClient, logger As Logger)
        _id = id
        _netClient = netClient
        _logger = logger
        _ident = "remote-collection-" + _id
        AddHandler _netClient.ReceivedMessage, AddressOf ReceivedMessageHandler
    End Sub

    Public Function GetCollection() As List(Of T)
        Dim msg As New NetMessage("S", _ident, "get-all")
        Dim answer = _netClient.SendMessageWaitAnswer(msg, _ident + "-objects")
    End Function

    Private Sub ReceivedMessageHandler(message As NetMessage)
        Try
            If message.Part(0).ToLower() = "remote-collection-" + _id Then
                Select Case message.Part(1)

                End Select
            End If
        Catch ex As Exception
            _logger.AddWarning(_ident + ": " + ex.Message)
        End Try
    End Sub
End Class
