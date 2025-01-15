using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005F RID: 95
	[ImmutableObject(true)]
	internal sealed class ListComparer<T> : IComparer<IList<T>>
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00009B8B File Offset: 0x00007D8B
		internal ListComparer(IComparer<T> itemComparer)
		{
			this._itemComparer = itemComparer;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00009B9C File Offset: 0x00007D9C
		public int Compare(IList<T> x, IList<T> y)
		{
			int count = x.Count;
			int count2 = y.Count;
			int num = count.CompareTo(count2);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < count; i++)
			{
				num = this._itemComparer.Compare(x[i], y[i]);
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x040000C3 RID: 195
		internal static readonly ListComparer<T> Default = new ListComparer<T>(Comparer<T>.Default);

		// Token: 0x040000C4 RID: 196
		private readonly IComparer<T> _itemComparer;
	}
}
