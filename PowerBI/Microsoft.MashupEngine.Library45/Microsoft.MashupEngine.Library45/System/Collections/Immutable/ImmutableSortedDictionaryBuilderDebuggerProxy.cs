using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020B7 RID: 8375
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060118D8 RID: 71896 RVA: 0x003C14DD File Offset: 0x003BF6DD
		public ImmutableSortedDictionaryBuilderDebuggerProxy(ImmutableSortedDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17002F0B RID: 12043
		// (get) Token: 0x060118D9 RID: 71897 RVA: 0x003C14F7 File Offset: 0x003BF6F7
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

		// Token: 0x04006973 RID: 26995
		private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04006974 RID: 26996
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
