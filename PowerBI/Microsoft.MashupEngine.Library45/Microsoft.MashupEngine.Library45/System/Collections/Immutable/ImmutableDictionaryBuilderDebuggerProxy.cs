using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200209E RID: 8350
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060116A8 RID: 71336 RVA: 0x003BBF60 File Offset: 0x003BA160
		public ImmutableDictionaryBuilderDebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17002EA0 RID: 11936
		// (get) Token: 0x060116A9 RID: 71337 RVA: 0x003BBF7A File Offset: 0x003BA17A
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

		// Token: 0x04006928 RID: 26920
		private readonly ImmutableDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04006929 RID: 26921
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
