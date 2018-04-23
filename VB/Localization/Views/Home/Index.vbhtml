@ModelType Localization.Models.Data
<h2>@LocalizationText.HomePageTitle</h2>

@Using (Html.BeginForm("Post", "Home", FormMethod.Post))
    Html.DevExpress().TextBox(
        Sub(settings)
            settings.Name = "Name"
            settings.Width = 200
            settings.ShowModelErrors = True
            settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText
        End Sub).Bind(Model.Name).GetHtml()
    Html.DevExpress().Button(
        Sub(settings)
            settings.Name = "Submit"
            settings.Text = "Submit"
            settings.UseSubmitBehavior = True
        End Sub).GetHtml()
End Using