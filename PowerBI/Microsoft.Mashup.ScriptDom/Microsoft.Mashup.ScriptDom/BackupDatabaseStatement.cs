using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034B RID: 843
	[Serializable]
	internal class BackupDatabaseStatement : BackupStatement
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x00169886 File Offset: 0x00167A86
		public IList<BackupRestoreFileInfo> Files
		{
			get
			{
				return this._files;
			}
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x0016988E File Offset: 0x00167A8E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0016989C File Offset: 0x00167A9C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Files.Count;
			while (i < count)
			{
				this.Files[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001CDB RID: 7387
		private List<BackupRestoreFileInfo> _files = new List<BackupRestoreFileInfo>();
	}
}
