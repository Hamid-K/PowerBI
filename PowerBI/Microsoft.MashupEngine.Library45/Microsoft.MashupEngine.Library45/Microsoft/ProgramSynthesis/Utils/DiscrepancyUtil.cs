using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000483 RID: 1155
	public static class DiscrepancyUtil
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0004EE5C File Offset: 0x0004D05C
		public static Optional<Discrepancy<T>> FirstDiscrepancy<T>(IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> equalityComparer = null)
		{
			equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
			IEnumerable<Optional<T>> enumerable = left.Select((T x) => x.Some<T>()).Concat(new Optional<T>[] { Optional<T>.Nothing });
			IEnumerable<Optional<T>> enumerable2 = right.Select((T x) => x.Some<T>()).Concat(new Optional<T>[] { Optional<T>.Nothing });
			return from discrepancy in enumerable.Zip(enumerable2, (Optional<T> leftValue, Optional<T> rightValue) => new { leftValue, rightValue }).Select((values, int index) => new { index, values }).FirstOrDefault(t => (t.values.leftValue.HasValue || t.values.rightValue.HasValue) && (t.values.leftValue.HasValue != t.values.rightValue.HasValue || !equalityComparer.Equals(t.values.leftValue.Value, t.values.rightValue.Value)))
					.SomeIfNotNull()
				select new Discrepancy<T>(discrepancy.index, discrepancy.values.leftValue, discrepancy.values.rightValue);
		}
	}
}
