using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	internal class OdbcConvertSpecification : ScalarExpression
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x001624C2 File Offset: 0x001606C2
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x001624CA File Offset: 0x001606CA
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x001624DA File Offset: 0x001606DA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x001624E6 File Offset: 0x001606E6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AE6 RID: 6886
		private Identifier _identifier;
	}
}
