using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002080 RID: 8320
	internal interface IStrongEnumerable<[Nullable(2)] out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x060114E8 RID: 70888
		TEnumerator GetEnumerator();
	}
}
