using System;
using System.Collections;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4
{
	// Token: 0x02000037 RID: 55
	internal interface ITuple
	{
		// Token: 0x0600019C RID: 412
		string ToString(StringBuilder sb);

		// Token: 0x0600019D RID: 413
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600019E RID: 414
		int Size { get; }
	}
}
