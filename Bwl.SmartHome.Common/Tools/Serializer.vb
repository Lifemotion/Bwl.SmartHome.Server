Imports System.IO
Imports System.Text
Imports System.Runtime.Serialization.Json

Public Class Serializer
    Public Shared Function SaveObjectToJsonBytes(data As Object) As Byte()
        Dim ds = New DataContractJsonSerializer(data.GetType())
        Dim ms = New MemoryStream()
        ds.WriteObject(ms, data)
        ms.Close()
        Return (ms.ToArray())
    End Function

    Public Shared Sub SaveObjectToJsonFile(data As Object, file As String)
        Dim stream = IO.File.Create(file)
        Dim ds = New DataContractJsonSerializer(data.GetType())
        ds.WriteObject(stream, data)
        stream.Close()
    End Sub

    Public Shared Function SaveObjectToJsonString(data As Object) As String
        Return Encoding.UTF8.GetChars(SaveObjectToJsonBytes(data))
    End Function

    Public Shared Function LoadObjectFromJsonBytes(Of T)(bytes As Byte()) As T
        Dim ds = New DataContractJsonSerializer(GetType(T))
        Return DirectCast(ds.ReadObject(New MemoryStream(bytes)), T)
    End Function

    Public Shared Function LoadObjectFromJsonFile(Of T)(file As String) As T
        Dim stream = IO.File.OpenRead(file)
        Dim ds = New DataContractJsonSerializer(GetType(T))
        Dim obj = DirectCast(ds.ReadObject(stream), T)
        stream.Close()
        Return obj
    End Function


    Public Shared Function LoadObjectFromJsonString(Of T)(jsonString As String) As T
        Dim bytes = Encoding.UTF8.GetBytes(jsonString)
        Return LoadObjectFromJsonBytes(Of T)(bytes)
    End Function
End Class
