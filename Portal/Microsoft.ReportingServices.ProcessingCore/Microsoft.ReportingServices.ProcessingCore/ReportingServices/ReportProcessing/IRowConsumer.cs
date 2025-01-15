using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200064D RID: 1613
	public interface IRowConsumer
	{
		// Token: 0x060057B3 RID: 22451
		void SetProcessingDataReader(IProcessingDataReader dataReader);

		// Token: 0x060057B4 RID: 22452
		void NextRow(RecordRow row);

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x060057B5 RID: 22453
		string ReportDataSetName { get; }
	}
}
