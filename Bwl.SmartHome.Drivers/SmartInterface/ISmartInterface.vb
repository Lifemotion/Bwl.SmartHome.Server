Public Interface ISmartInterface
    ReadOnly Property Guid As Guid
    Property Caption As String
    Sub Send(id As Guid, bytes As Byte())
    Event Received(id As Guid, bytes As Byte())
End Interface
