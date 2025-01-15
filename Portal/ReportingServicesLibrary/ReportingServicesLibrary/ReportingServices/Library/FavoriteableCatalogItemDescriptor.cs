using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000041 RID: 65
	internal sealed class FavoriteableCatalogItemDescriptor : CatalogItemDescriptor
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000F810 File Offset: 0x0000DA10
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000F818 File Offset: 0x0000DA18
		public bool IsFavorite
		{
			get
			{
				return this.m_isFavorite;
			}
			set
			{
				this.m_isFavorite = value;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000F821 File Offset: 0x0000DA21
		public FavoriteableCatalogItemDescriptor()
		{
			this.IsFavorite = false;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000F830 File Offset: 0x0000DA30
		public FavoriteableCatalogItemDescriptor(CatalogItemDescriptor itemDescriptor)
		{
			base.ID = itemDescriptor.ID;
			base.Name = itemDescriptor.Name;
			base.Path = itemDescriptor.Path;
			base.VirtualPath = itemDescriptor.VirtualPath;
			base.Type = itemDescriptor.Type;
			base.Size = itemDescriptor.Size;
			base.Description = itemDescriptor.Description;
			base.Hidden = itemDescriptor.Hidden;
			base.CreationDate = itemDescriptor.CreationDate;
			base.ModifiedDate = itemDescriptor.ModifiedDate;
			base.CreatedBy = itemDescriptor.CreatedBy;
			base.ModifiedBy = itemDescriptor.ModifiedBy;
			base.MimeType = itemDescriptor.MimeType;
			base.ExecutionDate = itemDescriptor.ExecutionDate;
			base.ComponentID = itemDescriptor.ComponentID;
			base.ItemMetadata = itemDescriptor.ItemMetadata;
		}

		// Token: 0x04000144 RID: 324
		private bool m_isFavorite;
	}
}
