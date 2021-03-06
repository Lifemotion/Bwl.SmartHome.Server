﻿Imports System.Runtime.Serialization
Imports Bwl.SmartHome

<CollectionDataContract>
Public Class SmartObjectsList
    Inherits List(Of SmartObject)
End Class

<DataContract>
Public Class SmartObject

    <DataMember> Public Property Guid As String = ""
    <DataMember> Public Property UserConfig As New SmartObjectUserConfig
    <DataMember> Public Property Scheme As New SmartObjectScheme
    <DataMember> Public Property StateValues As New List(Of SmartStateValue)

    <IgnoreDataMember> Public Property LastUpdated As DateTime

    Public Sub New()

    End Sub

    Public Sub New(guid As String)
        Me.Guid = guid
    End Sub

    Public Sub LoadConfig(configFile As String)
        If IO.File.Exists(configFile) Then
            Try
                _UserConfig = Serializer.LoadObjectFromJsonFile(Of SmartObjectUserConfig)(configFile)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub SetStateValue(stateId As String, ByRef value As String)
        Dim state = Scheme.GetStateScheme(stateId)
        If state IsNot Nothing Then
            For Each stateVal In StateValues
                If stateVal.ID.ToLower = stateId.ToLower Then
                    If state.Type = SmartStateType.actionOnOff Then
                        If value.ToLower = "toggle" Then
                            If stateVal.Value = "off" Then value = "on" Else value = "off"
                        End If
                    End If
                    stateVal.Value = value
                    stateVal.Updated = Now
                    Return
                End If
            Next
            Dim newState As New SmartStateValue
            newState.ID = stateId
            newState.Value = value
            StateValues.Add(newState)
        End If
    End Sub

    Public Function GetStateValue(stateId As String) As String
        Dim state = Scheme.GetStateScheme(stateId)
        If state IsNot Nothing Then
            For Each stateVal In StateValues
                If stateVal.ID.ToLower = stateId.ToLower Then
                    stateVal.Requested = Now
                    Return stateVal.Value
                End If
            Next
            Dim newState As New SmartStateValue
            newState.ID = stateId
            newState.Value = ""
            StateValues.Add(newState)
            Return newState.Value
        Else
            Return ""
        End If
    End Function
End Class
