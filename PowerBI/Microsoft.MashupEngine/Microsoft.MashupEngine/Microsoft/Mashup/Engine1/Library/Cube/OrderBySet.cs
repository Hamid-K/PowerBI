using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D66 RID: 3430
	internal class OrderBySet : Set
	{
		// Token: 0x06005D14 RID: 23828 RVA: 0x001425E7 File Offset: 0x001407E7
		public static Set New(Set set, IList<CubeSortOrder> order)
		{
			if (order.Count == 0)
			{
				return set;
			}
			return new OrderBySet(set, order);
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x001425FA File Offset: 0x001407FA
		protected OrderBySet(Set set, IList<CubeSortOrder> order)
		{
			this.set = set;
			this.order = order;
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x00142610 File Offset: 0x00140810
		public override SetKind Kind
		{
			get
			{
				return SetKind.OrderBy;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06005D17 RID: 23831 RVA: 0x00142614 File Offset: 0x00140814
		public override double Cardinality
		{
			get
			{
				return this.set.Cardinality;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x00142621 File Offset: 0x00140821
		public override Dimensionality Dimensionality
		{
			get
			{
				return this.set.Dimensionality;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x0014262E File Offset: 0x0014082E
		public override bool HasMeasureFilter
		{
			get
			{
				return this.set.HasMeasureFilter;
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06005D1A RID: 23834 RVA: 0x0014263B File Offset: 0x0014083B
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06005D1B RID: 23835 RVA: 0x00142643 File Offset: 0x00140843
		public IList<CubeSortOrder> Order
		{
			get
			{
				return this.order;
			}
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x0014264B File Offset: 0x0014084B
		public override IEnumerable<Set> GetSubsets()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0014265B File Offset: 0x0014085B
		public override Set EnsureUniqueHierarchyMembers()
		{
			return OrderBySet.New(this.set.EnsureUniqueHierarchyMembers(), this.order);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x00142673 File Offset: 0x00140873
		public override Set OrderBy(IList<CubeSortOrder> order)
		{
			return this.set.OrderBy(order);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x0014263B File Offset: 0x0014083B
		public override Set Unordered()
		{
			return this.set;
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x00142684 File Offset: 0x00140884
		public override Set NewScope(string scope)
		{
			CubeSortOrder[] array = new CubeSortOrder[this.order.Count];
			for (int i = 0; i < array.Length; i++)
			{
				CubeSortOrder cubeSortOrder = this.order[i];
				array[i] = new CubeSortOrder(cubeSortOrder.Expression.NewScope(scope), cubeSortOrder.Ascending);
			}
			return new OrderBySet(this.set.NewScope(scope), array);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x001426EC File Offset: 0x001408EC
		public bool Equals(OrderBySet other)
		{
			bool flag = other != null && this.set.Equals(other.Set) && this.order.Count == other.order.Count;
			int num = 0;
			while (flag && num < this.order.Count)
			{
				flag &= this.order[num].Equals(other.order[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00142765 File Offset: 0x00140965
		public override bool Equals(object other)
		{
			return this.Equals(other as OrderBySet);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x00142774 File Offset: 0x00140974
		public override int GetHashCode()
		{
			int num = this.set.GetHashCode();
			for (int i = 0; i < this.order.Count; i++)
			{
				num = num * 5011 + this.order[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x04003358 RID: 13144
		private readonly Set set;

		// Token: 0x04003359 RID: 13145
		private readonly IList<CubeSortOrder> order;
	}
}
