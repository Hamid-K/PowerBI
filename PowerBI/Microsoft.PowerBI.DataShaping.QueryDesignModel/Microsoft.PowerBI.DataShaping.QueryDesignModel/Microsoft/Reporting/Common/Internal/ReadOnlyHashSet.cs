using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028D RID: 653
	public sealed class ReadOnlyHashSet<T> : ReadOnlyHashSetBase<T>
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x0004DD17 File Offset: 0x0004BF17
		private ReadOnlyHashSet(HashSet<T> underlyingSet)
			: base(underlyingSet)
		{
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0004DD20 File Offset: 0x0004BF20
		public static ReadOnlyHashSet<T> Empty
		{
			get
			{
				return ReadOnlyHashSet<T>.EmptyInstance;
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0004DD27 File Offset: 0x0004BF27
		public static ReadOnlyHashSet<T> CopyFrom(IEnumerable<T> items)
		{
			return ReadOnlyHashSet<T>.Wrap(new HashSet<T>(items));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0004DD34 File Offset: 0x0004BF34
		public static ReadOnlyHashSet<T> CopyFrom(IEnumerable<T> items, IEqualityComparer<T> comparer)
		{
			return ReadOnlyHashSet<T>.Wrap(new HashSet<T>(items, comparer));
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0004DD42 File Offset: 0x0004BF42
		public static ReadOnlyHashSet<T> Wrap(HashSet<T> underlyingSet)
		{
			return new ReadOnlyHashSet<T>(underlyingSet);
		}

		// Token: 0x04000F31 RID: 3889
		private static readonly ReadOnlyHashSet<T> EmptyInstance = ReadOnlyHashSet<T>.Wrap(new HashSet<T>());
	}
}
