using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002EA RID: 746
	public class CatalogItem
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x0006C11C File Offset: 0x0006A31C
		public CatalogItem()
		{
			this.ID = null;
			this.Name = null;
			this.Path = null;
			this.VirtualPath = null;
			this.TypeName = ItemType.Unknown.ToString();
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
			this.ItemMetadata = null;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0006C1BC File Offset: 0x0006A3BC
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

		// Token: 0x06001ACE RID: 6862 RVA: 0x0006C1FC File Offset: 0x0006A3FC
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
			catalogItem.Description = item.Description;
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
			catalogItem.TypeName = item.ThisItemType.ToString();
			catalogItem.ItemMetadata = item.ItemMetadata.ConvertToProperties();
			return catalogItem;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0006C2F0 File Offset: 0x0006A4F0
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
				Hidden = item.Hidden,
				HiddenSpecified = item.Hidden,
				ID = item.ID,
				ModifiedBy = item.ModifiedBy,
				ModifiedDate = item.ModifiedDate,
				ModifiedDateSpecified = (item.ModifiedDate != DateTime.MinValue),
				Name = item.Name,
				Path = item.Path.Value,
				Size = item.Size,
				SizeSpecified = (item.Size >= 0),
				TypeName = item.Type.ToString(),
				VirtualPath = item.VirtualPath,
				ItemMetadata = item.ItemMetadata.ConvertToProperties()
			};
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0006C408 File Offset: 0x0006A608
		internal static ItemTypeEnum ItemTypeToSoapType(ItemType type)
		{
			switch (type)
			{
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
			case ItemType.Site:
				return ItemTypeEnum.Site;
			case ItemType.DataSet:
				return ItemTypeEnum.DataSet;
			case ItemType.Component:
				return ItemTypeEnum.Component;
			case ItemType.RdlxReport:
				return ItemTypeEnum.RdlxReport;
			default:
				return ItemTypeEnum.Unknown;
			}
		}

		// Token: 0x04000991 RID: 2449
		public string ID;

		// Token: 0x04000992 RID: 2450
		public string Name;

		// Token: 0x04000993 RID: 2451
		public string Path;

		// Token: 0x04000994 RID: 2452
		public string VirtualPath;

		// Token: 0x04000995 RID: 2453
		public string TypeName;

		// Token: 0x04000996 RID: 2454
		public int Size;

		// Token: 0x04000997 RID: 2455
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x04000998 RID: 2456
		public string Description;

		// Token: 0x04000999 RID: 2457
		public bool Hidden;

		// Token: 0x0400099A RID: 2458
		[XmlIgnore]
		public bool HiddenSpecified;

		// Token: 0x0400099B RID: 2459
		public DateTime CreationDate;

		// Token: 0x0400099C RID: 2460
		[XmlIgnore]
		public bool CreationDateSpecified;

		// Token: 0x0400099D RID: 2461
		public DateTime ModifiedDate;

		// Token: 0x0400099E RID: 2462
		[XmlIgnore]
		public bool ModifiedDateSpecified;

		// Token: 0x0400099F RID: 2463
		public string CreatedBy;

		// Token: 0x040009A0 RID: 2464
		public string ModifiedBy;

		// Token: 0x040009A1 RID: 2465
		public Property[] ItemMetadata;
	}
}
