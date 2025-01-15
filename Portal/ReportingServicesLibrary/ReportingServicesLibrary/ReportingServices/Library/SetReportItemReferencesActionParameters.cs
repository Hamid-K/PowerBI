using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C7 RID: 455
	internal class SetReportItemReferencesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00038C94 File Offset: 0x00036E94
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x00038C9C File Offset: 0x00036E9C
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00038CA5 File Offset: 0x00036EA5
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x00038CAD File Offset: 0x00036EAD
		public ItemReference[] ItemReferences
		{
			get
			{
				return this.m_itemReferences;
			}
			set
			{
				this.m_itemReferences = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00038CB6 File Offset: 0x00036EB6
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00038CBE File Offset: 0x00036EBE
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.ItemReferences == null)
			{
				throw new MissingParameterException("ItemReferences");
			}
			Array.ForEach<ItemReference>(this.ItemReferences, new Action<ItemReference>(SetReportItemReferencesActionParameters.ValidateItemReference));
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00038CFD File Offset: 0x00036EFD
		private static void ValidateItemReference(ItemReference reference)
		{
			if (reference == null)
			{
				throw new MissingElementException("ItemReference");
			}
			if (reference.Name == null)
			{
				throw new MissingElementException("Name");
			}
		}

		// Token: 0x04000647 RID: 1607
		private string m_itemPath;

		// Token: 0x04000648 RID: 1608
		private ItemReference[] m_itemReferences;
	}
}
