using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200008E RID: 142
	public interface IInvertedIndexUpdate
	{
		// Token: 0x060005BC RID: 1468
		IUpdateContext BeginUpdate(DataTable schemaTable);

		// Token: 0x060005BD RID: 1469
		void AddRidSignatures(IUpdateContext updateContext, int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures);

		// Token: 0x060005BE RID: 1470
		void RemoveRidSignatures(IUpdateContext updateContext, int entry, int lookupId, int hashTableIndex, IEnumerable<int> signatures);

		// Token: 0x060005BF RID: 1471
		void EndUpdate(IUpdateContext context);
	}
}
