using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018E RID: 398
	internal sealed class ProgressiveDataExtensionConnection : ServerDataExtensionConnection
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x00035C48 File Offset: 0x00033E48
		public ProgressiveDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken)
			: base(deInstance, threadUser, execType, additionalToken)
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00035C55 File Offset: 0x00033E55
		public ProgressiveDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken, IDbConnectionPool connectionPool)
			: base(deInstance, threadUser, execType, additionalToken, connectionPool)
		{
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00035C64 File Offset: 0x00033E64
		public ProgressiveDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken, IDbConnectionPool connectionPool, IOpenConnectionExtension openConnectionExtension)
			: base(deInstance, threadUser, execType, additionalToken, connectionPool, openConnectionExtension)
		{
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00035C78 File Offset: 0x00033E78
		internal static ServerDataExtensionConnectionWrapper Open(RSService service, CatalogItemContext dataSourceItemContext, DataSourceInfo dataSourceInfo, IDbConnectionPool connectionPool)
		{
			ServerDataExtensionConnectionWrapper serverDataExtensionConnectionWrapper;
			using (MonitoredScope.NewFormat("ProgressiveDataExtensionConnection.Open[DataSource={0}]", dataSourceInfo.Name))
			{
				IAdditionalToken additionalToken = new ServerAdditionalToken(service, dataSourceItemContext);
				DefaultOpenConnectionExtension defaultOpenConnectionExtension = new DefaultOpenConnectionExtension();
				IProcessingDataExtensionConnection processingDataExtensionConnection = new ProgressiveDataExtensionConnection(service.HowToCreateDataExtensionInstance, service.UserContext, ReportProcessing.ExecutionType.Live, additionalToken, connectionPool, defaultOpenConnectionExtension);
				serverDataExtensionConnectionWrapper = ServerDataExtensionConnectionWrapper.Open(dataSourceInfo, processingDataExtensionConnection);
			}
			return serverDataExtensionConnectionWrapper;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00035CE4 File Offset: 0x00033EE4
		protected override void OnConnectionOpenFailure()
		{
			base.OnConnectionOpenFailure();
			ProgressiveReportCounters.Current.DataSourceConnectionFailuresTotal.Increment();
		}
	}
}
