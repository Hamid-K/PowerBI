using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F4 RID: 2036
	public interface IProcessingDataReader : IDisposable
	{
		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x060071AE RID: 29102
		int AggregationFieldCount { get; }

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x060071AF RID: 29103
		bool ReaderExtensionsSupported { get; }

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x060071B0 RID: 29104
		bool ReaderFieldProperties { get; }

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x060071B1 RID: 29105
		bool IsAggregateRow { get; }

		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x060071B2 RID: 29106
		RecordSetInfo RecordSetInfo { get; }

		// Token: 0x060071B3 RID: 29107
		object GetColumn(int aliasIndex);

		// Token: 0x060071B4 RID: 29108
		bool GetNextRow();

		// Token: 0x060071B5 RID: 29109
		int GetPropertyCount(int aliasIndex);

		// Token: 0x060071B6 RID: 29110
		string GetPropertyName(int aliasIndex, int propertyIndex);

		// Token: 0x060071B7 RID: 29111
		object GetPropertyValue(int aliasIndex, int propertyIndex);

		// Token: 0x060071B8 RID: 29112
		RecordRow GetUnderlyingRecordRowObject();

		// Token: 0x060071B9 RID: 29113
		bool IsAggregationField(int aliasIndex);

		// Token: 0x060071BA RID: 29114
		void OverrideDataCacheCompareOptions(ref OnDemandProcessingContext context);

		// Token: 0x060071BB RID: 29115
		void OverrideWithDataReaderSettings(OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance, DataSetCore dataSetCore);

		// Token: 0x060071BC RID: 29116
		void GetDataReaderMappingForRowConsumer(DataSetInstance dataSetInstance, out bool mappingIdentical, out int[] mappingDataSetFieldIndexesToDataChunk);
	}
}
