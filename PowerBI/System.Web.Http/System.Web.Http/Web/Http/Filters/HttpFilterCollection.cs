using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C4 RID: 196
	public class HttpFilterCollection : IEnumerable<FilterInfo>, IEnumerable
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000DB87 File Offset: 0x0000BD87
		public int Count
		{
			get
			{
				return this._filters.Count;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public void Add(IFilter filter)
		{
			if (filter == null)
			{
				throw Error.ArgumentNull("filter");
			}
			this._filters.Add(HttpFilterCollection.CreateFilterInfo(filter));
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		public void AddRange(IEnumerable<IFilter> filters)
		{
			if (filters == null)
			{
				throw Error.ArgumentNull("filters");
			}
			IFilter[] array = filters.ToArray<IFilter>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SRResources.CollectionParameterContainsNullElement, new object[] { "filters" }), "filters");
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				this._filters.Add(HttpFilterCollection.CreateFilterInfo(array[j]));
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000DC36 File Offset: 0x0000BE36
		private static FilterInfo CreateFilterInfo(IFilter filter)
		{
			return new FilterInfo(filter, FilterScope.Global);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000DC3F File Offset: 0x0000BE3F
		public void Clear()
		{
			this._filters.Clear();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		public bool Contains(IFilter filter)
		{
			return this._filters.Any((FilterInfo f) => f.Instance == filter);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000DC7D File Offset: 0x0000BE7D
		public IEnumerator<FilterInfo> GetEnumerator()
		{
			return this._filters.GetEnumerator();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000DC8F File Offset: 0x0000BE8F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public void Remove(IFilter filter)
		{
			this._filters.RemoveAll((FilterInfo f) => f.Instance == filter);
		}

		// Token: 0x04000134 RID: 308
		private readonly List<FilterInfo> _filters = new List<FilterInfo>();
	}
}
