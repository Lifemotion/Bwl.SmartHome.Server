Imports System.Runtime.Serialization
Imports Bwl.SmartHome

<CollectionDataContract>
Public Class SmartObjectsList
    Inherits List(Of SmartObject)
End Class

<DataContract>
Public Class SmartObject

    <DataMember> Public Property Guid As String = ""
    <DataMember> Public Property Config As New SmartObjectConfig
    <DataMember> Public Property States As New List(Of SmartState)

    <IgnoreDataMember> Public Property LastUpdated As DateTime

    Public Sub New()

    End Sub

    Public Sub New(guid As String)
        Me.Guid = guid
    End Sub

    Public Sub LoadConfig(configFile As String)
        If IO.File.Exists(configFile) Then
            Try
                _Config = Serializer.LoadObjectFromJsonFile(Of SmartObjectConfig)(configFile)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub SetObject(newObj As SmartObject, mask As SmartObjectSetMask)
        If Guid <> newObj.Guid Then Throw New Exception("Cannot set SmartObject from object with different Guid")
        LastUpdated = Now
        If mask And SmartObjectSetMask.configAll Then
            _Config = newObj.Config
        End If
        If mask And SmartObjectSetMask.statesAll Then
            _States = newObj.States
        End If
        If mask And SmartObjectSetMask.configOnlyReplaceEmpty Then
            If Config.Caption = "" Then Config.Caption = newObj.Config.Caption
            If Config.Category = SmartObjectCategory.generic Then Config.Category = newObj.Config.Category
            If Config.ClassID = "" Then Config.ClassID = newObj.Config.ClassID
            If Config.Groups Is Nothing OrElse Config.Groups.Length = 0 Then Config.Groups = newObj.Config.Groups
            If Config.InterfaceGuid = "" Then Config.InterfaceGuid = newObj.Config.InterfaceGuid
            If Config.Location = "" Then Config.Location = newObj.Config.Location
            If Config.OtherParameters = "" Then Config.OtherParameters = newObj.Config.OtherParameters
            If Config.ShortName = "" Then Config.ShortName = newObj.Config.ShortName
        End If
        If mask And SmartObjectSetMask.statesOnlyReplaceEmpty Then
            For Each newState In newObj.States
                Dim found As SmartState = Nothing
                For Each oldState In States
                    If oldState.ID = newState.ID Then found = oldState
                Next
                If found Is Nothing Then
                    found = New SmartState
                    found.ID = newState.ID
                    States.Add(found)
                End If
                If found.Value = "" Then
                    found.Caption = newState.Caption
                    found.Type = newState.Type
                    found.Value = newState.Value
                End If
            Next
        End If
    End Sub

    Public Sub SetStateValue(stateId As String, value As String)
        For Each state In States
            If state.ID = stateId Then
                state.Value = value
            End If
        Next
    End Sub

    Public Function GetStateValue(stateId As String) As String
        For Each state In States
            If state.ID = stateId Then
                Return state.Value
            End If
        Next
        Return ""
    End Function
End Class
