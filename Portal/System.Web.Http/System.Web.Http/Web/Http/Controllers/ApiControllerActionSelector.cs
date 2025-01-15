using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Http.Internal;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000108 RID: 264
	public class ApiControllerActionSelector : IHttpActionSelector
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x00011544 File Offset: 0x0000F744
		public virtual HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			return this.GetInternalSelector(controllerContext.ControllerDescriptor).SelectAction(controllerContext);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00011566 File Offset: 0x0000F766
		public virtual ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			return this.GetInternalSelector(controllerDescriptor).GetActionMapping();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00011584 File Offset: 0x0000F784
		private ApiControllerActionSelector.ActionSelectorCacheItem GetInternalSelector(HttpControllerDescriptor controllerDescriptor)
		{
			if (this._fastCache == null)
			{
				ApiControllerActionSelector.ActionSelectorCacheItem actionSelectorCacheItem = new ApiControllerActionSelector.ActionSelectorCacheItem(controllerDescriptor);
				Interlocked.CompareExchange<ApiControllerActionSelector.ActionSelectorCacheItem>(ref this._fastCache, actionSelectorCacheItem, null);
				return actionSelectorCacheItem;
			}
			if (this._fastCache.HttpControllerDescriptor == controllerDescriptor)
			{
				return this._fastCache;
			}
			object obj;
			if (controllerDescriptor.Properties.TryGetValue(this._cacheKey, out obj))
			{
				return (ApiControllerActionSelector.ActionSelectorCacheItem)obj;
			}
			ApiControllerActionSelector.ActionSelectorCacheItem actionSelectorCacheItem2 = new ApiControllerActionSelector.ActionSelectorCacheItem(controllerDescriptor);
			controllerDescriptor.Properties.TryAdd(this._cacheKey, actionSelectorCacheItem2);
			return actionSelectorCacheItem2;
		}

		// Token: 0x040001C2 RID: 450
		private ApiControllerActionSelector.ActionSelectorCacheItem _fastCache;

		// Token: 0x040001C3 RID: 451
		private readonly object _cacheKey = new object();

		// Token: 0x02000200 RID: 512
		private class ActionSelectorCacheItem
		{
			// Token: 0x06000BBD RID: 3005 RVA: 0x0001EC70 File Offset: 0x0001CE70
			public ActionSelectorCacheItem(HttpControllerDescriptor controllerDescriptor)
			{
				this._controllerDescriptor = controllerDescriptor;
				MethodInfo[] array = Array.FindAll<MethodInfo>(this._controllerDescriptor.ControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public), new Predicate<MethodInfo>(ApiControllerActionSelector.ActionSelectorCacheItem.IsValidActionMethod));
				this._combinedCandidateActions = new CandidateAction[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MethodInfo methodInfo = array[i];
					ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = new ReflectedHttpActionDescriptor(this._controllerDescriptor, methodInfo);
					this._combinedCandidateActions[i] = new CandidateAction
					{
						ActionDescriptor = reflectedHttpActionDescriptor
					};
					HttpActionBinding actionBinding = reflectedHttpActionDescriptor.ActionBinding;
					this._actionParameterNames.Add(reflectedHttpActionDescriptor, (from binding in actionBinding.ParameterBindings
						where !binding.Descriptor.IsOptional && TypeHelper.CanConvertFromString(binding.Descriptor.ParameterType) && binding.WillReadUri()
						select binding.Descriptor.Prefix ?? binding.Descriptor.ParameterName).ToArray<string>());
				}
				this._combinedActionNameMapping = this._combinedCandidateActions.Select((CandidateAction c) => c.ActionDescriptor).ToLookup((HttpActionDescriptor actionDesc) => actionDesc.ActionName, StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001EDC1 File Offset: 0x0001CFC1
			public HttpControllerDescriptor HttpControllerDescriptor
			{
				get
				{
					return this._controllerDescriptor;
				}
			}

			// Token: 0x06000BBF RID: 3007 RVA: 0x0001EDCC File Offset: 0x0001CFCC
			private void InitializeStandardActions()
			{
				if (this._standardActions != null)
				{
					return;
				}
				ApiControllerActionSelector.StandardActionSelectionCache standardActionSelectionCache = new ApiControllerActionSelector.StandardActionSelectionCache();
				if (this._controllerDescriptor.IsAttributeRouted())
				{
					standardActionSelectionCache.StandardCandidateActions = new CandidateAction[0];
				}
				else
				{
					List<CandidateAction> list = new List<CandidateAction>();
					for (int i = 0; i < this._combinedCandidateActions.Length; i++)
					{
						CandidateAction candidateAction = this._combinedCandidateActions[i];
						if (((ReflectedHttpActionDescriptor)candidateAction.ActionDescriptor).MethodInfo.DeclaringType != this._controllerDescriptor.ControllerType || !candidateAction.ActionDescriptor.IsAttributeRouted())
						{
							list.Add(candidateAction);
						}
					}
					standardActionSelectionCache.StandardCandidateActions = list.ToArray();
				}
				standardActionSelectionCache.StandardActionNameMapping = standardActionSelectionCache.StandardCandidateActions.Select((CandidateAction c) => c.ActionDescriptor).ToLookup((HttpActionDescriptor actionDesc) => actionDesc.ActionName, StringComparer.OrdinalIgnoreCase);
				int num = ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds.Length;
				standardActionSelectionCache.CacheListVerbs = new CandidateAction[num][];
				for (int j = 0; j < num; j++)
				{
					standardActionSelectionCache.CacheListVerbs[j] = ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds[j], standardActionSelectionCache.StandardCandidateActions);
				}
				this._standardActions = standardActionSelectionCache;
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0001EF10 File Offset: 0x0001D110
			public HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
			{
				this.InitializeStandardActions();
				List<ApiControllerActionSelector.CandidateActionWithParams> list = this.FindMatchingActions(controllerContext, false);
				int count = list.Count;
				if (count == 0)
				{
					throw new HttpResponseException(this.CreateSelectionError(controllerContext));
				}
				if (count != 1)
				{
					string text = ApiControllerActionSelector.ActionSelectorCacheItem.CreateAmbiguousMatchList(list);
					throw Error.InvalidOperation(SRResources.ApiControllerActionSelector_AmbiguousMatch, new object[] { text });
				}
				ApiControllerActionSelector.ActionSelectorCacheItem.ElevateRouteData(controllerContext, list[0]);
				return list[0].ActionDescriptor;
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x0001EF7E File Offset: 0x0001D17E
			private static void ElevateRouteData(HttpControllerContext controllerContext, ApiControllerActionSelector.CandidateActionWithParams selectedCandidate)
			{
				controllerContext.RouteData = selectedCandidate.RouteDataSource;
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0001EF8C File Offset: 0x0001D18C
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindMatchingActions(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				IEnumerable<IHttpRouteData> subRoutes = controllerContext.RouteData.GetSubRoutes();
				IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> enumerable;
				if (subRoutes != null)
				{
					IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> initialCandidateWithParameterListForDirectRoutes = ApiControllerActionSelector.ActionSelectorCacheItem.GetInitialCandidateWithParameterListForDirectRoutes(controllerContext, subRoutes, ignoreVerbs);
					enumerable = initialCandidateWithParameterListForDirectRoutes;
				}
				else
				{
					enumerable = this.GetInitialCandidateWithParameterListForRegularRoutes(controllerContext, ignoreVerbs);
				}
				IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> enumerable2 = enumerable;
				List<ApiControllerActionSelector.CandidateActionWithParams> list = ApiControllerActionSelector.ActionSelectorCacheItem.RunPrecedenceFilter(ApiControllerActionSelector.ActionSelectorCacheItem.RunOrderFilter(this.FindActionMatchRequiredRouteAndQueryParameters(enumerable2)));
				return this.FindActionMatchMostRouteAndQueryParameters(list);
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
			private HttpResponseMessage CreateSelectionError(HttpControllerContext controllerContext)
			{
				List<ApiControllerActionSelector.CandidateActionWithParams> list = this.FindMatchingActions(controllerContext, true);
				if (list.Count > 0)
				{
					return ApiControllerActionSelector.ActionSelectorCacheItem.Create405Response(controllerContext, list);
				}
				return this.CreateActionNotFoundResponse(controllerContext);
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0001F008 File Offset: 0x0001D208
			private static HttpResponseMessage Create405Response(HttpControllerContext controllerContext, IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> allowedCandidates)
			{
				HttpMethod method = controllerContext.Request.Method;
				HttpResponseMessage httpResponseMessage = controllerContext.Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, Error.Format(SRResources.ApiControllerActionSelector_HttpMethodNotSupported, new object[] { method }));
				HashSet<HttpMethod> hashSet = new HashSet<HttpMethod>();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in allowedCandidates)
				{
					hashSet.UnionWith(candidateActionWithParams.ActionDescriptor.SupportedHttpMethods);
				}
				foreach (HttpMethod httpMethod in hashSet)
				{
					httpResponseMessage.Content.Headers.Allow.Add(httpMethod.ToString());
				}
				return httpResponseMessage;
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
			private HttpResponseMessage CreateActionNotFoundResponse(HttpControllerContext controllerContext)
			{
				return controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { controllerContext.Request.RequestUri }), Error.Format(SRResources.ApiControllerActionSelector_ActionNotFound, new object[] { this._controllerDescriptor.ControllerName }));
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0001F144 File Offset: 0x0001D344
			private HttpResponseMessage CreateActionNotFoundResponse(HttpControllerContext controllerContext, string actionName)
			{
				return controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { controllerContext.Request.RequestUri }), Error.Format(SRResources.ApiControllerActionSelector_ActionNameNotFound, new object[]
				{
					this._controllerDescriptor.ControllerName,
					actionName
				}));
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
			private static List<ApiControllerActionSelector.CandidateActionWithParams> GetInitialCandidateWithParameterListForDirectRoutes(HttpControllerContext controllerContext, IEnumerable<IHttpRouteData> subRoutes, bool ignoreVerbs)
			{
				HttpRequestMessage request = controllerContext.Request;
				HttpMethod method = controllerContext.Request.Method;
				IEnumerable<KeyValuePair<string, string>> queryNameValuePairs = request.GetQueryNameValuePairs();
				List<ApiControllerActionSelector.CandidateActionWithParams> list = new List<ApiControllerActionSelector.CandidateActionWithParams>();
				foreach (IHttpRouteData httpRouteData in subRoutes)
				{
					ISet<string> combinedParameterNames = ApiControllerActionSelector.ActionSelectorCacheItem.GetCombinedParameterNames(queryNameValuePairs, httpRouteData.Values);
					CandidateAction[] directRouteCandidates = httpRouteData.Route.GetDirectRouteCandidates();
					string text;
					httpRouteData.Values.TryGetValue("action", out text);
					foreach (CandidateAction candidateAction in directRouteCandidates)
					{
						if ((text == null || candidateAction.MatchName(text)) && (ignoreVerbs || candidateAction.MatchVerb(method)))
						{
							list.Add(new ApiControllerActionSelector.CandidateActionWithParams(candidateAction, combinedParameterNames, httpRouteData));
						}
					}
				}
				return list;
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x0001F284 File Offset: 0x0001D484
			private IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> GetInitialCandidateWithParameterListForRegularRoutes(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				CandidateAction[] initialCandidateList = this.GetInitialCandidateList(controllerContext, ignoreVerbs);
				return ApiControllerActionSelector.ActionSelectorCacheItem.GetCandidateActionsWithBindings(controllerContext, initialCandidateList);
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
			private CandidateAction[] GetInitialCandidateList(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				HttpMethod method = controllerContext.Request.Method;
				string text;
				CandidateAction[] array3;
				if (controllerContext.RouteData.Values.TryGetValue("action", out text))
				{
					HttpActionDescriptor[] array = this._standardActions.StandardActionNameMapping[text].ToArray<HttpActionDescriptor>();
					if (array.Length == 0)
					{
						throw new HttpResponseException(this.CreateActionNotFoundResponse(controllerContext, text));
					}
					CandidateAction[] array2 = new CandidateAction[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new CandidateAction
						{
							ActionDescriptor = array[i]
						};
					}
					if (ignoreVerbs)
					{
						array3 = array2;
					}
					else
					{
						array3 = ApiControllerActionSelector.ActionSelectorCacheItem.FilterIncompatibleVerbs(method, array2);
					}
				}
				else if (ignoreVerbs)
				{
					array3 = this._standardActions.StandardCandidateActions;
				}
				else
				{
					array3 = ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerb(method, this._standardActions.CacheListVerbs, this._standardActions.StandardCandidateActions);
				}
				return array3;
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0001F374 File Offset: 0x0001D574
			private static CandidateAction[] FilterIncompatibleVerbs(HttpMethod incomingMethod, CandidateAction[] candidatesFoundByName)
			{
				return candidatesFoundByName.Where((CandidateAction candidate) => candidate.ActionDescriptor.SupportedHttpMethods.Contains(incomingMethod)).ToArray<CandidateAction>();
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
			public ILookup<string, HttpActionDescriptor> GetActionMapping()
			{
				return this._combinedActionNameMapping;
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
			private static ISet<string> GetCombinedParameterNames(IEnumerable<KeyValuePair<string, string>> queryNameValuePairs, IDictionary<string, object> routeValues)
			{
				HashSet<string> hashSet = new HashSet<string>(routeValues.Keys, StringComparer.OrdinalIgnoreCase);
				hashSet.Remove("controller");
				hashSet.Remove("action");
				HashSet<string> hashSet2 = new HashSet<string>(hashSet, StringComparer.OrdinalIgnoreCase);
				if (queryNameValuePairs != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in queryNameValuePairs)
					{
						hashSet2.Add(keyValuePair.Key);
					}
				}
				return hashSet2;
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x0001F438 File Offset: 0x0001D638
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindActionMatchRequiredRouteAndQueryParameters(IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				List<ApiControllerActionSelector.CandidateActionWithParams> list = new List<ApiControllerActionSelector.CandidateActionWithParams>();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in candidatesFound)
				{
					HttpActionDescriptor actionDescriptor = candidateActionWithParams.ActionDescriptor;
					if (ApiControllerActionSelector.ActionSelectorCacheItem.IsSubset(this._actionParameterNames[actionDescriptor], candidateActionWithParams.CombinedParameterNames))
					{
						list.Add(candidateActionWithParams);
					}
				}
				return list;
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindActionMatchMostRouteAndQueryParameters(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count > 1)
				{
					return (from candidate in candidatesFound
						group candidate by this._actionParameterNames[candidate.ActionDescriptor].Length into g
						orderby g.Key descending
						select g).First<IGrouping<int, ApiControllerActionSelector.CandidateActionWithParams>>().ToList<ApiControllerActionSelector.CandidateActionWithParams>();
				}
				return candidatesFound;
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x0001F500 File Offset: 0x0001D700
			private static ApiControllerActionSelector.CandidateActionWithParams[] GetCandidateActionsWithBindings(HttpControllerContext controllerContext, CandidateAction[] candidatesFound)
			{
				IEnumerable<KeyValuePair<string, string>> queryNameValuePairs = controllerContext.Request.GetQueryNameValuePairs();
				IHttpRouteData routeData = controllerContext.RouteData;
				IDictionary<string, object> values = routeData.Values;
				ISet<string> combinedParameterNames = ApiControllerActionSelector.ActionSelectorCacheItem.GetCombinedParameterNames(queryNameValuePairs, values);
				return Array.ConvertAll<CandidateAction, ApiControllerActionSelector.CandidateActionWithParams>(candidatesFound, (CandidateAction candidate) => new ApiControllerActionSelector.CandidateActionWithParams(candidate, combinedParameterNames, routeData));
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x0001F558 File Offset: 0x0001D758
			private static bool IsSubset(string[] actionParameters, ISet<string> routeAndQueryParameters)
			{
				foreach (string text in actionParameters)
				{
					if (!routeAndQueryParameters.Contains(text))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000BD1 RID: 3025 RVA: 0x0001F588 File Offset: 0x0001D788
			private static List<ApiControllerActionSelector.CandidateActionWithParams> RunOrderFilter(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count == 0)
				{
					return candidatesFound;
				}
				int minOrder = candidatesFound.Min((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Order);
				return candidatesFound.Where((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Order == minOrder).AsList<ApiControllerActionSelector.CandidateActionWithParams>();
			}

			// Token: 0x06000BD2 RID: 3026 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
			private static List<ApiControllerActionSelector.CandidateActionWithParams> RunPrecedenceFilter(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count == 0)
				{
					return candidatesFound;
				}
				decimal highestPrecedence = candidatesFound.Min((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Precedence);
				return candidatesFound.Where((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Precedence == highestPrecedence).AsList<ApiControllerActionSelector.CandidateActionWithParams>();
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x0001F648 File Offset: 0x0001D848
			private static CandidateAction[] FindActionsForVerb(HttpMethod verb, CandidateAction[][] actionsByVerb, CandidateAction[] otherActions)
			{
				for (int i = 0; i < ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds.Length; i++)
				{
					if (verb == ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds[i])
					{
						return actionsByVerb[i];
					}
				}
				return ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(verb, otherActions);
			}

			// Token: 0x06000BD4 RID: 3028 RVA: 0x0001F67C File Offset: 0x0001D87C
			private static CandidateAction[] FindActionsForVerbWorker(HttpMethod verb, CandidateAction[] candidates)
			{
				List<CandidateAction> list = new List<CandidateAction>();
				ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(verb, candidates, list);
				return list.ToArray();
			}

			// Token: 0x06000BD5 RID: 3029 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
			private static void FindActionsForVerbWorker(HttpMethod verb, CandidateAction[] candidates, List<CandidateAction> listCandidates)
			{
				foreach (CandidateAction candidateAction in candidates)
				{
					if (candidateAction.ActionDescriptor != null && candidateAction.ActionDescriptor.SupportedHttpMethods.Contains(verb))
					{
						listCandidates.Add(candidateAction);
					}
				}
			}

			// Token: 0x06000BD6 RID: 3030 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
			private static string CreateAmbiguousMatchList(IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> ambiguousCandidates)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in ambiguousCandidates)
				{
					HttpActionDescriptor actionDescriptor = candidateActionWithParams.ActionDescriptor;
					string text;
					if (actionDescriptor.ControllerDescriptor != null && actionDescriptor.ControllerDescriptor.ControllerType != null)
					{
						text = actionDescriptor.ControllerDescriptor.ControllerType.FullName;
					}
					else
					{
						text = string.Empty;
					}
					stringBuilder.AppendLine();
					stringBuilder.Append(Error.Format(SRResources.ActionSelector_AmbiguousMatchType, new object[] { actionDescriptor.ActionName, text }));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x0001F798 File Offset: 0x0001D998
			private static bool IsValidActionMethod(MethodInfo methodInfo)
			{
				return !methodInfo.IsSpecialName && !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(TypeHelper.ApiControllerType) && methodInfo.GetCustomAttribute<NonActionAttribute>() == null;
			}

			// Token: 0x0400043B RID: 1083
			private readonly HttpControllerDescriptor _controllerDescriptor;

			// Token: 0x0400043C RID: 1084
			private readonly CandidateAction[] _combinedCandidateActions;

			// Token: 0x0400043D RID: 1085
			private readonly IDictionary<HttpActionDescriptor, string[]> _actionParameterNames = new Dictionary<HttpActionDescriptor, string[]>();

			// Token: 0x0400043E RID: 1086
			private readonly ILookup<string, HttpActionDescriptor> _combinedActionNameMapping;

			// Token: 0x0400043F RID: 1087
			private static readonly HttpMethod[] _cacheListVerbKinds = new HttpMethod[]
			{
				HttpMethod.Get,
				HttpMethod.Put,
				HttpMethod.Post
			};

			// Token: 0x04000440 RID: 1088
			private ApiControllerActionSelector.StandardActionSelectionCache _standardActions;
		}

		// Token: 0x02000201 RID: 513
		[DebuggerDisplay("{DebuggerToString()}")]
		private class CandidateActionWithParams
		{
			// Token: 0x06000BDA RID: 3034 RVA: 0x0001F802 File Offset: 0x0001DA02
			public CandidateActionWithParams(CandidateAction candidateAction, ISet<string> parameters, IHttpRouteData routeDataSource)
			{
				this.CandidateAction = candidateAction;
				this.CombinedParameterNames = parameters;
				this.RouteDataSource = routeDataSource;
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001F81F File Offset: 0x0001DA1F
			// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0001F827 File Offset: 0x0001DA27
			public CandidateAction CandidateAction { get; private set; }

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001F830 File Offset: 0x0001DA30
			// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0001F838 File Offset: 0x0001DA38
			public ISet<string> CombinedParameterNames { get; private set; }

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0001F841 File Offset: 0x0001DA41
			// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0001F849 File Offset: 0x0001DA49
			public IHttpRouteData RouteDataSource { get; private set; }

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001F852 File Offset: 0x0001DA52
			public HttpActionDescriptor ActionDescriptor
			{
				get
				{
					return this.CandidateAction.ActionDescriptor;
				}
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x0001F860 File Offset: 0x0001DA60
			private string DebuggerToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.CandidateAction.DebuggerToString());
				if (this.CombinedParameterNames.Count > 0)
				{
					stringBuilder.Append(", Params =");
					foreach (string text in this.CombinedParameterNames)
					{
						stringBuilder.AppendFormat(" {0}", text);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000202 RID: 514
		private class StandardActionSelectionCache
		{
			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001F8EC File Offset: 0x0001DAEC
			// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
			public ILookup<string, HttpActionDescriptor> StandardActionNameMapping { get; set; }

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0001F8FD File Offset: 0x0001DAFD
			// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0001F905 File Offset: 0x0001DB05
			public CandidateAction[] StandardCandidateActions { get; set; }

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0001F90E File Offset: 0x0001DB0E
			// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0001F916 File Offset: 0x0001DB16
			public CandidateAction[][] CacheListVerbs { get; set; }
		}
	}
}
