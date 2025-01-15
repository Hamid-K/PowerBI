using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D49 RID: 3401
	internal abstract class Set
	{
		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06005B7A RID: 23418
		public abstract SetKind Kind { get; }

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06005B7B RID: 23419
		public abstract double Cardinality { get; }

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06005B7C RID: 23420
		public abstract Dimensionality Dimensionality { get; }

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06005B7D RID: 23421
		public abstract bool HasMeasureFilter { get; }

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x0013F1E0 File Offset: 0x0013D3E0
		public virtual RowCount TakeCount
		{
			get
			{
				return RowCount.Infinite;
			}
		}

		// Token: 0x06005B7F RID: 23423
		public abstract IEnumerable<Set> GetSubsets();

		// Token: 0x06005B80 RID: 23424 RVA: 0x0013F1E7 File Offset: 0x0013D3E7
		public virtual IEnumerable<ICubeObject> GetResultObjects()
		{
			HashSet<ICubeLevel> objects = new HashSet<ICubeLevel>();
			foreach (ICubeHierarchy hierarchy in this.Dimensionality.Hierarchies)
			{
				CubeLevelRange levelRange = this.Dimensionality.GetLevelRange(hierarchy);
				int num;
				for (int i = levelRange.Coarse.Number; i <= levelRange.Fine.Number; i = num + 1)
				{
					ICubeLevel level = hierarchy.GetLevel(i);
					if (objects.Add(level))
					{
						yield return level;
					}
					num = i;
				}
				levelRange = null;
				hierarchy = null;
			}
			IEnumerator<ICubeHierarchy> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x0013F1F8 File Offset: 0x0013D3F8
		public Set Intersect(Set other)
		{
			Set set = this;
			foreach (Set set2 in other.GetSubsets())
			{
				set = set.IntersectAsLeft(set2);
			}
			Dimensionality dimensionality = this.Dimensionality.Union(other.Dimensionality);
			return set.ExpandTo(dimensionality);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0013F264 File Offset: 0x0013D464
		public virtual Set IntersectAsLeft(Set other)
		{
			Set set = other;
			foreach (Set set2 in this.GetSubsets())
			{
				set = set.IntersectAsRight(set2);
			}
			return set;
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0013F2B8 File Offset: 0x0013D4B8
		public virtual Set IntersectAsRight(Set other)
		{
			bool flag = this.Dimensionality.IsEverything || other.Dimensionality.IsEverything;
			foreach (ICubeHierarchy cubeHierarchy in other.Dimensionality.Hierarchies)
			{
				CubeLevelRange cubeLevelRange;
				if (this.Dimensionality.TryGetLevelRange(cubeHierarchy, out cubeLevelRange))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return this.CrossJoin(other);
			}
			return IntersectSet.New(this, other);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryIntersectAsFilter(FilterSet other, out Set result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryIntersectAsExcept(ExceptSet other, out Set result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0013F348 File Offset: 0x0013D548
		public virtual Set Except(Set other)
		{
			return ExceptSet.New(this, other);
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryExceptAsOtherAsUnion(UnionSet other, out Set result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0013F351 File Offset: 0x0013D551
		public Set Union(Set other)
		{
			return this.UnionAsLeft(other);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x0013F35A File Offset: 0x0013D55A
		public virtual Set UnionAsLeft(Set other)
		{
			return other.UnionAsRight(this);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0013F363 File Offset: 0x0013D563
		public virtual Set UnionAsRight(Set other)
		{
			return UnionSet.New(this, other);
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryUnionAsUnion(UnionSet other, out Set result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryUnionAsFilter(FilterSet other, out Set result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0013F36C File Offset: 0x0013D56C
		public Set CrossJoin(Set other)
		{
			return this.CrossJoinAsLeft(other);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0013F375 File Offset: 0x0013D575
		public virtual Set CrossJoinAsLeft(Set other)
		{
			return other.CrossJoinAsRight(this);
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x0013F37E File Offset: 0x0013D57E
		public virtual Set CrossJoinAsRight(Set other)
		{
			return new CrossJoinSet(new Set[] { this, other });
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0013F394 File Offset: 0x0013D594
		public virtual Set DescendTo(Dimensionality newDimensionality)
		{
			bool flag = false;
			Dictionary<ICubeHierarchy, CubeLevelRange> dictionary = new Dictionary<ICubeHierarchy, CubeLevelRange>();
			foreach (ICubeHierarchy cubeHierarchy in newDimensionality.Hierarchies)
			{
				CubeLevelRange cubeLevelRange = newDimensionality.GetLevelRange(cubeHierarchy);
				CubeLevelRange cubeLevelRange2;
				if (this.Dimensionality.TryGetLevelRange(cubeHierarchy, out cubeLevelRange2))
				{
					if (cubeLevelRange.Fine.Number > cubeLevelRange2.Fine.Number)
					{
						flag = true;
					}
					else
					{
						cubeLevelRange = cubeLevelRange2;
					}
					dictionary[cubeHierarchy] = cubeLevelRange;
				}
			}
			if (flag)
			{
				return new DescendToSet(this, new Dimensionality(dictionary.Values));
			}
			return this;
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0013F43C File Offset: 0x0013D63C
		public virtual Set OrderHierarchies(Dimensionality dimensionality)
		{
			return OrderHierarchiesSet.New(this, dimensionality);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0013F448 File Offset: 0x0013D648
		public Set ExpandTo(Dimensionality newDimensionality)
		{
			Set set = this;
			foreach (ICubeHierarchy cubeHierarchy in newDimensionality.Hierarchies)
			{
				CubeLevelRange levelRange;
				if (!this.Dimensionality.TryGetLevelRange(cubeHierarchy, out levelRange))
				{
					levelRange = newDimensionality.GetLevelRange(cubeHierarchy);
					set = set.CrossJoin(new LevelSet(levelRange.Coarse));
				}
			}
			set = set.DescendTo(newDimensionality);
			Dimensionality dimensionality = newDimensionality;
			foreach (Set set2 in this.GetSubsets())
			{
				dimensionality = dimensionality.Union(set2.Dimensionality);
			}
			set = set.OrderHierarchies(dimensionality);
			return set;
		}

		// Token: 0x06005B93 RID: 23443
		public abstract Set EnsureUniqueHierarchyMembers();

		// Token: 0x06005B94 RID: 23444 RVA: 0x0013F518 File Offset: 0x0013D718
		public virtual Set Project(IEnumerable<ICubeObject> objects)
		{
			return new ProjectSet(this, objects);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0013F521 File Offset: 0x0013D721
		public virtual Set SkipTake(RowRange rowRange)
		{
			return SkipTakeSet.New(this, rowRange);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0013F52C File Offset: 0x0013D72C
		public Set Skip(RowCount rowCount)
		{
			return this.SkipTake(RowRange.All.Skip(rowCount));
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x0013F550 File Offset: 0x0013D750
		public Set Take(RowCount rowCount)
		{
			return this.SkipTake(RowRange.All.Take(rowCount));
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x0013F571 File Offset: 0x0013D771
		public virtual Set OrderBy(IList<CubeSortOrder> order)
		{
			return OrderBySet.New(this.Unordered(), order);
		}

		// Token: 0x06005B99 RID: 23449
		public abstract Set Unordered();

		// Token: 0x06005B9A RID: 23450
		public abstract Set NewScope(string scope);

		// Token: 0x06005B9B RID: 23451
		public abstract override bool Equals(object other);

		// Token: 0x06005B9C RID: 23452
		public abstract override int GetHashCode();

		// Token: 0x040032FE RID: 13054
		protected const double levelMembers = 10.0;
	}
}
