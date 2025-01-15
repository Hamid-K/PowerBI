using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap2006
{
	// Token: 0x020002F9 RID: 761
	public class CatalogItem
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006D030 File Offset: 0x0006B230
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

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006D0D4 File Offset: 0x0006B2D4
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
			case ItemType.DataSource:
				return ItemTypeEnum.DataSource;
			case ItemType.Model:
				return ItemTypeEnum.Model;
			case ItemType.Site:
				return ItemTypeEnum.Site;
			}
			return ItemTypeEnum.Unknown;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006D110 File Offset: 0x0006B310
		internal static CatalogItem[] CatalogItemsToThisArray(CatalogItemList items)
		{
			if (items == null)
			{
				return null;
			}
			CatalogItem[] array = new CatalogItem[items.Count];
			for (int i = 0; i < items.Count; i++)
			{
				array[i] = CatalogItem.CatalogItemDescriptorToThis(items[i]);
			}
			return array;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006D150 File Offset: 0x0006B350
		internal static CatalogItem CatalogItemToThis(CatalogItem item)
		{
			if (item == null)
			{
				return null;
			}
			CatalogItem catalogItem = new CatalogItem();
			catalogItem.CreatedBy = item.CreatedBy;
			catalogItem.CreationDateSpecified = true;
			catalogItem.CreationDate = item.CreationDate;
			catalogItem.Description = item.Properties.Description;
			catalogItem.ExecutionDateSpecified = false;
			catalogItem.HiddenSpecified = false;
			catalogItem.ID = item.ItemID.ToString();
			catalogItem.ModifiedBy = item.ModifiedBy;
			catalogItem.ModifiedDate = item.ModificationDate;
			catalogItem.ModifiedDateSpecified = true;
			catalogItem.Name = item.ItemContext.ItemName;
			catalogItem.Path = item.ItemContext.ItemPath.Value;
			if (item.Content != null)
			{
				catalogItem.Size = item.Content.Length;
				catalogItem.SizeSpecified = true;
			}
			catalogItem.Type = CatalogItem.ItemTypeToSoapType(item.ThisItemType);
			return catalogItem;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006D238 File Offset: 0x0006B438
		internal static CatalogItem CatalogItemDescriptorToThis(CatalogItemDescriptor item)
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

		// Token: 0x04000A0C RID: 2572
		public string ID;

		// Token: 0x04000A0D RID: 2573
		public string Name;

		// Token: 0x04000A0E RID: 2574
		public string Path;

		// Token: 0x04000A0F RID: 2575
		public string VirtualPath;

		// Token: 0x04000A10 RID: 2576
		public ItemTypeEnum Type;

		// Token: 0x04000A11 RID: 2577
		public int Size;

		// Token: 0x04000A12 RID: 2578
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x04000A13 RID: 2579
		public string Description;

		// Token: 0x04000A14 RID: 2580
		public bool Hidden;

		// Token: 0x04000A15 RID: 2581
		[XmlIgnore]
		public bool HiddenSpecified;

		// Token: 0x04000A16 RID: 2582
		public DateTime CreationDate;

		// Token: 0x04000A17 RID: 2583
		[XmlIgnore]
		public bool CreationDateSpecified;

		// Token: 0x04000A18 RID: 2584
		public DateTime ModifiedDate;

		// Token: 0x04000A19 RID: 2585
		[XmlIgnore]
		public bool ModifiedDateSpecified;

		// Token: 0x04000A1A RID: 2586
		public string CreatedBy;

		// Token: 0x04000A1B RID: 2587
		public string ModifiedBy;

		// Token: 0x04000A1C RID: 2588
		public string MimeType;

		// Token: 0x04000A1D RID: 2589
		public DateTime ExecutionDate;

		// Token: 0x04000A1E RID: 2590
		[XmlIgnore]
		public bool ExecutionDateSpecified;
	}
}
