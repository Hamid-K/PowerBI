using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D58 RID: 3416
	internal class ExceptSet : Set
	{
		// Token: 0x06005C62 RID: 23650 RVA: 0x00140910 File Offset: 0x0013EB10
		public static Set New(Set set, Set except)
		{
			Dimensionality dimensionality = set.Dimensionality.Union(except.Dimensionality);
			return new ExceptSet(set.ExpandTo(dimensionality), except.ExpandTo(dimensionality));
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x00140942 File Offset: 0x0013EB42
		private ExceptSet(Set set, Set except)
		{
			this.set = set;
			this.except = except;
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x0000244F File Offset: 0x0000064F
		public override SetKind Kind
		{
			get
			{
				return SetKind.Except;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x06005C65 RID: 23653 RVA: 0x00140958 File Offset: 0x0013EB58
		public override double Cardinality
		{
			get
			{
				return Math.Max(this.set.Cardinality - this.except.Cardinality, 0.0);
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06005C66 RID: 23654 RVA: 0x0014097F File Offset: 0x0013EB7F
		public override Dimensionality Dimensionality
		{
			get
			{
				if (this.dimensionality == null)
				{
					this.dimensionality = this.set.Dimensionality.Union(this.except.Dimensionality);
				}
				return this.dimensionality;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x001409B0 File Offset: 0x0013EBB0
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter || this.except.HasMeasureFilter;
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x001409CC File Offset: 0x0013EBCC
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06005C69 RID: 23657 RVA: 0x001409D4 File Offset: 0x0013EBD4
		public Set _Except
		{
			get
			{
				return this.except;
			}
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x001409DC File Offset: 0x0013EBDC
		public override IEnumerable<Set> GetSubsets()
		{
			bool flag = true;
			foreach (Set set in this.set.GetSubsets())
			{
				yield return set.Except(this.except);
				flag = false;
			}
			IEnumerator<Set> enumerator = null;
			if (flag)
			{
				yield return this;
			}
			yield break;
			yield break;
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x001409EC File Offset: 0x0013EBEC
		public override Set IntersectAsLeft(Set other)
		{
			if (!this.set.Dimensionality.HasAllHierarchiesOf(other.Dimensionality))
			{
				return base.IntersectAsLeft(other);
			}
			Set set;
			if (other.TryIntersectAsExcept(this, out set))
			{
				return set;
			}
			return ExceptSet.New(this.set.Intersect(other), this.except);
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00140A40 File Offset: 0x0013EC40
		public override Set IntersectAsRight(Set other)
		{
			if (!this.set.Dimensionality.HasAllHierarchiesOf(other.Dimensionality))
			{
				return base.IntersectAsRight(other);
			}
			Set set;
			if (other.TryIntersectAsExcept(this, out set))
			{
				return set;
			}
			return ExceptSet.New(this.set.Intersect(other), this.except);
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x00140A91 File Offset: 0x0013EC91
		public override bool TryIntersectAsExcept(ExceptSet other, out Set result)
		{
			result = ExceptSet.New(this.set.Intersect(other.set), this.except.Union(other.except));
			return true;
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00140ABD File Offset: 0x0013ECBD
		public override Set Except(Set other)
		{
			return ExceptSet.New(this.set, this.except.Union(other));
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x00140AD6 File Offset: 0x0013ECD6
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return new ExceptSet(this.set.OrderHierarchies(dimensionality), this.except.OrderHierarchies(dimensionality));
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x00140AF5 File Offset: 0x0013ECF5
		public override Set EnsureUniqueHierarchyMembers()
		{
			return new ExceptSet(this.set.EnsureUniqueHierarchyMembers(), this.except.EnsureUniqueHierarchyMembers());
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x00140B12 File Offset: 0x0013ED12
		public override Set Unordered()
		{
			return new ExceptSet(this.set.Unordered(), this.except.Unordered());
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00140B2F File Offset: 0x0013ED2F
		public override Set NewScope(string scope)
		{
			return new ExceptSet(this.set.NewScope(scope), this.except.NewScope(scope));
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x00140B4E File Offset: 0x0013ED4E
		public bool Equals(ExceptSet other)
		{
			return other != null && this.set.Equals(other.set) && this.except.Equals(other.except);
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x00140B79 File Offset: 0x0013ED79
		public override bool Equals(object other)
		{
			return this.Equals(other as ExceptSet);
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00140B87 File Offset: 0x0013ED87
		public override int GetHashCode()
		{
			return this.set.GetHashCode() + 37 * this.except.GetHashCode();
		}

		// Token: 0x0400332A RID: 13098
		private readonly Set set;

		// Token: 0x0400332B RID: 13099
		private readonly Set except;

		// Token: 0x0400332C RID: 13100
		private Dimensionality dimensionality;
	}
}
