using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000085 RID: 133
	internal sealed class ExecuteQueriesContext
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		internal ExecuteQueriesContext(IDbConnection connection, IProcessingDataExtensionConnection dataExtensionConnection, DataSourceInfo dataSourceInfo, CreateAndRegisterStream createAndRegisterStream, IJobContext jobContext)
		{
			this.m_connection = connection;
			this.m_dataExtensionConnection = dataExtensionConnection;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_createAndRegisterStream = createAndRegisterStream;
			this.m_jobContext = jobContext;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001BF19 File Offset: 0x0001A119
		internal IDbConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001BF21 File Offset: 0x0001A121
		internal CreateAndRegisterStream CreateAndRegisterStream
		{
			get
			{
				return this.m_createAndRegisterStream;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001BF29 File Offset: 0x0001A129
		internal IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001BF31 File Offset: 0x0001A131
		internal IDbCommand CreateCommandWrapperForCancel(IDbCommand command)
		{
			return new CommandWrappedForCancel(command, this.m_dataExtensionConnection, null, this.m_dataSourceInfo, null, this.m_connection);
		}

		// Token: 0x04000209 RID: 521
		private readonly IDbConnection m_connection;

		// Token: 0x0400020A RID: 522
		private readonly IProcessingDataExtensionConnection m_dataExtensionConnection;

		// Token: 0x0400020B RID: 523
		private readonly DataSourceInfo m_dataSourceInfo;

		// Token: 0x0400020C RID: 524
		private readonly CreateAndRegisterStream m_createAndRegisterStream;

		// Token: 0x0400020D RID: 525
		private readonly IJobContext m_jobContext;
	}
}
