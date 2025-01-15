using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000352 RID: 850
	[Serializable]
	internal class FileStreamRestoreOption : RestoreOption
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x00169BA0 File Offset: 0x00167DA0
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x00169BA8 File Offset: 0x00167DA8
		public FileStreamDatabaseOption FileStreamOption
		{
			get
			{
				return this._fileStreamOption;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileStreamOption = value;
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00169BB8 File Offset: 0x00167DB8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00169BC4 File Offset: 0x00167DC4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileStreamOption != null)
			{
				this.FileStreamOption.Accept(visitor);
			}
		}

		// Token: 0x04001CE8 RID: 7400
		private FileStreamDatabaseOption _fileStreamOption;
	}
}
