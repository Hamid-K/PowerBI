using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015C RID: 348
	internal sealed class FindFolderResponse : ServiceResponse
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x00030194 File Offset: 0x0002F194
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "RootFolder");
			this.results.TotalCount = reader.ReadAttributeValue<int>("TotalItemsInView");
			this.results.MoreAvailable = !reader.ReadAttributeValue<bool>("IncludesLastItemInRange");
			this.results.NextPageOffset = (this.results.MoreAvailable ? reader.ReadNullableAttributeValue<int>("IndexedPagingOffset") : default(int?));
			reader.ReadStartElement(XmlNamespace.Types, "Folders");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						Folder folder = EwsUtilities.CreateEwsObjectFromXmlElementName<Folder>(reader.Service, reader.LocalName);
						if (folder == null)
						{
							reader.SkipCurrentElement();
						}
						else
						{
							folder.LoadFromXml(reader, true, this.propertySet, true);
							this.results.Folders.Add(folder);
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, "Folders"));
			}
			reader.ReadEndElement(XmlNamespace.Messages, "RootFolder");
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00030298 File Offset: 0x0002F298
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			JsonObject jsonObject = responseObject.ReadAsJsonObject("RootFolder");
			this.results.TotalCount = jsonObject.ReadAsInt("TotalItemsInView");
			this.results.MoreAvailable = jsonObject.ReadAsBool("IncludesLastItemInRange");
			if (this.results.MoreAvailable)
			{
				if (jsonObject.ContainsKey("IndexedPagingOffset"))
				{
					this.results.NextPageOffset = new int?(jsonObject.ReadAsInt("IndexedPagingOffset"));
				}
				else
				{
					this.results.NextPageOffset = default(int?);
				}
			}
			if (jsonObject.ContainsKey("Folders"))
			{
				List<Folder> list = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Folder>(jsonObject, "Folders", new GetObjectInstanceDelegate<Folder>(this.CreateFolderInstance), true, this.propertySet, true);
				list.ForEach(delegate(Folder folder)
				{
					this.results.Folders.Add(folder);
				});
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00030374 File Offset: 0x0002F374
		private Folder CreateFolderInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Folder>(service, xmlElementName);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003037D File Offset: 0x0002F37D
		internal FindFolderResponse(PropertySet propertySet)
		{
			this.propertySet = propertySet;
			EwsUtilities.Assert(this.propertySet != null, "FindFolderResponse.ctor", "PropertySet should not be null");
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000303B2 File Offset: 0x0002F3B2
		public FindFoldersResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x040009A2 RID: 2466
		private FindFoldersResults results = new FindFoldersResults();

		// Token: 0x040009A3 RID: 2467
		private PropertySet propertySet;
	}
}
