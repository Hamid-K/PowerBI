using System;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000109 RID: 265
	internal interface IThrottlerQueue<T>
	{
		// Token: 0x0600074F RID: 1871
		T Peek();

		// Token: 0x06000750 RID: 1872
		T Dequeue();

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000751 RID: 1873
		int Count { get; }

		// Token: 0x06000752 RID: 1874
		void Enqueue(T item);
	}
}
