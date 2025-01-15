using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000053 RID: 83
	public interface IDataRecordProvider<T>
	{
		// Token: 0x060002C2 RID: 706
		DataTable GetSchemaTable();

		// Token: 0x060002C3 RID: 707
		IDataRecord GetRecord(T item);
	}
}
