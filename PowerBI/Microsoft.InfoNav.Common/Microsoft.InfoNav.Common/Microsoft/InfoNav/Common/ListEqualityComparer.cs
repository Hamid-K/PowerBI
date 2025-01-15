using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000060 RID: 96
	[ImmutableObject(true)]
	internal sealed class ListEqualityComparer<T> : IEqualityComparer<IList<T>>, IEqualityComparer<IReadOnlyList<T>>
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00009C02 File Offset: 0x00007E02
		internal ListEqualityComparer(IEqualityComparer<T> itemComparer)
		{
			this._itemComparer = itemComparer;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009C14 File Offset: 0x00007E14
		public bool Equals(IList<T> x, IList<T> y)
		{
			bool? flag = Util.AreEqual<IList<T>>(x, y);
			if (flag == null)
			{
				return x.SequenceEqual(y, this._itemComparer);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009C48 File Offset: 0x00007E48
		public bool Equals(IReadOnlyList<T> x, IReadOnlyList<T> y)
		{
			bool? flag = Util.AreEqual<IReadOnlyList<T>>(x, y);
			if (flag == null)
			{
				return x.SequenceEqualReadOnly(y, this._itemComparer);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009C7B File Offset: 0x00007E7B
		public int GetHashCode(IList<T> list)
		{
			return Hashing.CombineHash<T>(list, this._itemComparer);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00009C89 File Offset: 0x00007E89
		public int GetHashCode(IReadOnlyList<T> list)
		{
			return Hashing.CombineHashReadOnly<T>(list, this._itemComparer);
		}

		// Token: 0x040000C5 RID: 197
		internal static readonly ListEqualityComparer<T> Default = new ListEqualityComparer<T>(EqualityComparer<T>.Default);

		// Token: 0x040000C6 RID: 198
		private readonly IEqualityComparer<T> _itemComparer;
	}
}
