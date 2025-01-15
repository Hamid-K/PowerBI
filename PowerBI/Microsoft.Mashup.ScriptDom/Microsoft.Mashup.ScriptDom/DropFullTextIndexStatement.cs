using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DD RID: 989
	[Serializable]
	internal class DropFullTextIndexStatement : TSqlStatement
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x0016D671 File Offset: 0x0016B871
		// (set) Token: 0x06002F7D RID: 12157 RVA: 0x0016D679 File Offset: 0x0016B879
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

		// Token: 0x06002F7E RID: 12158 RVA: 0x0016D689 File Offset: 0x0016B889
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0016D695 File Offset: 0x0016B895
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.TableName != null)
			{
				this.TableName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DEC RID: 7660
		private SchemaObjectName _tableName;
	}
}
