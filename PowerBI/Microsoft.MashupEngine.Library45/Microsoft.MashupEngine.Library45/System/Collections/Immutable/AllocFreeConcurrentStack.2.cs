using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002065 RID: 8293
	internal static class AllocFreeConcurrentStack
	{
		// Token: 0x040068A2 RID: 26786
		[Nullable(new byte[] { 2, 1, 1 })]
		[ThreadStatic]
		internal static Dictionary<Type, object> t_stacks;
	}
}
