using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData;

namespace Microsoft.ReportingServices.Portal.ODataWebApi
{
	// Token: 0x02000005 RID: 5
	public class NamespaceHttpControllerSelector : IHttpControllerSelector
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public NamespaceHttpControllerSelector(HttpConfiguration config)
		{
			this._configuration = config;
			this._controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(new Func<Dictionary<string, HttpControllerDescriptor>>(this.InitializeControllerDictionary));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002076 File Offset: 0x00000276
		public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
		{
			return this._controllers.Value;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
		public HttpControllerDescriptor SelectController(HttpRequestMessage request)
		{
			IHttpRouteData routeData = request.GetRouteData();
			if (routeData == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			string routeVariable = NamespaceHttpControllerSelector.GetRouteVariable<string>(routeData, "controller");
			if (routeVariable == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			HttpControllerDescriptor httpControllerDescriptor;
			if (this.GetControllerMapping().TryGetValue(routeVariable, out httpControllerDescriptor))
			{
				return httpControllerDescriptor;
			}
			string text;
			if (!UrlParser.TryGetNamespaceFromUri(request.RequestUri, out text))
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			string text2 = NamespaceHttpControllerSelector.GetControllerKey(text, routeVariable);
			if (!string.IsNullOrEmpty(text2) && text2.EndsWith("Controller"))
			{
				text2 = text2.Substring(0, text2.Length - 10);
			}
			if (this.GetControllerMapping().TryGetValue(text2, out httpControllerDescriptor))
			{
				return httpControllerDescriptor;
			}
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002134 File Offset: 0x00000334
		private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
		{
			Dictionary<string, HttpControllerDescriptor> dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
			IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
			foreach (Type type in this._configuration.Services.GetHttpControllerTypeResolver().GetControllerTypes(assembliesResolver))
			{
				string[] array = type.Namespace.Split(new char[] { Type.Delimiter });
				string text = type.Name.Remove(type.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
				if (array.Length < 2)
				{
					throw new IndexOutOfRangeException(string.Format("Invalid namespace detected for namespace: {0} (controller:{1})", type.Namespace, text));
				}
				string controllerKey = NamespaceHttpControllerSelector.GetControllerKey(array[array.Length - 2], text);
				if (dictionary.ContainsKey(controllerKey))
				{
					throw new Exception(string.Format("Ambiguous reference detected for controller: {0}", controllerKey));
				}
				dictionary[controllerKey] = new HttpControllerDescriptor(this._configuration, type.Name, type);
			}
			Type typeFromHandle = typeof(MetadataController);
			dictionary["metadata"] = new HttpControllerDescriptor(this._configuration, typeFromHandle.Name, typeFromHandle);
			return dictionary;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000227C File Offset: 0x0000047C
		private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
		{
			object obj = null;
			if (routeData.Values.TryGetValue(name, out obj))
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022AB File Offset: 0x000004AB
		private static string GetControllerKey(string versionString, string controllerName)
		{
			if (controllerName.EndsWith("Controller"))
			{
				controllerName = controllerName.Substring(0, controllerName.Length - 10);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", versionString, controllerName);
		}

		// Token: 0x04000035 RID: 53
		private const string ControllerKey = "controller";

		// Token: 0x04000036 RID: 54
		private readonly HttpConfiguration _configuration;

		// Token: 0x04000037 RID: 55
		private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
	}
}
