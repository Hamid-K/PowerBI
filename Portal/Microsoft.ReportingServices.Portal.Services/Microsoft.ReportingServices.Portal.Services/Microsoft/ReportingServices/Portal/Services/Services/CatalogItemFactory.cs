using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.Interfaces;
using Microsoft.ReportingServices.Portal.Services.Models;

namespace Microsoft.ReportingServices.Portal.Services.Services
{
	// Token: 0x02000039 RID: 57
	internal sealed class CatalogItemFactory : ICatalogItemFactory
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public ICatalogItem Create(IPrincipal userPrincipal, CatalogItemDescriptor itemDescriptor)
		{
			return this.Create(userPrincipal, itemDescriptor, null);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		internal ICatalogItem Create(IPrincipal userPrincipal, CatalogItemDescriptor itemDescriptor, Action<ICatalogItem> rsLoader)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (itemDescriptor == null)
			{
				throw new ArgumentNullException("itemDescriptor");
			}
			CatalogItemType catalogItemType = itemDescriptor.Type.ToCatalogItemType();
			if (catalogItemType <= CatalogItemType.Resource)
			{
				if (catalogItemType == CatalogItemType.Folder)
				{
					return this.CreateFolderCatalogItem(itemDescriptor);
				}
				if (catalogItemType == CatalogItemType.Resource)
				{
					return this.CreateResourceCatalogItem(itemDescriptor);
				}
			}
			else
			{
				if (catalogItemType == CatalogItemType.PowerBIReport)
				{
					return this.CreatePowerBIReportCatalogItem(itemDescriptor);
				}
				if (catalogItemType == CatalogItemType.ExcelWorkbook)
				{
					return this.CreateExcelWorkbookCatalogItem(itemDescriptor);
				}
			}
			return this.CreateUnknownCatalogItem(itemDescriptor);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		private IFolderCatalogItem CreateFolderCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Microsoft.ReportingServices.Portal.Services.Models.FolderCatalogItem folderCatalogItem = new Microsoft.ReportingServices.Portal.Services.Models.FolderCatalogItem();
			this.Populate(itemDescriptor, folderCatalogItem);
			return folderCatalogItem;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000FB00 File Offset: 0x0000DD00
		private IResourceCatalogItem CreateResourceCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Microsoft.ReportingServices.Portal.Services.Models.ResourceCatalogItem resourceCatalogItem = new Microsoft.ReportingServices.Portal.Services.Models.ResourceCatalogItem();
			this.Populate(itemDescriptor, resourceCatalogItem);
			resourceCatalogItem.ContentType = itemDescriptor.MimeType;
			return resourceCatalogItem;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000FB28 File Offset: 0x0000DD28
		private IPowerBIReportCatalogItem CreatePowerBIReportCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Microsoft.ReportingServices.Portal.Services.Models.PowerBIReportCatalogItem powerBIReportCatalogItem = new Microsoft.ReportingServices.Portal.Services.Models.PowerBIReportCatalogItem();
			this.Populate(itemDescriptor, powerBIReportCatalogItem);
			return powerBIReportCatalogItem;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000FB44 File Offset: 0x0000DD44
		private IExcelWorkbookCatalogItem CreateExcelWorkbookCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Microsoft.ReportingServices.Portal.Services.Models.ExcelWorkbookCatalogItem excelWorkbookCatalogItem = new Microsoft.ReportingServices.Portal.Services.Models.ExcelWorkbookCatalogItem();
			this.Populate(itemDescriptor, excelWorkbookCatalogItem);
			return excelWorkbookCatalogItem;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000FB60 File Offset: 0x0000DD60
		private ICatalogItem CreateUnknownCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			UnknownCatalogItem unknownCatalogItem = new UnknownCatalogItem();
			this.Populate(itemDescriptor, unknownCatalogItem);
			return unknownCatalogItem;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		private void Populate(CatalogItemDescriptor itemDescriptor, ICatalogItem catalogItem)
		{
			catalogItem.Id = itemDescriptor.ID;
			catalogItem.Name = itemDescriptor.Name;
			catalogItem.Description = itemDescriptor.Description;
			catalogItem.CreatedDate = itemDescriptor.CreationDate;
			catalogItem.CreatedBy = itemDescriptor.CreatedBy;
			catalogItem.ModifiedDate = itemDescriptor.ModifiedDate;
			catalogItem.ModifiedBy = itemDescriptor.ModifiedBy;
			catalogItem.Hidden = itemDescriptor.Hidden;
			catalogItem.Path = ((itemDescriptor.Path != null) ? itemDescriptor.Path.Value : null);
			catalogItem.Type = itemDescriptor.Type.ToCatalogItemType();
			catalogItem.Size = itemDescriptor.Size;
		}
	}
}
