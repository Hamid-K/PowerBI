using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060001DF RID: 479 RVA: 0x000061E8 File Offset: 0x000043E8
		public ImmutableDictionaryBuilderDebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006202 File Offset: 0x00004402
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

		// Token: 0x04000022 RID: 34
		private readonly ImmutableDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000023 RID: 35
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
