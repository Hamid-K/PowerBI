using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000071 RID: 113
	public interface IRecordUpdate
	{
		// Token: 0x060004BB RID: 1211
		IUpdateContext BeginUpdate(DataTable schemaTable);

		// Token: 0x060004BC RID: 1212
		void AddRecord(IUpdateContext context, IDataRecord r);

		// Token: 0x060004BD RID: 1213
		void RemoveRecord(IUpdateContext context, IDataRecord r);

		// Token: 0x060004BE RID: 1214
		void EndUpdate(IUpdateContext context);
	}
}
