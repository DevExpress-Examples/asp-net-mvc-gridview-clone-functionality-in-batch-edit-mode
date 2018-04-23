' Developer Express Code Central Example:
' GridView - How to implement copy functionality in Batch Edit mode
' 
' This example demonstrates how to add copied values to a new row on a custom
' command button click using a new client-side API implemented in the following
' threads:
' http://www.devexpress.com/scid=S172878
' http://www.devexpress.com/scid=S172826
' 
' WebForms
' version:
' http://www.devexpress.com/scid=T114985
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=T115891

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Namespace DXWebApplication1
	' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	' visit http://go.microsoft.com/?LinkId=9394801

	Public Class MvcApplication
		Inherits System.Web.HttpApplication
		Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
			filters.Add(New HandleErrorAttribute())
		End Sub

		Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
			routes.IgnoreRoute("{resource}.ashx/{*pathInfo}")

			routes.MapRoute("Default", "{controller}/{action}/{id}", New With {Key .controller = "Home", Key .action = "Index", Key .id = UrlParameter.Optional})

		End Sub
		Protected Sub Application_Start()
			AreaRegistration.RegisterAllAreas()

			RegisterGlobalFilters(GlobalFilters.Filters)
			RegisterRoutes(RouteTable.Routes)

			ModelBinders.Binders.DefaultBinder = New DevExpress.Web.Mvc.DevExpressEditorsBinder()
		End Sub
	End Class
End Namespace