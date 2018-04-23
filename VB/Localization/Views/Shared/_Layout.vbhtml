<!DOCTYPE HTML>

<script type="text/javascript">
    function Init(s, e) {
        s.SetValue(ASPxClientUtils.GetCookie("Culture"));
    }
    function SelectedIndexChanged(s) {
        ASPxClientUtils.SetCookie("Culture", s.GetValue());
        $("#form").submit();
    }
</script>

<html>
<head>
    <title>@ViewBag.Title</title>
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    @Html.DevExpress().GetStyleSheets(
         New StyleSheet With {.ExtensionSuite = ExtensionSuite.All}
    )
    @Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.All}
    )
</head>

<body>
    @Html.DevExpress().Menu(
         Sub(settings)
                 settings.Name = "Menu"
                 settings.Items.Add(
                     Sub(item)
                             item.Text = LocalizationText.MenuItemTwo
                     End Sub)
                 settings.Items.Add(
                     Sub(item)
                             item.Text = LocalizationText.MenuItemTwo
                     End Sub)
         End Sub).GetHtml()

    <br />

    @Using (Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "form"}))
        Html.DevExpress().ComboBox(
            Sub(settings)
                settings.Name = "ComboBox"
                settings.Width = 180
                settings.SelectedIndex = 0
                settings.Properties.ValueType = GetType(String)
                settings.Properties.Items.Add("German", "de-DE")
                settings.Properties.Items.Add("English", "en-US")
                settings.Properties.ClientSideEvents.SelectedIndexChanged = "SelectedIndexChanged"
                settings.Properties.ClientSideEvents.Init = "Init"
            End Sub).GetHtml()
    End Using

    @RenderBody()
</body>
</html>
