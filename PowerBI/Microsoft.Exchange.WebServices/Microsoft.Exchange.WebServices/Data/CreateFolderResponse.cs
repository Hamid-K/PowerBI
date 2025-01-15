using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000150 RID: 336
	internal sealed class CreateFolderResponse : ServiceResponse
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0002FA1E File Offset: 0x0002EA1E
		internal CreateFolderResponse(Folder folder)
		{
			this.folder = folder;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002FA2D File Offset: 0x0002EA2D
		private Folder GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			if (this.folder != null)
			{
				return this.folder;
			}
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Folder>(service, xmlElementName);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002FA48 File Offset: 0x0002EA48
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Folder> list = reader.ReadServiceObjectsCollectionFromXml<Folder>("Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), false, null, false);
			this.folder = list[0];
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002FA84 File Offset: 0x0002EA84
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			List<Folder> list = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Folder>(responseObject, "Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), false, null, false);
			this.folder = list[0];
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002FAC7 File Offset: 0x0002EAC7
		internal override void Loaded()
		{
			if (base.Result == ServiceResult.Success)
			{
				this.folder.ClearChangeLog();
			}
		}

		// Token: 0x04000995 RID: 2453
		private Folder folder;
	}
}
