using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028F RID: 655
	public sealed class ReadOnlyOrderedHashSet<T> : ReadOnlyHashSetBase<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001BEF RID: 7151 RVA: 0x0004DDB9 File Offset: 0x0004BFB9
		private ReadOnlyOrderedHashSet(HashSet<T> underlyingSet, IList<T> orderedItems)
			: base(underlyingSet)
		{
			this._orderedItems = orderedItems;
		}

		// Token: 0x170007C4 RID: 1988
		public T this[int index]
		{
			get
			{
				return this._orderedItems[index];
			}
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0004DDD8 File Offset: 0x0004BFD8
		public static ReadOnlyOrderedHashSet<T> CopyFrom(IEnumerable<T> items)
		{
			IList<T> list = items.Evaluate<T>();
			HashSet<T> hashSet = new HashSet<T>(list);
			if (list.Count != hashSet.Count)
			{
				IEnumerable<T> enumerable = list;
				list = new List<T>(hashSet.Count);
				Dictionary<T, bool> dictionary = new Dictionary<T, bool>(hashSet.Count);
				foreach (T t in enumerable)
				{
					if (!dictionary.ContainsKey(t))
					{
						dictionary.Add(t, true);
						list.Add(t);
					}
				}
			}
			return new ReadOnlyOrderedHashSet<T>(hashSet, list);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0004DE70 File Offset: 0x0004C070
		public override IEnumerator<T> GetEnumerator()
		{
			return this._orderedItems.GetEnumerator();
		}

		// Token: 0x04000F32 RID: 3890
		private readonly IList<T> _orderedItems;
	}
}
