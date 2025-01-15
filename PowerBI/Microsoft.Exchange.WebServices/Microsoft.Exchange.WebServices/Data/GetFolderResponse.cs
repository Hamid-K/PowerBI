using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000169 RID: 361
	public sealed class GetFolderResponse : ServiceResponse
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x000311EA File Offset: 0x000301EA
		internal GetFolderResponse(Folder folder, PropertySet propertySet)
		{
			this.folder = folder;
			this.propertySet = propertySet;
			EwsUtilities.Assert(this.propertySet != null, "GetFolderResponse.ctor", "PropertySet should not be null");
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003121C File Offset: 0x0003021C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Folder> list = reader.ReadServiceObjectsCollectionFromXml<Folder>("Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), true, this.propertySet, false);
			this.folder = list[0];
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00031260 File Offset: 0x00030260
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			List<Folder> list = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Folder>(responseObject, "Folders", new GetObjectInstanceDelegate<Folder>(this.GetObjectInstance), true, this.propertySet, false);
			this.folder = list[0];
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000312A8 File Offset: 0x000302A8
		private Folder GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			if (this.Folder != null)
			{
				return this.Folder;
			}
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Folder>(service, xmlElementName);
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x000312C0 File Offset: 0x000302C0
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040009C0 RID: 2496
		private Folder folder;

		// Token: 0x040009C1 RID: 2497
		private PropertySet propertySet;
	}
}
