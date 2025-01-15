using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D7 RID: 727
	internal interface ISubreportRetrieval : IRdlPersistence, IDisposable
	{
		// Token: 0x06001A0A RID: 6666
		void GetSubreport(ICatalogItemContext reportContext, string subreportPath, string newChunkName, ReportProcessing.NeedsUpgrade needsUpgradeCallback, ParameterInfoCollection parentQueryParameters, out ICatalogItemContext subreportContext, out string description, out IChunkFactory compiledDefinitionChunkFactory, out ParameterInfoCollection parameters);

		// Token: 0x06001A0B RID: 6667
		void GetSubreportDataSources(ICatalogItemContext reportContext, string subreportPath, ReportProcessing.NeedsUpgrade needsUpgradeCallback, out ICatalogItemContext subreportContext, out IChunkFactory compiledDefinitionChunkFactory, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets);

		// Token: 0x06001A0C RID: 6668
		void SetTargetSnapshot(ReportSnapshot target);
	}
}
