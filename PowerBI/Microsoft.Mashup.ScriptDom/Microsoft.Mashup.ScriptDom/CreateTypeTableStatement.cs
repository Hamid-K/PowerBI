using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000438 RID: 1080
	[Serializable]
	internal class CreateTypeTableStatement : CreateTypeStatement
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x0016F539 File Offset: 0x0016D739
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x0016F541 File Offset: 0x0016D741
		public TableDefinition Definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._definition = value;
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x0016F551 File Offset: 0x0016D751
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x0016F55D File Offset: 0x0016D75D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Definition != null)
			{
				this.Definition.Accept(visitor);
			}
		}

		// Token: 0x04001E73 RID: 7795
		private TableDefinition _definition;
	}
}
