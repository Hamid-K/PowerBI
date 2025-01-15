using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000370 RID: 880
	[Serializable]
	internal class AlterCertificateStatement : CertificateStatementBase
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x0016A8F8 File Offset: 0x00168AF8
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x0016A900 File Offset: 0x00168B00
		public AlterCertificateStatementKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x0016A909 File Offset: 0x00168B09
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x0016A911 File Offset: 0x00168B11
		public Literal AttestedBy
		{
			get
			{
				return this._attestedBy;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._attestedBy = value;
			}
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0016A921 File Offset: 0x00168B21
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0016A92D File Offset: 0x00168B2D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.AttestedBy != null)
			{
				this.AttestedBy.Accept(visitor);
			}
		}

		// Token: 0x04001D24 RID: 7460
		private AlterCertificateStatementKind _kind;

		// Token: 0x04001D25 RID: 7461
		private Literal _attestedBy;
	}
}
