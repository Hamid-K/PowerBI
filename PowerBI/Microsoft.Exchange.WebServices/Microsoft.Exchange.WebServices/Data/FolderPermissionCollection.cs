using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000063 RID: 99
	public sealed class FolderPermissionCollection : ComplexPropertyCollection<FolderPermission>
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00010E77 File Offset: 0x0000FE77
		internal FolderPermissionCollection(Folder owner)
		{
			this.isCalendarFolder = owner is CalendarFolder;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00010E99 File Offset: 0x0000FE99
		private string InnerCollectionXmlElementName
		{
			get
			{
				if (!this.isCalendarFolder)
				{
					return "Permissions";
				}
				return "CalendarPermissions";
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00010EAE File Offset: 0x0000FEAE
		private string CollectionItemXmlElementName
		{
			get
			{
				if (!this.isCalendarFolder)
				{
					return "Permission";
				}
				return "CalendarPermission";
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00010EC3 File Offset: 0x0000FEC3
		internal override string GetCollectionItemXmlElementName(FolderPermission complexProperty)
		{
			return this.CollectionItemXmlElementName;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010ECC File Offset: 0x0000FECC
		internal override void LoadFromXml(EwsServiceXmlReader reader, string localElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, localElementName);
			reader.ReadStartElement(XmlNamespace.Types, this.InnerCollectionXmlElementName);
			base.LoadFromXml(reader, this.InnerCollectionXmlElementName);
			reader.ReadEndElementIfNecessary(XmlNamespace.Types, this.InnerCollectionXmlElementName);
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Types, "UnknownEntries"))
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Types, "UnknownEntry"))
					{
						this.unknownEntries.Add(reader.ReadElementValue());
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, "UnknownEntries"));
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00010F50 File Offset: 0x0000FF50
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			object[] array = jsonProperty.ReadAsArray(this.InnerCollectionXmlElementName);
			foreach (object obj in array)
			{
				FolderPermission folderPermission = new FolderPermission();
				folderPermission.LoadFromJson(obj as JsonObject, service);
				base.InternalAdd(folderPermission);
			}
			object[] array3 = jsonProperty.ReadAsArray("UnknownEntries");
			foreach (object obj2 in array3)
			{
				this.unknownEntries.Add(obj2 as string);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00010FDC File Offset: 0x0000FFDC
		internal void Validate()
		{
			for (int i = 0; i < base.Items.Count; i++)
			{
				FolderPermission folderPermission = base.Items[i];
				folderPermission.Validate(this.isCalendarFolder, i);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001101C File Offset: 0x0001001C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.InnerCollectionXmlElementName);
			foreach (FolderPermission folderPermission in this)
			{
				folderPermission.WriteToXml(writer, this.GetCollectionItemXmlElementName(folderPermission), this.isCalendarFolder);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011084 File Offset: 0x00010084
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			List<object> list = new List<object>();
			foreach (FolderPermission folderPermission in this)
			{
				list.Add(folderPermission.InternalToJson(service, this.isCalendarFolder));
			}
			jsonObject.AddTypeParameter(this.InnerCollectionXmlElementName);
			jsonObject.Add(this.InnerCollectionXmlElementName, list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011104 File Offset: 0x00010104
		internal override FolderPermission CreateComplexProperty(string xmlElementName)
		{
			return new FolderPermission();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001110B File Offset: 0x0001010B
		internal override FolderPermission CreateDefaultComplexProperty()
		{
			return new FolderPermission();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011112 File Offset: 0x00010112
		public void Add(FolderPermission permission)
		{
			base.InternalAdd(permission);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001111C File Offset: 0x0001011C
		public void AddRange(IEnumerable<FolderPermission> permissions)
		{
			EwsUtilities.ValidateParam(permissions, "permissions");
			foreach (FolderPermission folderPermission in permissions)
			{
				this.Add(folderPermission);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011170 File Offset: 0x00010170
		public void Clear()
		{
			base.InternalClear();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00011178 File Offset: 0x00010178
		public bool Remove(FolderPermission permission)
		{
			return base.InternalRemove(permission);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00011181 File Offset: 0x00010181
		public void RemoveAt(int index)
		{
			base.InternalRemoveAt(index);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001118A File Offset: 0x0001018A
		public Collection<string> UnknownEntries
		{
			get
			{
				return this.unknownEntries;
			}
		}

		// Token: 0x040001A9 RID: 425
		private bool isCalendarFolder;

		// Token: 0x040001AA RID: 426
		private Collection<string> unknownEntries = new Collection<string>();
	}
}
