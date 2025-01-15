using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	internal class SubqueryComparisonPredicate : BooleanExpression
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00160716 File Offset: 0x0015E916
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x0016071E File Offset: 0x0015E91E
		public ScalarExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expression = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0016072E File Offset: 0x0015E92E
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x00160736 File Offset: 0x0015E936
		public BooleanComparisonType ComparisonType
		{
			get
			{
				return this._comparisonType;
			}
			set
			{
				this._comparisonType = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0016073F File Offset: 0x0015E93F
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x00160747 File Offset: 0x0015E947
		public ScalarSubquery Subquery
		{
			get
			{
				return this._subquery;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._subquery = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x00160757 File Offset: 0x0015E957
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x0016075F File Offset: 0x0015E95F
		public SubqueryComparisonPredicateType SubqueryComparisonPredicateType
		{
			get
			{
				return this._subqueryComparisonPredicateType;
			}
			set
			{
				this._subqueryComparisonPredicateType = value;
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00160768 File Offset: 0x0015E968
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00160774 File Offset: 0x0015E974
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			if (this.Subquery != null)
			{
				this.Subquery.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A66 RID: 6758
		private ScalarExpression _expression;

		// Token: 0x04001A67 RID: 6759
		private BooleanComparisonType _comparisonType;

		// Token: 0x04001A68 RID: 6760
		private ScalarSubquery _subquery;

		// Token: 0x04001A69 RID: 6761
		private SubqueryComparisonPredicateType _subqueryComparisonPredicateType;
	}
}
