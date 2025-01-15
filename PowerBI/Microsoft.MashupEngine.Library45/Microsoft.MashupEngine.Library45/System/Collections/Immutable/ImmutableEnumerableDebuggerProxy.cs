using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020A0 RID: 8352
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableEnumerableDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060116AB RID: 71339 RVA: 0x003BBFAF File Offset: 0x003BA1AF
		public ImmutableEnumerableDebuggerProxy(IEnumerable<T> enumerable)
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17002EA1 RID: 11937
		// (get) Token: 0x060116AC RID: 71340 RVA: 0x003BBFCC File Offset: 0x003BA1CC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				T[] array;
				if ((array = this._cachedContents) == null)
				{
					array = (this._cachedContents = this._enumerable.ToArray<T>());
				}
				return array;
			}
		}

		// Token: 0x0400692A RID: 26922
		private readonly IEnumerable<T> _enumerable;

		// Token: 0x0400692B RID: 26923
		private T[] _cachedContents;
	}
}
