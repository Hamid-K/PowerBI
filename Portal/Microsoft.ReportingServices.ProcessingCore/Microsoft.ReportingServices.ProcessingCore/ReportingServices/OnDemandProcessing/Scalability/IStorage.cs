using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000889 RID: 2185
	public interface IStorage : IDisposable
	{
		// Token: 0x06007808 RID: 30728
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset, out long persistedSize);

		// Token: 0x06007809 RID: 30729
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable Retrieve(long offset);

		// Token: 0x0600780A RID: 30730
		T Retrieve<T>(long offset, out long persistedSize) where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, new();

		// Token: 0x0600780B RID: 30731
		long Allocate(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj);

		// Token: 0x0600780C RID: 30732
		void Free(long offset, int size);

		// Token: 0x0600780D RID: 30733
		long Update(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable obj, long offset, long oldPersistedSize);

		// Token: 0x0600780E RID: 30734
		void Close();

		// Token: 0x0600780F RID: 30735
		void Flush();

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x06007810 RID: 30736
		long StreamSize { get; }

		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x06007811 RID: 30737
		// (set) Token: 0x06007812 RID: 30738
		IScalabilityCache ScalabilityCache { get; set; }

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x06007813 RID: 30739
		IReferenceCreator ReferenceCreator { get; }

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x06007814 RID: 30740
		// (set) Token: 0x06007815 RID: 30741
		bool FreezeAllocations { get; set; }

		// Token: 0x06007816 RID: 30742
		void TraceStats();
	}
}
