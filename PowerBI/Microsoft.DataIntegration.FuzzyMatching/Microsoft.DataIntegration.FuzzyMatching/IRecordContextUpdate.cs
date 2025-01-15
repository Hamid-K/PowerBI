using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000073 RID: 115
	public interface IRecordContextUpdate
	{
		// Token: 0x060004C0 RID: 1216
		IUpdateContext BeginUpdate();

		// Token: 0x060004C1 RID: 1217
		void AddRecordContext(IUpdateContext context, RecordContext recordContext);

		// Token: 0x060004C2 RID: 1218
		void RemoveRecordContext(IUpdateContext context, RecordContext recordContext);

		// Token: 0x060004C3 RID: 1219
		void EndUpdate(IUpdateContext context);
	}
}
