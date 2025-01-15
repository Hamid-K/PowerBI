using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000881 RID: 2177
	public interface IReference : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060077E8 RID: 30696
		IDisposable PinValue();

		// Token: 0x060077E9 RID: 30697
		void UnPinValue();

		// Token: 0x060077EA RID: 30698
		void Free();

		// Token: 0x060077EB RID: 30699
		void UpdateSize(int sizeDeltaBytes);

		// Token: 0x060077EC RID: 30700
		IReference TransferTo(IScalabilityCache scaleCache);

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x060077ED RID: 30701
		ReferenceID Id { get; }
	}
}
