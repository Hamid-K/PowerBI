using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D4E RID: 3406
	internal class LevelSet : Set
	{
		// Token: 0x06005BE0 RID: 23520 RVA: 0x0013FAAB File Offset: 0x0013DCAB
		public LevelSet(ICubeLevel level)
		{
			this.level = level;
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00002475 File Offset: 0x00000675
		public override SetKind Kind
		{
			get
			{
				return SetKind.Level;
			}
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x0013FABA File Offset: 0x0013DCBA
		public override double Cardinality
		{
			get
			{
				return 10.0;
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06005BE3 RID: 23523 RVA: 0x0013FAC5 File Offset: 0x0013DCC5
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = new Dimensionality(new CubeLevelRange[]
					{
						new CubeLevelRange(this.level, this.level)
					});
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x06005BE4 RID: 23524 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasMeasureFilter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x0013FAFA File Offset: 0x0013DCFA
		public ICubeLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x0013FB02 File Offset: 0x0013DD02
		public override IEnumerable<Set> GetSubsets()
		{
			yield break;
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0013FB0C File Offset: 0x0013DD0C
		public override Set IntersectAsLeft(Set other)
		{
			CubeLevelRange cubeLevelRange;
			if (other.Dimensionality.TryGetLevelRange(this.level.Hierarchy, out cubeLevelRange) && cubeLevelRange.Coarse.Number <= this.level.Number)
			{
				return other.ExpandTo(other.Dimensionality.Union(this.Dimensionality));
			}
			return base.IntersectAsLeft(other);
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x0013FB6C File Offset: 0x0013DD6C
		public override Set IntersectAsRight(Set other)
		{
			CubeLevelRange cubeLevelRange;
			if (other.Dimensionality.TryGetLevelRange(this.level.Hierarchy, out cubeLevelRange) && cubeLevelRange.Coarse.Number <= this.level.Number)
			{
				return other.ExpandTo(other.Dimensionality.Union(this.Dimensionality));
			}
			return base.IntersectAsRight(other);
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0013FBCC File Offset: 0x0013DDCC
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			CubeLevelRange cubeLevelRange;
			if (newDimensionality.TryGetLevelRange(this.level.Hierarchy, out cubeLevelRange) && cubeLevelRange.Fine.Number <= this.level.Number)
			{
				return this;
			}
			return base.DescendTo(newDimensionality);
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this;
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x0013FC0F File Offset: 0x0013DE0F
		public override Set EnsureUniqueHierarchyMembers()
		{
			return new DistinctSet(this);
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set Unordered()
		{
			return this;
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0013FC17 File Offset: 0x0013DE17
		public override Set NewScope(string scope)
		{
			return new LevelSet(this.level.NewScope(scope));
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0013FC2A File Offset: 0x0013DE2A
		public bool Equals(LevelSet other)
		{
			return other != null && this.level.Equals(other.level);
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0013FC42 File Offset: 0x0013DE42
		public override bool Equals(object other)
		{
			return this.Equals(other as LevelSet);
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0013FC50 File Offset: 0x0013DE50
		public override int GetHashCode()
		{
			return this.level.GetHashCode();
		}

		// Token: 0x0400330D RID: 13069
		private readonly ICubeLevel level;

		// Token: 0x0400330E RID: 13070
		private Dimensionality dimensionality;
	}
}
