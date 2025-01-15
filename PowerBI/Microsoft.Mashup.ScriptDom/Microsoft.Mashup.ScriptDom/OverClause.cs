using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	internal class OverClause : TSqlFragment
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x0016229D File Offset: 0x0016049D
		public IList<ScalarExpression> Partitions
		{
			get
			{
				return this._partitions;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x001622A5 File Offset: 0x001604A5
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x001622AD File Offset: 0x001604AD
		public OrderByClause OrderByClause
		{
			get
			{
				return this._orderByClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._orderByClause = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x001622BD File Offset: 0x001604BD
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x001622C5 File Offset: 0x001604C5
		public WindowFrameClause WindowFrameClause
		{
			get
			{
				return this._windowFrameClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._windowFrameClause = value;
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x001622D5 File Offset: 0x001604D5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x001622E4 File Offset: 0x001604E4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Partitions.Count;
			while (i < count)
			{
				this.Partitions[i].Accept(visitor);
				i++;
			}
			if (this.OrderByClause != null)
			{
				this.OrderByClause.Accept(visitor);
			}
			if (this.WindowFrameClause != null)
			{
				this.WindowFrameClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ADD RID: 6877
		private List<ScalarExpression> _partitions = new List<ScalarExpression>();

		// Token: 0x04001ADE RID: 6878
		private OrderByClause _orderByClause;

		// Token: 0x04001ADF RID: 6879
		private WindowFrameClause _windowFrameClause;
	}
}
