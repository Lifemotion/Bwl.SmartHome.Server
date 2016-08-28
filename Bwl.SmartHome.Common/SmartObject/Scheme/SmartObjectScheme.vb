Imports System.Runtime.Serialization

''' <summary>
''' Схема обьекта, содержащая значения по умолчанию, набор состояний, не изменяется пользователем, публикуется драйвером.
''' </summary>
<DataContract>
Public Class SmartObjectScheme
    <DataMember> Public Property ClassID As String = ""

    <DataMember> Public Property DefaultShortName As String = ""
    <DataMember> Public Property DefaultCaption As String = ""
    <DataMember> Public Property DefaultCategory As SmartObjectCategory = SmartObjectCategory.generic
    <DataMember> Public Property DefaultGroups As String() = {}
    <DataMember> Public Property DefaultOtherParameters As String = ""
    <DataMember> Public Property States As New List(Of SmartStateScheme)

    Public Sub New()
        Dim rnd As New Random
        DefaultShortName = "obj_" + rnd.Next(0, 999999).ToString
    End Sub

    Public Function GetStateScheme(stateId As String) As SmartStateScheme
        For Each state In States
            If state.ID = stateId Then
                Return state
            End If
        Next
        Return Nothing
    End Function

End Class
