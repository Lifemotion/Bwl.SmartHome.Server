Imports Bwl.SmartHome

Public Class IrdaInterface
    Implements ISmartInterface

    Public Property Caption As String Implements ISmartInterface.Caption

    Public ReadOnly Property Guid As Guid Implements ISmartInterface.Guid

    Public Event Received(id As Guid, bytes() As Byte) Implements ISmartInterface.Received

    Public Sub Send(id As Guid, bytes() As Byte) Implements ISmartInterface.Send

    End Sub
End Class
