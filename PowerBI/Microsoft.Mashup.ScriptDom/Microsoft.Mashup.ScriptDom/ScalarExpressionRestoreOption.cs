using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034F RID: 847
	[Serializable]
	internal class ScalarExpressionRestoreOption : RestoreOption
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x00169A5C File Offset: 0x00167C5C
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x00169A64 File Offset: 0x00167C64
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00169A74 File Offset: 0x00167C74
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00169A80 File Offset: 0x00167C80
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001CE2 RID: 7394
		private ScalarExpression _value;
	}
}
