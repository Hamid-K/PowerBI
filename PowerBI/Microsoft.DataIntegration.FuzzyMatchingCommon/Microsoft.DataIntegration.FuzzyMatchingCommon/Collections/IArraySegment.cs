using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000096 RID: 150
	public interface IArraySegment<T>
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600066B RID: 1643
		T[] Array { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600066C RID: 1644
		int Offset { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600066D RID: 1645
		int Count { get; }

		// Token: 0x170000FE RID: 254
		T this[int i] { get; }
	}
}
