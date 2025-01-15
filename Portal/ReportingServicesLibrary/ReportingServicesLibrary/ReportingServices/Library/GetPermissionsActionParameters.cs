using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F8 RID: 504
	internal sealed class GetPermissionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0003AFC9 File Offset: 0x000391C9
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0003AFD1 File Offset: 0x000391D1
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0003AFDA File Offset: 0x000391DA
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0003AFE2 File Offset: 0x000391E2
		public StringCollection Operations
		{
			get
			{
				return this.m_operations;
			}
			set
			{
				this.m_operations = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0003AFEB File Offset: 0x000391EB
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003AFF3 File Offset: 0x000391F3
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Item");
			}
		}

		// Token: 0x0400067C RID: 1660
		private string m_itemPath;

		// Token: 0x0400067D RID: 1661
		private StringCollection m_operations;
	}
}
