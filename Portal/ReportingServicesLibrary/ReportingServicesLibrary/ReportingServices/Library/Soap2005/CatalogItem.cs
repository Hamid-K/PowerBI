using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x020002FF RID: 767
	public class CatalogItem
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x0006D4F8 File Offset: 0x0006B6F8
		public CatalogItem()
		{
			this.ID = null;
			this.Name = null;
			this.Path = null;
			this.VirtualPath = null;
			this.Type = ItemTypeEnum.Unknown;
			this.Size = 0;
			this.SizeSpecified = false;
			this.Description = null;
			this.Hidden = false;
			this.HiddenSpecified = false;
			this.CreationDate = DateTime.MinValue;
			this.CreationDateSpecified = false;
			this.ModifiedDate = DateTime.MinValue;
			this.ModifiedDateSpecified = false;
			this.CreatedBy = null;
			this.ModifiedBy = null;
			this.MimeType = null;
			this.ExecutionDate = DateTime.MinValue;
			this.ExecutionDateSpecified = false;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006D59C File Offset: 0x0006B79C
		internal static ItemTypeEnum ItemTypeToSoapType(ItemType type)
		{
			switch (type)
			{
			case ItemType.Unknown:
				return ItemTypeEnum.Unknown;
			case ItemType.Folder:
				return ItemTypeEnum.Folder;
			case ItemType.Report:
				return ItemTypeEnum.Report;
			case ItemType.Resource:
				return ItemTypeEnum.Resource;
			case ItemType.LinkedReport:
				return ItemTypeEnum.LinkedReport;
			case ItemType.DataSource:
				return ItemTypeEnum.DataSource;
			case ItemType.Model:
				return ItemTypeEnum.Model;
			default:
				return ItemTypeEnum.Unknown;
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006D5D4 File Offset: 0x0006B7D4
		internal static CatalogItem[] CatalogItemsToThisArray(CatalogItemList items)
		{
			if (items == null)
			{
				return null;
			}
			CatalogItem[] array = new CatalogItem[items.Count];
			for (int i = 0; i < items.Count; i++)
			{
				array[i] = CatalogItem.CatalogItemToThis(items[i]);
			}
			return array;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006D614 File Offset: 0x0006B814
		internal static CatalogItem CatalogItemToThis(CatalogItemDescriptor item)
		{
			if (item == null)
			{
				return null;
			}
			return new CatalogItem
			{
				CreatedBy = item.CreatedBy,
				CreationDate = item.CreationDate,
				CreationDateSpecified = (item.CreationDate != DateTime.MinValue),
				Description = item.Description,
				ExecutionDate = item.ExecutionDate,
				ExecutionDateSpecified = (item.ExecutionDate != DateTime.MinValue),
				Hidden = item.Hidden,
				HiddenSpecified = item.Hidden,
				ID = item.ID,
				MimeType = item.MimeType,
				ModifiedBy = item.ModifiedBy,
				ModifiedDate = item.ModifiedDate,
				ModifiedDateSpecified = (item.ModifiedDate != DateTime.MinValue),
				Name = item.Name,
				Path = item.Path.Value,
				Size = item.Size,
				SizeSpecified = (item.Size >= 0),
				Type = CatalogItem.ItemTypeToSoapType(item.Type),
				VirtualPath = item.VirtualPath
			};
		}

		// Token: 0x04000A3A RID: 2618
		public string ID;

		// Token: 0x04000A3B RID: 2619
		public string Name;

		// Token: 0x04000A3C RID: 2620
		public string Path;

		// Token: 0x04000A3D RID: 2621
		public string VirtualPath;

		// Token: 0x04000A3E RID: 2622
		public ItemTypeEnum Type;

		// Token: 0x04000A3F RID: 2623
		public int Size;

		// Token: 0x04000A40 RID: 2624
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x04000A41 RID: 2625
		public string Description;

		// Token: 0x04000A42 RID: 2626
		public bool Hidden;

		// Token: 0x04000A43 RID: 2627
		[XmlIgnore]
		public bool HiddenSpecified;

		// Token: 0x04000A44 RID: 2628
		public DateTime CreationDate;

		// Token: 0x04000A45 RID: 2629
		[XmlIgnore]
		public bool CreationDateSpecified;

		// Token: 0x04000A46 RID: 2630
		public DateTime ModifiedDate;

		// Token: 0x04000A47 RID: 2631
		[XmlIgnore]
		public bool ModifiedDateSpecified;

		// Token: 0x04000A48 RID: 2632
		public string CreatedBy;

		// Token: 0x04000A49 RID: 2633
		public string ModifiedBy;

		// Token: 0x04000A4A RID: 2634
		public string MimeType;

		// Token: 0x04000A4B RID: 2635
		public DateTime ExecutionDate;

		// Token: 0x04000A4C RID: 2636
		[XmlIgnore]
		public bool ExecutionDateSpecified;
	}
}
