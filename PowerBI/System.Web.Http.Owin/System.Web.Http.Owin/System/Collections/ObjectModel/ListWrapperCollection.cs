using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000018 RID: 24
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000039A8 File Offset: 0x00001BA8
		internal ListWrapperCollection()
			: this(new List<T>())
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000039B5 File Offset: 0x00001BB5
		internal ListWrapperCollection(List<T> list)
			: base(list)
		{
			this._items = list;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000039C5 File Offset: 0x00001BC5
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly List<T> _items;
	}
}
