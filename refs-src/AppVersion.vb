Imports System.IO

Public Module AppVersion
    Public Function GetAppCompileTime() As String
        Return File.GetLastWriteTime(Windows.Forms.Application.ExecutablePath).ToString("(dd.MM.yyyy HH:mm:ss)")
    End Function

    Public Function GetAppVersionAndTime() As String
        Return " " + Windows.Forms.Application.ProductVersion + " от " + GetAppCompileTime()
    End Function
End Module
