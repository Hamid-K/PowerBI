using System;
using System.Data.SqlClient;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000085 RID: 133
	internal interface ISqlInvertedIndexReader
	{
		// Token: 0x0600054B RID: 1355
		ISession CreateReadSession(SqlConnection connection);

		// Token: 0x0600054C RID: 1356
		bool TryGetSignatureRidList(ISession _session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList);
	}
}
