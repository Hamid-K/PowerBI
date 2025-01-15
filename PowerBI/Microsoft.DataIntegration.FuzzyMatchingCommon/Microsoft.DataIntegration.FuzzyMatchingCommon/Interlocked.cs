using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000016 RID: 22
	public struct Interlocked<T> : IInterlocked<T> where T : class
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002638 File Offset: 0x00000838
		public T CompareExchange(ref T location1, T value, T comparand)
		{
			return Interlocked.CompareExchange<T>(ref location1, value, comparand);
		}
	}
}
