using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F5 RID: 2037
	internal sealed class ProcessingDataReader : IProcessingDataReader, IDisposable
	{
		// Token: 0x060071BD RID: 29117 RVA: 0x001D8557 File Offset: 0x001D6757
		internal ProcessingDataReader(OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance, string dataSetName, IDataReader sourceReader, bool hasServerAggregateMetadata, string[] aliases, string[] names, DataSourceErrorInspector errorInspector)
		{
			this.m_recordSetInfo = new RecordSetInfo(hasServerAggregateMetadata, odpContext.IsSharedDataSetExecutionOnly, dataSetInstance, odpContext.ExecutionTime);
			this.m_dataSourceDataReader = new MappingDataReader(dataSetName, sourceReader, aliases, names, errorInspector);
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x001D8590 File Offset: 0x001D6790
		internal ProcessingDataReader(DataSetInstance dataSetInstance, DataSet dataSet, OnDemandProcessingContext odpContext, bool overrideWithSharedDataSetChunkSettings)
		{
			if (odpContext.IsSharedDataSetExecutionOnly)
			{
				this.m_dataSnapshotReader = new ChunkManager.DataChunkReader(dataSetInstance, odpContext, odpContext.ExternalDataSetContext.CachedDataChunkName);
			}
			else
			{
				this.m_dataSnapshotReader = odpContext.GetDataChunkReader(dataSet.IndexInCollection);
			}
			this.m_recordSetInfo = this.m_dataSnapshotReader.RecordSetInfo;
			this.m_dataSnapshotReader.MoveToFirstRow();
			if (overrideWithSharedDataSetChunkSettings)
			{
				this.OverrideWithDataReaderSettings(odpContext, dataSetInstance, dataSet.DataSetCore);
				return;
			}
			this.OverrideDataCacheCompareOptions(ref odpContext);
		}

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x060071BF RID: 29119 RVA: 0x001D860F File Offset: 0x001D680F
		public RecordSetInfo RecordSetInfo
		{
			get
			{
				return this.m_recordSetInfo;
			}
		}

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x060071C0 RID: 29120 RVA: 0x001D8617 File Offset: 0x001D6817
		public bool ReaderExtensionsSupported
		{
			get
			{
				if (this.m_dataSourceDataReader != null)
				{
					return this.m_dataSourceDataReader.ReaderExtensionsSupported;
				}
				return this.m_dataSnapshotReader.ReaderExtensionsSupported;
			}
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x060071C1 RID: 29121 RVA: 0x001D8638 File Offset: 0x001D6838
		public bool ReaderFieldProperties
		{
			get
			{
				if (this.m_dataSourceDataReader != null)
				{
					return this.m_dataSourceDataReader.ReaderFieldProperties;
				}
				return this.m_dataSnapshotReader.ReaderFieldProperties;
			}
		}

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x060071C2 RID: 29122 RVA: 0x001D8659 File Offset: 0x001D6859
		public bool IsAggregateRow
		{
			get
			{
				if (this.m_dataSourceDataReader != null)
				{
					return this.m_dataSourceDataReader.IsAggregateRow;
				}
				return this.m_dataSnapshotReader.IsAggregateRow;
			}
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x060071C3 RID: 29123 RVA: 0x001D867A File Offset: 0x001D687A
		public int AggregationFieldCount
		{
			get
			{
				if (this.m_dataSourceDataReader != null)
				{
					return this.m_dataSourceDataReader.AggregationFieldCount;
				}
				return this.m_dataSnapshotReader.AggregationFieldCount;
			}
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x001D869B File Offset: 0x001D689B
		public void Dispose()
		{
			if (this.m_dataSourceDataReader != null)
			{
				((IDisposable)this.m_dataSourceDataReader).Dispose();
				return;
			}
			((IDisposable)this.m_dataSnapshotReader).Dispose();
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x001D86BC File Offset: 0x001D68BC
		public void OverrideDataCacheCompareOptions(ref OnDemandProcessingContext context)
		{
			if (this.m_dataSnapshotReader != null && (context.ProcessWithCachedData || context.SnapshotProcessing) && this.m_dataSnapshotReader.ValidCompareOptions)
			{
				context.ClrCompareOptions = this.m_dataSnapshotReader.CompareOptions;
			}
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x001D86F7 File Offset: 0x001D68F7
		public bool GetNextRow()
		{
			if (this.m_dataSourceDataReader != null)
			{
				return this.m_dataSourceDataReader.GetNextRow();
			}
			return this.m_dataSnapshotReader.GetNextRow();
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x001D8718 File Offset: 0x001D6918
		public RecordRow GetUnderlyingRecordRowObject()
		{
			if (this.m_dataSnapshotReader != null)
			{
				return this.m_dataSnapshotReader.RecordRow;
			}
			return null;
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x001D8730 File Offset: 0x001D6930
		public object GetColumn(int aliasIndex)
		{
			object obj;
			if (this.m_dataSourceDataReader != null)
			{
				obj = this.m_dataSourceDataReader.GetFieldValue(aliasIndex);
			}
			else
			{
				obj = this.m_dataSnapshotReader.GetFieldValue(aliasIndex);
			}
			if (obj is DBNull)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x001D876E File Offset: 0x001D696E
		public bool IsAggregationField(int aliasIndex)
		{
			if (this.m_dataSourceDataReader != null)
			{
				return this.m_dataSourceDataReader.IsAggregationField(aliasIndex);
			}
			return this.m_dataSnapshotReader.IsAggregationField(aliasIndex);
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x001D8794 File Offset: 0x001D6994
		public int GetPropertyCount(int aliasIndex)
		{
			if (this.m_dataSourceDataReader != null)
			{
				return this.m_dataSourceDataReader.GetPropertyCount(aliasIndex);
			}
			if (this.m_dataSnapshotReader != null && this.m_dataSnapshotReader.FieldPropertyNames != null && this.m_dataSnapshotReader.FieldPropertyNames[aliasIndex] != null)
			{
				List<string> propertyNames = this.m_dataSnapshotReader.FieldPropertyNames.GetPropertyNames(aliasIndex);
				if (propertyNames != null)
				{
					return propertyNames.Count;
				}
			}
			return 0;
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x001D87FC File Offset: 0x001D69FC
		public string GetPropertyName(int aliasIndex, int propertyIndex)
		{
			if (this.m_dataSourceDataReader != null)
			{
				return this.m_dataSourceDataReader.GetPropertyName(aliasIndex, propertyIndex);
			}
			if (this.m_dataSnapshotReader != null && this.m_dataSnapshotReader.FieldPropertyNames != null)
			{
				return this.m_dataSnapshotReader.FieldPropertyNames.GetPropertyName(aliasIndex, propertyIndex);
			}
			return null;
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x001D8848 File Offset: 0x001D6A48
		public object GetPropertyValue(int aliasIndex, int propertyIndex)
		{
			object obj = null;
			if (this.m_dataSourceDataReader != null)
			{
				obj = this.m_dataSourceDataReader.GetPropertyValue(aliasIndex, propertyIndex);
			}
			else if (this.m_dataSnapshotReader != null)
			{
				obj = this.m_dataSnapshotReader.GetPropertyValue(aliasIndex, propertyIndex);
			}
			if (obj is DBNull)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x001D8890 File Offset: 0x001D6A90
		public void OverrideWithDataReaderSettings(OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance, DataSetCore dataSetCore)
		{
			ChunkManager.DataChunkReader.OverrideWithDataReaderSettings(this.m_recordSetInfo, odpContext, dataSetInstance, dataSetCore);
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x001D88A0 File Offset: 0x001D6AA0
		public void GetDataReaderMappingForRowConsumer(DataSetInstance dataSetInstance, out bool mappingIdentical, out int[] mappingDataSetFieldIndexesToDataChunk)
		{
			mappingIdentical = true;
			mappingDataSetFieldIndexesToDataChunk = null;
			ChunkManager.DataChunkReader.CreateDataChunkFieldMapping(dataSetInstance, this.m_recordSetInfo, false, out mappingIdentical, out mappingDataSetFieldIndexesToDataChunk);
		}

		// Token: 0x04003A82 RID: 14978
		private RecordSetInfo m_recordSetInfo;

		// Token: 0x04003A83 RID: 14979
		private MappingDataReader m_dataSourceDataReader;

		// Token: 0x04003A84 RID: 14980
		private ChunkManager.DataChunkReader m_dataSnapshotReader;
	}
}
