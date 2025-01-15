using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000065 RID: 101
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000CA23 File Offset: 0x0000AC23
		internal ListWrapperCollection()
			: this(new List<T>())
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000CA30 File Offset: 0x0000AC30
		internal ListWrapperCollection(List<T> list)
			: base(list)
		{
			this._items = list;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000CA40 File Offset: 0x0000AC40
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x040000C1 RID: 193
		private readonly List<T> _items;
	}
}
