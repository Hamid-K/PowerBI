using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001B RID: 27
	public interface IDataReaderExtension : IDataReader, IDisposable
	{
		// Token: 0x0600003E RID: 62
		bool IsAggregationField(int index);

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63
		bool IsAggregateRow { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64
		int AggregationFieldCount { get; }
	}
}
