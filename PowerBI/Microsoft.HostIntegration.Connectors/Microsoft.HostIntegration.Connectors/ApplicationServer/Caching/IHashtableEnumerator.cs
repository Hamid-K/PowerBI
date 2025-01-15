using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000231 RID: 561
	internal interface IHashtableEnumerator : IEnumerator<ADMCacheItem>, IDisposable, IEnumerator
	{
		// Token: 0x060012B5 RID: 4789
		ADMCacheItem GetNext();
	}
}
