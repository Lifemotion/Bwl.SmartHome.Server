Public Class SmartValue
    Protected _value As String = ""
    Public Property ID As String = ""
    Public Property ValueChanged As Boolean

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(val As String)
            _value = val
            ValueChanged = True
        End Set
    End Property
End Class

Public Enum SmartStateType
    yesNo
    value
End Enum

Public Class SmartState
    Inherits SmartValue
    Public Property Type As SmartStateType
End Class

Public Enum SmartActType
    button
    switch
    intVal
    strVal
End Enum

Public Class SmartAct
    Inherits SmartValue
    Public Property Type As SmartActType
End Class
