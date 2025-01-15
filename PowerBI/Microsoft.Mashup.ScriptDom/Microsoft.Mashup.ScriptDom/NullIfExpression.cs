using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000202 RID: 514
	[Serializable]
	internal class NullIfExpression : PrimaryExpression
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0016125D File Offset: 0x0015F45D
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x00161265 File Offset: 0x0015F465
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00161275 File Offset: 0x0015F475
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0016127D File Offset: 0x0015F47D
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

		// Token: 0x060023FA RID: 9210 RVA: 0x0016128D File Offset: 0x0015F48D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00161299 File Offset: 0x0015F499
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FirstExpression != null)
			{
				this.FirstExpression.Accept(visitor);
			}
			if (this.SecondExpression != null)
			{
				this.SecondExpression.Accept(visitor);
			}
		}

		// Token: 0x04001A96 RID: 6806
		private ScalarExpression _firstExpression;

		// Token: 0x04001A97 RID: 6807
		private ScalarExpression _secondExpression;
	}
}
