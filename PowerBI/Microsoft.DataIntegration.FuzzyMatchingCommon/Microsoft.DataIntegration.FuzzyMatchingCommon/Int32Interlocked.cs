using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000017 RID: 23
	public struct Int32Interlocked : IInterlocked<int>
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002642 File Offset: 0x00000842
		public int CompareExchange(ref int location1, int value, int comparand)
		{
			return Interlocked.CompareExchange(ref location1, value, comparand);
		}
	}
}
