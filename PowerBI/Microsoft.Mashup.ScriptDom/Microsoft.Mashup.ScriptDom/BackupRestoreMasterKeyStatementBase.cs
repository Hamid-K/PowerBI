using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000422 RID: 1058
	[Serializable]
	internal abstract class BackupRestoreMasterKeyStatementBase : TSqlStatement
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x0016EEEF File Offset: 0x0016D0EF
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x0016EEF7 File Offset: 0x0016D0F7
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0016EF07 File Offset: 0x0016D107
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x0016EF0F File Offset: 0x0016D10F
		public Literal Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0016EF1F File Offset: 0x0016D11F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E59 RID: 7769
		private Literal _file;

		// Token: 0x04001E5A RID: 7770
		private Literal _password;
	}
}
