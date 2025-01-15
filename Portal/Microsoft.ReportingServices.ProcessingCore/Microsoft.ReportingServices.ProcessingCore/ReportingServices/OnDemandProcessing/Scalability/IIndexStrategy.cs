using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200088E RID: 2190
	internal interface IIndexStrategy
	{
		// Token: 0x06007821 RID: 30753
		ReferenceID GenerateId(ReferenceID tempId);

		// Token: 0x06007822 RID: 30754
		ReferenceID GenerateTempId();

		// Token: 0x06007823 RID: 30755
		long Retrieve(ReferenceID id);

		// Token: 0x06007824 RID: 30756
		void Update(ReferenceID id, long value);

		// Token: 0x06007825 RID: 30757
		void Close();

		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x06007826 RID: 30758
		ReferenceID MaxId { get; }
	}
}
