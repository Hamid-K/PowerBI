using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027C RID: 636
	[Serializable]
	internal class DropXmlSchemaCollectionStatement : TSqlStatement
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00164963 File Offset: 0x00162B63
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0016496B File Offset: 0x00162B6B
		public SchemaObjectName Name
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

		// Token: 0x060026FA RID: 9978 RVA: 0x0016497B File Offset: 0x00162B7B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00164987 File Offset: 0x00162B87
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B77 RID: 7031
		private SchemaObjectName _name;
	}
}
