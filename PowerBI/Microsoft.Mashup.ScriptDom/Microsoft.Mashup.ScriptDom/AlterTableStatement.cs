using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027E RID: 638
	[Serializable]
	internal abstract class AlterTableStatement : TSqlStatement
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x00164A21 File Offset: 0x00162C21
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x00164A29 File Offset: 0x00162C29
		public SchemaObjectName SchemaObjectName
		{
			get
			{
				return this._schemaObjectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObjectName = value;
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00164A39 File Offset: 0x00162C39
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B7A RID: 7034
		private SchemaObjectName _schemaObjectName;
	}
}
