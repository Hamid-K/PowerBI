using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000421 RID: 1057
	[Serializable]
	internal class BackupCertificateStatement : CertificateStatementBase
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x0016EEA6 File Offset: 0x0016D0A6
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x0016EEAE File Offset: 0x0016D0AE
		public Literal File
		{
			get
			{
				return this._file;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._file = value;
			}
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0016EEBE File Offset: 0x0016D0BE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0016EECA File Offset: 0x0016D0CA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E58 RID: 7768
		private Literal _file;
	}
}
