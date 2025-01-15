using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A5 RID: 933
	[Serializable]
	internal class BinaryExpression : ScalarExpression
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x0016BE9E File Offset: 0x0016A09E
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x0016BEA6 File Offset: 0x0016A0A6
		public BinaryExpressionType BinaryExpressionType
		{
			get
			{
				return this._binaryExpressionType;
			}
			set
			{
				this._binaryExpressionType = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x0016BEAF File Offset: 0x0016A0AF
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x0016BEB7 File Offset: 0x0016A0B7
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x0016BEC7 File Offset: 0x0016A0C7
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x0016BECF File Offset: 0x0016A0CF
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

		// Token: 0x06002E24 RID: 11812 RVA: 0x0016BEDF File Offset: 0x0016A0DF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x0016BEEB File Offset: 0x0016A0EB
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

		// Token: 0x04001D85 RID: 7557
		private BinaryExpressionType _binaryExpressionType;

		// Token: 0x04001D86 RID: 7558
		private ScalarExpression _firstExpression;

		// Token: 0x04001D87 RID: 7559
		private ScalarExpression _secondExpression;
	}
}
