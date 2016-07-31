''' <summary>
''' Найстройки конкретного экземпляра умного объекта, хранится на диске, содержит изменяемые пользователем параметры.
''' </summary>
Public Class SmartObjectConfig
    Public Property Guid As String
    Public Property ShortName As String = ""
    Public Property ClassID As String = ""
    Public Property Caption As String = ""
    Public Property Category As SmartObjectCategory
    Public Property Location As String = ""
    Public Property Groups As String() = {}
    Public Property InterfaceGuid As String = ""
    Public Property OtherParameters As String = ""
    Public Property Registered As Boolean
    Public Sub New()

    End Sub

    Public Sub New(guid As Guid, category As SmartObjectCategory, classID As String)
        Dim rnd As New Random
        Me.Guid = GuidTool.GuidToString(guid)
        Me.Category = category
        Me.ShortName = category.ToString + rnd.Next(0, 999999).ToString
        Me.ClassID = classID
    End Sub
End Class
