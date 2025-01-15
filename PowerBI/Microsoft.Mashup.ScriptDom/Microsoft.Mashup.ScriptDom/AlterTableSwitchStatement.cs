using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028B RID: 651
	[Serializable]
	internal class AlterTableSwitchStatement : AlterTableStatement
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00164E7A File Offset: 0x0016307A
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x00164E82 File Offset: 0x00163082
		public ScalarExpression SourcePartitionNumber
		{
			get
			{
				return this._sourcePartitionNumber;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourcePartitionNumber = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00164E92 File Offset: 0x00163092
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x00164E9A File Offset: 0x0016309A
		public ScalarExpression TargetPartitionNumber
		{
			get
			{
				return this._targetPartitionNumber;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._targetPartitionNumber = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00164EAA File Offset: 0x001630AA
		// (set) Token: 0x06002751 RID: 10065 RVA: 0x00164EB2 File Offset: 0x001630B2
		public SchemaObjectName TargetTable
		{
			get
			{
				return this._targetTable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._targetTable = value;
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00164EC2 File Offset: 0x001630C2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00164ED0 File Offset: 0x001630D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			if (this.SourcePartitionNumber != null)
			{
				this.SourcePartitionNumber.Accept(visitor);
			}
			if (this.TargetPartitionNumber != null)
			{
				this.TargetPartitionNumber.Accept(visitor);
			}
			if (this.TargetTable != null)
			{
				this.TargetTable.Accept(visitor);
			}
		}

		// Token: 0x04001B8D RID: 7053
		private ScalarExpression _sourcePartitionNumber;

		// Token: 0x04001B8E RID: 7054
		private ScalarExpression _targetPartitionNumber;

		// Token: 0x04001B8F RID: 7055
		private SchemaObjectName _targetTable;
	}
}
