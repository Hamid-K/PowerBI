using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200002D RID: 45
	public interface IDataReaderExtension : IDataReader, IDisposable
	{
		// Token: 0x060000B4 RID: 180
		bool IsAggregationField(int index);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B5 RID: 181
		bool IsAggregateRow { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B6 RID: 182
		int AggregationFieldCount { get; }
	}
}
