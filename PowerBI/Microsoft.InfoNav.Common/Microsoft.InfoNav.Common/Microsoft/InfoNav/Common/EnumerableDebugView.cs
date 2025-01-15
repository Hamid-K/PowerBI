using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200003F RID: 63
	internal sealed class EnumerableDebugView<T>
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00008658 File Offset: 0x00006858
		internal EnumerableDebugView(IEnumerable<T> items)
		{
			this._items = items.Evaluate<T>();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000866C File Offset: 0x0000686C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal T[] Items
		{
			get
			{
				T[] array = new T[this._items.Count];
				this._items.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400009D RID: 157
		private readonly IList<T> _items;
	}
}
