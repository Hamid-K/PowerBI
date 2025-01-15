using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000034 RID: 52
	internal sealed class DataShapingConnectionAdapter : IDbPoolableConnection, Microsoft.ReportingServices.DataProcessing.IDbConnection, IDisposable, IExtension
	{
		// Token: 0x0600010C RID: 268 RVA: 0x0000444E File Offset: 0x0000264E
		internal DataShapingConnectionAdapter(Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection connection)
		{
			this.m_connection = connection;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000445D File Offset: 0x0000265D
		public Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004465 File Offset: 0x00002665
		public bool IsAlive
		{
			get
			{
				return this.m_connection.IsAliveAsync().Result;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004477 File Offset: 0x00002677
		public string GetConnectionStringForPooling()
		{
			throw DataShapingConnectionAdapter.CreateNotSupportedException();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000447E File Offset: 0x0000267E
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004486 File Offset: 0x00002686
		public bool IsFromPool { get; set; }

		// Token: 0x06000112 RID: 274 RVA: 0x0000448F File Offset: 0x0000268F
		void Microsoft.ReportingServices.DataProcessing.IDbConnection.Open()
		{
			throw DataShapingConnectionAdapter.CreateNotSupportedException();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004496 File Offset: 0x00002696
		void Microsoft.ReportingServices.DataProcessing.IDbConnection.Close()
		{
			this.Dispose();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000449E File Offset: 0x0000269E
		Microsoft.ReportingServices.DataProcessing.IDbCommand Microsoft.ReportingServices.DataProcessing.IDbConnection.CreateCommand()
		{
			throw DataShapingConnectionAdapter.CreateNotSupportedException();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000044A5 File Offset: 0x000026A5
		IDbTransaction Microsoft.ReportingServices.DataProcessing.IDbConnection.BeginTransaction()
		{
			throw DataShapingConnectionAdapter.CreateNotSupportedException();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000044B3 File Offset: 0x000026B3
		string Microsoft.ReportingServices.DataProcessing.IDbConnection.ConnectionString
		{
			get
			{
				throw DataShapingConnectionAdapter.CreateNotSupportedException();
			}
			set
			{
				throw DataShapingConnectionAdapter.CreateNotSupportedException();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000044BA File Offset: 0x000026BA
		int Microsoft.ReportingServices.DataProcessing.IDbConnection.ConnectionTimeout
		{
			get
			{
				throw DataShapingConnectionAdapter.CreateNotSupportedException();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000044C1 File Offset: 0x000026C1
		public void Dispose()
		{
			this.m_connection.Dispose();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000044CE File Offset: 0x000026CE
		private static Exception CreateNotSupportedException()
		{
			throw new InvalidOperationException("The requested method is not supported on an adapter for a data shaping connection.");
		}

		// Token: 0x040000E5 RID: 229
		private readonly Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection m_connection;
	}
}
