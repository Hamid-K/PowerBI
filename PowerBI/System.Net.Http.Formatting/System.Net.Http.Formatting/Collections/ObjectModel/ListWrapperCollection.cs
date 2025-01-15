using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000060 RID: 96
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000CB0F File Offset: 0x0000AD0F
		internal ListWrapperCollection()
			: this(new List<T>())
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		internal ListWrapperCollection(List<T> list)
			: base(list)
		{
			this._items = list;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly List<T> _items;
	}
}
