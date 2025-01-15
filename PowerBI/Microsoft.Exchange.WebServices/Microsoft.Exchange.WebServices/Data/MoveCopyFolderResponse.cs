using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017A RID: 378
	public sealed class MoveCopyFolderResponse : ServiceResponse
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x00031AAB File Offset: 0x00030AAB
		internal MoveCopyFolderResponse()
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00031AB3 File Offset: 0x00030AB3
		private Folder GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Folder>(service, xmlElementName);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00031ABC File Offset: 0x00030ABC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Folder> list = reader.ReadServiceObjectsCollectionFromXml<Folder>("Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), false, null, false);
			this.folder = list[0];
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00031AF8 File Offset: 0x00030AF8
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			EwsServiceJsonReader ewsServiceJsonReader = new EwsServiceJsonReader(service);
			List<Folder> list = ewsServiceJsonReader.ReadServiceObjectsCollectionFromJson<Folder>(responseObject, "Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), false, null, false);
			this.folder = list[0];
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00031B35 File Offset: 0x00030B35
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040009D3 RID: 2515
		private Folder folder;
	}
}
