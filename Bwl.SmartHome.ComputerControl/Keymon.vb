Imports System.Windows.Forms
Imports MouseKeyboardActivityMonitor
Imports MouseKeyboardActivityMonitor.WinApi

Public Class Keymon
    Implements IDisposable

    Private WithEvents keyhook As KeyboardHookListener
    Private _rulesPath As String
    Private _rules As New List(Of String)
    Private _client As SmartHomeClient

    Public Sub New(client As SmartHomeClient, rulesPath As String)
        _client = client
        _rulesPath = rulesPath


        Reload()

        If _rules.Count > 0 Then
            keyhook = New KeyboardHookListener(New GlobalHooker())
            keyhook.Start()
        End If

    End Sub

    Private _shift As Boolean
    Private _control As Boolean

    Private Sub keyhook_KeyDown(sender As Object, e As KeyEventArgs) Handles keyhook.KeyDown
        If e.KeyCode = Keys.LShiftKey Then _shift = True
        If e.KeyCode = Keys.LControlKey Then _control = True

        If _shift And _control And e.KeyCode <> Keys.LShiftKey And e.KeyCode <> Keys.LControlKey Then
            '_shift = False
            '_control = False
            For Each rule In _rules
                Dim parts = rule.Split(",")
                If parts.Length = 4 Then
                    If parts(0) = e.KeyCode.ToString Then
                        Try
                            _client.SmartHome.Objects.SetValue(parts(1), parts(2), parts(3), ChangedBy.user)
                        Catch ex As Exception
                            '_client.
                        End Try
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub keyhook_KeyUp(sender As Object, e As KeyEventArgs) Handles keyhook.KeyUp
        If e.KeyCode = Keys.LShiftKey Then _shift = False
        If e.KeyCode = Keys.LControlKey Then _control = False
    End Sub

    Friend Sub Reload()
        Try
            _rules.Clear()
            _rules.AddRange(IO.File.ReadAllLines(_rulesPath))
        Catch ex As Exception
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Для определения избыточных вызовов

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            Try
                keyhook.Stop()
            Catch ex As Exception
            End Try
            Try
                keyhook.Dispose()
            Catch ex As Exception
            End Try
        End If
        disposedValue = True
    End Sub

    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Не изменяйте этот код. Разместите код очистки выше в методе Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: раскомментировать следующую строку, если Finalize() переопределен выше.
        ' GC.SuppressFinalize(Me)
    End Sub




#End Region

End Class
