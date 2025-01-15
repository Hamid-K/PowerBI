using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	internal class XmlNamespacesAliasElement : XmlNamespacesElement
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0015FE1F File Offset: 0x0015E01F
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x0015FE27 File Offset: 0x0015E027
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

		// Token: 0x060022DA RID: 8922 RVA: 0x0015FE37 File Offset: 0x0015E037
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0015FE43 File Offset: 0x0015E043
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
		}

		// Token: 0x04001A45 RID: 6725
		private Identifier _identifier;
	}
}
