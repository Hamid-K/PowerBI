using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D5D RID: 3421
	internal class CrossJoinSet : Set
	{
		// Token: 0x06005CA3 RID: 23715 RVA: 0x0014148F File Offset: 0x0013F68F
		public CrossJoinSet(params Set[] sets)
		{
			this.sets = sets;
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06005CA4 RID: 23716 RVA: 0x00002105 File Offset: 0x00000305
		public override SetKind Kind
		{
			get
			{
				return SetKind.CrossJoin;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x001414A0 File Offset: 0x0013F6A0
		public override double Cardinality
		{
			get
			{
				double num = 1.0;
				foreach (Set set in this.sets)
				{
					num *= set.Cardinality;
				}
				return num;
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x001414DC File Offset: 0x0013F6DC
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = Dimensionality.Union(this.sets.Select((Set s) => s.Dimensionality));
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x0014152C File Offset: 0x0013F72C
		public override bool HasMeasureFilter
		{
			get
			{
				return this.sets.Any((Set s) => s.HasMeasureFilter);
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x00141558 File Offset: 0x0013F758
		public Set[] Sets
		{
			get
			{
				return this.sets;
			}
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00141560 File Offset: 0x0013F760
		public override Set CrossJoinAsLeft(Set other)
		{
			return this.CrossJoinCommon(other);
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x00141560 File Offset: 0x0013F760
		public override Set CrossJoinAsRight(Set other)
		{
			return this.CrossJoinCommon(other);
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x0014156C File Offset: 0x0013F76C
		private Set CrossJoinCommon(Set other)
		{
			Set[] array = new Set[this.sets.Length + 1];
			this.sets.CopyTo(array, 0);
			array[array.Length - 1] = other;
			return new CrossJoinSet(array);
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x001415A4 File Offset: 0x0013F7A4
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].DescendTo(newDimensionality);
			}
			return new CrossJoinSet(array);
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x001415E4 File Offset: 0x0013F7E4
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			Dictionary<ICubeHierarchy, Set> dictionary = new Dictionary<ICubeHierarchy, Set>();
			for (int i = 0; i < this.sets.Length; i++)
			{
				Set set = this.sets[i].OrderHierarchies(dimensionality);
				ICubeHierarchy cubeHierarchy = set.Dimensionality.Hierarchies.FirstOrDefault<ICubeHierarchy>();
				if (cubeHierarchy == null || dictionary.ContainsKey(cubeHierarchy))
				{
					dictionary = null;
					break;
				}
				dictionary.Add(cubeHierarchy, set);
			}
			if (dictionary != null)
			{
				ArrayBuilder<Set> arrayBuilder = new ArrayBuilder<Set>(this.sets.Length);
				foreach (ICubeHierarchy cubeHierarchy2 in dimensionality.Union(this.Dimensionality).Hierarchies)
				{
					Set set2;
					if (dictionary.TryGetValue(cubeHierarchy2, out set2))
					{
						arrayBuilder.Add(set2);
					}
				}
				return OrderHierarchiesSet.New(new CrossJoinSet(arrayBuilder.ToArray()), dimensionality);
			}
			return base.OrderHierarchies(dimensionality);
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x001416D0 File Offset: 0x0013F8D0
		public override Set EnsureUniqueHierarchyMembers()
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].EnsureUniqueHierarchyMembers();
			}
			return new CrossJoinSet(array);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00141710 File Offset: 0x0013F910
		public override Set Unordered()
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].Unordered();
			}
			return new CrossJoinSet(array);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00141750 File Offset: 0x0013F950
		public override Set NewScope(string scope)
		{
			Set[] array = new Set[this.sets.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.sets[i].NewScope(scope);
			}
			return new CrossJoinSet(array);
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00141790 File Offset: 0x0013F990
		public override IEnumerable<Set> GetSubsets()
		{
			foreach (Set set in this.sets)
			{
				foreach (Set set2 in set.GetSubsets())
				{
					yield return set2;
				}
				IEnumerator<Set> enumerator = null;
			}
			Set[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001417A0 File Offset: 0x0013F9A0
		public override Set IntersectAsLeft(Set other)
		{
			Set set;
			if (this.TryIntersect(other, out set))
			{
				return set;
			}
			return base.IntersectAsLeft(other);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x001417C4 File Offset: 0x0013F9C4
		public override Set IntersectAsRight(Set other)
		{
			Set set;
			if (this.TryIntersect(other, out set))
			{
				return set;
			}
			return base.IntersectAsRight(other);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x001417E8 File Offset: 0x0013F9E8
		private bool TryIntersect(Set other, out Set result)
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
				result = new CrossJoinSet(array);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x0014185C File Offset: 0x0013FA5C
		public override Set Except(Set other)
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
					array[i] = this.sets[i].Except(other);
				}
			}
			if (array != null)
			{
				return new CrossJoinSet(array);
			}
			return base.Except(other);
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001418D0 File Offset: 0x0013FAD0
		public bool Equals(CrossJoinSet other)
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

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00141928 File Offset: 0x0013FB28
		public override bool Equals(object other)
		{
			return this.Equals(other as CrossJoinSet);
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00141938 File Offset: 0x0013FB38
		public override int GetHashCode()
		{
			int num = 5011;
			for (int i = 0; i < this.sets.Length; i++)
			{
				num += this.sets[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0400333A RID: 13114
		private readonly Set[] sets;

		// Token: 0x0400333B RID: 13115
		private Dimensionality dimensionality;
	}
}
