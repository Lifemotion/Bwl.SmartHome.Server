Imports System.Web
Imports System.Web.Services

Public Class _Default1
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim act = context.Request.Params.Get("act")
        If act = Nothing Then
            context.Response.ContentType = "text/plain"
            context.Response.Write("Go away!")
        Else
            Select Case act
                Case "get-obj-all"
                    Dim text = GetAllObjectsJson()
                    context.Response.ContentType = "text/plain"
                    context.Response.Write(text)
                Case "get-obj-all-hash"
                    Dim text = GetAllObjectsJsonHash()
                    context.Response.ContentType = "text/plain"
                    context.Response.Write(text)
                Case "set-obj-state"
                    Dim guid = context.Request.Params.Get("oid")
                    Dim stateid = context.Request.Params.Get("sid")
                    Dim value = context.Request.Params.Get("val")
                    SetObjectState(guid, stateid, value)
            End Select
        End If
    End Sub

    Private Sub SetObjectState(guid As String, stateid As String, value As String)
        Global_asax.SmartHomeClient.SmartHome.Objects.SetValue(guid, stateid, value, ChangedBy.user)
    End Sub

    Private Function GetAllObjectsJsonHash() As String
        Dim hash As Long
        Dim objs As New SmartObjectsList
        objs.AddRange(Global_asax.SmartHomeClient.SmartHome.Objects.GetObjects(""))

        For Each obj In objs
            For Each ch In obj.Guid
                hash += Asc(ch)
            Next
            If obj IsNot Nothing Then
                For Each state In obj.StateValues
                    For Each ch In state.Value
                        hash += Asc(ch)
                    Next
                Next
            End If
        Next
        Return hash.ToString
    End Function

    Private Function GetAllObjectsJson() As String
        Dim objs As New SmartObjectsList
        objs.AddRange(Global_asax.SmartHomeClient.SmartHome.Objects.GetObjects(""))
        Dim text = Serializer.SaveObjectToJsonString(objs)
        Return text
    End Function

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class