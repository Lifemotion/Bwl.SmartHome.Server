Imports System.IO
Imports System.Xml

Public Class PluginManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dFile = Request.Params("plugName")
        If dFile IsNot Nothing Then
            Dim filePath = Server.MapPath(Path.Combine("~/plugins/", dFile))
            filePath = filePath.Substring(0, filePath.Length - 3)
            If File.Exists(filePath + "dll") Then File.Delete(filePath + "dll")
            If File.Exists(filePath + "dsc") Then File.Delete(filePath + "dsc")
            CreatePluginMap()
        End If
        Dim xml = New XmlDocument()
        Dim str = Server.MapPath("~/plugins/plugins.xml")
        If File.Exists(str) Then
            xml.Load(str)
            For Each n As XmlNode In xml.SelectNodes("/plugins/plugin")
                Dim row = New TableRow()
                Dim cell = New TableCell()
                Dim plugName = New Label()

                plugName.Text = n.SelectSingleNode("file").InnerText
                cell.Controls.Add(plugName)
                row.Cells.Add(cell)

                Dim plugDescription = New Label()
                plugDescription.Text = n.SelectSingleNode("description").InnerText
                cell = New TableCell()
                cell.Controls.Add(plugDescription)
                row.Cells.Add(cell)

                Dim plugDate = New Label()
                plugDate.Text = n.SelectSingleNode("date").InnerText
                cell = New TableCell()
                cell.Controls.Add(plugDate)
                row.Cells.Add(cell)

                Dim plugAction = New HyperLink()
                plugAction.NavigateUrl = "/Pluginmanager.aspx?plugName=" + n.SelectSingleNode("file").InnerText
                plugAction.Text = "Remove"
                cell = New TableCell()
                cell.Controls.Add(plugAction)
                row.Cells.Add(cell)
                PluginsTable.Rows.Add(row)
            Next
        End If
    End Sub

    Protected Sub CreatePluginMap()
        Dim files = Directory.GetFiles(Server.MapPath("~/plugins/"))
        If File.Exists(Server.MapPath("~/plugins/plugins.xml")) Then
            File.Delete(Server.MapPath("~/plugins/plugins.xml"))
        End If
        Using sw As StreamWriter = File.CreateText(Server.MapPath("~/plugins/plugins.xml"))
            sw.WriteLine("<plugins>")
            For Each file In files
                If Path.GetExtension(file) = ".dsc" Then
                    sw.WriteLine(IO.File.ReadAllText(file))
                End If
            Next
            sw.WriteLine("</plugins>")
        End Using
    End Sub

    Protected Sub btnUploadClick(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim file = Request.Files("pluginFile")
        If (file IsNot Nothing And file.ContentLength > 0) Then
            Dim fileName = Path.GetFileName(file.FileName)
            If Path.GetExtension(fileName) = ".dll" Then
                If IO.File.Exists(Server.MapPath(Path.Combine("~/plugins/", fileName))) Then IO.File.Delete(Server.MapPath(Path.Combine("~/plugins/", fileName)))
                If IO.File.Exists(Server.MapPath(Path.Combine("~/plugins/", fileName.Substring(0, fileName.Length - 3) + "dsc"))) Then IO.File.Delete(Server.MapPath(Path.Combine("~/plugins/", fileName.Substring(0, fileName.Length - 3) + "dsc")))
                file.SaveAs(Server.MapPath(Path.Combine("~/plugins/", fileName)))
                newPluginDescription.Text = ""
                Dim description = Request.Params("newPluginDescription")
                Using sw As StreamWriter = IO.File.CreateText(Server.MapPath(Path.Combine("~/plugins/", fileName.Substring(0, fileName.Length - 3) + "dsc")))
                    sw.WriteLine("<plugin>")
                    sw.WriteLine("  <file>" + fileName + "</file>")
                    sw.WriteLine("  <description>" + description + "</description>")
                    sw.WriteLine("  <date>" + Now.ToString("dd.MM.yyy") + "</date>")
                    sw.WriteLine("</plugin>")
                End Using
                CreatePluginMap()
                Response.Redirect("/PluginManager.aspx")
            Else
                Response.Write("Uploaded file is not DLL")
            End If
        End If
    End Sub
End Class