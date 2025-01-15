using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000647 RID: 1607
	public interface IProcessingDataExtensionConnection
	{
		// Token: 0x0600578D RID: 22413
		void DataSetRetrieveForReportInstance(ICatalogItemContext itemContext, ParameterInfoCollection reportParameters);

		// Token: 0x0600578E RID: 22414
		IDbConnection OpenDataSourceExtensionConnection(IProcessingDataSource dataSource, string connectionString, DataSourceInfo dataSourceInfo, string datasetName);

		// Token: 0x0600578F RID: 22415
		void HandleImpersonation(IProcessingDataSource dataSource, DataSourceInfo dataSourceInfo, string datasetName, IDbConnection connection, Action afterImpersonationAction);

		// Token: 0x06005790 RID: 22416
		void CloseConnectionWithoutPool(IDbConnection connection);

		// Token: 0x06005791 RID: 22417
		void CloseConnection(IDbConnection connection, IProcessingDataSource dataSource, DataSourceInfo dataSourceInfo);

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06005792 RID: 22418
		bool MustResolveSharedDataSources { get; }
	}
}
