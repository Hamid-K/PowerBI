using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000056 RID: 86
	internal sealed class ModelMetadataRequest : SchemaCommandRequest
	{
		// Token: 0x060001DE RID: 478 RVA: 0x000057CA File Offset: 0x000039CA
		public ModelMetadataRequest(ExploreHostDataSourceInfo dataSource, TranslationsBehavior translationsBehavior, string modelMetadataVersion, IDbConnectionPool connectionPool, IConnectionFactory connectionFactory, QueryExecutionOptionsBase queryExecutionOptions, IConnectionUserImpersonator connectionUserImpersonator, ITelemetryService telemetryService, ConnectionType connectionType)
			: base(dataSource, connectionPool, connectionFactory, queryExecutionOptions, connectionUserImpersonator, telemetryService)
		{
			this.TranslationsBehavior = translationsBehavior;
			this.ModelMetadaVersion = modelMetadataVersion;
			this.ConnectionType = connectionType;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000057F3 File Offset: 0x000039F3
		public TranslationsBehavior TranslationsBehavior { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000057FB File Offset: 0x000039FB
		public string ModelMetadaVersion { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005803 File Offset: 0x00003A03
		public ConnectionType ConnectionType { get; }
	}
}
