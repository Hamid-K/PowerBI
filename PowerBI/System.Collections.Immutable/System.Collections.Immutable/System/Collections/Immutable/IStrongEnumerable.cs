using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000026 RID: 38
	internal interface IStrongEnumerable<[Nullable(2)] out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x060000FA RID: 250
		TEnumerator GetEnumerator();
	}
}
