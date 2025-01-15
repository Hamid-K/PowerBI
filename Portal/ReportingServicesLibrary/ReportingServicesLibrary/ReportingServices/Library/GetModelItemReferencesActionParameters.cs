using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200015A RID: 346
	internal sealed class GetModelItemReferencesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000305E5 File Offset: 0x0002E7E5
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x000305ED File Offset: 0x0002E7ED
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000305F6 File Offset: 0x0002E7F6
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x000305FE File Offset: 0x0002E7FE
		public ItemReferenceData[] ItemReferences
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00030607 File Offset: 0x0002E807
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003060F File Offset: 0x0002E80F
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x0400053C RID: 1340
		private string m_itemPath;

		// Token: 0x0400053D RID: 1341
		private ItemReferenceData[] m_itemReferences;
	}
}
