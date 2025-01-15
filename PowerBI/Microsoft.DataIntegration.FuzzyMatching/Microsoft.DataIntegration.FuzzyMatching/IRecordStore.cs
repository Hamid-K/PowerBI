using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000066 RID: 102
	internal interface IRecordStore : IMemoryLimit, IMemoryUsage, ISessionable
	{
		// Token: 0x06000420 RID: 1056
		IUpdateContext BeginUpdate(DataTable schemaTable);

		// Token: 0x06000421 RID: 1057
		void EndUpdate(IUpdateContext updateContext);

		// Token: 0x06000422 RID: 1058
		int AddRecord(IDataRecord record);

		// Token: 0x06000423 RID: 1059
		void AddRecord(int rid, object[] values);

		// Token: 0x06000424 RID: 1060
		bool TryGetRecord(ISession session, int rid, ref object[] values);

		// Token: 0x06000425 RID: 1061
		bool TryRemoveRecord(IDataRecord record, out int rid);
	}
}
