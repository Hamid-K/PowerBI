using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200064E RID: 1614
	public interface ISharedDataSet
	{
		// Token: 0x060057B6 RID: 22454
		void Process(DataSetInfo sharedDataSet, string targetChunkNameInReportSnapshot, bool originalRequestNeedsDataChunk, IRowConsumer originalRequest, ParameterInfoCollection dataSetParameterValues, ReportProcessingContext originalProcessingContext);

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x060057B7 RID: 22455
		bool HasUserDependencies { get; }

		// Token: 0x1700201A RID: 8218
		// (set) Token: 0x060057B8 RID: 22456
		IChunkFactory TargetSnapshot { set; }
	}
}
