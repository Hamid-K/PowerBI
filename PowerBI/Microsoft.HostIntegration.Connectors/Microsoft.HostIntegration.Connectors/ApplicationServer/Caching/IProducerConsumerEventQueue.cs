using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000028 RID: 40
	internal interface IProducerConsumerEventQueue
	{
		// Token: 0x0600012A RID: 298
		void Enqueue(CacheNotificationGroup e);

		// Token: 0x0600012B RID: 299
		CacheNotificationGroup Dequeue();

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012C RID: 300
		long CountPendingEvent { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012D RID: 301
		bool IsEmpty { get; }
	}
}
