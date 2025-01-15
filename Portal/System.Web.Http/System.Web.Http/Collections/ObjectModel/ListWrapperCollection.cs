using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000187 RID: 391
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x0001A0FB File Offset: 0x000182FB
		internal ListWrapperCollection()
			: this(new List<T>())
		{
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001A108 File Offset: 0x00018308
		internal ListWrapperCollection(List<T> list)
			: base(list)
		{
			this._items = list;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001A118 File Offset: 0x00018318
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x040002B2 RID: 690
		private readonly List<T> _items;
	}
}
