using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C1 RID: 961
	[Serializable]
	internal class OrderByClause : TSqlFragment
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x0016CB59 File Offset: 0x0016AD59
		public IList<ExpressionWithSortOrder> OrderByElements
		{
			get
			{
				return this._orderByElements;
			}
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0016CB61 File Offset: 0x0016AD61
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0016CB70 File Offset: 0x0016AD70
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.OrderByElements.Count;
			while (i < count)
			{
				this.OrderByElements[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DBB RID: 7611
		private List<ExpressionWithSortOrder> _orderByElements = new List<ExpressionWithSortOrder>();
	}
}
