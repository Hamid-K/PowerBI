using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000328 RID: 808
	internal interface IValueStore
	{
		// Token: 0x06001D31 RID: 7473
		long GetValue();

		// Token: 0x06001D32 RID: 7474
		void Add(long count);

		// Token: 0x06001D33 RID: 7475
		void Increment();

		// Token: 0x06001D34 RID: 7476
		void Decrement();

		// Token: 0x06001D35 RID: 7477
		void SetValue(long value);
	}
}
