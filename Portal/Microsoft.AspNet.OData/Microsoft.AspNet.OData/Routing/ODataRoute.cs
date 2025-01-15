using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000069 RID: 105
	public class ODataRoute : HttpRoute
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000CF19 File Offset: 0x0000B119
		public ODataRoute(string routePrefix, ODataPathRouteConstraint pathConstraint)
			: this(routePrefix, pathConstraint, null, null, null, null)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000CF19 File Offset: 0x0000B119
		public ODataRoute(string routePrefix, IHttpRouteConstraint routeConstraint)
			: this(routePrefix, routeConstraint, null, null, null, null)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000CF27 File Offset: 0x0000B127
		public ODataRoute(string routePrefix, ODataPathRouteConstraint pathConstraint, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler)
			: this(routePrefix, pathConstraint, defaults, constraints, dataTokens, handler)
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CF38 File Offset: 0x0000B138
		public ODataRoute(string routePrefix, IHttpRouteConstraint routeConstraint, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler)
			: base(ODataRoute.GetRouteTemplate(routePrefix), defaults, constraints, dataTokens, handler)
		{
			this.RouteConstraint = routeConstraint;
			this.Initialize(routePrefix, routeConstraint as ODataPathRouteConstraint);
			if (routeConstraint != null)
			{
				base.Constraints.Add(ODataRouteConstants.ConstraintName, routeConstraint);
			}
			base.Constraints.Add(ODataRouteConstants.VersionConstraintName, new ODataVersionConstraint());
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000CF95 File Offset: 0x0000B195
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public IHttpRouteConstraint RouteConstraint { get; private set; }

		// Token: 0x06000401 RID: 1025 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			object obj;
			if (values != null && values.Keys.Contains(HttpRoute.HttpRouteKey, StringComparer.OrdinalIgnoreCase) && values.TryGetValue(ODataRouteConstants.ODataPath, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					if (!this.CanGenerateDirectLink)
					{
						return base.GetVirtualPath(request, values);
					}
					return this.GenerateLinkDirectly(text);
				}
			}
			return null;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D004 File Offset: 0x0000B204
		[Obsolete("The version constraint is relaxed by default")]
		public ODataRoute HasRelaxedODataVersionConstraint()
		{
			return this.SetODataVersionConstraint(true);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D010 File Offset: 0x0000B210
		private ODataRoute SetODataVersionConstraint(bool isRelaxedMatch)
		{
			object obj;
			if (base.Constraints.TryGetValue(ODataRouteConstants.VersionConstraintName, out obj))
			{
				((ODataVersionConstraint)obj).IsRelaxedMatch = isRelaxedMatch;
			}
			return this;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D040 File Offset: 0x0000B240
		internal HttpVirtualPathData GenerateLinkDirectly(string odataPath)
		{
			string text = ODataRoute.CombinePathSegments(this.RoutePrefix, odataPath);
			text = ODataRoute.UriEncode(text);
			return new HttpVirtualPathData(this, text);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D068 File Offset: 0x0000B268
		private void Initialize(string routePrefix, ODataPathRouteConstraint pathRouteConstraint)
		{
			this.RoutePrefix = routePrefix;
			this.PathRouteConstraint = pathRouteConstraint;
			this.CanGenerateDirectLink = routePrefix == null || routePrefix.IndexOf('{') == -1;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D08F File Offset: 0x0000B28F
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000D097 File Offset: 0x0000B297
		public string RoutePrefix { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public ODataPathRouteConstraint PathRouteConstraint { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000D0B1 File Offset: 0x0000B2B1
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		internal bool CanGenerateDirectLink { get; private set; }

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D0C2 File Offset: 0x0000B2C2
		private static string GetRouteTemplate(string prefix)
		{
			if (!string.IsNullOrEmpty(prefix))
			{
				return prefix + "/" + ODataRouteConstants.ODataPathTemplate;
			}
			return ODataRouteConstants.ODataPathTemplate;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000D0E2 File Offset: 0x0000B2E2
		private static string CombinePathSegments(string routePrefix, string odataPath)
		{
			if (string.IsNullOrEmpty(routePrefix))
			{
				return odataPath;
			}
			if (!string.IsNullOrEmpty(odataPath))
			{
				return routePrefix + "/" + odataPath;
			}
			return routePrefix;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000D104 File Offset: 0x0000B304
		private static string UriEncode(string str)
		{
			return Uri.EscapeUriString(str).Replace("#", ODataRoute._escapedHashMark).Replace("?", ODataRoute._escapedQuestionMark);
		}

		// Token: 0x040000CC RID: 204
		private static readonly string _escapedHashMark = Uri.EscapeDataString("#");

		// Token: 0x040000CD RID: 205
		private static readonly string _escapedQuestionMark = Uri.EscapeDataString("?");
	}
}
