using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing.Template;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000090 RID: 144
	public class AttributeRoutingConvention : IODataRoutingConvention
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x00010471 File Offset: 0x0000E671
		public AttributeRoutingConvention(string routeName, HttpConfiguration configuration)
			: this(routeName, configuration, AttributeRoutingConvention._defaultPathHandler)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010480 File Offset: 0x0000E680
		public AttributeRoutingConvention(string routeName, HttpConfiguration configuration, IODataPathTemplateHandler pathTemplateHandler)
			: this(routeName)
		{
			AttributeRoutingConvention <>4__this = this;
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (pathTemplateHandler == null)
			{
				throw Error.ArgumentNull("pathTemplateHandler");
			}
			this.ODataPathTemplateHandler = pathTemplateHandler;
			IODataPathHandler iodataPathHandler = pathTemplateHandler as IODataPathHandler;
			if (iodataPathHandler != null && iodataPathHandler.UrlKeyDelimiter == null)
			{
				ODataUrlKeyDelimiter urlKeyDelimiter = configuration.GetUrlKeyDelimiter();
				iodataPathHandler.UrlKeyDelimiter = urlKeyDelimiter;
			}
			Action<HttpConfiguration> oldInitializer = configuration.Initializer;
			bool initialized = false;
			configuration.Initializer = delegate(HttpConfiguration config)
			{
				if (!initialized)
				{
					initialized = true;
					oldInitializer(config);
					IHttpControllerSelector httpControllerSelector = ServicesExtensions.GetHttpControllerSelector(config.Services);
					<>4__this._attributeMappings = <>4__this.BuildAttributeMappings(httpControllerSelector.GetControllerMapping().Values);
				}
			};
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00010509 File Offset: 0x0000E709
		public AttributeRoutingConvention(string routeName, IEnumerable<HttpControllerDescriptor> controllers)
			: this(routeName, controllers, AttributeRoutingConvention._defaultPathHandler)
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00010518 File Offset: 0x0000E718
		public AttributeRoutingConvention(string routeName, IEnumerable<HttpControllerDescriptor> controllers, IODataPathTemplateHandler pathTemplateHandler)
			: this(routeName)
		{
			if (controllers == null)
			{
				throw Error.ArgumentNull("controllers");
			}
			if (pathTemplateHandler == null)
			{
				throw Error.ArgumentNull("pathTemplateHandler");
			}
			this.ODataPathTemplateHandler = pathTemplateHandler;
			this._attributeMappings = this.BuildAttributeMappings(controllers);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00010551 File Offset: 0x0000E751
		internal IDictionary<ODataPathTemplate, IWebApiActionDescriptor> AttributeMappings
		{
			get
			{
				if (this._attributeMappings == null)
				{
					throw Error.InvalidOperation(SRResources.Object_NotYetInitialized, new object[0]);
				}
				return this._attributeMappings;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000032B9 File Offset: 0x000014B9
		public virtual bool ShouldMapController(HttpControllerDescriptor controller)
		{
			return true;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010574 File Offset: 0x0000E774
		public string SelectController(ODataPath odataPath, HttpRequestMessage request)
		{
			SelectControllerResult selectControllerResult = AttributeRoutingConvention.SelectControllerImpl(odataPath, new WebApiRequestMessage(request), this.AttributeMappings);
			if (selectControllerResult != null)
			{
				request.Properties["AttributeRouteData"] = selectControllerResult.Values;
			}
			if (selectControllerResult == null)
			{
				return null;
			}
			return selectControllerResult.ControllerName;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			if (odataPath == null)
			{
				throw Error.ArgumentNull("odataPath");
			}
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (actionMap == null)
			{
				throw Error.ArgumentNull("actionMap");
			}
			object obj = null;
			controllerContext.Request.Properties.TryGetValue("AttributeRouteData", out obj);
			SelectControllerResult selectControllerResult = new SelectControllerResult(controllerContext.ControllerDescriptor.ControllerName, obj as IDictionary<string, object>);
			return AttributeRoutingConvention.SelectActionImpl(new WebApiControllerContext(controllerContext, selectControllerResult));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001062C File Offset: 0x0000E82C
		private IDictionary<ODataPathTemplate, IWebApiActionDescriptor> BuildAttributeMappings(IEnumerable<HttpControllerDescriptor> controllers)
		{
			Dictionary<ODataPathTemplate, IWebApiActionDescriptor> dictionary = new Dictionary<ODataPathTemplate, IWebApiActionDescriptor>();
			foreach (HttpControllerDescriptor httpControllerDescriptor in controllers)
			{
				if (AttributeRoutingConvention.IsODataController(httpControllerDescriptor) && this.ShouldMapController(httpControllerDescriptor))
				{
					HttpActionDescriptor[] array = ServicesExtensions.GetActionSelector(httpControllerDescriptor.Configuration.Services).GetActionMapping(httpControllerDescriptor).SelectMany((IGrouping<string, HttpActionDescriptor> a) => a)
						.ToArray<HttpActionDescriptor>();
					foreach (string text in AttributeRoutingConvention.GetODataRoutePrefixes(httpControllerDescriptor))
					{
						foreach (HttpActionDescriptor httpActionDescriptor in array)
						{
							foreach (ODataPathTemplate odataPathTemplate in this.GetODataPathTemplates(text, httpActionDescriptor))
							{
								dictionary.Add(odataPathTemplate, new WebApiActionDescriptor(httpActionDescriptor));
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010778 File Offset: 0x0000E978
		private static bool IsODataController(HttpControllerDescriptor controller)
		{
			return typeof(ODataController).IsAssignableFrom(controller.ControllerType);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001078F File Offset: 0x0000E98F
		private static IEnumerable<string> GetODataRoutePrefixes(HttpControllerDescriptor controllerDescriptor)
		{
			return AttributeRoutingConvention.GetODataRoutePrefixes(controllerDescriptor.GetCustomAttributes<ODataRoutePrefixAttribute>(false), controllerDescriptor.ControllerType.FullName);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000107A8 File Offset: 0x0000E9A8
		private IEnumerable<ODataPathTemplate> GetODataPathTemplates(string prefix, HttpActionDescriptor action)
		{
			IEnumerable<ODataRouteAttribute> customAttributes = action.GetCustomAttributes<ODataRouteAttribute>(false);
			IServiceProvider requestContainer = action.Configuration.GetODataRootContainer(this._routeName);
			string controllerName = action.ControllerDescriptor.ControllerName;
			string actionName = action.ActionName;
			return from route in customAttributes
				where string.IsNullOrEmpty(route.RouteName) || route.RouteName == this._routeName
				select this.GetODataPathTemplate(prefix, route.PathTemplate, requestContainer, controllerName, actionName) into template
				where template != null
				select template;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001084A File Offset: 0x0000EA4A
		private AttributeRoutingConvention(string routeName)
		{
			if (routeName == null)
			{
				throw Error.ArgumentNull("routeName");
			}
			this._routeName = routeName;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00010867 File Offset: 0x0000EA67
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001086F File Offset: 0x0000EA6F
		public IODataPathTemplateHandler ODataPathTemplateHandler { get; private set; }

		// Token: 0x06000520 RID: 1312 RVA: 0x00010878 File Offset: 0x0000EA78
		internal static SelectControllerResult SelectControllerImpl(ODataPath odataPath, IWebApiRequestMessage request, IDictionary<ODataPathTemplate, IWebApiActionDescriptor> attributeMappings)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (KeyValuePair<ODataPathTemplate, IWebApiActionDescriptor> keyValuePair in attributeMappings)
			{
				ODataPathTemplate key = keyValuePair.Key;
				IWebApiActionDescriptor value = keyValuePair.Value;
				if (value.IsHttpMethodSupported(request.Method) && key.TryMatch(odataPath, dictionary))
				{
					dictionary["action"] = value.ActionName;
					return new SelectControllerResult(value.ControllerName, dictionary);
				}
			}
			return null;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010910 File Offset: 0x0000EB10
		internal static string SelectActionImpl(IWebApiControllerContext controllerContext)
		{
			IDictionary<string, object> routeData = controllerContext.RouteData;
			IDictionary<string, object> routingConventionsStore = controllerContext.Request.Context.RoutingConventionsStore;
			IDictionary<string, object> values = controllerContext.ControllerResult.Values;
			if (values != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in values)
				{
					if (keyValuePair.Key.StartsWith("DF908045-6922-46A0-82F2-2F6E7F43D1B1_", StringComparison.Ordinal) && keyValuePair.Value is ODataParameterValue)
					{
						routingConventionsStore.Add(keyValuePair);
					}
					else
					{
						routeData.Add(keyValuePair);
					}
				}
				return values["action"] as string;
			}
			return null;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000109C0 File Offset: 0x0000EBC0
		private static IEnumerable<string> GetODataRoutePrefixes(IEnumerable<ODataRoutePrefixAttribute> prefixAttributes, string controllerName)
		{
			if (!prefixAttributes.Any<ODataRoutePrefixAttribute>())
			{
				yield return null;
			}
			else
			{
				foreach (ODataRoutePrefixAttribute odataRoutePrefixAttribute in prefixAttributes)
				{
					string text = odataRoutePrefixAttribute.Prefix;
					if (text != null && text.StartsWith("/", StringComparison.Ordinal))
					{
						throw Error.InvalidOperation(SRResources.RoutePrefixStartsWithSlash, new object[] { text, controllerName });
					}
					if (text != null && text.EndsWith("/", StringComparison.Ordinal))
					{
						text = text.TrimEnd(new char[] { '/' });
					}
					yield return text;
				}
				IEnumerator<ODataRoutePrefixAttribute> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000109D8 File Offset: 0x0000EBD8
		private ODataPathTemplate GetODataPathTemplate(string prefix, string pathTemplate, IServiceProvider requestContainer, string controllerName, string actionName)
		{
			if (prefix != null && !pathTemplate.StartsWith("/", StringComparison.Ordinal))
			{
				if (string.IsNullOrEmpty(pathTemplate))
				{
					pathTemplate = prefix;
				}
				else if (pathTemplate.StartsWith("(", StringComparison.Ordinal))
				{
					pathTemplate = prefix + pathTemplate;
				}
				else
				{
					pathTemplate = prefix + "/" + pathTemplate;
				}
			}
			if (pathTemplate.StartsWith("/", StringComparison.Ordinal))
			{
				pathTemplate = pathTemplate.Substring(1);
			}
			ODataPathTemplate odataPathTemplate;
			try
			{
				odataPathTemplate = this.ODataPathTemplateHandler.ParseTemplate(pathTemplate, requestContainer);
			}
			catch (ODataException ex)
			{
				throw Error.InvalidOperation(SRResources.InvalidODataRouteOnAction, new object[] { pathTemplate, actionName, controllerName, ex.Message });
			}
			return odataPathTemplate;
		}

		// Token: 0x04000127 RID: 295
		private static readonly DefaultODataPathHandler _defaultPathHandler = new DefaultODataPathHandler();

		// Token: 0x04000128 RID: 296
		private readonly string _routeName;

		// Token: 0x04000129 RID: 297
		private IDictionary<ODataPathTemplate, IWebApiActionDescriptor> _attributeMappings;
	}
}
