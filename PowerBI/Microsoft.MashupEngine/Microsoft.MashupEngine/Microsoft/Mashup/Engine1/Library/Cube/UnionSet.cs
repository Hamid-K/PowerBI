using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D5A RID: 3418
	internal class UnionSet : Set
	{
		// Token: 0x06005C7F RID: 23679 RVA: 0x00140D4C File Offset: 0x0013EF4C
		public static Set New(Set set1, Set set2)
		{
			if (set1.Equals(set2))
			{
				return set1;
			}
			Dimensionality dimensionality = set1.Dimensionality.Union(set2.Dimensionality);
			return new UnionSet(new Set[]
			{
				set1.ExpandTo(dimensionality),
				set2.ExpandTo(dimensionality)
			});
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00140D95 File Offset: 0x0013EF95
		private static Set New(params Set[] sets)
		{
			if (sets.Length == 1)
			{
				return sets[0];
			}
			return new UnionSet(sets);
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x00140DA7 File Offset: 0x0013EFA7
		private UnionSet(Set[] sets)
		{
			this.sets = sets;
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override SetKind Kind
		{
			get
			{
				return SetKind.Union;
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x00140DBC File Offset: 0x0013EFBC
		public override double Cardinality
		{
			get
			{
				double num = 0.0;
				foreach (Set set in this.sets)
				{
					num += set.Cardinality;
				}
				return num;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x00140DF8 File Offset: 0x0013EFF8
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = Dimensionality.Empty;
					foreach (Set set in this.sets)
					{
						this.dimensionality = this.dimensionality.Union(set.Dimensionality);
					}
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x06005C85 RID: 23685 RVA: 0x00140E4E File Offset: 0x0013F04E
		public override bool HasMeasureFilter
		{
			get
			{
				return this.sets.Any((Set s) => s.HasMeasureFilter);
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06005C86 RID: 23686 RVA: 0x00140E7A File Offset: 0x0013F07A
		public Set[] Sets
		{
			get
			{
				return this.sets;
			}
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x00140E82 File Offset: 0x0013F082
		public override IEnumerable<Set> GetSubsets()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x00140E94 File Offset: 0x0013F094
		public override Set IntersectAsLeft(Set other)
		{
			Set set;
			if (this.TryIntersect(other, out set))
			{
				return set;
			}
			return base.IntersectAsLeft(other);
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00140EB8 File Offset: 0x0013F0B8
		public override Set IntersectAsRight(Set other)
		{
			Set set;
			if (this.TryIntersect(other, out set))
			{
				return set;
			}
			return base.IntersectAsRight(other);
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00140EDC File Offset: 0x0013F0DC
		private bool TryIntersect(Set other, out Set result)
		{
			if (other.Dimensionality.IsSubsetOf(this.Dimensionality) && (other.Cardinality < this.Cardinality / (double)this.sets.Length || other.Dimensionality.HierarchyCount < this.Dimensionality.HierarchyCount))
			{
				Set[] array = null;
				for (int i = 0; i < this.sets.Length; i++)
				{
					if (this.sets[i].Dimensionality.HasAllHierarchiesOf(other.Dimensionality))
					{
						if (array == null)
						{
							array = (Set[])this.sets.Clone();
						}
						array[i] = this.sets[i].Intersect(other);
					}
				}
				if (array != null)
				{
					result = UnionSet.New(array);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x00140F94 File Offset: 0x0013F194
		public override Set Except(Set other)
		{
			Set set;
			if (other.TryExceptAsOtherAsUnion(this, out set))
			{
				return set;
			}
			for (int i = 0; i < this.sets.Length; i++)
			{
				if (this.sets[i].Equals(other))
				{
					Set[] array = new Set[this.sets.Length - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.sets[j];
					}
					for (int k = i + 1; k < this.sets.Length; k++)
					{
						array[k - 1] = this.sets[k];
					}
					return UnionSet.New(array);
				}
			}
			return base.Except(other);
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x00141030 File Offset: 0x0013F230
		public override bool TryExceptAsOtherAsUnion(UnionSet set, out Set result)
		{
			HashSet<Set> hashSet = new HashSet<Set>(set.sets);
			HashSet<Set> hashSet2 = new HashSet<Set>();
			for (int i = 0; i < this.sets.Length; i++)
			{
				Set set2 = this.sets[i];
				if (!hashSet.Remove(set2))
				{
					hashSet2.Add(set2);
				}
			}
			if (hashSet.Count < set.sets.Length)
			{
				result = UnionSet.New(hashSet.ToArray<Set>());
				if (hashSet2.Count > 0)
				{
					result = result.Except(UnionSet.New(hashSet2.ToArray<Set>()));
				}
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x001410BC File Offset: 0x0013F2BC
		public override Set UnionAsLeft(Set other)
		{
			Set set;
			if (this.TryUnion(other, out set))
			{
				return set;
			}
			return base.UnionAsLeft(other);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x001410E0 File Offset: 0x0013F2E0
		public override Set UnionAsRight(Set other)
		{
			Set set;
			if (this.TryUnion(other, out set))
			{
				return set;
			}
			return base.UnionAsRight(other);
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x00141104 File Offset: 0x0013F304
		private bool TryUnion(Set other, out Set result)
		{
			if (!this.Dimensionality.HasAllHierarchiesOf(other.Dimensionality))
			{
				result = null;
				return false;
			}
			if (other.TryUnionAsUnion(this, out result))
			{
				return true;
			}
			Dimensionality dimensionality = this.Dimensionality.Union(other.Dimensionality);
			Set[] array = new Set[this.sets.Length + 1];
			for (int i = 0; i < this.sets.Length; i++)
			{
				if (this.sets[i].Equals(other))
				{
					result = this;
					return true;
				}
				array[i] = this.sets[i].ExpandTo(dimensionality);
			}
			array[array.Length - 1] = other.ExpandTo(dimensionality);
			result = UnionSet.New(array);
			return true;
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x001411A8 File Offset: 0x0013F3A8
		public override bool TryUnionAsUnion(UnionSet other, out Set result)
		{
			Dimensionality dimensionality = this.Dimensionality.Union(other.Dimensionality);
			HashSet<Set> hashSet = new HashSet<Set>();
			for (int i = 0; i < this.sets.Length; i++)
			{
				hashSet.Add(this.sets[i].ExpandTo(dimensionality));
			}
			for (int j = 0; j < other.sets.Length; j++)
			{
				hashSet.Add(other.sets[j].ExpandTo(dimensionality));
			}
			result = UnionSet.New(hashSet.ToArray<Set>());
			return true;
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x0014122C File Offset: 0x0013F42C
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].OrderHierarchies(dimensionality);
			}
			return UnionSet.New(array);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x0014126C File Offset: 0x0013F46C
		public override Set EnsureUniqueHierarchyMembers()
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].EnsureUniqueHierarchyMembers();
			}
			return UnionSet.New(array);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x001412AC File Offset: 0x0013F4AC
		public override Set Unordered()
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].Unordered();
			}
			return UnionSet.New(array);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x001412EC File Offset: 0x0013F4EC
		public override Set NewScope(string scope)
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].NewScope(scope);
			}
			return UnionSet.New(array);
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x0014132C File Offset: 0x0013F52C
		public bool Equals(UnionSet other)
		{
			HashSet<Set> hashSet = new HashSet<Set>(this.sets);
			bool flag = other != null && this.sets.Length == other.sets.Length;
			int num = 0;
			while (flag && num < other.sets.Length)
			{
				flag = hashSet.Contains(other.sets[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00141384 File Offset: 0x0013F584
		public override bool Equals(object other)
		{
			return this.Equals(other as UnionSet);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00141394 File Offset: 0x0013F594
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.sets.Length; i++)
			{
				num += this.sets[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x04003332 RID: 13106
		private readonly Set[] sets;

		// Token: 0x04003333 RID: 13107
		private Dimensionality dimensionality;
	}
}
