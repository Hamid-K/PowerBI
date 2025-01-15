using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000235 RID: 565
	public interface IObjectPool<Tkey, Tvalue>
	{
		// Token: 0x06000EB7 RID: 3767
		void CheckIn(Tkey key, Tvalue obj);

		// Token: 0x06000EB8 RID: 3768
		bool TryCheckOut(Tkey key, out Tvalue obj);

		// Token: 0x06000EB9 RID: 3769
		bool TryTouch(Tkey key, out Tvalue obj);

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000EBA RID: 3770
		int Count { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000EBB RID: 3771
		IEnumerable<Tvalue> Values { get; }

		// Token: 0x06000EBC RID: 3772
		void Clear();
	}
}
