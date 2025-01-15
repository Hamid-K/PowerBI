using System;
using System.Collections.Generic;

namespace dotless.Core.Cache
{
	// Token: 0x020000C2 RID: 194
	public class InMemoryCache : ICache
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x000182E9 File Offset: 0x000164E9
		public InMemoryCache()
		{
			this._cache = new Dictionary<string, string>();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000182FC File Offset: 0x000164FC
		public void Insert(string fileName, IEnumerable<string> imports, string css)
		{
			this._cache[fileName] = css;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001830B File Offset: 0x0001650B
		public bool Exists(string filename)
		{
			return this._cache.ContainsKey(filename);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018319 File Offset: 0x00016519
		public string Retrieve(string filename)
		{
			if (this._cache.ContainsKey(filename))
			{
				return this._cache[filename];
			}
			return "";
		}

		// Token: 0x04000132 RID: 306
		private readonly Dictionary<string, string> _cache;
	}
}
