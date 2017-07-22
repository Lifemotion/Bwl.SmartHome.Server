Public Class SmartHomeDebugger
    Public Property SmartHome As SmartHome

    Private Sub bObjectsUpdate_Click(sender As Object, e As EventArgs) Handles bObjectsUpdate.Click
        Dim objs = SmartHome.Objects.GetObjects("")
        lbObjects.Items.Clear()
        For Each obj In objs
            Dim name = obj.UserConfig.Caption
            If name = "" Then name = "[" + obj.Scheme.DefaultCaption + "]"
            lbObjects.Items.Add(obj.Guid + vbTab + name)
        Next
    End Sub

    Private _selectedGuid As String = ""
    Private Sub lbObjects_DoubleClick(sender As Object, e As EventArgs) Handles lbObjects.DoubleClick
        If lbObjects.SelectedItem IsNot Nothing Then
            gbSelected.Text = lbObjects.SelectedItem
            _selectedGuid = gbSelected.Text.Split(vbTab)(0)
            bUpdateSelectedObject_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub bUpdateSelectedObject_Click(sender As Object, e As EventArgs) Handles bUpdateSelectedObject.Click
        lbStates.Items.Clear()
        Dim obj = SmartHome.Objects.GetObject(_selectedGuid)
        If obj IsNot Nothing Then
            For Each state In obj.Scheme.States
                Dim caption = state.DefaultCaption
                Dim value = SmartHome.Objects.GetValue(_selectedGuid, state.ID)
                lbStates.Items.Add(state.ID + vbTab + state.Type.ToString + vbTab + caption + vbTab + value)
            Next
        End If
    End Sub

    Private Sub lbStates_DoubleClick(sender As Object, e As EventArgs) Handles lbStates.DoubleClick
        If lbStates.SelectedItem IsNot Nothing Then
            Dim idtext As String = lbStates.SelectedItem
            Dim id = idtext.Split(vbTab)(0)
            Dim type = idtext.Split(vbTab)(1)
            Dim value = SmartHome.Objects.GetValue(_selectedGuid, id)
            Select Case type
                Case "actionOnOff"
                    If value = "off" Then
                        SmartHome.Objects.SetValue(_selectedGuid, id, "on", ChangedBy.user)
                    Else
                        SmartHome.Objects.SetValue(_selectedGuid, id, "off", ChangedBy.user)
                    End If
                Case "actionButton"
                    SmartHome.Objects.SetValue(_selectedGuid, id, "1", ChangedBy.user)
                Case "actionString"

            End Select
            bUpdateSelectedObject_Click(Nothing, Nothing)

        End If
    End Sub

    Private Sub SmartHomeDebugger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bObjectsUpdate_Click(Nothing, Nothing)
    End Sub
End Class