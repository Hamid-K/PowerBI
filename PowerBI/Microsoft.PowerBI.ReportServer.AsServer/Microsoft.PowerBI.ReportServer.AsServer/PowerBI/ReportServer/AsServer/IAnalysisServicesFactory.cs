using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x0200001B RID: 27
	internal interface IAnalysisServicesFactory
	{
		// Token: 0x06000095 RID: 149
		TOMWrapper CreateASWrapper(AnalysisServicesSettings settings);

		// Token: 0x06000096 RID: 150
		IDataSourceReader CreateASDatabaseWrapper(AnalysisServicesSettings settings, string databaseName);

		// Token: 0x06000097 RID: 151
		TOMWrapper CreateASWrapperUsingTimeout(AnalysisServicesSettings settings);
	}
}
