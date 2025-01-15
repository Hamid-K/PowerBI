using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D4B RID: 3403
	internal abstract class DelegatingSet : Set
	{
		// Token: 0x06005BA7 RID: 23463 RVA: 0x0013F79B File Offset: 0x0013D99B
		protected DelegatingSet(Set set)
		{
			this.set = set;
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual Set New(Set set)
		{
			return set;
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x0013F7AA File Offset: 0x0013D9AA
		public override SetKind Kind
		{
			get
			{
				return this.set.Kind;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0013F7B7 File Offset: 0x0013D9B7
		public override double Cardinality
		{
			get
			{
				return this.set.Cardinality;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x0013F7C4 File Offset: 0x0013D9C4
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.set.Dimensionality;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x0013F7D1 File Offset: 0x0013D9D1
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x0013F7DE File Offset: 0x0013D9DE
		public override RowCount TakeCount
		{
			get
			{
				return this.set.TakeCount;
			}
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x0013F7EB File Offset: 0x0013D9EB
		public override IEnumerable<Set> GetSubsets()
		{
			return this.set.GetSubsets();
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0013F7F8 File Offset: 0x0013D9F8
		public override IEnumerable<ICubeObject> GetResultObjects()
		{
			return this.set.GetResultObjects();
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0013F805 File Offset: 0x0013DA05
		public override Set IntersectAsLeft(Set other)
		{
			return this.New(this.set.IntersectAsLeft(other));
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x0013F819 File Offset: 0x0013DA19
		public override Set IntersectAsRight(Set other)
		{
			return this.New(this.set.IntersectAsRight(other));
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x0013F82D File Offset: 0x0013DA2D
		public override bool TryIntersectAsFilter(FilterSet other, out Set result)
		{
			if (this.set.TryIntersectAsFilter(other, out result))
			{
				result = this.New(result);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x0013F84E File Offset: 0x0013DA4E
		public override bool TryIntersectAsExcept(ExceptSet other, out Set result)
		{
			if (this.set.TryIntersectAsExcept(other, out result))
			{
				result = this.New(result);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x0013F86F File Offset: 0x0013DA6F
		public override Set Except(Set other)
		{
			return this.New(this.set.Except(other));
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0013F883 File Offset: 0x0013DA83
		public override bool TryExceptAsOtherAsUnion(UnionSet other, out Set result)
		{
			if (this.set.TryExceptAsOtherAsUnion(other, out result))
			{
				result = this.New(result);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0013F8A4 File Offset: 0x0013DAA4
		public override Set UnionAsLeft(Set other)
		{
			return this.New(this.set.UnionAsLeft(other));
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0013F8B8 File Offset: 0x0013DAB8
		public override Set UnionAsRight(Set other)
		{
			return this.New(this.set.UnionAsRight(other));
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0013F8CC File Offset: 0x0013DACC
		public override bool TryUnionAsUnion(UnionSet other, out Set result)
		{
			if (this.set.TryUnionAsUnion(other, out result))
			{
				result = this.New(result);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0013F8ED File Offset: 0x0013DAED
		public override bool TryUnionAsFilter(FilterSet other, out Set result)
		{
			if (this.set.TryUnionAsFilter(other, out result))
			{
				result = this.New(result);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x0013F90E File Offset: 0x0013DB0E
		public override Set CrossJoinAsLeft(Set other)
		{
			return this.New(this.set.CrossJoinAsLeft(other));
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0013F922 File Offset: 0x0013DB22
		public override Set CrossJoinAsRight(Set other)
		{
			return this.New(this.set.CrossJoinAsRight(other));
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0013F936 File Offset: 0x0013DB36
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			return this.New(this.set.DescendTo(newDimensionality));
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0013F94A File Offset: 0x0013DB4A
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this.New(this.set.OrderHierarchies(dimensionality));
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0013F95E File Offset: 0x0013DB5E
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this.New(this.set.EnsureUniqueHierarchyMembers());
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x0013F971 File Offset: 0x0013DB71
		public override Set Project(IEnumerable<ICubeObject> objects)
		{
			return this.New(this.set.Project(objects));
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x0013F985 File Offset: 0x0013DB85
		public override Set SkipTake(RowRange rowRange)
		{
			return this.New(this.set.SkipTake(rowRange));
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0013F999 File Offset: 0x0013DB99
		public override Set OrderBy(IList<CubeSortOrder> order)
		{
			return this.New(this.set.OrderBy(order));
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0013F9AD File Offset: 0x0013DBAD
		public override Set Unordered()
		{
			return this.New(this.set.Unordered());
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0013F9C0 File Offset: 0x0013DBC0
		public override Set NewScope(string scope)
		{
			return this.New(this.set.NewScope(scope));
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0013F9D4 File Offset: 0x0013DBD4
		public override bool Equals(object other)
		{
			return this.set.Equals(other);
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0013F9E2 File Offset: 0x0013DBE2
		public override int GetHashCode()
		{
			return this.set.GetHashCode();
		}

		// Token: 0x04003308 RID: 13064
		protected readonly Set set;
	}
}
