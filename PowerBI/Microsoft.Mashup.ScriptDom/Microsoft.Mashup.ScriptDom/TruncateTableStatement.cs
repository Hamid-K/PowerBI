using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000303 RID: 771
	[Serializable]
	internal class TruncateTableStatement : TSqlStatement
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00167AF9 File Offset: 0x00165CF9
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x00167B01 File Offset: 0x00165D01
		public SchemaObjectName TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableName = value;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00167B11 File Offset: 0x00165D11
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00167B1D File Offset: 0x00165D1D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.TableName != null)
			{
				this.TableName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C47 RID: 7239
		private SchemaObjectName _tableName;
	}
}
