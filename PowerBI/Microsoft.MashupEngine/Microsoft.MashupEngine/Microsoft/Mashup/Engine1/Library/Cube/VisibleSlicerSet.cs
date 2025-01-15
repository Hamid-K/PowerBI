using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D79 RID: 3449
	internal class VisibleSlicerSet : Set
	{
		// Token: 0x06005DF3 RID: 24051 RVA: 0x00144789 File Offset: 0x00142989
		public VisibleSlicerSet(Dimensionality visibleAxisDimensionality)
			: this(EverythingSet.Instance.ExpandTo(visibleAxisDimensionality), EverythingSet.Instance)
		{
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001447A1 File Offset: 0x001429A1
		public VisibleSlicerSet(Set visibleAxis, Set slicerAxis)
		{
			this.visibleAxis = visibleAxis;
			this.slicerAxis = slicerAxis;
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x0006808E File Offset: 0x0006628E
		public override SetKind Kind
		{
			get
			{
				return SetKind.Other;
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x001447B7 File Offset: 0x001429B7
		public override double Cardinality
		{
			get
			{
				return Math.Min(this.visibleAxis.Cardinality, this.slicerAxis.Cardinality);
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x001447D4 File Offset: 0x001429D4
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.visibleAxis.Dimensionality;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x001447E1 File Offset: 0x001429E1
		public override bool HasMeasureFilter
		{
			get
			{
				return this.visibleAxis.HasMeasureFilter || this.slicerAxis.HasMeasureFilter;
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x001447FD File Offset: 0x001429FD
		public Set VisibleAxis
		{
			get
			{
				return this.visibleAxis;
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x00144805 File Offset: 0x00142A05
		public Set SlicerAxis
		{
			get
			{
				return this.slicerAxis;
			}
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0014480D File Offset: 0x00142A0D
		public override IEnumerable<Set> GetSubsets()
		{
			foreach (Set set in this.visibleAxis.GetSubsets())
			{
				yield return set;
			}
			IEnumerator<Set> enumerator = null;
			foreach (Set set2 in this.slicerAxis.GetSubsets())
			{
				yield return set2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set CrossJoinAsLeft(Set other)
		{
			return this;
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set CrossJoinAsRight(Set other)
		{
			return this;
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			return this;
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0014481D File Offset: 0x00142A1D
		public override Set IntersectAsLeft(Set other)
		{
			return this.IntersectCommon(other);
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0014481D File Offset: 0x00142A1D
		public override Set IntersectAsRight(Set other)
		{
			return this.IntersectCommon(other);
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x00144828 File Offset: 0x00142A28
		private Set IntersectCommon(Set other)
		{
			Set set = this.visibleAxis;
			Set set2 = this.slicerAxis;
			if (other.HasMeasureFilter)
			{
				if (!other.Dimensionality.Equals(set.Dimensionality))
				{
					throw new NotSupportedException();
				}
				set = set.Intersect(other);
			}
			else
			{
				set2 = set2.Intersect(other);
			}
			return new VisibleSlicerSet(set, set2);
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x0014487D File Offset: 0x00142A7D
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return new VisibleSlicerSet(this.visibleAxis.OrderHierarchies(dimensionality), this.slicerAxis.OrderHierarchies(dimensionality));
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x0014489C File Offset: 0x00142A9C
		public override Set EnsureUniqueHierarchyMembers()
		{
			return new VisibleSlicerSet(this.visibleAxis.EnsureUniqueHierarchyMembers(), this.slicerAxis.EnsureUniqueHierarchyMembers());
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x001448B9 File Offset: 0x00142AB9
		public override Set Unordered()
		{
			return new VisibleSlicerSet(this.visibleAxis.Unordered(), this.slicerAxis.Unordered());
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x001448D6 File Offset: 0x00142AD6
		public override Set NewScope(string scope)
		{
			return new VisibleSlicerSet(this.visibleAxis.NewScope(scope), this.slicerAxis.NewScope(scope));
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x001448F5 File Offset: 0x00142AF5
		public bool Equals(VisibleSlicerSet other)
		{
			return other != null && this.visibleAxis.Equals(other.visibleAxis) && this.slicerAxis.Equals(other.slicerAxis);
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x00144920 File Offset: 0x00142B20
		public override bool Equals(object other)
		{
			return this.Equals(other as VisibleSlicerSet);
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x0014492E File Offset: 0x00142B2E
		public override int GetHashCode()
		{
			return this.visibleAxis.GetHashCode() + 5011 * this.slicerAxis.GetHashCode();
		}

		// Token: 0x0400337F RID: 13183
		private readonly Set visibleAxis;

		// Token: 0x04003380 RID: 13184
		private readonly Set slicerAxis;
	}
}
