using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x0200003D RID: 61
	internal sealed class EnumerableDebugView<TKey, TValue>
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00009069 File Offset: 0x00007269
		public EnumerableDebugView(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
		{
			this._enumerable = enumerable;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009078 File Offset: 0x00007278
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<TKey, TValue>[] Items
		{
			get
			{
				return this._enumerable.ToArray<KeyValuePair<TKey, TValue>>();
			}
		}

		// Token: 0x04000098 RID: 152
		private readonly IEnumerable<KeyValuePair<TKey, TValue>> _enumerable;
	}
}
