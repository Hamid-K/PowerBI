using System;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000051 RID: 81
	internal static class CatalogItemTypeExtensions
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0001275C File Offset: 0x0001095C
		public static CatalogItemType ToCatalogItemType(this ItemType itemType)
		{
			switch (itemType)
			{
			case ItemType.Folder:
				return CatalogItemType.Folder;
			case ItemType.Report:
				return CatalogItemType.Report;
			case ItemType.Resource:
				return CatalogItemType.Resource;
			case ItemType.LinkedReport:
				return CatalogItemType.LinkedReport;
			case ItemType.DataSource:
				return CatalogItemType.DataSource;
			case ItemType.Model:
				return CatalogItemType.ReportModel;
			case ItemType.DataSet:
				return CatalogItemType.DataSet;
			case ItemType.Component:
				return CatalogItemType.Component;
			case ItemType.Kpi:
				return CatalogItemType.Kpi;
			case ItemType.PowerBIReport:
				return CatalogItemType.PowerBIReport;
			case ItemType.ExcelWorkbook:
				return CatalogItemType.ExcelWorkbook;
			}
			return CatalogItemType.Unknown;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000127C8 File Offset: 0x000109C8
		public static ItemType ToLibraryItemType(this CatalogItemType itemType)
		{
			switch (itemType)
			{
			case CatalogItemType.Folder:
				return ItemType.Folder;
			case CatalogItemType.Report:
				return ItemType.Report;
			case CatalogItemType.DataSource:
				return ItemType.DataSource;
			case CatalogItemType.DataSet:
				return ItemType.DataSet;
			case CatalogItemType.Component:
				return ItemType.Component;
			case CatalogItemType.Resource:
				return ItemType.Resource;
			case CatalogItemType.Kpi:
				return ItemType.Kpi;
			case CatalogItemType.LinkedReport:
				return ItemType.LinkedReport;
			case CatalogItemType.ReportModel:
				return ItemType.Model;
			case CatalogItemType.PowerBIReport:
				return ItemType.PowerBIReport;
			case CatalogItemType.ExcelWorkbook:
				return ItemType.ExcelWorkbook;
			}
			return ItemType.Unknown;
		}
	}
}
