using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000018 RID: 24
	public struct Int64Interlocked : IInterlocked<long>
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000264C File Offset: 0x0000084C
		public long CompareExchange(ref long location1, long value, long comparand)
		{
			return Interlocked.CompareExchange(ref location1, value, comparand);
		}
	}
}
