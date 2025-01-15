using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200009C RID: 156
	public interface IMultiDimSignatureGenerator : IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000629 RID: 1577
		int NumHashtables { get; }

		// Token: 0x0600062A RID: 1578
		void Reset(int signIdx);
	}
}
