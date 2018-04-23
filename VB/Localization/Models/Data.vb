Imports Microsoft.VisualBasic
Imports Localization.Content
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Reflection
Imports System.Web

Namespace Localization.Models
    Public Class Data

        Public Property ID As Integer

        <Required(ErrorMessageResourceName:="RequiredValidationMessage", ErrorMessageResourceType:=GetType(LocalizationText))> _
        Public Property Name As String
    End Class
End Namespace