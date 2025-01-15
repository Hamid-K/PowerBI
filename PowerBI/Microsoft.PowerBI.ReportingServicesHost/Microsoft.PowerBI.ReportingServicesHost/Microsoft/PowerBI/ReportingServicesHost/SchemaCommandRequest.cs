using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000058 RID: 88
	internal class SchemaCommandRequest
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00005827 File Offset: 0x00003A27
		public SchemaCommandRequest(ExploreHostDataSourceInfo dataSource, IDbConnectionPool connectionPool, IConnectionFactory connectionFactory, QueryExecutionOptionsBase queryExecutionOptions, IConnectionUserImpersonator connectionUserImpersonator, ITelemetryService telemetryService)
		{
			this.DataSource = dataSource;
			this.ConnectionPool = connectionPool;
			this.ConnectionFactory = connectionFactory;
			this.QueryExecutionOptions = queryExecutionOptions;
			this.ConnectionUserImpersonator = connectionUserImpersonator;
			this.TelemetryService = telemetryService;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000585C File Offset: 0x00003A5C
		public ExploreHostDataSourceInfo DataSource { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00005864 File Offset: 0x00003A64
		public IDbConnectionPool ConnectionPool { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000586C File Offset: 0x00003A6C
		public IConnectionFactory ConnectionFactory { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005874 File Offset: 0x00003A74
		public QueryExecutionOptionsBase QueryExecutionOptions { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000587C File Offset: 0x00003A7C
		public IConnectionUserImpersonator ConnectionUserImpersonator { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005884 File Offset: 0x00003A84
		public ITelemetryService TelemetryService { get; }
	}
}
