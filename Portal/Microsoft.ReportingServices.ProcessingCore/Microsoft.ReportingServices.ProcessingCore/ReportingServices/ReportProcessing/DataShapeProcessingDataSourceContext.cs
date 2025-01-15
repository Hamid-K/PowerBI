using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FC RID: 1532
	internal sealed class DataShapeProcessingDataSourceContext
	{
		// Token: 0x06005462 RID: 21602 RVA: 0x001624C3 File Offset: 0x001606C3
		public DataShapeProcessingDataSourceContext(DataSourceInfo dataSourceInfo, string connectionCategory, int commandTimeout)
		{
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_connectionCategory = connectionCategory;
			this.m_commandTimeout = commandTimeout;
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x001624E0 File Offset: 0x001606E0
		public DataSourceInfo DataSourceInfo
		{
			get
			{
				return this.m_dataSourceInfo;
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x001624E8 File Offset: 0x001606E8
		public string ConnectionCategory
		{
			get
			{
				return this.m_connectionCategory;
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x001624F0 File Offset: 0x001606F0
		public int CommandTimeout
		{
			get
			{
				return this.m_commandTimeout;
			}
		}

		// Token: 0x04002CEC RID: 11500
		private readonly DataSourceInfo m_dataSourceInfo;

		// Token: 0x04002CED RID: 11501
		private readonly string m_connectionCategory;

		// Token: 0x04002CEE RID: 11502
		private readonly int m_commandTimeout;
	}
}
