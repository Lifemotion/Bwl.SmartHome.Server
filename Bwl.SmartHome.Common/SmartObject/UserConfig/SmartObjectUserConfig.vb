Imports System.Runtime.Serialization
''' <summary>
''' Найстройки конкретного экземпляра умного объекта, хранится на диске, содержит изменяемые пользователем параметры.
''' </summary>
<DataContract>
Public Class SmartObjectUserConfig
    <DataMember> Public Property ShortName As String = ""
    <DataMember> Public Property Caption As String = ""
    <DataMember> Public Property Category As SmartObjectCategory = SmartObjectCategory.generic
    <DataMember> Public Property Location As String = ""
    <DataMember> Public Property Groups As String() = {}
    <DataMember> Public Property OtherParameters As String = ""



End Class
