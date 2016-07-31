Public Class GuidTool
    Public Shared Function GuidToString(guid As Guid) As String
        Return guid.ToString("D")
    End Function

    Public Shared Function GuidToString() As String
        Return GuidToString(Guid.NewGuid)
    End Function

    Public Shared Function StringToGuid(str As String) As Guid
        Return Guid.Parse(str)
    End Function


End Class
