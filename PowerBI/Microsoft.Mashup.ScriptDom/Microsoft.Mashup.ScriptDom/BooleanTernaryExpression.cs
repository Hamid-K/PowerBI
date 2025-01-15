using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CF RID: 975
	[Serializable]
	internal class BooleanTernaryExpression : BooleanExpression
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x0016D1FE File Offset: 0x0016B3FE
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x0016D206 File Offset: 0x0016B406
		public BooleanTernaryExpressionType TernaryExpressionType
		{
			get
			{
				return this._ternaryExpressionType;
			}
			set
			{
				this._ternaryExpressionType = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x0016D20F File Offset: 0x0016B40F
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x0016D217 File Offset: 0x0016B417
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x0016D227 File Offset: 0x0016B427
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x0016D22F File Offset: 0x0016B42F
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x0016D23F File Offset: 0x0016B43F
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x0016D247 File Offset: 0x0016B447
		public ScalarExpression ThirdExpression
		{
			get
			{
				return this._thirdExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._thirdExpression = value;
			}
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0016D257 File Offset: 0x0016B457
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0016D264 File Offset: 0x0016B464
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
			if (this.ThirdExpression != null)
			{
				this.ThirdExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DD8 RID: 7640
		private BooleanTernaryExpressionType _ternaryExpressionType;

		// Token: 0x04001DD9 RID: 7641
		private ScalarExpression _firstExpression;

		// Token: 0x04001DDA RID: 7642
		private ScalarExpression _secondExpression;

		// Token: 0x04001DDB RID: 7643
		private ScalarExpression _thirdExpression;
	}
}
