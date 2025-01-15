using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D60 RID: 3424
	internal class FilterSet : Set
	{
		// Token: 0x06005CC6 RID: 23750 RVA: 0x00141B3B File Offset: 0x0013FD3B
		public static Set New(Set set, Dimensionality predicateDimensionality, CubeExpression predicate, bool predicateHasMeasureFilter)
		{
			if (ConstantCubeExpression.True.Equals(predicate))
			{
				return set;
			}
			return new FilterSet(set, predicateDimensionality, predicate, predicateHasMeasureFilter);
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x00141B55 File Offset: 0x0013FD55
		private FilterSet(Set set, Dimensionality predicateDimensionality, CubeExpression predicate, bool predicateHasMeasureFilter)
		{
			this.set = set;
			this.predicateDimensionality = predicateDimensionality;
			this.predicate = predicate;
			this.predicateHasMeasureFilter = predicateHasMeasureFilter;
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x00141B7A File Offset: 0x0013FD7A
		private Set New(Set set)
		{
			return FilterSet.New(set, this.predicateDimensionality, this.predicate, this.predicateHasMeasureFilter);
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00075E2C File Offset: 0x0007402C
		public override SetKind Kind
		{
			get
			{
				return SetKind.Filter;
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x00141B94 File Offset: 0x0013FD94
		public override double Cardinality
		{
			get
			{
				return this.set.Cardinality * 0.1;
			}
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x00141BAB File Offset: 0x0013FDAB
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.set.Dimensionality;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x00141BB8 File Offset: 0x0013FDB8
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter || this.predicateHasMeasureFilter;
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06005CCD RID: 23757 RVA: 0x00141BCF File Offset: 0x0013FDCF
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x00141BD7 File Offset: 0x0013FDD7
		public CubeExpression Predicate
		{
			get
			{
				return this.predicate;
			}
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x00141BDF File Offset: 0x0013FDDF
		public override IEnumerable<Set> GetSubsets()
		{
			foreach (Set set in this.set.GetSubsets())
			{
				yield return set;
			}
			IEnumerator<Set> enumerator = null;
			yield return this.New(EverythingSet.Instance.ExpandTo(this.predicateDimensionality));
			yield break;
			yield break;
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00141BF0 File Offset: 0x0013FDF0
		public override Set IntersectAsLeft(Set other)
		{
			if (!other.Dimensionality.IsDescendedTo(this.predicateDimensionality))
			{
				return base.IntersectAsLeft(other);
			}
			Set set;
			if (other.TryIntersectAsFilter(this, out set))
			{
				return set;
			}
			return this.New(this.set.Intersect(other));
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x00141C38 File Offset: 0x0013FE38
		public override Set IntersectAsRight(Set other)
		{
			if (!other.Dimensionality.IsDescendedTo(this.predicateDimensionality))
			{
				return base.IntersectAsRight(other);
			}
			Set set;
			if (other.TryIntersectAsFilter(this, out set))
			{
				return set;
			}
			return this.New(this.set.Intersect(other));
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x00141C80 File Offset: 0x0013FE80
		public override bool TryIntersectAsFilter(FilterSet other, out Set result)
		{
			if (other.predicateDimensionality.IsDescendedTo(this.predicateDimensionality) && this.predicateDimensionality.IsDescendedTo(other.predicateDimensionality))
			{
				result = FilterSet.New(this.set.Intersect(other.set), this.predicateDimensionality, this.predicate.And(other.predicate), this.predicateHasMeasureFilter || other.predicateHasMeasureFilter);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x00141CFC File Offset: 0x0013FEFC
		public override Set UnionAsLeft(Set other)
		{
			Set set;
			if (other.Dimensionality.IsDescendedTo(this.predicateDimensionality) && other.TryUnionAsFilter(this, out set))
			{
				return set;
			}
			return base.UnionAsLeft(other);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00141D30 File Offset: 0x0013FF30
		public override Set UnionAsRight(Set other)
		{
			Set set;
			if (other.Dimensionality.IsDescendedTo(this.predicateDimensionality) && other.TryUnionAsFilter(this, out set))
			{
				return set;
			}
			return base.UnionAsRight(other);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x00141D64 File Offset: 0x0013FF64
		public override bool TryUnionAsFilter(FilterSet other, out Set result)
		{
			if (other.predicateDimensionality.IsDescendedTo(this.predicateDimensionality) && this.predicateDimensionality.IsDescendedTo(other.predicateDimensionality))
			{
				if (this.predicate.Equals(other.predicate))
				{
					result = this.New(this.set.Union(other.set));
					return true;
				}
				CubeExpression cubeExpression;
				ConstantCubeExpression constantCubeExpression;
				CubeExpression cubeExpression2;
				if (this.set.Equals(other.set) && (!this.predicate.TryGetConstantFilter(out cubeExpression, out constantCubeExpression) || !other.predicate.TryGetConstantFilter(out cubeExpression2, out constantCubeExpression) || !cubeExpression.Equals(cubeExpression2)))
				{
					result = FilterSet.New(this.set, this.predicateDimensionality, this.predicate.Or(other.predicate), this.predicateHasMeasureFilter || other.predicateHasMeasureFilter);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00141E44 File Offset: 0x00140044
		public override Set CrossJoinAsLeft(Set other)
		{
			if (this.predicateDimensionality.IsEverything)
			{
				return this.New(this.set.CrossJoin(other));
			}
			return base.CrossJoinAsLeft(other);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00141E6D File Offset: 0x0014006D
		public override Set CrossJoinAsRight(Set other)
		{
			if (this.predicateDimensionality.IsEverything)
			{
				return this.New(this.set.CrossJoin(other));
			}
			return base.CrossJoinAsRight(other);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00141E96 File Offset: 0x00140096
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this.New(this.set.OrderHierarchies(dimensionality));
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00141EAA File Offset: 0x001400AA
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this.New(this.set.EnsureUniqueHierarchyMembers());
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00141EBD File Offset: 0x001400BD
		public override Set Unordered()
		{
			return this.New(this.set.Unordered());
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00141ED0 File Offset: 0x001400D0
		public override Set NewScope(string scope)
		{
			Set set = this.set.NewScope(scope);
			Dimensionality dimensionality = this.predicateDimensionality.NewScope(scope);
			CubeExpression cubeExpression = this.predicate.NewScope(scope);
			return new FilterSet(set, dimensionality, cubeExpression, this.predicateHasMeasureFilter);
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x00141F10 File Offset: 0x00140110
		public bool Equals(FilterSet other)
		{
			return other != null && this.set.Equals(other.set) && this.predicate.Equals(other.predicate);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x00141F3B File Offset: 0x0014013B
		public override bool Equals(object other)
		{
			return this.Equals(other as FilterSet);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00141F49 File Offset: 0x00140149
		public override int GetHashCode()
		{
			return this.set.GetHashCode() + 37 * this.predicate.GetHashCode();
		}

		// Token: 0x04003346 RID: 13126
		private const double filterSelectivity = 0.1;

		// Token: 0x04003347 RID: 13127
		private readonly Set set;

		// Token: 0x04003348 RID: 13128
		private readonly Dimensionality predicateDimensionality;

		// Token: 0x04003349 RID: 13129
		private readonly CubeExpression predicate;

		// Token: 0x0400334A RID: 13130
		private readonly bool predicateHasMeasureFilter;
	}
}
