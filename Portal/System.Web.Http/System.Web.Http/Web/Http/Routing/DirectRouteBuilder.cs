using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200013F RID: 319
	internal class DirectRouteBuilder : IDirectRouteBuilder
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x000159A9 File Offset: 0x00013BA9
		public DirectRouteBuilder(IReadOnlyCollection<HttpActionDescriptor> actions, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			this._actions = actions.ToArray<HttpActionDescriptor>();
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000159D2 File Offset: 0x00013BD2
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000159DA File Offset: 0x00013BDA
		public string Name { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000159E3 File Offset: 0x00013BE3
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x000159EB File Offset: 0x00013BEB
		public string Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this.ParsedRoute = null;
				this._template = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000159FB File Offset: 0x00013BFB
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00015A03 File Offset: 0x00013C03
		public IDictionary<string, object> Defaults { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00015A0C File Offset: 0x00013C0C
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00015A14 File Offset: 0x00013C14
		public IDictionary<string, object> Constraints { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00015A1D File Offset: 0x00013C1D
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00015A25 File Offset: 0x00013C25
		public IDictionary<string, object> DataTokens { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00015A2E File Offset: 0x00013C2E
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00015A36 File Offset: 0x00013C36
		internal HttpParsedRoute ParsedRoute { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00015A3F File Offset: 0x00013C3F
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00015A47 File Offset: 0x00013C47
		public int Order { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00015A50 File Offset: 0x00013C50
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00015A58 File Offset: 0x00013C58
		public decimal Precedence { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00015A61 File Offset: 0x00013C61
		public IReadOnlyCollection<HttpActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00015A69 File Offset: 0x00013C69
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00015A74 File Offset: 0x00013C74
		public virtual RouteEntry Build()
		{
			if (this.ParsedRoute == null)
			{
				this.ParsedRoute = RouteParser.Parse(this.Template);
			}
			this.ValidateParameters(this.ParsedRoute);
			HttpRouteValueDictionary httpRouteValueDictionary = DirectRouteBuilder.Copy(this.Defaults);
			HttpRouteValueDictionary httpRouteValueDictionary2 = DirectRouteBuilder.Copy(this.Constraints);
			HttpRouteValueDictionary httpRouteValueDictionary3 = DirectRouteBuilder.Copy(this.DataTokens) ?? new HttpRouteValueDictionary();
			httpRouteValueDictionary3["actions"] = this._actions;
			if (!this.TargetIsAction)
			{
				httpRouteValueDictionary3["controller"] = this._actions[0].ControllerDescriptor;
			}
			int order = this.Order;
			if (order != 0)
			{
				httpRouteValueDictionary3["order"] = order;
			}
			decimal precedence = this.Precedence;
			if (precedence != 0m)
			{
				httpRouteValueDictionary3["precedence"] = precedence;
			}
			if (httpRouteValueDictionary2 != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in httpRouteValueDictionary2)
				{
					HttpRoute.ValidateConstraint(this.Template, keyValuePair.Key, keyValuePair.Value);
				}
			}
			HttpMessageHandler httpMessageHandler = null;
			IHttpRoute httpRoute = new HttpRoute(this.Template, httpRouteValueDictionary, httpRouteValueDictionary2, httpRouteValueDictionary3, httpMessageHandler, this.ParsedRoute);
			return new RouteEntry(this.Name, httpRoute);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00015BC8 File Offset: 0x00013DC8
		internal virtual void ValidateParameters(HttpParsedRoute parsedRoute)
		{
			if (parsedRoute.PathSegments != null)
			{
				foreach (PathContentSegment pathContentSegment in parsedRoute.PathSegments.OfType<PathContentSegment>())
				{
					if (pathContentSegment != null && pathContentSegment.Subsegments != null)
					{
						foreach (PathParameterSubsegment pathParameterSubsegment in pathContentSegment.Subsegments.OfType<PathParameterSubsegment>())
						{
							if (pathParameterSubsegment != null)
							{
								if (string.Equals(pathParameterSubsegment.ParameterName, "controller", StringComparison.OrdinalIgnoreCase))
								{
									throw Error.InvalidOperation(SRResources.DirectRoute_InvalidParameter_Controller, new object[0]);
								}
								if (this.TargetIsAction && string.Equals(pathParameterSubsegment.ParameterName, "action", StringComparison.OrdinalIgnoreCase))
								{
									throw Error.InvalidOperation(SRResources.DirectRoute_InvalidParameter_Action, new object[0]);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00015CC4 File Offset: 0x00013EC4
		internal static void ValidateRouteEntry(RouteEntry entry)
		{
			IHttpRoute route = entry.Route;
			HttpActionDescriptor[] targetActionDescriptors = route.GetTargetActionDescriptors();
			if (targetActionDescriptors == null || targetActionDescriptors.Length == 0)
			{
				throw new InvalidOperationException(SRResources.DirectRoute_MissingActionDescriptors);
			}
			if (route.Handler != null)
			{
				throw new InvalidOperationException(SRResources.DirectRoute_HandlerNotSupported);
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00015D02 File Offset: 0x00013F02
		private static HttpRouteValueDictionary Copy(IDictionary<string, object> routeDictionary)
		{
			if (routeDictionary == null)
			{
				return null;
			}
			return new HttpRouteValueDictionary(routeDictionary);
		}

		// Token: 0x04000257 RID: 599
		private readonly HttpActionDescriptor[] _actions;

		// Token: 0x04000258 RID: 600
		private readonly bool _targetIsAction;

		// Token: 0x04000259 RID: 601
		private string _template;
	}
}
