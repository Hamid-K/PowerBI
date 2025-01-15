using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000218 RID: 536
	[Serializable]
	internal class UserDefinedTypeCallTarget : CallTarget
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x001620C4 File Offset: 0x001602C4
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x001620CC File Offset: 0x001602CC
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

		// Token: 0x060024B4 RID: 9396 RVA: 0x001620DC File Offset: 0x001602DC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x001620E8 File Offset: 0x001602E8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AD7 RID: 6871
		private SchemaObjectName _schemaObjectName;
	}
}
