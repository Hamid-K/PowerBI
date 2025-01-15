using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	internal class FunctionCallSetClause : SetClause
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x00163E14 File Offset: 0x00162014
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x00163E1C File Offset: 0x0016201C
		public FunctionCall MutatorFunction
		{
			get
			{
				return this._mutatorFunction;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._mutatorFunction = value;
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00163E2C File Offset: 0x0016202C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00163E38 File Offset: 0x00162038
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.MutatorFunction != null)
			{
				this.MutatorFunction.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B4B RID: 6987
		private FunctionCall _mutatorFunction;
	}
}
