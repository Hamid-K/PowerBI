using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200014E RID: 334
	internal class RouteCollectionRoute : IHttpRoute, IReadOnlyCollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00016EE0 File Offset: 0x000150E0
		public void EnsureInitialized(Func<IReadOnlyCollection<IHttpRoute>> initializer)
		{
			if (this._beingInitialized && this._subRoutes == null)
			{
				return;
			}
			try
			{
				this._beingInitialized = true;
				this._subRoutes = initializer();
			}
			finally
			{
				this._beingInitialized = false;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00016F2C File Offset: 0x0001512C
		private IReadOnlyCollection<IHttpRoute> SubRoutes
		{
			get
			{
				if (this._subRoutes == null)
				{
					throw new InvalidOperationException(Error.Format(SRResources.Object_NotYetInitialized, new object[0]));
				}
				return this._subRoutes;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00004CCE File Offset: 0x00002ECE
		public string RouteTemplate
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00016F52 File Offset: 0x00015152
		public IDictionary<string, object> Defaults
		{
			get
			{
				return RouteCollectionRoute._empty;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00016F52 File Offset: 0x00015152
		public IDictionary<string, object> Constraints
		{
			get
			{
				return RouteCollectionRoute._empty;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0000413B File Offset: 0x0000233B
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0000413B File Offset: 0x0000233B
		public HttpMessageHandler Handler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00016F5C File Offset: 0x0001515C
		public IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			List<IHttpRouteData> list = new List<IHttpRouteData>();
			foreach (IHttpRoute httpRoute in this.SubRoutes)
			{
				IHttpRouteData routeData = httpRoute.GetRouteData(virtualPathRoot, request);
				if (routeData != null)
				{
					list.Add(routeData);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new RouteCollectionRoute.RouteCollectionRouteData(this, list.ToArray());
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000413B File Offset: 0x0000233B
		public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			return null;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00016FD0 File Offset: 0x000151D0
		public int Count
		{
			get
			{
				return this.SubRoutes.Count;
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00016FDD File Offset: 0x000151DD
		public IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this.SubRoutes.GetEnumerator();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00016FDD File Offset: 0x000151DD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.SubRoutes.GetEnumerator();
		}

		// Token: 0x04000272 RID: 626
		public const string SubRouteDataKey = "MS_SubRoutes";

		// Token: 0x04000273 RID: 627
		private IReadOnlyCollection<IHttpRoute> _subRoutes;

		// Token: 0x04000274 RID: 628
		private static readonly IDictionary<string, object> _empty = EmptyReadOnlyDictionary<string, object>.Value;

		// Token: 0x04000275 RID: 629
		private bool _beingInitialized;

		// Token: 0x02000246 RID: 582
		private class RouteCollectionRouteData : IHttpRouteData
		{
			// Token: 0x06000CAB RID: 3243 RVA: 0x00021F5F File Offset: 0x0002015F
			public RouteCollectionRouteData(IHttpRoute parent, IHttpRouteData[] subRouteDatas)
			{
				this.Route = parent;
				this.Values = new HttpRouteValueDictionary { { "MS_SubRoutes", subRouteDatas } };
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00021F85 File Offset: 0x00020185
			// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00021F8D File Offset: 0x0002018D
			public IHttpRoute Route { get; private set; }

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00021F96 File Offset: 0x00020196
			// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00021F9E File Offset: 0x0002019E
			public IDictionary<string, object> Values { get; private set; }
		}
	}
}
