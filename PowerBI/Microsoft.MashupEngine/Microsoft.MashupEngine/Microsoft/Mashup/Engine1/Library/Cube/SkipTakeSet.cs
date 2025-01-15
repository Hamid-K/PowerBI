using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D64 RID: 3428
	internal class SkipTakeSet : Set
	{
		// Token: 0x06005CF7 RID: 23799 RVA: 0x00142296 File Offset: 0x00140496
		public static Set New(Set set, RowRange rowRange)
		{
			if (rowRange.IsAll)
			{
				return set;
			}
			return new SkipTakeSet(set, rowRange);
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001422AA File Offset: 0x001404AA
		protected SkipTakeSet(Set set, RowRange rowRange)
		{
			this.set = set;
			this.rowRange = rowRange;
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06005CF9 RID: 23801 RVA: 0x001422C0 File Offset: 0x001404C0
		public override SetKind Kind
		{
			get
			{
				return SetKind.SkipTake;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x001422C4 File Offset: 0x001404C4
		public override double Cardinality
		{
			get
			{
				double num = this.set.Cardinality;
				if (!this.rowRange.SkipCount.IsInfinite)
				{
					num = Math.Max(num - (double)this.rowRange.SkipCount.Value, 0.0);
				}
				if (!this.rowRange.TakeCount.IsInfinite)
				{
					num = Math.Min(num, (double)this.rowRange.TakeCount.Value);
				}
				return num;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x00142354 File Offset: 0x00140554
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.set.Dimensionality;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x00142361 File Offset: 0x00140561
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06005CFD RID: 23805 RVA: 0x00142370 File Offset: 0x00140570
		public override RowCount TakeCount
		{
			get
			{
				return RowRange.All.Take(this.set.TakeCount).Skip(this.rowRange.SkipCount).Take(this.rowRange.TakeCount)
					.TakeCount;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x001423C9 File Offset: 0x001405C9
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06005CFF RID: 23807 RVA: 0x001423D1 File Offset: 0x001405D1
		public RowRange RowRange
		{
			get
			{
				return this.rowRange;
			}
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001423D9 File Offset: 0x001405D9
		public override IEnumerable<Set> GetSubsets()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001423E9 File Offset: 0x001405E9
		public override Set EnsureUniqueHierarchyMembers()
		{
			return new SkipTakeSet(this.set.EnsureUniqueHierarchyMembers(), this.rowRange);
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x00142404 File Offset: 0x00140604
		public override Set SkipTake(RowRange rowRange)
		{
			return new SkipTakeSet(this.set, this.rowRange.Skip(rowRange.SkipCount).Take(rowRange.TakeCount));
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set Unordered()
		{
			return this;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x00142440 File Offset: 0x00140640
		public override Set NewScope(string scope)
		{
			return new SkipTakeSet(this.set.NewScope(scope), this.rowRange);
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x00142459 File Offset: 0x00140659
		public bool Equals(SkipTakeSet other)
		{
			return other != null && this.set.Equals(other.Set) && SkipTakeSet.Equals(this.rowRange, other.rowRange);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x00142484 File Offset: 0x00140684
		public override bool Equals(object other)
		{
			return this.Equals(other as SkipTakeSet);
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00142492 File Offset: 0x00140692
		public override int GetHashCode()
		{
			return this.set.GetHashCode() + SkipTakeSet.GetHashCode(this.rowRange);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x001424AB File Offset: 0x001406AB
		private static bool Equals(RowRange rowRange1, RowRange rowRange2)
		{
			return SkipTakeSet.Equals(rowRange1.TakeCount, rowRange2.TakeCount) && SkipTakeSet.Equals(rowRange1.SkipCount, rowRange2.SkipCount);
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x001424D7 File Offset: 0x001406D7
		private static bool Equals(RowCount rowCount1, RowCount rowCount2)
		{
			return (rowCount1.IsInfinite && rowCount2.IsInfinite) || rowCount1.Value == rowCount2.Value;
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x001424FD File Offset: 0x001406FD
		private static int GetHashCode(RowRange rowRange)
		{
			return SkipTakeSet.GetHashCode(rowRange.TakeCount) + 9123 * SkipTakeSet.GetHashCode(rowRange.SkipCount);
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x0014251E File Offset: 0x0014071E
		private static int GetHashCode(RowCount rowCount)
		{
			if (!rowCount.IsInfinite)
			{
				return (int)rowCount.Value;
			}
			return -1;
		}

		// Token: 0x04003352 RID: 13138
		private readonly Set set;

		// Token: 0x04003353 RID: 13139
		private readonly RowRange rowRange;
	}
}
