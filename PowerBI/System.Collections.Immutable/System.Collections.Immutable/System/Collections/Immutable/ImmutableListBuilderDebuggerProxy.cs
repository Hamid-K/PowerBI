using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableListBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000075AE File Offset: 0x000057AE
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000075C8 File Offset: 0x000057C8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._cachedContents == null)
				{
					this._cachedContents = this._list.ToArray(this._list.Count);
				}
				return this._cachedContents;
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04000029 RID: 41
		private T[] _cachedContents;
	}
}
