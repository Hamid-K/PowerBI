using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B0 RID: 944
	[Serializable]
	internal class BooleanComparisonExpression : BooleanExpression
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x0016C485 File Offset: 0x0016A685
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x0016C48D File Offset: 0x0016A68D
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x0016C496 File Offset: 0x0016A696
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x0016C49E File Offset: 0x0016A69E
		public ScalarExpression FirstExpression
		{
			get
			{
				return this._firstExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._firstExpression = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x0016C4AE File Offset: 0x0016A6AE
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x0016C4B6 File Offset: 0x0016A6B6
		public ScalarExpression SecondExpression
		{
			get
			{
				return this._secondExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondExpression = value;
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0016C4C6 File Offset: 0x0016A6C6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0016C4D2 File Offset: 0x0016A6D2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.FirstExpression != null)
			{
				this.FirstExpression.Accept(visitor);
			}
			if (this.SecondExpression != null)
			{
				this.SecondExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DA0 RID: 7584
		private BooleanComparisonType _comparisonType;

		// Token: 0x04001DA1 RID: 7585
		private ScalarExpression _firstExpression;

		// Token: 0x04001DA2 RID: 7586
		private ScalarExpression _secondExpression;
	}
}
