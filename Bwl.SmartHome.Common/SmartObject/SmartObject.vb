Imports Bwl.SmartHome

Public Class SmartObject
    Implements IRemoteObject

    Public ReadOnly Property ObjectConfig As SmartObjectConfig
    Public ReadOnly Property Acts As SmartAct()
    Public ReadOnly Property States As SmartState()
    Public Property LastUpdated As DateTime

    Public ReadOnly Property ID As String Implements IRemoteObject.ID
        Get
            Return ObjectConfig.Guid
        End Get
    End Property
End Class
