using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableEnumerableDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00006237 File Offset: 0x00004437
		public ImmutableEnumerableDebuggerProxy(IEnumerable<T> enumerable)
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006254 File Offset: 0x00004454
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

		// Token: 0x04000024 RID: 36
		private readonly IEnumerable<T> _enumerable;

		// Token: 0x04000025 RID: 37
		private T[] _cachedContents;
	}
}
