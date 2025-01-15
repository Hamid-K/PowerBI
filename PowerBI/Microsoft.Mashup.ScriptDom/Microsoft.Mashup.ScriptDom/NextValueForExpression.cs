using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	internal class NextValueForExpression : PrimaryExpression
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x001642CD File Offset: 0x001624CD
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x001642D5 File Offset: 0x001624D5
		public SchemaObjectName SequenceName
		{
			get
			{
				return this._sequenceName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sequenceName = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x001642E5 File Offset: 0x001624E5
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x001642ED File Offset: 0x001624ED
		public OverClause OverClause
		{
			get
			{
				return this._overClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._overClause = value;
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x001642FD File Offset: 0x001624FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00164309 File Offset: 0x00162509
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SequenceName != null)
			{
				this.SequenceName.Accept(visitor);
			}
			if (this.OverClause != null)
			{
				this.OverClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B5E RID: 7006
		private SchemaObjectName _sequenceName;

		// Token: 0x04001B5F RID: 7007
		private OverClause _overClause;
	}
}
