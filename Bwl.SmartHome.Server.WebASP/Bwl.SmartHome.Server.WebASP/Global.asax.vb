Imports Bwl.Framework

Public Class Global_asax
    Inherits HttpApplication
    Private Shared _appBase As AppBase
    Private Shared _client As SmartHomeClient
    Private Shared _sync As New Object

    Public Shared ReadOnly Property SmartHomeClient As SmartHomeClient
        Get
            Dim app = AppBase
            SyncLock _sync
                If _client Is Nothing Then _client = New SmartHomeClient(app.RootStorage, app.RootLogger)
            End SyncLock
            Try
                If _client.Transport.IsConnected = False Then _client.Transport.OpenAndRegister()
            Catch ex As Exception

            End Try
            Return _client
        End Get
    End Property

    Public Shared ReadOnly Property AppBase As AppBase
        Get
            SyncLock _sync
                If _appBase Is Nothing Then _appBase = New AppBase
            End SyncLock
            Return _appBase
        End Get
    End Property

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при запуске приложения
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при начале сеанса
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается в начале каждого запроса
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при попытке выполнить проверку подлинности для запроса
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при возникновении ошибки
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при завершении сеанса
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Порождается при завершении приложения
    End Sub

End Class