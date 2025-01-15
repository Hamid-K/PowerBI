using System;
using System.Collections;

namespace System
{
	// Token: 0x020000B0 RID: 176
	internal interface ITupleInternal
	{
		// Token: 0x06000578 RID: 1400
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000579 RID: 1401
		int Size { get; }

		// Token: 0x0600057A RID: 1402
		string ToStringEnd();
	}
}
