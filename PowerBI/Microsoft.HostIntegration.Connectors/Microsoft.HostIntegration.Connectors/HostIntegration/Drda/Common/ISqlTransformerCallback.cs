using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200081E RID: 2078
	public interface ISqlTransformerCallback
	{
		// Token: 0x060041C1 RID: 16833
		Tuple<string, string, bool, bool>[] GetTableColumns(string tableName);
	}
}
