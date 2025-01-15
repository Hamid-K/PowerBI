using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	internal class TSEqualCall : BooleanExpression
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0016406F File Offset: 0x0016226F
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x00164077 File Offset: 0x00162277
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x00164087 File Offset: 0x00162287
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x0016408F File Offset: 0x0016228F
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

		// Token: 0x06002672 RID: 9842 RVA: 0x0016409F File Offset: 0x0016229F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x001640AB File Offset: 0x001622AB
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

		// Token: 0x04001B53 RID: 6995
		private ScalarExpression _firstExpression;

		// Token: 0x04001B54 RID: 6996
		private ScalarExpression _secondExpression;
	}
}
