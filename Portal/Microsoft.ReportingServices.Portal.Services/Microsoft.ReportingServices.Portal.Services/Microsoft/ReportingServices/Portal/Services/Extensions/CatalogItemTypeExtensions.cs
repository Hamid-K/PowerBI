using System;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000065 RID: 101
	internal static class CatalogItemTypeExtensions
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00014E0C File Offset: 0x0001300C
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
	}
}
