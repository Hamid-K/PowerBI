using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000297 RID: 663
	internal class FolderWrapper : AbstractFolderIdWrapper
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x0003FA2B File Offset: 0x0003EA2B
		internal FolderWrapper(Folder folder)
		{
			EwsUtilities.Assert(folder != null, "FolderWrapper.ctor", "folder is null");
			EwsUtilities.Assert(!folder.IsNew, "FolderWrapper.ctor", "folder does not have an Id");
			this.folder = folder;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0003FA68 File Offset: 0x0003EA68
		public override Folder GetFolder()
		{
			return this.folder;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0003FA70 File Offset: 0x0003EA70
		internal override void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.folder.Id.WriteToXml(writer);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0003FA83 File Offset: 0x0003EA83
		internal override object InternalToJson(ExchangeService service)
		{
			return this.folder.Id.InternalToJson(service);
		}

		// Token: 0x04001352 RID: 4946
		private Folder folder;
	}
}
