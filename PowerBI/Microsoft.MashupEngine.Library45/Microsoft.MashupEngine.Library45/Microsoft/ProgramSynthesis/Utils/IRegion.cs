using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200049E RID: 1182
	public interface IRegion<T> : IComparable<T> where T : IRegion<T>
	{
		// Token: 0x06001A8D RID: 6797
		bool Contains(T other);

		// Token: 0x06001A8E RID: 6798
		bool IntersectNonEmpty(T other);

		// Token: 0x06001A8F RID: 6799
		bool IsBefore(T other);

		// Token: 0x06001A90 RID: 6800
		T ClipBefore(T other);
	}
}
