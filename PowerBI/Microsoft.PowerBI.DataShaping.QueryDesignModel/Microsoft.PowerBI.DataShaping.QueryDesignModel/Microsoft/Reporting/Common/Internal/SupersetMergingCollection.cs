using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000290 RID: 656
	internal sealed class SupersetMergingCollection<TSet, T> : ReadOnlyCollection<TSet> where TSet : ISet<T>
	{
		// Token: 0x06001BF3 RID: 7155 RVA: 0x0004DE7D File Offset: 0x0004C07D
		private SupersetMergingCollection(IList<TSet> list)
			: base(list)
		{
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0004DE88 File Offset: 0x0004C088
		internal static SupersetMergingCollection<TSet, T> CopyFrom(IEnumerable<TSet> source)
		{
			List<TSet> list = new List<TSet>(source);
			bool[] array = new bool[list.Count];
			for (int i = 0; i < list.Count - 1; i++)
			{
				TSet tset = list[i];
				for (int j = i + 1; j < list.Count; j++)
				{
					if (!array[j])
					{
						TSet tset2 = list[j];
						if (tset.IsSupersetOf(tset2))
						{
							array[j] = true;
						}
						else if (tset2.IsSupersetOf(tset))
						{
							array[i] = true;
						}
					}
				}
			}
			for (int k = list.Count - 1; k >= 0; k--)
			{
				if (array[k])
				{
					list.RemoveAt(k);
				}
			}
			return new SupersetMergingCollection<TSet, T>(list);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0004DF4C File Offset: 0x0004C14C
		internal IEnumerable<TSet> GetSupersetsOf(TSet set)
		{
			return this.Where((TSet s) => s.IsSupersetOf(set));
		}
	}
}
