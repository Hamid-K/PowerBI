using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040A RID: 1034
	[Serializable]
	internal class DropQueueStatement : TSqlStatement
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x0016E419 File Offset: 0x0016C619
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x0016E421 File Offset: 0x0016C621
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

		// Token: 0x06003070 RID: 12400 RVA: 0x0016E431 File Offset: 0x0016C631
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0016E43D File Offset: 0x0016C63D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E26 RID: 7718
		private SchemaObjectName _name;
	}
}
