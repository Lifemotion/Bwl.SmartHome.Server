Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objects = Global_asax.SmartHomeClient.SmartHome.Objects
        Dim list = objects.GetObjectList
        For Each item In list.Split({","}, StringSplitOptions.RemoveEmptyEntries)
            Dim obj = objects.GetObject(item)
            Dim ph As New Panel
            ph.ID = obj.Guid
            ph.CssClass = "SmartObject"
            Dim caption As New Label
            caption.Text = obj.Config.Caption
            ph.Controls.Add(caption)
            For Each state In obj.States
                Dim statePanel As New Panel
                statePanel.CssClass = "SmartObjectState"
                ph.Controls.Add(statePanel)
                Dim lbl As New Label
                lbl.Text = state.Caption
                lbl.CssClass = "SmartObjectStateLabel"
                statePanel.Controls.Add(lbl)

                Select Case state.Type
                    Case SmartStateType.actionButton
                        Dim btn As New Button
                        btn.Text = "Do"
                        statePanel.Controls.Add(btn)
                        AddHandler btn.Click, Sub()
                                                  objects.SetStateValue(obj.Guid, state.ID, "1")
                                                  Page.Response.Redirect(Request.RawUrl)
                                              End Sub
                    Case SmartStateType.actionOnOff


                        Dim btn1 As New Button
                        btn1.Text = "Off"

                        Dim btn2 As New Button
                        btn2.Text = "On"

                        If state.Value.ToLower = "on" Then
                            btn1.Enabled = True
                            btn2.Enabled = False
                        Else
                            btn1.Enabled = False
                            btn2.Enabled = True
                        End If
                        statePanel.Controls.Add(btn1)
                        statePanel.Controls.Add(btn2)
                        AddHandler btn1.Click, Sub()
                                                   objects.SetStateValue(obj.Guid, state.ID, "off")
                                                   Page.Response.Redirect(Request.RawUrl)
                                               End Sub
                        AddHandler btn2.Click, Sub()
                                                   objects.SetStateValue(obj.Guid, state.ID, "on")
                                                   Page.Response.Redirect(Request.RawUrl)
                                               End Sub
                    Case SmartStateType.actionString
                        Dim btn As New TextBox
                        btn.Text = state.Value
                        statePanel.Controls.Add(btn)
                        AddHandler btn.TextChanged, Sub()
                                                        objects.SetStateValue(obj.Guid, state.ID, btn.Text)
                                                    End Sub
                    Case SmartStateType.stateString
                        Dim lbl1 As New Label
                        lbl1.Text = ": " + state.Value
                        statePanel.Controls.Add(lbl1)
                    Case SmartStateType.stateYesNo
                        Dim lbl1 As New Label
                        lbl1.Text = ": " + state.Value
                        statePanel.Controls.Add(lbl1)
                End Select
            Next
            smartObjects.Controls.Add(ph)
        Next
    End Sub

End Class