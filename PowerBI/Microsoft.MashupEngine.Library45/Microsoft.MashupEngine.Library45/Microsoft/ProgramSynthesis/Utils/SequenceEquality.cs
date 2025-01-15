using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200050D RID: 1293
	public class SequenceEquality<T> : IEqualityComparer<IEnumerable<T>>
	{
		// Token: 0x06001CC7 RID: 7367 RVA: 0x00002130 File Offset: 0x00000330
		private SequenceEquality()
		{
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00055C43 File Offset: 0x00053E43
		public static SequenceEquality<T> Comparer
		{
			get
			{
				return SequenceEquality<T>.Lazy.Value;
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00055C4F File Offset: 0x00053E4F
		public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
		{
			return (x == null && y == null) || (x != null && y != null && x.SequenceEqual(y));
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00055C68 File Offset: 0x00053E68
		public int GetHashCode(IEnumerable<T> obj)
		{
			return obj.OrderDependentHashCode(EqualityComparer<T>.Default);
		}

		// Token: 0x04000E05 RID: 3589
		private static readonly Lazy<SequenceEquality<T>> Lazy = new Lazy<SequenceEquality<T>>(() => new SequenceEquality<T>());
	}
}
