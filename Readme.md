<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128566207/14.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T108173)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# ASP.NET MVC - How to change the current culture at runtime

This example demonstrates how to use the DevExpress [ComboBox](https://docs.devexpress.com/AspNetMvc/8984/components/data-editors-extensions/combobox) extension to change an application's culture on the fly. When a user selects an item, the extension sends a request to the server to change the culture.  Between requests, the combo box value is stored in cookies.

## Implementation Details

### 1. Add and set up a ComboBox extension.

Wrap the ComboBox extension in the [HTML Form](https://www.w3schools.com/html/html_forms.asp) element to submit the form and send a request to the server when the selected item is changed. 

Handle the extension's client events to save and restore the current culture (selected item) in cookies.

```cs  
@using (Html.BeginForm(null, null, FormMethod.Post, new { id = "form" })) {  
    @Html.DevExpress().ComboBox(  
        settings => {  
            settings.Name = "ComboBox";  
            settings.Width = 180;  
            settings.SelectedIndex = 0;  
            settings.Properties.ValueType = typeof(string);  
            settings.Properties.Items.Add("German", "de-DE");  
            settings.Properties.Items.Add("English", "en-US");  
            settings.Properties.ClientSideEvents.SelectedIndexChanged = "SelectedIndexChanged";  
            settings.Properties.ClientSideEvents.Init = "Init";  
        }  
    ).GetHtml()  
}  
```  

```js
function Init(s, e) {
    s.SetValue(ASPxClientUtils.GetCookie("Culture"));
}
function SelectedIndexChanged(s) {
    ASPxClientUtils.SetCookie("Culture", s.GetValue());
    $("#form").submit();
}
```

### 2. Change the application culture at runtime.

Specify [Thread.CurrentCulture](https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.currentculture) and [Thread.CurrentUICulture](https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.currentuiculture) properties in the [HttpApplication.AcquireRequestState](https://learn.microsoft.com/en-us/dotnet/api/system.web.httpapplication.acquirerequeststate) to apply a culture in an ASP.NET MVC Application at runtime.

```cs  
protected void Application_AcquireRequestState(object sender, EventArgs e) {  
    if (Request.Cookies["Culture"] != null && !string.IsNullOrEmpty(Request.Cookies["Culture"].Value)) {  
        string culture = Request.Cookies["Culture"].Value;  
        CultureInfo ci = new CultureInfo(culture);  
        Thread.CurrentThread.CurrentUICulture = ci;  
        Thread.CurrentThread.CurrentCulture = ci;  
    }  
}  
```  

### 3. Build a multi-language UI in the application.

Use satellite resource assemblies to localize DevExpress MVC Extensions. This technique is described in detail in the following document and is not covered in this example: [Satellite Resource Assemblies](https://docs.devexpress.com/AspNet/12050/common-concepts/localization/satellite-resource-assemblies). This example demonstrates how to localize your custom UI elements on top of our MVC Extensions.

#### 3.1 Provide localized strings.

Create custom [Resource Files](https://learn.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc296240(v=vs.95)) for different cultures and populate them with static resource strings. In this example, we created the following resources files:

* [LocalizationText.resx](./CS/Localization/Content/LocalizationText.resx) - the default resource file.
* [LocalizationText.en-US.resx](./CS/Localization/Content/LocalizationText.en-US.resx) contains English resource strings.
* [LocalizationText.de-DE.resx](./CS/Localization/Content/LocalizationText.de-DE.resx) contains German resource strings.

The resource strings maintained in the files can be accessed at runtime as properties of the `LocalizationText` class.

#### 3.2 Display localized strings in UI elements.

This section lists examples how to display the localized strings in the following UI elements:

* A page title.

    ```cs  
    <h2>@LocalizationText.HomePageTitle</h2>  
    ```  

* Custom items in DevExpress [Menu](https://docs.devexpress.com/AspNetMvc/8968/components/site-navigation-and-layout/menu) extension:  
  
    ```cs  
    @Html.DevExpress().Menu(  
        settings => {  
            settings.Name = "Menu";  
            settings.Items.Add(item => {  
                item.Text = LocalizationText.MenuItemOne;  
            });  
            settings.Items.Add(item => {  
                item.Text = LocalizationText.MenuItemTwo;  
            });  
    }).GetHtml()  
    ```  

* An editor's validation message text.

    Bind an editor to a field in the Model and enable the `ShowModelErrors` property to show a model error message in the editor.

    ```cs  
    @Html.DevExpress().TextBox(  
        settings => {  
            settings.Name = "Name";  
            settings.ShowModelErrors = true;  
            settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;  
    }).Bind(Model.Name).GetHtml()  
    ```  

    Configure the `Required` field attribute to load the validation message from the custom resource file string. 
    
    ```cs
    [Required(ErrorMessageResourceName = "RequiredValidationMessage", ErrorMessageResourceType = typeof(LocalizationText))]
    public string Name { get; set; }
    ```

## Files to Review

* [Global.asax.cs](./CS/Localization/Global.asax.cs) (VB: [Global.asax.vb](./VB/Localization/Global.asax.vb))
* [Index.cshtml](./CS/Localization/Views/Home/Index.cshtml) (VB: [Index.vbhtml](./VB/Localization/Views/Home/Index.vbhtml))
* [_Layout.cshtml](./CS/Localization/Views/Shared/_Layout.cshtml) (VB: [_Layout.vbhtml](./VB/Localization/Views/Shared/_Layout.vbhtml))
* [Data.cs](./CS/Localization/Models/Data.cs) (VB: [Data.vb](./VB/Localization/Models/Data.vb))

## Documentation

* [Localization](https://docs.devexpress.com/AspNetMvc/402209/common-features/localization)
