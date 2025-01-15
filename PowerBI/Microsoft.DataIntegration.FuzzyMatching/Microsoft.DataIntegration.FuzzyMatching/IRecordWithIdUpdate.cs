using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008D RID: 141
	public interface IRecordWithIdUpdate
	{
		// Token: 0x060005B8 RID: 1464
		IUpdateContext BeginUpdate(DataTable schemaTable);

		// Token: 0x060005B9 RID: 1465
		void AddRecord(IUpdateContext updateContext, IDataRecord record, out int recordId);

		// Token: 0x060005BA RID: 1466
		bool TryRemoveRecord(IUpdateContext updateContext, IDataRecord record, out int rid);

		// Token: 0x060005BB RID: 1467
		void EndUpdate(IUpdateContext context);
	}
}
