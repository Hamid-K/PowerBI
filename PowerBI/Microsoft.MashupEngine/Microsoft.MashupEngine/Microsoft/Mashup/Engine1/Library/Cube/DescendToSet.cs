using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D52 RID: 3410
	internal class DescendToSet : Set
	{
		// Token: 0x06005C10 RID: 23568 RVA: 0x0013FE6F File Offset: 0x0013E06F
		public DescendToSet(Set set, Dimensionality dimensionality)
		{
			this.set = set;
			this.dimensionality = dimensionality.IntersectHierarchies(this.set.Dimensionality);
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x00002139 File Offset: 0x00000339
		public override SetKind Kind
		{
			get
			{
				return SetKind.DescendTo;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06005C12 RID: 23570 RVA: 0x0013FE98 File Offset: 0x0013E098
		public override double Cardinality
		{
			get
			{
				double num = this.set.Cardinality;
				foreach (ICubeHierarchy cubeHierarchy in this.set.Dimensionality.Hierarchies)
				{
					CubeLevelRange levelRange = this.set.Dimensionality.GetLevelRange(cubeHierarchy);
					int num2 = this.dimensionality.GetLevelRange(cubeHierarchy).Fine.Number - levelRange.Fine.Number;
					num *= Math.Pow(10.0, (double)num2);
				}
				return num;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x0013FF40 File Offset: 0x0013E140
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.dimensionality;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06005C14 RID: 23572 RVA: 0x0013FF48 File Offset: 0x0013E148
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x0013FF55 File Offset: 0x0013E155
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x0013FF5D File Offset: 0x0013E15D
		public override IEnumerable<Set> GetSubsets()
		{
			return this.set.GetSubsets();
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x0013FF6A File Offset: 0x0013E16A
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			return this.set.DescendTo(this.dimensionality.Union(newDimensionality));
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x0013FF83 File Offset: 0x0013E183
		public override Set IntersectAsLeft(Set other)
		{
			return this.set.Intersect(other).DescendTo(this.dimensionality);
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x0013FF83 File Offset: 0x0013E183
		public override Set IntersectAsRight(Set other)
		{
			return this.set.Intersect(other).DescendTo(this.dimensionality);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x0013FF9C File Offset: 0x0013E19C
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this.set.EnsureUniqueHierarchyMembers().DescendTo(this.dimensionality);
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x0013FFB4 File Offset: 0x0013E1B4
		public override Set Unordered()
		{
			return this.set.Unordered().DescendTo(this.dimensionality);
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x0013FFCC File Offset: 0x0013E1CC
		public override Set NewScope(string scope)
		{
			return this.set.NewScope(scope).DescendTo(this.dimensionality.NewScope(scope));
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x0013FFEB File Offset: 0x0013E1EB
		public bool Equals(DescendToSet other)
		{
			return other != null && this.set.Equals(other.set) && this.dimensionality.Equals(other.dimensionality);
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x00140016 File Offset: 0x0013E216
		public override bool Equals(object other)
		{
			return this.Equals(other as DescendToSet);
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x00140024 File Offset: 0x0013E224
		public override int GetHashCode()
		{
			return this.set.GetHashCode() + 37 * this.dimensionality.GetHashCode();
		}

		// Token: 0x04003319 RID: 13081
		private readonly Set set;

		// Token: 0x0400331A RID: 13082
		private readonly Dimensionality dimensionality;
	}
}
