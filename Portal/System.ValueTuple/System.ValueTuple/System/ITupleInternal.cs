using System;
using System.Collections;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal interface ITupleInternal
	{
		// Token: 0x06000042 RID: 66
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000043 RID: 67
		int Size { get; }

		// Token: 0x06000044 RID: 68
		string ToStringEnd();
	}
}
