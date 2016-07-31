Imports Bwl.SmartHome

Public Interface ISmartObjects
    ReadOnly Property Path As String
    Sub SetObject(newObj As SmartObject, mask As SmartObjectSetMask)
    Sub SetStateValue(objGuid As String, stateId As String, value As String)
    Function GetObject(guid As String) As SmartObject
    Function GetObjectList() As String
    Function GetStateValue(objGuid As String, stateId As String) As String
End Interface
