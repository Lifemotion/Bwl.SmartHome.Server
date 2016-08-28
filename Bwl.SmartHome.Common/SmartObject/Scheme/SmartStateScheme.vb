Public Class SmartStateScheme
    Public Property ID As String = ""
    Public Property Type As SmartStateType
    Public Property DefaultCaption As String = ""

    Public Sub New()

    End Sub

    Public Sub New(id As String, type As SmartStateType, defaultCaption As String)
        Me.ID = id
        Me.Type = type
        Me.DefaultCaption = defaultCaption
    End Sub
End Class
