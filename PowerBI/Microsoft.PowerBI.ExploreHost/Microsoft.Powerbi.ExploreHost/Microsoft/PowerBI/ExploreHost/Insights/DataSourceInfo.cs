using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007A RID: 122
	internal sealed class DataSourceInfo : IDataSourceInfo
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000AB58 File Offset: 0x00008D58
		public DataSourceInfo(string name, string extension, string connectionString)
		{
			this.m_name = name;
			this.m_extension = extension;
			this.m_connectionString = connectionString;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000AB75 File Offset: 0x00008D75
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000AB7D File Offset: 0x00008D7D
		public string Extension
		{
			get
			{
				return this.m_extension;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000AB85 File Offset: 0x00008D85
		public string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
		}

		// Token: 0x0400017C RID: 380
		private string m_name;

		// Token: 0x0400017D RID: 381
		private string m_extension;

		// Token: 0x0400017E RID: 382
		private string m_connectionString;
	}
}
