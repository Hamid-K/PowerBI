using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F6 RID: 502
	internal sealed class DeletePoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0003AE65 File Offset: 0x00039065
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0003AE6D File Offset: 0x0003906D
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

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0003AE76 File Offset: 0x00039076
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003AE7E File Offset: 0x0003907E
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Item");
			}
		}

		// Token: 0x0400067B RID: 1659
		private string m_itemPath;
	}
}
