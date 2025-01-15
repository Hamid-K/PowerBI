using System;
using System.Collections.Generic;

namespace dotless.Core.Cache
{
	// Token: 0x020000C1 RID: 193
	public interface ICache
	{
		// Token: 0x060005AE RID: 1454
		void Insert(string cacheKey, IEnumerable<string> fileDependancies, string css);

		// Token: 0x060005AF RID: 1455
		bool Exists(string cacheKey);

		// Token: 0x060005B0 RID: 1456
		string Retrieve(string cacheKey);
	}
}
