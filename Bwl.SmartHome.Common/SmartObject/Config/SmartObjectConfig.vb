Imports System.Runtime.Serialization
''' <summary>
''' Найстройки конкретного экземпляра умного объекта, хранится на диске, содержит изменяемые пользователем параметры.
''' </summary>
<DataContract>
Public Class SmartObjectConfig
    <DataMember> Public Property ShortName As String = ""
    <DataMember> Public Property ClassID As String = ""
    <DataMember> Public Property Caption As String = ""
    <DataMember> Public Property Category As SmartObjectCategory = SmartObjectCategory.generic
    <DataMember> Public Property Location As String = ""
    <DataMember> Public Property Groups As String() = {}
    <DataMember> Public Property InterfaceGuid As String = ""
    <DataMember> Public Property OtherParameters As String = ""

    Public Sub New()
        Dim rnd As New Random
        ShortName = "obj_" + rnd.Next(0, 999999).ToString
    End Sub

End Class
