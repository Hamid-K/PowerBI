using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000160 RID: 352
	internal sealed class GetPowerBIReportContentsActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00030A6C File Offset: 0x0002EC6C
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00030A74 File Offset: 0x0002EC74
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

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00030A7D File Offset: 0x0002EC7D
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00030A85 File Offset: 0x0002EC85
		public byte[] Content
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00030A8E File Offset: 0x0002EC8E
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00030A96 File Offset: 0x0002EC96
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000542 RID: 1346
		private string m_itemPath;

		// Token: 0x04000543 RID: 1347
		private byte[] m_content;
	}
}
