using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000289 RID: 649
	[Serializable]
	internal class AlterTableAddTableElementStatement : AlterTableStatement
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x00164D6E File Offset: 0x00162F6E
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x00164D76 File Offset: 0x00162F76
		public ConstraintEnforcement ExistingRowsCheckEnforcement
		{
			get
			{
				return this._existingRowsCheckEnforcement;
			}
			set
			{
				this._existingRowsCheckEnforcement = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x00164D7F File Offset: 0x00162F7F
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x00164D87 File Offset: 0x00162F87
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

		// Token: 0x0600273F RID: 10047 RVA: 0x00164D97 File Offset: 0x00162F97
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00164DA3 File Offset: 0x00162FA3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			if (this.Definition != null)
			{
				this.Definition.Accept(visitor);
			}
		}

		// Token: 0x04001B87 RID: 7047
		private ConstraintEnforcement _existingRowsCheckEnforcement;

		// Token: 0x04001B88 RID: 7048
		private TableDefinition _definition;
	}
}
