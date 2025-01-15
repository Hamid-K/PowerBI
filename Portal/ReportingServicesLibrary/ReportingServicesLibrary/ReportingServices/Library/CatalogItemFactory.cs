using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CC RID: 204
	internal sealed class CatalogItemFactory
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x000239C8 File Offset: 0x00021BC8
		internal CatalogItemFactory(RSService service)
		{
			this.m_syncCache = new Dictionary<string, string>(CatalogItemFactory.m_cacheComparer);
			this.m_service = service;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000239E7 File Offset: 0x00021BE7
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext)
		{
			return this.GetCatalogItem(itemContext, false);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000239F4 File Offset: 0x00021BF4
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, bool doSync)
		{
			bool flag;
			return this.InternalGetCatalogItem(itemContext, doSync, out flag);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00023A0B File Offset: 0x00021C0B
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, out bool wasSynched)
		{
			return this.InternalGetCatalogItem(itemContext, true, out wasSynched);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00023A16 File Offset: 0x00021C16
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, ItemType itemType)
		{
			return this.GetCatalogItem(itemContext, itemType, false);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00023A24 File Offset: 0x00021C24
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, ItemType itemType, bool doSync)
		{
			bool flag;
			CatalogItem catalogItem = this.InternalGetCatalogItem(itemContext, doSync, out flag);
			catalogItem.ThrowIfWrongItemType(new ItemType[] { itemType });
			return catalogItem;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00023A4D File Offset: 0x00021C4D
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, Guid itemID, ItemType itemType, byte[] secDesc)
		{
			CatalogItem catalogItem = CatalogItemFactory.CreateCatalogItem(itemType, this.m_service);
			catalogItem.Initialize(itemContext, itemID, secDesc);
			return catalogItem;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00023A68 File Offset: 0x00021C68
		internal CatalogItem GetCatalogItem(CatalogItemContext itemContext, Guid itemID, ItemType itemType, byte[] secDesc, Guid linkId)
		{
			CatalogItem catalogItem = CatalogItemFactory.CreateCatalogItem(itemType, this.m_service);
			if (itemType == ItemType.LinkedReport)
			{
				(catalogItem as LinkedReportCatalogItem).Initialize(itemContext, itemID, secDesc, linkId);
			}
			else
			{
				catalogItem.Initialize(itemContext, itemID, secDesc);
			}
			return catalogItem;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00023AA4 File Offset: 0x00021CA4
		private CatalogItem InternalGetCatalogItem(CatalogItemContext itemContext, bool doSync, out bool wasSynched)
		{
			CatalogItem catalogItem2;
			using (MonitoredScope.New("CatalogItemFactory.InternalGetCatalogItem"))
			{
				wasSynched = false;
				ExternalItemPath itemPath = itemContext.ItemPath;
				doSync &= !itemPath.IsEditSession;
				if (doSync && !this.ItemHasSynced(itemPath))
				{
					wasSynched = this.m_service.ServiceHelper.SyncToRSCatalog(itemPath);
					this.MarkItemSynced(itemPath);
				}
				ItemType itemType;
				Guid guid;
				int num;
				byte[] array;
				int num2;
				Guid guid2;
				Guid guid3;
				if (!this.m_service.Storage.ObjectExists(itemContext.ItemPath, out itemType, out guid, out num, out array, out num2, out guid2, out guid3))
				{
					throw new ItemNotFoundException(itemContext.OriginalItemPath.Value);
				}
				using (MonitoredScope.New("CatalogItemFactory.InternalGetCatalogItem - create and initialize catalog item"))
				{
					CatalogItem catalogItem = CatalogItemFactory.CreateCatalogItem(itemType, this.m_service);
					if (itemType == ItemType.Report || itemType == ItemType.LinkedReport || itemType == ItemType.RdlxReport)
					{
						(catalogItem as BaseReportCatalogItem).Initialize(itemContext, guid, array, num, num2, guid3);
					}
					else
					{
						catalogItem.Initialize(itemContext, guid, array);
					}
					catalogItem2 = catalogItem;
				}
			}
			return catalogItem2;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00023BB4 File Offset: 0x00021DB4
		internal Guid? GetCatalogItemGuidByPath(CatalogItemContext itemContext)
		{
			Guid? guid2;
			using (MonitoredScope.New("CatalogItemFactory.GetCatalogItemId"))
			{
				ItemType itemType;
				Guid guid;
				int num;
				if (!this.m_service.Storage.ObjectExists(itemContext.ItemPath, out itemType, out guid, out num))
				{
					guid2 = null;
					guid2 = guid2;
				}
				else
				{
					guid2 = new Guid?(guid);
				}
			}
			return guid2;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00023C20 File Offset: 0x00021E20
		internal static CatalogItem CreateCatalogItem(ItemType itemType, RSService service)
		{
			switch (itemType)
			{
			case ItemType.Folder:
				return new FolderCatalogItem(service);
			case ItemType.Report:
				return new ProfessionalReportCatalogItem(service);
			case ItemType.Resource:
				return new ResourceCatalogItem(service);
			case ItemType.LinkedReport:
				return new LinkedReportCatalogItem(service);
			case ItemType.DataSource:
				return new DataSourceCatalogItem(service);
			case ItemType.Model:
				return new ModelCatalogItem(service);
			case ItemType.Site:
				return new SiteCatalogItem(service);
			case ItemType.DataSet:
				return new DataSetCatalogItem(service);
			case ItemType.Component:
				return new ComponentCatalogItem(service);
			case ItemType.RdlxReport:
				return new RdlxReportCatalogItem(service);
			case ItemType.Kpi:
				return new KpiCatalogItem(service);
			case ItemType.PowerBIReport:
				return new PowerBIReportCatalogItem(service);
			case ItemType.ExcelWorkbook:
				return new ExcelWorkbookCatalogItem(service);
			}
			throw new InternalCatalogException("Unknown item type");
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00023CF0 File Offset: 0x00021EF0
		private bool ItemHasSynced(ExternalItemPath itemPath)
		{
			return !ItemPathBase.IsNullOrEmpty(itemPath) && this.m_syncCache.ContainsKey(itemPath.Value);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00023D0D File Offset: 0x00021F0D
		private void MarkItemSynced(ExternalItemPath itemPath)
		{
			if (ItemPathBase.IsNullOrEmpty(itemPath))
			{
				return;
			}
			if (this.m_syncCache.ContainsKey(itemPath.Value))
			{
				return;
			}
			this.m_syncCache.Add(itemPath.Value, itemPath.Value);
		}

		// Token: 0x0400044F RID: 1103
		private static readonly IEqualityComparer<string> m_cacheComparer = StringComparer.Create(Localization.CatalogCulture, true);

		// Token: 0x04000450 RID: 1104
		private readonly IDictionary<string, string> m_syncCache;

		// Token: 0x04000451 RID: 1105
		private readonly RSService m_service;
	}
}
