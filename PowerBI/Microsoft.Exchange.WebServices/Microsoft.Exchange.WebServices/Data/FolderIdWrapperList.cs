using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000296 RID: 662
	internal class FolderIdWrapperList : IEnumerable<AbstractFolderIdWrapper>, IEnumerable
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x0003F7EF File Offset: 0x0003E7EF
		internal void Add(Folder folder)
		{
			this.ids.Add(new FolderWrapper(folder));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0003F804 File Offset: 0x0003E804
		internal void AddRange(IEnumerable<Folder> folders)
		{
			if (folders != null)
			{
				foreach (Folder folder in folders)
				{
					this.Add(folder);
				}
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003F850 File Offset: 0x0003E850
		internal void Add(FolderId folderId)
		{
			this.ids.Add(new FolderIdWrapper(folderId));
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0003F864 File Offset: 0x0003E864
		internal void AddRange(IEnumerable<FolderId> folderIds)
		{
			if (folderIds != null)
			{
				foreach (FolderId folderId in folderIds)
				{
					this.Add(folderId);
				}
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0003F8B0 File Offset: 0x0003E8B0
		internal void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace ewsNamesapce, string xmlElementName)
		{
			if (this.Count > 0)
			{
				writer.WriteStartElement(ewsNamesapce, xmlElementName);
				foreach (AbstractFolderIdWrapper abstractFolderIdWrapper in this.ids)
				{
					abstractFolderIdWrapper.WriteToXml(writer);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0003F91C File Offset: 0x0003E91C
		internal object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (AbstractFolderIdWrapper abstractFolderIdWrapper in this.ids)
			{
				list.Add(((IJsonSerializable)abstractFolderIdWrapper).ToJson(service));
			}
			return list.ToArray();
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0003F984 File Offset: 0x0003E984
		internal int Count
		{
			get
			{
				return this.ids.Count;
			}
		}

		// Token: 0x170005B0 RID: 1456
		internal AbstractFolderIdWrapper this[int index]
		{
			get
			{
				return this.ids[index];
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0003F9A0 File Offset: 0x0003E9A0
		internal void Validate(ExchangeVersion version)
		{
			foreach (AbstractFolderIdWrapper abstractFolderIdWrapper in this.ids)
			{
				abstractFolderIdWrapper.Validate(version);
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0003F9F4 File Offset: 0x0003E9F4
		public IEnumerator<AbstractFolderIdWrapper> GetEnumerator()
		{
			return this.ids.GetEnumerator();
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0003FA06 File Offset: 0x0003EA06
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ids.GetEnumerator();
		}

		// Token: 0x04001351 RID: 4945
		private List<AbstractFolderIdWrapper> ids = new List<AbstractFolderIdWrapper>();
	}
}
