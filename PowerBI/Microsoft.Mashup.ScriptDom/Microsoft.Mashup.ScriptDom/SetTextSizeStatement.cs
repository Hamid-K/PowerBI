using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030E RID: 782
	[Serializable]
	internal class SetTextSizeStatement : TSqlStatement
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00167D63 File Offset: 0x00165F63
		// (set) Token: 0x06002A31 RID: 10801 RVA: 0x00167D6B File Offset: 0x00165F6B
		public ScalarExpression TextSize
		{
			get
			{
				return this._textSize;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._textSize = value;
			}
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00167D7B File Offset: 0x00165F7B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00167D87 File Offset: 0x00165F87
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.TextSize != null)
			{
				this.TextSize.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C52 RID: 7250
		private ScalarExpression _textSize;
	}
}
