using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000095 RID: 149
	public interface ISegmentAllocator<T>
	{
		// Token: 0x06000669 RID: 1641
		ArraySegment<T> New(int length);

		// Token: 0x0600066A RID: 1642
		void Resize(ref ArraySegment<T> segment, int newLength);
	}
}
