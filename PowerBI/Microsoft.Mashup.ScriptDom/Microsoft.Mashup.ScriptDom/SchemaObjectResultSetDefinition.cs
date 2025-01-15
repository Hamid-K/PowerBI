using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	internal class SchemaObjectResultSetDefinition : ResultSetDefinition
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0015EF56 File Offset: 0x0015D156
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x0015EF5E File Offset: 0x0015D15E
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

		// Token: 0x0600220D RID: 8717 RVA: 0x0015EF6E File Offset: 0x0015D16E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0015EF7A File Offset: 0x0015D17A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
		}

		// Token: 0x04001A09 RID: 6665
		private SchemaObjectName _name;
	}
}
