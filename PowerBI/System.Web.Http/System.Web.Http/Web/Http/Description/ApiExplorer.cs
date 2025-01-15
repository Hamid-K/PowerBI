using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using System.Web.Http.Services;

namespace System.Web.Http.Description
{
	// Token: 0x020000E6 RID: 230
	public class ApiExplorer : IApiExplorer
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0000EA7F File Offset: 0x0000CC7F
		public ApiExplorer(HttpConfiguration configuration)
		{
			this._config = configuration;
			this._apiDescriptions = new Lazy<Collection<ApiDescription>>(new Func<Collection<ApiDescription>>(this.InitializeApiDescriptions));
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000EAA5 File Offset: 0x0000CCA5
		public Collection<ApiDescription> ApiDescriptions
		{
			get
			{
				return this._apiDescriptions.Value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000EAB2 File Offset: 0x0000CCB2
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000EABA File Offset: 0x0000CCBA
		public IDocumentationProvider DocumentationProvider { get; set; }

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		public virtual bool ShouldExploreController(string controllerVariableValue, HttpControllerDescriptor controllerDescriptor, IHttpRoute route)
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			ApiExplorerSettingsAttribute apiExplorerSettingsAttribute = controllerDescriptor.GetCustomAttributes<ApiExplorerSettingsAttribute>().FirstOrDefault<ApiExplorerSettingsAttribute>();
			return (apiExplorerSettingsAttribute == null || !apiExplorerSettingsAttribute.IgnoreApi) && ApiExplorer.MatchRegexConstraint(route, "controller", controllerVariableValue);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public virtual bool ShouldExploreAction(string actionVariableValue, HttpActionDescriptor actionDescriptor, IHttpRoute route)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			ApiExplorerSettingsAttribute apiExplorerSettingsAttribute = actionDescriptor.GetCustomAttributes<ApiExplorerSettingsAttribute>().FirstOrDefault<ApiExplorerSettingsAttribute>();
			return (apiExplorerSettingsAttribute == null || !apiExplorerSettingsAttribute.IgnoreApi) && ApiExplorer.MatchRegexConstraint(route, "action", actionVariableValue);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public virtual Collection<HttpMethod> GetHttpMethodsSupportedByAction(IHttpRoute route, HttpActionDescriptor actionDescriptor)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			IList<HttpMethod> list = new List<HttpMethod>();
			IList<HttpMethod> supportedHttpMethods = actionDescriptor.SupportedHttpMethods;
			HttpMethodConstraint httpMethodConstraint = route.Constraints.Values.FirstOrDefault((object c) => typeof(HttpMethodConstraint).IsAssignableFrom(c.GetType())) as HttpMethodConstraint;
			if (httpMethodConstraint == null)
			{
				list = supportedHttpMethods;
			}
			else
			{
				list = httpMethodConstraint.AllowedMethods.Intersect(supportedHttpMethods).ToList<HttpMethod>();
			}
			return new Collection<HttpMethod>(list);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000EBEE File Offset: 0x0000CDEE
		private IEnumerable<IHttpRoute> FlattenRoutes(IEnumerable<IHttpRoute> routes)
		{
			foreach (IHttpRoute httpRoute in routes)
			{
				IEnumerable<IHttpRoute> enumerable = httpRoute as IEnumerable<IHttpRoute>;
				if (enumerable != null)
				{
					foreach (IHttpRoute httpRoute2 in this.FlattenRoutes(enumerable))
					{
						yield return httpRoute2;
					}
					IEnumerator<IHttpRoute> enumerator2 = null;
				}
				else
				{
					yield return httpRoute;
				}
			}
			IEnumerator<IHttpRoute> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private static HttpControllerDescriptor GetDirectRouteController(CandidateAction[] directRouteCandidates)
		{
			if (directRouteCandidates != null)
			{
				HttpControllerDescriptor controllerDescriptor = directRouteCandidates[0].ActionDescriptor.ControllerDescriptor;
				for (int i = 1; i < directRouteCandidates.Length; i++)
				{
					if (directRouteCandidates[i].ActionDescriptor.ControllerDescriptor != controllerDescriptor)
					{
						return null;
					}
				}
				return controllerDescriptor;
			}
			return null;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		private Collection<ApiDescription> InitializeApiDescriptions()
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			IDictionary<string, HttpControllerDescriptor> controllerMapping = this._config.Services.GetHttpControllerSelector().GetControllerMapping();
			if (controllerMapping != null)
			{
				ApiExplorer.ApiDescriptionComparer apiDescriptionComparer = new ApiExplorer.ApiDescriptionComparer();
				foreach (IHttpRoute httpRoute in this.FlattenRoutes(this._config.Routes))
				{
					CandidateAction[] directRouteCandidates = httpRoute.GetDirectRouteCandidates();
					HttpControllerDescriptor directRouteController = ApiExplorer.GetDirectRouteController(directRouteCandidates);
					foreach (ApiDescription apiDescription in ApiExplorer.RemoveInvalidApiDescriptions((directRouteController != null && directRouteCandidates != null) ? this.ExploreDirectRoute(directRouteController, directRouteCandidates, httpRoute) : this.ExploreRouteControllers(controllerMapping, httpRoute)))
					{
						if (!collection.Contains(apiDescription, apiDescriptionComparer))
						{
							collection.Add(apiDescription);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000ED48 File Offset: 0x0000CF48
		private Collection<ApiDescription> ExploreDirectRoute(HttpControllerDescriptor controllerDescriptor, CandidateAction[] candidates, IHttpRoute route)
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			if (this.ShouldExploreController(controllerDescriptor.ControllerName, controllerDescriptor, route))
			{
				for (int i = 0; i < candidates.Length; i++)
				{
					HttpActionDescriptor actionDescriptor = candidates[i].ActionDescriptor;
					string actionName = actionDescriptor.ActionName;
					if (this.ShouldExploreAction(actionName, actionDescriptor, route))
					{
						string text = route.RouteTemplate;
						if (ApiExplorer._actionVariableRegex.IsMatch(text))
						{
							text = ApiExplorer._actionVariableRegex.Replace(text, actionName);
						}
						this.PopulateActionDescriptions(actionDescriptor, route, text, collection);
					}
				}
			}
			return collection;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		private Collection<ApiDescription> ExploreRouteControllers(IDictionary<string, HttpControllerDescriptor> controllerMappings, IHttpRoute route)
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			string routeTemplate = route.RouteTemplate;
			string key;
			if (ApiExplorer._controllerVariableRegex.IsMatch(routeTemplate))
			{
				using (IEnumerator<KeyValuePair<string, HttpControllerDescriptor>> enumerator = controllerMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, HttpControllerDescriptor> keyValuePair = enumerator.Current;
						key = keyValuePair.Key;
						HttpControllerDescriptor value = keyValuePair.Value;
						if (this.ShouldExploreController(key, value, route))
						{
							string text = ApiExplorer._controllerVariableRegex.Replace(routeTemplate, key);
							this.ExploreRouteActions(route, text, value, collection);
						}
					}
					return collection;
				}
			}
			HttpControllerDescriptor httpControllerDescriptor;
			if (route.Defaults.TryGetValue("controller", out key) && controllerMappings.TryGetValue(key, out httpControllerDescriptor) && this.ShouldExploreController(key, httpControllerDescriptor, route))
			{
				this.ExploreRouteActions(route, routeTemplate, httpControllerDescriptor, collection);
			}
			return collection;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000EE98 File Offset: 0x0000D098
		private void ExploreRouteActions(IHttpRoute route, string localPath, HttpControllerDescriptor controllerDescriptor, Collection<ApiDescription> apiDescriptions)
		{
			if (!controllerDescriptor.IsAttributeRouted())
			{
				ILookup<string, HttpActionDescriptor> actionMapping = controllerDescriptor.Configuration.Services.GetActionSelector().GetActionMapping(controllerDescriptor);
				if (actionMapping != null)
				{
					string key;
					if (ApiExplorer._actionVariableRegex.IsMatch(localPath))
					{
						using (IEnumerator<IGrouping<string, HttpActionDescriptor>> enumerator = actionMapping.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IGrouping<string, HttpActionDescriptor> grouping = enumerator.Current;
								key = grouping.Key;
								string text = ApiExplorer._actionVariableRegex.Replace(localPath, key);
								this.PopulateActionDescriptions(grouping, key, route, text, apiDescriptions);
							}
							return;
						}
					}
					if (route.Defaults.TryGetValue("action", out key))
					{
						this.PopulateActionDescriptions(actionMapping[key], key, route, localPath, apiDescriptions);
						return;
					}
					foreach (IGrouping<string, HttpActionDescriptor> grouping2 in actionMapping)
					{
						this.PopulateActionDescriptions(grouping2, null, route, localPath, apiDescriptions);
					}
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000EF98 File Offset: 0x0000D198
		private void PopulateActionDescriptions(IEnumerable<HttpActionDescriptor> actionDescriptors, string actionVariableValue, IHttpRoute route, string localPath, Collection<ApiDescription> apiDescriptions)
		{
			foreach (HttpActionDescriptor httpActionDescriptor in actionDescriptors)
			{
				if (this.ShouldExploreAction(actionVariableValue, httpActionDescriptor, route) && !httpActionDescriptor.IsAttributeRouted())
				{
					this.PopulateActionDescriptions(httpActionDescriptor, route, localPath, apiDescriptions);
				}
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		private void PopulateActionDescriptions(HttpActionDescriptor actionDescriptor, IHttpRoute route, string localPath, Collection<ApiDescription> apiDescriptions)
		{
			string apiDocumentation = this.GetApiDocumentation(actionDescriptor);
			HttpParsedRoute httpParsedRoute = RouteParser.Parse(localPath);
			IList<ApiParameterDescription> list = this.CreateParameterDescriptions(actionDescriptor, httpParsedRoute, route.Defaults);
			string text;
			if (!ApiExplorer.TryExpandUriParameters(route, httpParsedRoute, list, out text))
			{
				return;
			}
			ApiParameterDescription bodyParameter = list.FirstOrDefault((ApiParameterDescription description) => description.Source == ApiParameterSource.FromBody);
			IEnumerable<MediaTypeFormatter> enumerable = ((bodyParameter != null) ? actionDescriptor.Configuration.Formatters.Where((MediaTypeFormatter f) => f.CanReadType(bodyParameter.ParameterDescriptor.ParameterType)) : Enumerable.Empty<MediaTypeFormatter>());
			ResponseDescription responseDescription = this.CreateResponseDescription(actionDescriptor);
			Type returnType = responseDescription.ResponseType ?? responseDescription.DeclaredType;
			IEnumerable<MediaTypeFormatter> enumerable2 = ((returnType != null && returnType != typeof(void)) ? actionDescriptor.Configuration.Formatters.Where((MediaTypeFormatter f) => f.CanWriteType(returnType)) : Enumerable.Empty<MediaTypeFormatter>());
			enumerable = ApiExplorer.GetInnerFormatters(enumerable);
			enumerable2 = ApiExplorer.GetInnerFormatters(enumerable2);
			foreach (HttpMethod httpMethod in ((IEnumerable<HttpMethod>)this.GetHttpMethodsSupportedByAction(route, actionDescriptor)))
			{
				apiDescriptions.Add(new ApiDescription
				{
					Documentation = apiDocumentation,
					HttpMethod = httpMethod,
					RelativePath = text,
					ActionDescriptor = actionDescriptor,
					Route = route,
					SupportedResponseFormatters = new Collection<MediaTypeFormatter>(enumerable2.ToList<MediaTypeFormatter>()),
					SupportedRequestBodyFormatters = new Collection<MediaTypeFormatter>(enumerable.ToList<MediaTypeFormatter>()),
					ParameterDescriptions = new Collection<ApiParameterDescription>(list),
					ResponseDescription = responseDescription
				});
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		private ResponseDescription CreateResponseDescription(HttpActionDescriptor actionDescriptor)
		{
			Type type = (from attribute in actionDescriptor.GetCustomAttributes<ResponseTypeAttribute>()
				select attribute.ResponseType).FirstOrDefault<Type>();
			return new ResponseDescription
			{
				DeclaredType = actionDescriptor.ReturnType,
				ResponseType = type,
				Documentation = this.GetApiResponseDocumentation(actionDescriptor)
			};
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000F21A File Offset: 0x0000D41A
		private static IEnumerable<MediaTypeFormatter> GetInnerFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
		{
			foreach (MediaTypeFormatter mediaTypeFormatter in mediaTypeFormatters)
			{
				yield return Decorator.GetInner<MediaTypeFormatter>(mediaTypeFormatter);
			}
			IEnumerator<MediaTypeFormatter> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000F22A File Offset: 0x0000D42A
		private static bool ShouldEmitPrefixes(ICollection<ApiParameterDescription> parameterDescriptions)
		{
			return parameterDescriptions.Count((ApiParameterDescription parameter) => parameter.Source == ApiParameterSource.FromUri && parameter.ParameterDescriptor != null && !TypeHelper.CanConvertFromString(parameter.ParameterDescriptor.ParameterType) && parameter.CanConvertPropertiesFromString()) > 1;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000F254 File Offset: 0x0000D454
		internal static bool TryExpandUriParameters(IHttpRoute route, HttpParsedRoute parsedRoute, ICollection<ApiParameterDescription> parameterDescriptions, out string expandedRouteTemplate)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			bool flag = ApiExplorer.ShouldEmitPrefixes(parameterDescriptions);
			string text = string.Empty;
			foreach (ApiParameterDescription apiParameterDescription in parameterDescriptions)
			{
				if (apiParameterDescription.Source == ApiParameterSource.FromUri)
				{
					if (apiParameterDescription.ParameterDescriptor == null)
					{
						ApiExplorer.AddPlaceholder(dictionary, apiParameterDescription.Name);
					}
					else if (TypeHelper.CanConvertFromString(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						ApiExplorer.AddPlaceholder(dictionary, apiParameterDescription.Name);
					}
					else if (ApiExplorer.IsBindableCollection(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						string parameterName = apiParameterDescription.ParameterDescriptor.ParameterName;
						PropertyInfo[] array = ApiParameterDescription.GetBindableProperties(ApiExplorer.GetCollectionElementType(apiParameterDescription.ParameterDescriptor.ParameterType)).ToArray<PropertyInfo>();
						if (array.Any<PropertyInfo>())
						{
							ApiExplorer.AddPlaceholderForProperties(dictionary, array, parameterName + "[0].");
							ApiExplorer.AddPlaceholderForProperties(dictionary, array, parameterName + "[1].");
						}
						else
						{
							ApiExplorer.AddPlaceholder(dictionary, parameterName + "[0]");
							ApiExplorer.AddPlaceholder(dictionary, parameterName + "[1]");
						}
					}
					else if (ApiExplorer.IsBindableKeyValuePair(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						ApiExplorer.AddPlaceholder(dictionary, "key");
						ApiExplorer.AddPlaceholder(dictionary, "value");
					}
					else if (ApiExplorer.IsBindableDictionry(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						string parameterName2 = apiParameterDescription.ParameterDescriptor.ParameterName;
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[0].key");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[0].value");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[1].key");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[1].value");
					}
					else if (apiParameterDescription.CanConvertPropertiesFromString())
					{
						if (flag)
						{
							text = apiParameterDescription.Name + ".";
						}
						ApiExplorer.AddPlaceholderForProperties(dictionary, apiParameterDescription.GetBindableProperties(), text);
					}
				}
			}
			BoundRouteTemplate boundRouteTemplate = parsedRoute.Bind(null, dictionary, new HttpRouteValueDictionary(route.Defaults), new HttpRouteValueDictionary(route.Constraints));
			if (boundRouteTemplate == null)
			{
				expandedRouteTemplate = null;
				return false;
			}
			expandedRouteTemplate = Uri.UnescapeDataString(boundRouteTemplate.BoundTemplate);
			return true;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		private static Type GetCollectionElementType(Type collectionType)
		{
			Type type = collectionType.GetElementType();
			if (type == null)
			{
				type = CollectionModelBinderUtil.GetGenericBinderTypeArgs(typeof(ICollection<>), collectionType).First<Type>();
			}
			return type;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		private static void AddPlaceholderForProperties(Dictionary<string, object> parameterValuesForRoute, IEnumerable<PropertyInfo> properties, string prefix)
		{
			foreach (PropertyInfo propertyInfo in properties)
			{
				string text = prefix + propertyInfo.Name;
				ApiExplorer.AddPlaceholder(parameterValuesForRoute, text);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000F538 File Offset: 0x0000D738
		private static bool IsBindableCollection(Type type)
		{
			return type.IsArray || new CollectionModelBinderProvider().GetBinder(null, type) != null;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000F553 File Offset: 0x0000D753
		private static bool IsBindableDictionry(Type type)
		{
			return new DictionaryModelBinderProvider().GetBinder(null, type) != null;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000F564 File Offset: 0x0000D764
		private static bool IsBindableKeyValuePair(Type type)
		{
			return TypeHelper.GetTypeArgumentsIfMatch(type, typeof(KeyValuePair<, >)) != null;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000F579 File Offset: 0x0000D779
		private static void AddPlaceholder(Dictionary<string, object> parameterValuesForRoute, string queryParameterName)
		{
			if (!parameterValuesForRoute.ContainsKey(queryParameterName))
			{
				parameterValuesForRoute.Add(queryParameterName, "{" + queryParameterName + "}");
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000F59C File Offset: 0x0000D79C
		private IList<ApiParameterDescription> CreateParameterDescriptions(HttpActionDescriptor actionDescriptor, HttpParsedRoute parsedRoute, IDictionary<string, object> routeDefaults)
		{
			IList<ApiParameterDescription> list = new List<ApiParameterDescription>();
			HttpActionBinding actionBinding = ApiExplorer.GetActionBinding(actionDescriptor);
			if (actionBinding != null)
			{
				HttpParameterBinding[] parameterBindings = actionBinding.ParameterBindings;
				if (parameterBindings != null)
				{
					foreach (HttpParameterBinding httpParameterBinding in parameterBindings)
					{
						list.Add(this.CreateParameterDescriptionFromBinding(httpParameterBinding));
					}
				}
			}
			else
			{
				Collection<HttpParameterDescriptor> parameters = actionDescriptor.GetParameters();
				if (parameters != null)
				{
					foreach (HttpParameterDescriptor httpParameterDescriptor in parameters)
					{
						list.Add(this.CreateParameterDescriptionFromDescriptor(httpParameterDescriptor));
					}
				}
			}
			ApiExplorer.AddUndeclaredRouteParameters(parsedRoute, routeDefaults, list);
			return list;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000F64C File Offset: 0x0000D84C
		private static void AddUndeclaredRouteParameters(HttpParsedRoute parsedRoute, IDictionary<string, object> routeDefaults, IList<ApiParameterDescription> parameterDescriptions)
		{
			foreach (PathSegment pathSegment in parsedRoute.PathSegments)
			{
				PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
				if (pathContentSegment != null)
				{
					foreach (PathSubsegment pathSubsegment in pathContentSegment.Subsegments)
					{
						PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
						if (pathParameterSubsegment != null)
						{
							string parameterName = pathParameterSubsegment.ParameterName;
							object obj;
							if (!parameterDescriptions.Any((ApiParameterDescription p) => string.Equals(p.Name, parameterName, StringComparison.OrdinalIgnoreCase)) && (!routeDefaults.TryGetValue(parameterName, out obj) || obj != RouteParameter.Optional))
							{
								parameterDescriptions.Add(new ApiParameterDescription
								{
									Name = parameterName,
									Source = ApiParameterSource.FromUri
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000F750 File Offset: 0x0000D950
		private ApiParameterDescription CreateParameterDescriptionFromDescriptor(HttpParameterDescriptor parameter)
		{
			return new ApiParameterDescription
			{
				ParameterDescriptor = parameter,
				Name = (parameter.Prefix ?? parameter.ParameterName),
				Documentation = this.GetApiParameterDocumentation(parameter),
				Source = ApiParameterSource.Unknown
			};
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000F788 File Offset: 0x0000D988
		private ApiParameterDescription CreateParameterDescriptionFromBinding(HttpParameterBinding parameterBinding)
		{
			ApiParameterDescription apiParameterDescription = this.CreateParameterDescriptionFromDescriptor(parameterBinding.Descriptor);
			if (parameterBinding.WillReadBody)
			{
				apiParameterDescription.Source = ApiParameterSource.FromBody;
			}
			else if (parameterBinding.WillReadUri())
			{
				apiParameterDescription.Source = ApiParameterSource.FromUri;
			}
			return apiParameterDescription;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		private string GetApiDocumentation(HttpActionDescriptor actionDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? actionDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetDocumentation(actionDescriptor);
			}
			return null;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		private string GetApiParameterDocumentation(HttpParameterDescriptor parameterDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? parameterDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetDocumentation(parameterDescriptor);
			}
			return null;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000F82C File Offset: 0x0000DA2C
		private string GetApiResponseDocumentation(HttpActionDescriptor actionDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? actionDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetResponseDocumentation(actionDescriptor);
			}
			return null;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000F860 File Offset: 0x0000DA60
		private static Collection<ApiDescription> RemoveInvalidApiDescriptions(Collection<ApiDescription> apiDescriptions)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (ApiDescription apiDescription in apiDescriptions)
			{
				string id = apiDescription.ID;
				if (hashSet2.Contains(id))
				{
					hashSet.Add(id);
				}
				else
				{
					hashSet2.Add(id);
				}
			}
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			foreach (ApiDescription apiDescription2 in apiDescriptions)
			{
				string id2 = apiDescription2.ID;
				if (!hashSet.Contains(id2))
				{
					collection.Add(apiDescription2);
				}
			}
			return collection;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000F930 File Offset: 0x0000DB30
		private static bool MatchRegexConstraint(IHttpRoute route, string parameterName, string parameterValue)
		{
			IDictionary<string, object> constraints = route.Constraints;
			object obj;
			if (constraints != null && constraints.TryGetValue(parameterName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					string text2 = "^(" + text + ")$";
					return parameterValue != null && Regex.IsMatch(parameterValue, text2, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
				}
			}
			return true;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000F980 File Offset: 0x0000DB80
		private static HttpActionBinding GetActionBinding(HttpActionDescriptor actionDescriptor)
		{
			HttpControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;
			if (controllerDescriptor == null)
			{
				return null;
			}
			IActionValueBinder actionValueBinder = controllerDescriptor.Configuration.Services.GetActionValueBinder();
			if (actionValueBinder == null)
			{
				return null;
			}
			return actionValueBinder.GetBinding(actionDescriptor);
		}

		// Token: 0x0400015E RID: 350
		private Lazy<Collection<ApiDescription>> _apiDescriptions;

		// Token: 0x0400015F RID: 351
		private readonly HttpConfiguration _config;

		// Token: 0x04000160 RID: 352
		private static readonly Regex _actionVariableRegex = new Regex(string.Format(CultureInfo.CurrentCulture, "{{{0}}}", new object[] { "action" }), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000161 RID: 353
		private static readonly Regex _controllerVariableRegex = new Regex(string.Format(CultureInfo.CurrentCulture, "{{{0}}}", new object[] { "controller" }), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x020001E5 RID: 485
		private sealed class ApiDescriptionComparer : IEqualityComparer<ApiDescription>
		{
			// Token: 0x06000B5E RID: 2910 RVA: 0x0001D57E File Offset: 0x0001B77E
			public bool Equals(ApiDescription x, ApiDescription y)
			{
				return string.Equals(x.ID, y.ID, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x0001D592 File Offset: 0x0001B792
			public int GetHashCode(ApiDescription obj)
			{
				return obj.ID.ToUpperInvariant().GetHashCode();
			}
		}
	}
}
