using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000883 RID: 2179
	public interface IReferenceCreator
	{
		// Token: 0x060077EF RID: 30703
		bool TryCreateReference(IStorable refTarget, out BaseReference newReference);

		// Token: 0x060077F0 RID: 30704
		bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference newReference);
	}
}
