using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000338 RID: 824
	[Serializable]
	internal class FileStreamDatabaseOption : DatabaseOption
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00168C92 File Offset: 0x00166E92
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x00168C9A File Offset: 0x00166E9A
		public NonTransactedFileStreamAccess? NonTransactedAccess
		{
			get
			{
				return this._nonTransactedAccess;
			}
			set
			{
				this._nonTransactedAccess = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x00168CA3 File Offset: 0x00166EA3
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x00168CAB File Offset: 0x00166EAB
		public Literal DirectoryName
		{
			get
			{
				return this._directoryName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._directoryName = value;
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00168CBB File Offset: 0x00166EBB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00168CC7 File Offset: 0x00166EC7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DirectoryName != null)
			{
				this.DirectoryName.Accept(visitor);
			}
		}

		// Token: 0x04001C9D RID: 7325
		private NonTransactedFileStreamAccess? _nonTransactedAccess;

		// Token: 0x04001C9E RID: 7326
		private Literal _directoryName;
	}
}
