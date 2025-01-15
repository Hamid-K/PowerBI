using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D56 RID: 3414
	internal class IntersectSet : Set
	{
		// Token: 0x06005C46 RID: 23622 RVA: 0x00140370 File Offset: 0x0013E570
		public static Set New(Set set1, Set set2)
		{
			Dimensionality dimensionality = set1.Dimensionality.Union(set2.Dimensionality);
			return new IntersectSet(set1.ExpandTo(dimensionality), set2.ExpandTo(dimensionality));
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x001403A2 File Offset: 0x0013E5A2
		private IntersectSet(Set set1, Set set2)
		{
			this.set1 = set1;
			this.set2 = set2;
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x00002461 File Offset: 0x00000661
		public override SetKind Kind
		{
			get
			{
				return SetKind.Intersect;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06005C49 RID: 23625 RVA: 0x001403B8 File Offset: 0x0013E5B8
		public override double Cardinality
		{
			get
			{
				return Math.Min(this.set1.Cardinality, this.set2.Cardinality);
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x001403D5 File Offset: 0x0013E5D5
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = this.set1.Dimensionality.Union(this.set2.Dimensionality);
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06005C4B RID: 23627 RVA: 0x00140406 File Offset: 0x0013E606
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set1.HasMeasureFilter || this.set2.HasMeasureFilter;
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00140422 File Offset: 0x0013E622
		public Set Set1
		{
			get
			{
				return this.set1;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x0014042A File Offset: 0x0013E62A
		public Set Set2
		{
			get
			{
				return this.set2;
			}
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x00140432 File Offset: 0x0013E632
		public override IEnumerable<Set> GetSubsets()
		{
			foreach (Set set in this.set1.GetSubsets())
			{
				yield return set;
			}
			IEnumerator<Set> enumerator = null;
			foreach (Set set2 in this.set2.GetSubsets())
			{
				yield return set2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x00140444 File Offset: 0x0013E644
		public override Set IntersectAsLeft(Set other)
		{
			bool flag = this.set1.Dimensionality.HasAllHierarchiesOf(other.Dimensionality);
			bool flag2 = this.set2.Dimensionality.HasAllHierarchiesOf(other.Dimensionality);
			double cardinality = this.set1.Cardinality;
			double cardinality2 = this.set2.Cardinality;
			double cardinality3 = other.Cardinality;
			if (flag && (!flag2 || (cardinality <= cardinality2 && cardinality3 <= cardinality2)))
			{
				return new IntersectSet(this.set1.DescendTo(other.Dimensionality).Intersect(other), this.set2.DescendTo(other.Dimensionality));
			}
			if (flag2 && (!flag || (cardinality2 <= cardinality && cardinality3 <= cardinality)))
			{
				return new IntersectSet(this.set1.DescendTo(other.Dimensionality), this.set2.DescendTo(other.Dimensionality).Intersect(other));
			}
			return base.IntersectAsLeft(other);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00140520 File Offset: 0x0013E720
		public override Set IntersectAsRight(Set other)
		{
			bool flag = this.set1.Dimensionality.HasAllHierarchiesOf(other.Dimensionality);
			bool flag2 = this.set2.Dimensionality.HasAllHierarchiesOf(other.Dimensionality);
			double cardinality = this.set1.Cardinality;
			double cardinality2 = this.set2.Cardinality;
			double cardinality3 = other.Cardinality;
			if (flag && (!flag2 || (cardinality <= cardinality2 && cardinality3 <= cardinality2)))
			{
				return new IntersectSet(this.set1.DescendTo(other.Dimensionality).Intersect(other), this.set2.DescendTo(other.Dimensionality));
			}
			if (flag2 && (!flag || (cardinality2 <= cardinality && cardinality3 <= cardinality)))
			{
				return new IntersectSet(this.set1.DescendTo(other.Dimensionality), this.set2.DescendTo(other.Dimensionality).Intersect(other));
			}
			return base.IntersectAsRight(other);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x001405FC File Offset: 0x0013E7FC
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return new IntersectSet(this.set1.OrderHierarchies(dimensionality), this.set2.OrderHierarchies(dimensionality));
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x0014061B File Offset: 0x0013E81B
		public override Set EnsureUniqueHierarchyMembers()
		{
			return new IntersectSet(this.set1.EnsureUniqueHierarchyMembers(), this.set2.EnsureUniqueHierarchyMembers());
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x00140638 File Offset: 0x0013E838
		public override Set Unordered()
		{
			return new IntersectSet(this.set1.Unordered(), this.set2.Unordered());
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x00140655 File Offset: 0x0013E855
		public override Set NewScope(string scope)
		{
			return new IntersectSet(this.set1.NewScope(scope), this.set2.NewScope(scope));
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x00140674 File Offset: 0x0013E874
		public bool Equals(IntersectSet other)
		{
			return other != null && ((this.set1.Equals(other.set1) && this.set2.Equals(other.set2)) || (this.set1.Equals(other.set2) && this.set2.Equals(other.set1)));
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x001406D4 File Offset: 0x0013E8D4
		public override bool Equals(object other)
		{
			return this.Equals(other as IntersectSet);
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x001406E2 File Offset: 0x0013E8E2
		public override int GetHashCode()
		{
			return this.set1.GetHashCode() + this.set2.GetHashCode();
		}

		// Token: 0x04003322 RID: 13090
		private readonly Set set1;

		// Token: 0x04003323 RID: 13091
		private readonly Set set2;

		// Token: 0x04003324 RID: 13092
		private Dimensionality dimensionality;
	}
}
