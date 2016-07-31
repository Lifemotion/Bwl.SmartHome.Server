Public Class SmartState
    Protected _value As String = ""

    Public Property ID As String = ""
    Public Property ValueChanged As Boolean
    Public Property Type As SmartStateType
    Public Property Caption As String = ""

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(val As String)
            If _value <> val Then
                _value = val
                ValueChanged = True
            End If
        End Set
    End Property
End Class
