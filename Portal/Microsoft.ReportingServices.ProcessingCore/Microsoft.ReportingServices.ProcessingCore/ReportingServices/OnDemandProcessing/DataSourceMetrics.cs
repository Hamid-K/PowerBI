using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000817 RID: 2071
	internal sealed class DataSourceMetrics
	{
		// Token: 0x0600730B RID: 29451 RVA: 0x001DE544 File Offset: 0x001DC744
		public DataSourceMetrics(string dataSourceName, string dataSourceReference, string dataSourceType, DataProcessingMetrics aggregatedMetrics, DataProcessingMetrics[] dataSetsMetrics)
			: this(dataSourceName, dataSourceReference, dataSourceType, aggregatedMetrics.ResolvedConnectionString, aggregatedMetrics.OpenConnectionDurationMs, aggregatedMetrics.ConnectionFromPool)
		{
			this.m_dataSetsMetrics = dataSetsMetrics;
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x001DE56C File Offset: 0x001DC76C
		public DataSourceMetrics(string dataSourceName, string dataSourceReference, string dataSourceType, DataProcessingMetrics parallelDataSetMetrics)
			: this(dataSourceName, dataSourceReference, dataSourceType, parallelDataSetMetrics.ResolvedConnectionString, parallelDataSetMetrics.OpenConnectionDurationMs, parallelDataSetMetrics.ConnectionFromPool)
		{
			this.m_dataSetsMetrics = new DataProcessingMetrics[1];
			this.m_dataSetsMetrics[0] = parallelDataSetMetrics;
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x001DE5A2 File Offset: 0x001DC7A2
		private DataSourceMetrics(string dataSourceName, string dataSourceReference, string dataSourceType, string embeddedConnectionString, long openConnectionDurationMs, bool? connectionFromPool)
		{
			this.m_dataSourceName = dataSourceName;
			this.m_dataSourceReference = dataSourceReference;
			this.m_dataSourceType = dataSourceType;
			this.m_embeddedConnectionString = ((dataSourceReference == null) ? embeddedConnectionString : null);
			this.m_openConnectionDurationMs = openConnectionDurationMs;
			this.m_connectionFromPool = connectionFromPool;
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x001DE5E0 File Offset: 0x001DC7E0
		internal Connection ToAdditionalInfoConnection(IJobContext jobContext)
		{
			if (jobContext == null)
			{
				return null;
			}
			Connection connection = new Connection();
			connection.ConnectionOpenTime = new long?(this.m_openConnectionDurationMs);
			connection.ConnectionFromPool = this.m_connectionFromPool;
			if (jobContext.ExecutionLogLevel == ExecutionLogLevel.Verbose)
			{
				DataSource dataSource = new DataSource();
				dataSource.Name = this.m_dataSourceName;
				if (this.m_dataSourceReference != null)
				{
					dataSource.DataSourceReference = this.m_dataSourceReference;
				}
				else if (this.m_embeddedConnectionString != null)
				{
					dataSource.ConnectionString = this.m_embeddedConnectionString;
				}
				dataSource.DataExtension = this.m_dataSourceType;
				connection.DataSource = dataSource;
			}
			if (this.m_dataSetsMetrics != null)
			{
				connection.DataSets = new List<DataSet>(this.m_dataSetsMetrics.Length);
				for (int i = 0; i < this.m_dataSetsMetrics.Length; i++)
				{
					connection.DataSets.Add(this.m_dataSetsMetrics[i].ToAdditionalInfoDataSet(jobContext));
				}
			}
			return connection;
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x0600730F RID: 29455 RVA: 0x001DE6B5 File Offset: 0x001DC8B5
		public long OpenConnectionDurationMs
		{
			get
			{
				return this.m_openConnectionDurationMs;
			}
		}

		// Token: 0x04003AF1 RID: 15089
		private readonly string m_dataSourceName;

		// Token: 0x04003AF2 RID: 15090
		private readonly string m_dataSourceReference;

		// Token: 0x04003AF3 RID: 15091
		private readonly string m_dataSourceType;

		// Token: 0x04003AF4 RID: 15092
		private readonly string m_embeddedConnectionString;

		// Token: 0x04003AF5 RID: 15093
		private readonly long m_openConnectionDurationMs;

		// Token: 0x04003AF6 RID: 15094
		private readonly bool? m_connectionFromPool;

		// Token: 0x04003AF7 RID: 15095
		private readonly DataProcessingMetrics[] m_dataSetsMetrics;
	}
}
