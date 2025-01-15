using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FB RID: 251
	internal sealed class GetDataSetItemReferencesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000275EA File Offset: 0x000257EA
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x000275F2 File Offset: 0x000257F2
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000275FB File Offset: 0x000257FB
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00027603 File Offset: 0x00025803
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002760C File Offset: 0x0002580C
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00027614 File Offset: 0x00025814
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000488 RID: 1160
		private string m_itemPath;

		// Token: 0x04000489 RID: 1161
		private ItemReferenceData[] m_itemReferences;
	}
}
