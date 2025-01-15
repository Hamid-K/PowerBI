using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000097 RID: 151
	public interface IReadWriteArraySegment<T>
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600066F RID: 1647
		// (set) Token: 0x06000670 RID: 1648
		T[] Array { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000671 RID: 1649
		// (set) Token: 0x06000672 RID: 1650
		int Offset { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000673 RID: 1651
		// (set) Token: 0x06000674 RID: 1652
		int Count { get; set; }

		// Token: 0x17000102 RID: 258
		T this[int i] { get; set; }
	}
}
