Public Class SmartObjectConfigStorage

    Public ReadOnly Property Path As String

    Public Sub New(path As String)
        If Not IO.Directory.Exists(path) Then IO.Directory.CreateDirectory(path)
        Me.Path = path
    End Sub

    Public Sub Save(obj As SmartObjectConfig)
        Dim name = obj.Guid + ".json"
        Dim fullPath = IO.Path.Combine(Me.Path, name)
        Serializer.SaveObjectToJsonFile(obj, fullPath)
    End Sub

    Public Function Load(guid As Guid) As SmartObjectConfig
        Dim name = GuidTool.GuidToString(guid) + ".json"
        Dim fullPath = IO.Path.Combine(Me.Path, name)
        Dim obj = Serializer.LoadObjectFromJsonFile(Of SmartObjectConfig)(fullPath)
        Return obj
    End Function
End Class
