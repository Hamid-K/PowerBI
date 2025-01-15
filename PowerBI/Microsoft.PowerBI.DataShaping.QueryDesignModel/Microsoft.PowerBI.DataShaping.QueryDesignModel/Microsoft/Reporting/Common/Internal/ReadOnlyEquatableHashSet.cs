using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028E RID: 654
	public sealed class ReadOnlyEquatableHashSet<T> : ReadOnlyHashSetBase<T>, IEquatable<ReadOnlyEquatableHashSet<T>> where T : IEquatable<T>
	{
		// Token: 0x06001BEA RID: 7146 RVA: 0x0004DD5B File Offset: 0x0004BF5B
		private ReadOnlyEquatableHashSet(HashSet<T> items)
			: base(items)
		{
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0004DD64 File Offset: 0x0004BF64
		public static ReadOnlyEquatableHashSet<T> CopyFrom(IEnumerable<T> items)
		{
			return new ReadOnlyEquatableHashSet<T>(new HashSet<T>(items));
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0004DD71 File Offset: 0x0004BF71
		public bool Equals(ReadOnlyEquatableHashSet<T> other)
		{
			return other != null && base.SetEquals(other);
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0004DD7F File Offset: 0x0004BF7F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ReadOnlyEquatableHashSet<T>);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0004DD8D File Offset: 0x0004BF8D
		public override int GetHashCode()
		{
			return Hashing.CombineHashUnordered(this.Select((T item) => item.GetHashCode()));
		}
	}
}
