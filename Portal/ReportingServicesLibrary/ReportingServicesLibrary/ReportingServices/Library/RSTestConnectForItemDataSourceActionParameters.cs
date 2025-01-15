using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000EF RID: 239
	internal sealed class RSTestConnectForItemDataSourceActionParameters : TestConnectActionParameters
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00026CC0 File Offset: 0x00024EC0
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00026CC8 File Offset: 0x00024EC8
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00026CD1 File Offset: 0x00024ED1
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00026CD9 File Offset: 0x00024ED9
		public string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
			set
			{
				this.m_dataSourceName = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00026CE2 File Offset: 0x00024EE2
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00026CEA File Offset: 0x00024EEA
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x0400047A RID: 1146
		private string m_itemPath;

		// Token: 0x0400047B RID: 1147
		private string m_dataSourceName;
	}
}
