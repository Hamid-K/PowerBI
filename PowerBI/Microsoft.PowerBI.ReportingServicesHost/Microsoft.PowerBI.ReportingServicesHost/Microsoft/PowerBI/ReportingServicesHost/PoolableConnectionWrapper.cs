using System;
using System.Threading.Tasks;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000046 RID: 70
	internal sealed class PoolableConnectionWrapper : IDisposable
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00004CB7 File Offset: 0x00002EB7
		private PoolableConnectionWrapper(IDbConnectionPool connectionPool, IDbConnection connection)
		{
			this.Connection = connection;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004CC8 File Offset: 0x00002EC8
		public static PoolableConnectionWrapper Create(string connectionString, IDbConnectionPool connectionPool, IConnectionFactory connectionFactory)
		{
			IDbConnection dbConnection = connectionFactory.CreateConnection("DAX", connectionString);
			return new PoolableConnectionWrapper(connectionPool, dbConnection);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004CEC File Offset: 0x00002EEC
		public async Task Close()
		{
			await this.Connection.CloseAsync();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004D2F File Offset: 0x00002F2F
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
				this.Close().WaitAndUnwrapException();
				this.Connection.Dispose();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004D56 File Offset: 0x00002F56
		public IDbConnection Connection { get; }

		// Token: 0x0600018E RID: 398 RVA: 0x00004D60 File Offset: 0x00002F60
		public async Task OpenAsync(IConnectionUserImpersonator connectionUserImpersonator)
		{
			if (connectionUserImpersonator != null)
			{
				await connectionUserImpersonator.ExecuteInContextAsync(new Func<Task>(this.Connection.OpenAsync));
			}
			else
			{
				await this.Connection.OpenAsync();
			}
		}

		// Token: 0x040000FD RID: 253
		private bool m_disposed;
	}
}
