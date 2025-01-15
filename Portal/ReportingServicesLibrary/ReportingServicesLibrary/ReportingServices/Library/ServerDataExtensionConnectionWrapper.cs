using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F4 RID: 244
	internal sealed class ServerDataExtensionConnectionWrapper : IDisposable
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x000270C8 File Offset: 0x000252C8
		private ServerDataExtensionConnectionWrapper(DataSourceInfo dataSourceInfo, IProcessingDataExtensionConnection connectionFactory)
		{
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_connectionFactory = connectionFactory;
			string text;
			if (dataSourceInfo.UseOriginalConnectionString)
			{
				text = dataSourceInfo.GetOriginalConnectionString(DataProtection.Instance);
			}
			else
			{
				text = dataSourceInfo.GetConnectionString(DataProtection.Instance);
			}
			this.m_connection = this.m_connectionFactory.OpenDataSourceExtensionConnection(null, text, this.m_dataSourceInfo, null);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00027125 File Offset: 0x00025325
		public void Close()
		{
			this.m_connectionFactory.CloseConnection(this.m_connection, null, this.m_dataSourceInfo);
			this.m_connectionFactory = null;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00027146 File Offset: 0x00025346
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
				this.Close();
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002715D File Offset: 0x0002535D
		public IProcessingDataExtensionConnection ConnectionFactory
		{
			get
			{
				return this.m_connectionFactory;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00027165 File Offset: 0x00025365
		public IDbConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002716D File Offset: 0x0002536D
		public static ServerDataExtensionConnectionWrapper Open(DataSourceInfo dataSourceInfo, IProcessingDataExtensionConnection connectionFactory)
		{
			Global.m_Tracer.Assert(connectionFactory != null, "connectionFactory != null");
			return new ServerDataExtensionConnectionWrapper(dataSourceInfo, connectionFactory);
		}

		// Token: 0x0400047D RID: 1149
		private readonly DataSourceInfo m_dataSourceInfo;

		// Token: 0x0400047E RID: 1150
		private IProcessingDataExtensionConnection m_connectionFactory;

		// Token: 0x0400047F RID: 1151
		private IDbConnection m_connection;

		// Token: 0x04000480 RID: 1152
		private bool m_disposed;
	}
}
