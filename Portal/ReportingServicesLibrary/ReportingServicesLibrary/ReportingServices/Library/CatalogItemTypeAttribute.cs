using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class CatalogItemTypeAttribute : Attribute
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00023D43 File Offset: 0x00021F43
		public CatalogItemTypeAttribute(ItemType itemType)
		{
			this.m_itemType = itemType;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00023D52 File Offset: 0x00021F52
		public ItemType Type
		{
			get
			{
				return this.m_itemType;
			}
		}

		// Token: 0x04000452 RID: 1106
		private readonly ItemType m_itemType;
	}
}
