using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000433 RID: 1075
	[Serializable]
	internal class MergeActionClause : TSqlFragment
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x0016F38D File Offset: 0x0016D58D
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x0016F395 File Offset: 0x0016D595
		public MergeCondition Condition
		{
			get
			{
				return this._condition;
			}
			set
			{
				this._condition = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x0016F39E File Offset: 0x0016D59E
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x0016F3A6 File Offset: 0x0016D5A6
		public BooleanExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x0016F3B6 File Offset: 0x0016D5B6
		// (set) Token: 0x06003170 RID: 12656 RVA: 0x0016F3BE File Offset: 0x0016D5BE
		public MergeAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0016F3CE File Offset: 0x0016D5CE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x0016F3DA File Offset: 0x0016D5DA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E6D RID: 7789
		private MergeCondition _condition;

		// Token: 0x04001E6E RID: 7790
		private BooleanExpression _searchCondition;

		// Token: 0x04001E6F RID: 7791
		private MergeAction _action;
	}
}
