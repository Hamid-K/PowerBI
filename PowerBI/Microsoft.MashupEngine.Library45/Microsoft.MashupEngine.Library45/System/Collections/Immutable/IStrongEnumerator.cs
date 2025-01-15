using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002081 RID: 8321
	[NullableContext(1)]
	internal interface IStrongEnumerator<[Nullable(2)] T>
	{
		// Token: 0x17002E3F RID: 11839
		// (get) Token: 0x060114E9 RID: 70889
		T Current { get; }

		// Token: 0x060114EA RID: 70890
		bool MoveNext();
	}
}
