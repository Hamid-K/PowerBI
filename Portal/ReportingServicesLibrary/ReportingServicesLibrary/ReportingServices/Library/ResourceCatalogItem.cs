using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D8 RID: 216
	[CatalogItemType(ItemType.Resource)]
	internal class ResourceCatalogItem : CatalogItem
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x00004F8E File Offset: 0x0000318E
		internal ResourceCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002543C File Offset: 0x0002363C
		internal void LoadContent()
		{
			base.LoadDefinition();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00025A76 File Offset: 0x00023C76
		protected override void ContentLoadSecurityCheck()
		{
			this.ThrowIfNoAccess(ResourceOperation.ReadContent);
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00004FE5 File Offset: 0x000031E5
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

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00025A7F File Offset: 0x00023C7F
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00025A8C File Offset: 0x00023C8C
		internal string MimeType
		{
			get
			{
				return this.m_itemMetadata.MimeType;
			}
			set
			{
				this.m_itemMetadata.MimeType = value;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000050E1 File Offset: 0x000032E1
		internal void ThrowIfNoAccess(ResourceOperation operation)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, operation, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00025A9A File Offset: 0x00023C9A
		internal override void Save(bool preventCreate)
		{
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.Resource, this.Content, Guid.Empty, null, Guid.Empty, this.MimeType);
			base.AdjustModificationInfo();
		}
	}
}
