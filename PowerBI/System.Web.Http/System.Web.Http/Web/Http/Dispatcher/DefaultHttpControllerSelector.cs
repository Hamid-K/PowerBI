using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000082 RID: 130
	public class DefaultHttpControllerSelector : IHttpControllerSelector
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000972C File Offset: 0x0000792C
		public DefaultHttpControllerSelector(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._controllerInfoCache = new Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>>(new Func<ConcurrentDictionary<string, HttpControllerDescriptor>>(this.InitializeControllerInfoCache));
			this._configuration = configuration;
			this._controllerTypeCache = new HttpControllerTypeCache(this._configuration);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000977C File Offset: 0x0000797C
		public virtual HttpControllerDescriptor SelectController(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IHttpRouteData routeData = request.GetRouteData();
			HttpControllerDescriptor directRouteController;
			if (routeData != null)
			{
				directRouteController = DefaultHttpControllerSelector.GetDirectRouteController(routeData);
				if (directRouteController != null)
				{
					return directRouteController;
				}
			}
			string controllerName = this.GetControllerName(request);
			if (string.IsNullOrEmpty(controllerName))
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { request.RequestUri }), Error.Format(SRResources.ControllerNameNotFound, new object[] { request.RequestUri })));
			}
			if (this._controllerInfoCache.Value.TryGetValue(controllerName, out directRouteController))
			{
				return directRouteController;
			}
			ICollection<Type> controllerTypes = this._controllerTypeCache.GetControllerTypes(controllerName);
			if (controllerTypes.Count == 0)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { request.RequestUri }), Error.Format(SRResources.DefaultControllerFactory_ControllerNameNotFound, new object[] { controllerName })));
			}
			throw DefaultHttpControllerSelector.CreateAmbiguousControllerException(request.GetRouteData().Route, controllerName, controllerTypes);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000987C File Offset: 0x00007A7C
		public virtual IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
		{
			return this._controllerInfoCache.Value.ToDictionary((KeyValuePair<string, HttpControllerDescriptor> c) => c.Key, (KeyValuePair<string, HttpControllerDescriptor> c) => c.Value, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000098DC File Offset: 0x00007ADC
		public virtual string GetControllerName(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IHttpRouteData routeData = request.GetRouteData();
			if (routeData == null)
			{
				return null;
			}
			string text = null;
			routeData.Values.TryGetValue("controller", out text);
			return text;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000991C File Offset: 0x00007B1C
		private static HttpControllerDescriptor GetDirectRouteController(IHttpRouteData routeData)
		{
			CandidateAction[] directRouteCandidates = routeData.GetDirectRouteCandidates();
			if (directRouteCandidates != null)
			{
				HttpControllerDescriptor controllerDescriptor = directRouteCandidates[0].ActionDescriptor.ControllerDescriptor;
				for (int i = 1; i < directRouteCandidates.Length; i++)
				{
					if (directRouteCandidates[i].ActionDescriptor.ControllerDescriptor != controllerDescriptor)
					{
						throw DefaultHttpControllerSelector.CreateDirectRouteAmbiguousControllerException(directRouteCandidates);
					}
				}
				return controllerDescriptor;
			}
			return null;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000996C File Offset: 0x00007B6C
		private static Exception CreateDirectRouteAmbiguousControllerException(CandidateAction[] candidates)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			for (int i = 0; i < candidates.Length; i++)
			{
				hashSet.Add(candidates[i].ActionDescriptor.ControllerDescriptor.ControllerType);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in hashSet)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			return Error.InvalidOperation(SRResources.DirectRoute_AmbiguousController, new object[]
			{
				stringBuilder,
				Environment.NewLine
			});
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009A18 File Offset: 0x00007C18
		private static Exception CreateAmbiguousControllerException(IHttpRoute route, string controllerName, ICollection<Type> matchingTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in matchingTypes)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			return new InvalidOperationException(Error.Format(SRResources.DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate, new object[]
			{
				controllerName,
				route.RouteTemplate,
				stringBuilder,
				Environment.NewLine
			}));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009AA4 File Offset: 0x00007CA4
		private ConcurrentDictionary<string, HttpControllerDescriptor> InitializeControllerInfoCache()
		{
			ConcurrentDictionary<string, HttpControllerDescriptor> concurrentDictionary = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet = new HashSet<string>();
			foreach (KeyValuePair<string, ILookup<string, Type>> keyValuePair in this._controllerTypeCache.Cache)
			{
				string key = keyValuePair.Key;
				foreach (IGrouping<string, Type> grouping in keyValuePair.Value)
				{
					foreach (Type type in grouping)
					{
						if (concurrentDictionary.Keys.Contains(key))
						{
							hashSet.Add(key);
							break;
						}
						concurrentDictionary.TryAdd(key, new HttpControllerDescriptor(this._configuration, key, type));
					}
				}
			}
			foreach (string text in hashSet)
			{
				HttpControllerDescriptor httpControllerDescriptor;
				concurrentDictionary.TryRemove(text, out httpControllerDescriptor);
			}
			return concurrentDictionary;
		}

		// Token: 0x040000B4 RID: 180
		public static readonly string ControllerSuffix = "Controller";

		// Token: 0x040000B5 RID: 181
		private const string ControllerKey = "controller";

		// Token: 0x040000B6 RID: 182
		private readonly HttpConfiguration _configuration;

		// Token: 0x040000B7 RID: 183
		private readonly HttpControllerTypeCache _controllerTypeCache;

		// Token: 0x040000B8 RID: 184
		private readonly Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>> _controllerInfoCache;
	}
}
