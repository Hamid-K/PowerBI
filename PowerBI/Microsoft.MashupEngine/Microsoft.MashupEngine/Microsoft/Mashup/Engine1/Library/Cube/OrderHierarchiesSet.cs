using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D55 RID: 3413
	internal class OrderHierarchiesSet : Set
	{
		// Token: 0x06005C35 RID: 23605 RVA: 0x001401A0 File Offset: 0x0013E3A0
		public static Set New(Set set, Dimensionality dimensionality)
		{
			dimensionality = dimensionality.IntersectHierarchies(set.Dimensionality);
			bool flag = false;
			using (IEnumerator<ICubeHierarchy> enumerator = set.Dimensionality.Hierarchies.GetEnumerator())
			{
				using (IEnumerator<ICubeHierarchy> enumerator2 = dimensionality.Hierarchies.GetEnumerator())
				{
					for (;;)
					{
						bool flag2 = enumerator.MoveNext();
						bool flag3 = enumerator2.MoveNext();
						if (flag2 != flag3 || (flag2 && enumerator.Current != enumerator2.Current))
						{
							break;
						}
						if (!flag2)
						{
							goto Block_7;
						}
					}
					flag = true;
					Block_7:;
				}
			}
			if (flag)
			{
				return new OrderHierarchiesSet(set, dimensionality);
			}
			return set;
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x00140244 File Offset: 0x0013E444
		private OrderHierarchiesSet(Set set, Dimensionality dimensionality)
		{
			this.set = set;
			this.dimensionality = dimensionality;
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x0014025A File Offset: 0x0013E45A
		public override SetKind Kind
		{
			get
			{
				return SetKind.OrderHierarchies;
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06005C38 RID: 23608 RVA: 0x0014025E File Offset: 0x0013E45E
		public override double Cardinality
		{
			get
			{
				return this.set.Cardinality;
			}
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06005C39 RID: 23609 RVA: 0x0014026B File Offset: 0x0013E46B
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.dimensionality;
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06005C3A RID: 23610 RVA: 0x00140273 File Offset: 0x0013E473
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x00140280 File Offset: 0x0013E480
		public override RowCount TakeCount
		{
			get
			{
				return this.set.TakeCount;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x0014028D File Offset: 0x0013E48D
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x00140295 File Offset: 0x0013E495
		public override IEnumerable<Set> GetSubsets()
		{
			return this.set.GetSubsets();
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x001402A2 File Offset: 0x0013E4A2
		public override Set DescendTo(Dimensionality newDimensionality)
		{
			return this.set.DescendTo(this.dimensionality.Union(newDimensionality));
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x001402BB File Offset: 0x0013E4BB
		public override Set OrderHierarchies(Dimensionality dimensionality)
		{
			return this.set.OrderHierarchies(dimensionality);
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x001402C9 File Offset: 0x0013E4C9
		public override Set EnsureUniqueHierarchyMembers()
		{
			return this.set.EnsureUniqueHierarchyMembers().OrderHierarchies(this.dimensionality);
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x001402E1 File Offset: 0x0013E4E1
		public override Set Unordered()
		{
			return this.set.Unordered().OrderHierarchies(this.dimensionality);
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x001402F9 File Offset: 0x0013E4F9
		public override Set NewScope(string scope)
		{
			return this.set.NewScope(scope).OrderHierarchies(this.dimensionality.NewScope(scope));
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x00140318 File Offset: 0x0013E518
		public bool Equals(OrderHierarchiesSet other)
		{
			return other != null && this.set.Equals(other.set) && this.dimensionality.Equals(other.dimensionality);
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x00140343 File Offset: 0x0013E543
		public override bool Equals(object other)
		{
			return this.Equals(other as OrderHierarchiesSet);
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x00140351 File Offset: 0x0013E551
		public override int GetHashCode()
		{
			return this.set.GetHashCode() + 5011 * this.dimensionality.GetHashCode();
		}

		// Token: 0x04003320 RID: 13088
		private readonly Set set;

		// Token: 0x04003321 RID: 13089
		private readonly Dimensionality dimensionality;
	}
}
