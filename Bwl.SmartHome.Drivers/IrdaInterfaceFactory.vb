Imports Bwl.SmartHome

Public Class IrdaInterfaceFactory
    Implements ISmartInterfaceFactory

    Public Function CreateInterface(guid As Guid) As ISmartInterface Implements ISmartInterfaceFactory.CreateInterface
        Throw New NotImplementedException
    End Function
End Class
