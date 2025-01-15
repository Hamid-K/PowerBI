using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F7 RID: 759
	[Serializable]
	internal class DropSchemaStatement : TSqlStatement
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x0016763C File Offset: 0x0016583C
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x00167644 File Offset: 0x00165844
		public SchemaObjectName Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schema = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x00167654 File Offset: 0x00165854
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x0016765C File Offset: 0x0016585C
		public DropSchemaBehavior DropBehavior
		{
			get
			{
				return this._dropBehavior;
			}
			set
			{
				this._dropBehavior = value;
			}
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00167665 File Offset: 0x00165865
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00167671 File Offset: 0x00165871
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Schema != null)
			{
				this.Schema.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C30 RID: 7216
		private SchemaObjectName _schema;

		// Token: 0x04001C31 RID: 7217
		private DropSchemaBehavior _dropBehavior;
	}
}
