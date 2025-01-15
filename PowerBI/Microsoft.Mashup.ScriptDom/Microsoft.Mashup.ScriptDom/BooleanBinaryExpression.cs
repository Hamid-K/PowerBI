using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B1 RID: 945
	[Serializable]
	internal class BooleanBinaryExpression : BooleanExpression
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x0016C50B File Offset: 0x0016A70B
		// (set) Token: 0x06002E78 RID: 11896 RVA: 0x0016C513 File Offset: 0x0016A713
		public BooleanBinaryExpressionType BinaryExpressionType
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x0016C51C File Offset: 0x0016A71C
		// (set) Token: 0x06002E7A RID: 11898 RVA: 0x0016C524 File Offset: 0x0016A724
		public BooleanExpression FirstExpression
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x0016C534 File Offset: 0x0016A734
		// (set) Token: 0x06002E7C RID: 11900 RVA: 0x0016C53C File Offset: 0x0016A73C
		public BooleanExpression SecondExpression
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

		// Token: 0x06002E7D RID: 11901 RVA: 0x0016C54C File Offset: 0x0016A74C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0016C558 File Offset: 0x0016A758
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

		// Token: 0x04001DA3 RID: 7587
		private BooleanBinaryExpressionType _binaryExpressionType;

		// Token: 0x04001DA4 RID: 7588
		private BooleanExpression _firstExpression;

		// Token: 0x04001DA5 RID: 7589
		private BooleanExpression _secondExpression;
	}
}
