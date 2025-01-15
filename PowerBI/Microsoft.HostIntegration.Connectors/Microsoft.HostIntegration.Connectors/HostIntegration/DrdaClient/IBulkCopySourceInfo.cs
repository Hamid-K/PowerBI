using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009AF RID: 2479
	internal interface IBulkCopySourceInfo
	{
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06004CB7 RID: 19639
		int FieldCount { get; }

		// Token: 0x06004CB8 RID: 19640
		Type GetFieldType(int index);

		// Token: 0x06004CB9 RID: 19641
		string GetFieldName(int index);
	}
}
