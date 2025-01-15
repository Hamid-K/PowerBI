using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FD RID: 1021
	[Serializable]
	internal class IdentifierPrincipalOption : PrincipalOption
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0016E00E File Offset: 0x0016C20E
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x0016E016 File Offset: 0x0016C216
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

		// Token: 0x0600302A RID: 12330 RVA: 0x0016E026 File Offset: 0x0016C226
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0016E032 File Offset: 0x0016C232
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
		}

		// Token: 0x04001E15 RID: 7701
		private Identifier _identifier;
	}
}
