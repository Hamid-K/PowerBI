using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000074 RID: 116
	public interface IRowsetConsumer
	{
		// Token: 0x060004C4 RID: 1220
		void RequestRowsets(IRowsetDistributor rowsetDistributor);

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004C5 RID: 1221
		IList<IRowsetSink> RowsetSinks { get; }
	}
}
