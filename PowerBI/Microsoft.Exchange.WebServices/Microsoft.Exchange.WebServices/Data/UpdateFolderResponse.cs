using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000188 RID: 392
	internal sealed class UpdateFolderResponse : ServiceResponse
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x0003246E File Offset: 0x0003146E
		internal UpdateFolderResponse(Folder folder)
		{
			EwsUtilities.Assert(folder != null, "UpdateFolderResponse.ctor", "folder is null");
			this.folder = folder;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00032493 File Offset: 0x00031493
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadServiceObjectsCollectionFromXml<Folder>("Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), false, null, false);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000324B7 File Offset: 0x000314B7
		internal override void Loaded()
		{
			if (base.Result == ServiceResult.Success)
			{
				this.folder.ClearChangeLog();
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000324CC File Offset: 0x000314CC
		private Folder GetObjectInstance(ExchangeService session, string xmlElementName)
		{
			return this.folder;
		}

		// Token: 0x040009DF RID: 2527
		private Folder folder;
	}
}
