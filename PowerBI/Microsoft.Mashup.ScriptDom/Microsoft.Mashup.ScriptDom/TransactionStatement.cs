using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	internal abstract class TransactionStatement : TSqlStatement
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x0016262E File Offset: 0x0016082E
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x00162636 File Offset: 0x00160836
		public IdentifierOrValueExpression Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00162646 File Offset: 0x00160846
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AE8 RID: 6888
		private IdentifierOrValueExpression _name;
	}
}
