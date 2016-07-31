Public Class SmartObjectsServer
    Implements ISmartObjects
    Public ReadOnly Property Path As String Implements ISmartObjects.Path
    Private _list As New List(Of SmartObject)

    Public Sub New(storagePath As String)
        If Not IO.Directory.Exists(storagePath) Then IO.Directory.CreateDirectory(storagePath)
        Me.Path = storagePath
    End Sub

    Public Sub SetObject(newObj As SmartObject, mask As SmartObjectSetMask) Implements ISmartObjects.SetObject
        Dim oldObj = GetObject(newObj.Guid)
        oldObj.SetObject(newObj, mask)
        Dim configFile = IO.Path.Combine(Me.Path, oldObj.Guid + ".config.json")
        Serializer.SaveObjectToJsonFile(oldObj.Config, configFile)
    End Sub

    Public Function GetObjectList() As String Implements ISmartObjects.GetObjectList
        Dim list As String = ""
        For Each item In _list
            If list > "" Then list += ","
            list += item.Guid
        Next
        Return list
    End Function

    Public Function GetObject(guid As String) As SmartObject Implements ISmartObjects.GetObject
        Dim obj As SmartObject = Nothing
        For Each item In _list
            If item.Guid = guid Then
                obj = item
            End If
        Next
        If obj Is Nothing Then
            obj = New SmartObject(guid)
            _list.Add(obj)
            Dim configFile = IO.Path.Combine(Me.Path, obj.Guid + ".config.json")
            obj.LoadConfig(configFile)
        End If
        Return obj
    End Function

    Public Sub SetStateValue(objGuid As String, stateId As String, value As String) Implements ISmartObjects.SetStateValue
        Dim obj = GetObject(objGuid)
        obj.SetStateValue(stateId, value)
    End Sub

    Public Function GetStateValue(objGuid As String, stateId As String) As String Implements ISmartObjects.GetStateValue
        Dim obj = GetObject(objGuid)
        Return obj.GetStateValue(stateId)
    End Function
End Class
