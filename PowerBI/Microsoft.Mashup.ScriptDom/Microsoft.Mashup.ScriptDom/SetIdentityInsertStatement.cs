using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030F RID: 783
	[Serializable]
	internal class SetIdentityInsertStatement : SetOnOffStatement
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x00167DAC File Offset: 0x00165FAC
		// (set) Token: 0x06002A36 RID: 10806 RVA: 0x00167DB4 File Offset: 0x00165FB4
		public SchemaObjectName Table
		{
			get
			{
				return this._table;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._table = value;
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00167DC4 File Offset: 0x00165FC4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00167DD0 File Offset: 0x00165FD0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Table != null)
			{
				this.Table.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C53 RID: 7251
		private SchemaObjectName _table;
	}
}
