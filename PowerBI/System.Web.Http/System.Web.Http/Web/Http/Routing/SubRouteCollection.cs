using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000155 RID: 341
	internal class SubRouteCollection : IReadOnlyCollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x000178A0 File Offset: 0x00015AA0
		public void Add(RouteEntry entry)
		{
			IHttpRoute route = entry.Route;
			string name = entry.Name;
			if (name != null)
			{
				RouteEntry routeEntry = this._entries.SingleOrDefault((RouteEntry e) => e.Name == name);
				if (routeEntry != null)
				{
					SubRouteCollection.ThrowExceptionForDuplicateRouteNames(name, route, routeEntry.Route);
				}
			}
			this._routes.Add(route);
			this._entries.Add(entry);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00017914 File Offset: 0x00015B14
		public void AddRange(IEnumerable<RouteEntry> entries)
		{
			foreach (RouteEntry routeEntry in entries)
			{
				this.Add(routeEntry);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001795C File Offset: 0x00015B5C
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00017969 File Offset: 0x00015B69
		public IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this._routes.GetEnumerator();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001797B File Offset: 0x00015B7B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._routes).GetEnumerator();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00017988 File Offset: 0x00015B88
		public IReadOnlyCollection<RouteEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00017990 File Offset: 0x00015B90
		private static void ThrowExceptionForDuplicateRouteNames(string name, IHttpRoute route1, IHttpRoute route2)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SRResources.SubRouteCollection_DuplicateRouteName, new object[] { name, route1.RouteTemplate, route2.RouteTemplate }));
		}

		// Token: 0x04000280 RID: 640
		private readonly List<IHttpRoute> _routes = new List<IHttpRoute>();

		// Token: 0x04000281 RID: 641
		private readonly List<RouteEntry> _entries = new List<RouteEntry>();
	}
}
