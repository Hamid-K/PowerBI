using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001C RID: 28
	[CatalogItemType(ItemType.ExcelWorkbook)]
	internal class ExcelWorkbookCatalogItem : CatalogItem
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004F8E File Offset: 0x0000318E
		internal ExcelWorkbookCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F97 File Offset: 0x00003197
		internal void LoadContent()
		{
			base.LoadDefinition();
			if (!base.Service.Storage.GetCatalogExtendedContentData(this.m_itemID, ExtendedContentType.CatalogItem, out this.m_content))
			{
				throw new ItemNotFoundException(base.ItemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004FD4 File Offset: 0x000031D4
		protected override void ContentLoadSecurityCheck()
		{
			base.ThrowIfNoAccess(ReportOperation.ReadProperties);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004FE5 File Offset: 0x000031E5
		internal override byte[] Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004FEE File Offset: 0x000031EE
		internal override void Save(bool preventCreate)
		{
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.ExcelWorkbook, this.Content, Guid.Empty, null, Guid.Empty, null);
			base.AdjustModificationInfo();
		}
	}
}
