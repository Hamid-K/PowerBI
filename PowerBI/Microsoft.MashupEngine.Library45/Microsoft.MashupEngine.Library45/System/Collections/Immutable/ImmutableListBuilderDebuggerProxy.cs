using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020AB RID: 8363
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableListBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060117F4 RID: 71668 RVA: 0x003BF346 File Offset: 0x003BD546
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17002ED0 RID: 11984
		// (get) Token: 0x060117F5 RID: 71669 RVA: 0x003BF360 File Offset: 0x003BD560
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

		// Token: 0x04006947 RID: 26951
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04006948 RID: 26952
		private T[] _cachedContents;
	}
}
