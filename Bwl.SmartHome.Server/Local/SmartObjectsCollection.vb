Imports Bwl.SmartHome

Public Class SmartObjectsCollection
    Implements ISmartObjectsCollection

    Public ReadOnly Property Path As String
    Private _list As New List(Of SmartObject)
    Public Event CollectionChanged As ISmartObjectsCollection.CollectionChangedEventHandler Implements ISmartObjectsCollection.CollectionChanged
    Public Event StateChanged As ISmartObjectsCollection.StateChangedEventHandler Implements ISmartObjectsCollection.StateChanged

    Public Sub New(storagePath As String)
        If Not IO.Directory.Exists(storagePath) Then IO.Directory.CreateDirectory(storagePath)
        Me.Path = storagePath
    End Sub

    Public Function GetObject(guid As String) As SmartObject Implements ISmartObjectsCollection.GetObject
        Dim obj As SmartObject = Nothing
        For Each item In _list
            If item.Guid = guid Then
                obj = item
            End If
        Next
        Return obj
    End Function

    Public Function GetObjects(guids As String) As SmartObject() Implements ISmartObjectsCollection.GetObjects
        Return _list.ToArray
    End Function

    Public Sub SetScheme(objGuid As String, scheme As SmartObjectScheme) Implements ISmartObjectsCollection.SetScheme
        Dim obj = GetObject(objGuid)
        If obj Is Nothing Then
            obj = New SmartObject(objGuid)
            _list.Add(obj)
            Dim configFile = IO.Path.Combine(Me.Path, obj.Guid + ".userconfig.json")
            obj.LoadConfig(configFile)
        End If
        obj.Scheme = scheme
    End Sub

    Public Sub SetUserConfig(objGuid As String, config As SmartObjectUserConfig) Implements ISmartObjectsCollection.SetUserConfig
        Dim obj = GetObject(objGuid)
        If obj IsNot Nothing Then
            obj.UserConfig = config
            Dim configFile = IO.Path.Combine(Me.Path, obj.Guid + ".userconfig.json")
            Serializer.SaveObjectToJsonFile(obj.UserConfig, configFile)
        End If
    End Sub

    Public Function GetValue(objGuid As String, stateId As String) As String Implements ISmartObjectsCollection.GetValue
        Dim obj = GetObject(objGuid)
        If obj IsNot Nothing Then
            Return obj.GetStateValue(stateId)
        Else
            Return ""
        End If
    End Function

    Public Sub SetValue(objGuid As String, stateId As String, value As String, changedBy As ChangedBy) Implements ISmartObjectsCollection.SetValue
        Dim obj = GetObject(objGuid)
        If obj IsNot Nothing Then
            Dim lastVal = obj.GetStateValue(stateId)
            If lastVal <> value Then
                obj.SetStateValue(stateId, value)
                RaiseEvent StateChanged(objGuid, stateId, lastVal, value, changedBy)
            End If
        End If
    End Sub
End Class
