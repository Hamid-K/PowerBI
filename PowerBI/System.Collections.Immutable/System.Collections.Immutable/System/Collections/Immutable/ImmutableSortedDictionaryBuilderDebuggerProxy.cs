using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000824A File Offset: 0x0000644A
		public ImmutableSortedDictionaryBuilderDebuggerProxy(ImmutableSortedDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00008264 File Offset: 0x00006464
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<TKey, TValue>[] Contents
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get
			{
				if (this._contents == null)
				{
					this._contents = this._map.ToArray(this._map.Count);
				}
				return this._contents;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000034 RID: 52
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
