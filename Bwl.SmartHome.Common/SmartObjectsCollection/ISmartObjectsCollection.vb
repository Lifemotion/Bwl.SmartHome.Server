Imports Bwl.SmartHome

Public Enum ChangedBy
    unknown = 0
    script = 1
    user = 2
    device = 3
End Enum

Public Interface ISmartObjectsCollection
    Event CollectionChanged()
    Function GetObjects(filters As String) As SmartObject()
    Function GetObject(guid As String) As SmartObject

    Sub SetScheme(objGuid As String, scheme As SmartObjectScheme)
    Sub SetUserConfig(objGuid As String, config As SmartObjectUserConfig)

    Event StateChanged(objGuid As String, stateId As String, lastValue As String, currentValue As String, changedBy As ChangedBy)
    Function GetValue(objGuid As String, stateId As String) As String
    Sub SetValue(objGuid As String, stateId As String, value As String, changedBy As ChangedBy)
End Interface
