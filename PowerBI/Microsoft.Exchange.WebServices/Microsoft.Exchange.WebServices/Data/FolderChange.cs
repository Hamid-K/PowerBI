using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000307 RID: 775
	public sealed class FolderChange : Change
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x00049970 File Offset: 0x00048970
		internal FolderChange()
		{
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00049978 File Offset: 0x00048978
		internal override ServiceId CreateId()
		{
			return new FolderId();
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0004997F File Offset: 0x0004897F
		public Folder Folder
		{
			get
			{
				return (Folder)base.ServiceObject;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0004998C File Offset: 0x0004898C
		public FolderId FolderId
		{
			get
			{
				return (FolderId)base.Id;
			}
		}
	}
}
