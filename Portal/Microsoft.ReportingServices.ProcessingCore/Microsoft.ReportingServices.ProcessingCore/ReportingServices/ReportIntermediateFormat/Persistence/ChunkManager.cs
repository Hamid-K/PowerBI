using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000530 RID: 1328
	internal sealed class ChunkManager
	{
		// Token: 0x06004860 RID: 18528 RVA: 0x00131F48 File Offset: 0x00130148
		internal static string GenerateDataChunkName(OnDemandProcessingContext context, int dataSetID, bool isInSubReport)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataChunk");
			stringBuilder.Append('x');
			if (isInSubReport)
			{
				stringBuilder.Append(context.SubReportUniqueName);
				stringBuilder.Append('x');
				stringBuilder.Append(context.SubReportDataChunkNameModifier);
				stringBuilder.Append('x');
			}
			stringBuilder.Append(dataSetID.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00131FBC File Offset: 0x001301BC
		internal static string GenerateLegacySharedSubReportDataChunkName(OnDemandProcessingContext context, int dataSetID)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataChunk");
			stringBuilder.Append('x');
			stringBuilder.Append(context.SubReportUniqueName);
			stringBuilder.Append('x');
			stringBuilder.Append(dataSetID.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00132014 File Offset: 0x00130214
		private static string GenerateDataChunkName(DataSetInstance dataSetInstance, OnDemandProcessingContext context)
		{
			DataSet dataSetDef = dataSetInstance.DataSetDef;
			string text;
			if (context.InSubreport)
			{
				text = ChunkManager.GenerateDataChunkName(context, dataSetDef.ID, true);
			}
			else
			{
				text = ChunkManager.GenerateDataChunkName(null, dataSetDef.ID, false);
			}
			return text;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00132054 File Offset: 0x00130254
		internal static void SerializeReport(Report report, Stream stream, IConfiguration configuration)
		{
			int compatibilityVersion = ReportProcessingCompatibilityVersion.GetCompatibilityVersion(configuration);
			IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, compatibilityVersion);
			intermediateFormatWriter.Write(report);
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x0013207C File Offset: 0x0013027C
		internal static Report DeserializeReport(bool keepReferences, GlobalIDOwnerCollection globalIDOwnerCollection, IDOwner parentIDOwner, ReportItem parentReportItem, Stream stream)
		{
			IntermediateFormatReader intermediateFormatReader = new IntermediateFormatReader(stream, new ProcessingRIFObjectCreator(parentIDOwner, parentReportItem), globalIDOwnerCollection);
			Report report = (Report)intermediateFormatReader.ReadRIFObject();
			report.ReportOrDescendentHasUserSortFilter = report.HasUserSortFilter;
			if (!keepReferences)
			{
				intermediateFormatReader.ClearReferences();
			}
			return report;
		}

		// Token: 0x04002043 RID: 8259
		internal const string Definition = "CompiledDefinition";

		// Token: 0x04002044 RID: 8260
		internal const string DocumentMap = "DocumentMap";

		// Token: 0x04002045 RID: 8261
		internal const string ShowHideInfo = "ShowHideInfo";

		// Token: 0x04002046 RID: 8262
		internal const string Bookmarks = "Bookmarks";

		// Token: 0x04002047 RID: 8263
		internal const string Drillthrough = "Drillthrough";

		// Token: 0x04002048 RID: 8264
		internal const string QuickFind = "QuickFind";

		// Token: 0x04002049 RID: 8265
		internal const string SortFilterEventInfo = "SortFilterEventInfo";

		// Token: 0x0400204A RID: 8266
		internal const string DataChunkPrefix = "DataChunk";

		// Token: 0x0400204B RID: 8267
		internal const string GroupTree = "GroupTree";

		// Token: 0x0400204C RID: 8268
		internal const string LookupInfo = "LookupInfo";

		// Token: 0x0400204D RID: 8269
		internal const string Metadata = "Metadata";

		// Token: 0x0400204E RID: 8270
		internal const string SharedDataSet = "SharedDataSet";

		// Token: 0x0400204F RID: 8271
		internal const char Delimiter = 'x';

		// Token: 0x02000995 RID: 2453
		internal sealed class DataChunkWriter : PersistenceHelper
		{
			// Token: 0x060080D7 RID: 32983 RVA: 0x00212774 File Offset: 0x00210974
			internal DataChunkWriter(RecordSetInfo recordSetInfo, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			{
				Global.Tracer.Assert(odpContext.ChunkFactory != null, "(null != context.ChunkFactory)");
				this.m_reportChunkFactory = odpContext.ChunkFactory;
				this.m_recordSetInfo = recordSetInfo;
				this.m_odpContext = odpContext;
				if (odpContext.IsSharedDataSetExecutionOnly)
				{
					this.m_dataSetChunkName = odpContext.ExternalDataSetContext.TargetChunkNameInSnapshot ?? "SharedDataSet";
					return;
				}
				this.m_dataSetChunkName = ChunkManager.GenerateDataChunkName(dataSetInstance, odpContext);
				odpContext.OdpMetadata.AddDataChunk(this.m_dataSetChunkName, dataSetInstance);
			}

			// Token: 0x060080D8 RID: 32984 RVA: 0x002127FB File Offset: 0x002109FB
			internal DataChunkWriter(DataSetInstance dataSetInstance, OnDemandProcessingContext context)
			{
				Global.Tracer.Assert(context.ChunkFactory != null, "(null != context.ChunkFactory)");
				this.m_odpContext = context;
				this.m_dataSetChunkName = ChunkManager.GenerateDataChunkName(dataSetInstance, context);
				this.m_reportChunkFactory = context.ChunkFactory;
			}

			// Token: 0x060080D9 RID: 32985 RVA: 0x0021283B File Offset: 0x00210A3B
			internal void Close()
			{
				this.m_chunkWriter = null;
				if (this.m_chunkStream != null)
				{
					this.m_chunkStream.Close();
					this.m_chunkStream = null;
				}
			}

			// Token: 0x060080DA RID: 32986 RVA: 0x00212864 File Offset: 0x00210A64
			internal void CloseAndEraseChunk()
			{
				this.Close();
				if (this.m_reportChunkFactory == null)
				{
					return;
				}
				this.m_reportChunkFactory.Erase(this.m_dataSetChunkName, ReportProcessing.ReportChunkTypes.Data);
				if (!this.m_odpContext.IsSharedDataSetExecutionOnly)
				{
					this.m_odpContext.OdpMetadata.DeleteDataChunk(this.m_dataSetChunkName);
				}
			}

			// Token: 0x060080DB RID: 32987 RVA: 0x002128B8 File Offset: 0x00210AB8
			internal void CreateDataChunkAndWriteHeader(RecordSetInfo recordSetInfo)
			{
				if (this.m_chunkStream == null)
				{
					this.m_recordSetInfo = recordSetInfo;
					this.m_chunkStream = this.m_reportChunkFactory.CreateChunk(this.m_dataSetChunkName, ReportProcessing.ReportChunkTypes.Data, null);
					this.m_chunkWriter = new IntermediateFormatWriter?(new IntermediateFormatWriter(this.m_chunkStream, ChunkManager.DataChunkWriter.m_DataChunkDeclarations, this, this.m_odpContext.GetActiveCompatibilityVersion(), this.m_odpContext.ProhibitSerializableValues));
					this.m_chunkWriter.Value.Write(this.m_recordSetInfo);
				}
			}

			// Token: 0x060080DC RID: 32988 RVA: 0x00212938 File Offset: 0x00210B38
			internal void WriteRecordRow(RecordRow recordRow)
			{
				try
				{
					if (this.m_chunkStream == null)
					{
						this.CreateDataChunkAndWriteHeader(this.m_recordSetInfo);
					}
					recordRow.StreamPosition = this.m_chunkStream.Position;
					this.m_chunkWriter.Value.Write(recordRow);
				}
				catch (Exception)
				{
					this.Close();
					throw;
				}
			}

			// Token: 0x060080DD RID: 32989 RVA: 0x0021299C File Offset: 0x00210B9C
			private static List<Declaration> GetDataChunkDeclarations()
			{
				return new List<Declaration>(4)
				{
					RecordSetInfo.GetDeclaration(),
					RecordRow.GetDeclaration(),
					RecordField.GetDeclaration(),
					RecordSetPropertyNames.GetDeclaration()
				};
			}

			// Token: 0x04004191 RID: 16785
			private static List<Declaration> m_DataChunkDeclarations = ChunkManager.DataChunkWriter.GetDataChunkDeclarations();

			// Token: 0x04004192 RID: 16786
			private IChunkFactory m_reportChunkFactory;

			// Token: 0x04004193 RID: 16787
			private string m_dataSetChunkName;

			// Token: 0x04004194 RID: 16788
			private RecordSetInfo m_recordSetInfo;

			// Token: 0x04004195 RID: 16789
			private IntermediateFormatWriter? m_chunkWriter;

			// Token: 0x04004196 RID: 16790
			private Stream m_chunkStream;

			// Token: 0x04004197 RID: 16791
			private OnDemandProcessingContext m_odpContext;
		}

		// Token: 0x02000996 RID: 2454
		internal sealed class DataChunkReader : IRecordRowReader, IDisposable
		{
			// Token: 0x060080DF RID: 32991 RVA: 0x002129DC File Offset: 0x00210BDC
			internal DataChunkReader(DataSetInstance dataSetInstance, OnDemandProcessingContext context, string chunkName)
			{
				this.m_recordSetSize = dataSetInstance.RecordSetSize;
				Global.Tracer.Assert(context.ChunkFactory != null && !string.IsNullOrEmpty(chunkName), "null != context.ChunkFactory && !String.IsNullOrEmpty(chunkName)");
				string text;
				this.m_chunkStream = context.ChunkFactory.GetChunk(chunkName, ReportProcessing.ReportChunkTypes.Data, ChunkMode.Open, out text);
				Global.Tracer.Assert(this.m_chunkStream != null, "Missing Expected DataChunk with name: {0}", new object[] { chunkName });
				this.m_chunkReader = new IntermediateFormatReader?(new IntermediateFormatReader(this.m_chunkStream, default(DataReaderRIFObjectCreator)));
				this.m_recordSetInfo = (RecordSetInfo)this.m_chunkReader.Value.ReadRIFObject();
				if (context.IsSharedDataSetExecutionOnly || dataSetInstance.DataSetDef.IsReferenceToSharedDataSet)
				{
					ChunkManager.DataChunkReader.CreateDataChunkFieldMapping(dataSetInstance, this.m_recordSetInfo, context.IsSharedDataSetExecutionOnly, out this.m_mappingIdentical, out this.m_mappingDataSetFieldIndexesToDataChunk);
				}
				this.m_firstRow = this.m_chunkStream.Position;
				if (-1 == this.m_recordSetSize)
				{
					this.m_streamLength = this.m_chunkStream.Length;
					Global.Tracer.Assert(this.m_streamLength >= this.m_firstRow, "(m_streamLength >= m_firstRow)");
				}
			}

			// Token: 0x170029AB RID: 10667
			// (get) Token: 0x060080E0 RID: 32992 RVA: 0x00212B3C File Offset: 0x00210D3C
			internal bool ReaderExtensionsSupported
			{
				get
				{
					return this.m_chunkStream != null && this.m_recordSetInfo.ReaderExtensionsSupported;
				}
			}

			// Token: 0x170029AC RID: 10668
			// (get) Token: 0x060080E1 RID: 32993 RVA: 0x00212B53 File Offset: 0x00210D53
			internal bool ReaderFieldProperties
			{
				get
				{
					return this.m_recordSetInfo != null && this.m_recordSetInfo.FieldPropertyNames != null;
				}
			}

			// Token: 0x170029AD RID: 10669
			// (get) Token: 0x060080E2 RID: 32994 RVA: 0x00212B6D File Offset: 0x00210D6D
			internal bool ValidCompareOptions
			{
				get
				{
					return this.m_chunkStream != null && this.m_recordSetInfo.ValidCompareOptions;
				}
			}

			// Token: 0x170029AE RID: 10670
			// (get) Token: 0x060080E3 RID: 32995 RVA: 0x00212B84 File Offset: 0x00210D84
			internal CompareOptions CompareOptions
			{
				get
				{
					return this.m_recordSetInfo.CompareOptions;
				}
			}

			// Token: 0x170029AF RID: 10671
			// (get) Token: 0x060080E4 RID: 32996 RVA: 0x00212B91 File Offset: 0x00210D91
			internal RecordSetInfo RecordSetInfo
			{
				get
				{
					return this.m_recordSetInfo;
				}
			}

			// Token: 0x170029B0 RID: 10672
			// (get) Token: 0x060080E5 RID: 32997 RVA: 0x00212B99 File Offset: 0x00210D99
			public RecordRow RecordRow
			{
				get
				{
					return this.m_recordRow;
				}
			}

			// Token: 0x170029B1 RID: 10673
			// (get) Token: 0x060080E6 RID: 32998 RVA: 0x00212BA1 File Offset: 0x00210DA1
			internal bool IsAggregateRow
			{
				get
				{
					return this.m_recordRow.IsAggregateRow;
				}
			}

			// Token: 0x170029B2 RID: 10674
			// (get) Token: 0x060080E7 RID: 32999 RVA: 0x00212BAE File Offset: 0x00210DAE
			internal int AggregationFieldCount
			{
				get
				{
					return this.m_recordRow.AggregationFieldCount;
				}
			}

			// Token: 0x170029B3 RID: 10675
			// (get) Token: 0x060080E8 RID: 33000 RVA: 0x00212BBB File Offset: 0x00210DBB
			internal RecordSetPropertyNamesList FieldPropertyNames
			{
				get
				{
					return this.m_recordSetInfo.FieldPropertyNames;
				}
			}

			// Token: 0x060080E9 RID: 33001 RVA: 0x00212BC8 File Offset: 0x00210DC8
			internal static void OverrideWithDataReaderSettings(RecordSetInfo recordSetInfo, OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance, DataSetCore dataSetCore)
			{
				if (recordSetInfo != null)
				{
					dataSetCore.MergeCollationSettings(null, null, recordSetInfo.CultureName, (recordSetInfo.CompareOptions & CompareOptions.IgnoreCase) == CompareOptions.None, (recordSetInfo.CompareOptions & CompareOptions.IgnoreNonSpace) == CompareOptions.None, (recordSetInfo.CompareOptions & CompareOptions.IgnoreKanaType) == CompareOptions.None, (recordSetInfo.CompareOptions & CompareOptions.IgnoreWidth) == CompareOptions.None);
					odpContext.SetComparisonInformation(dataSetCore);
					odpContext.ReportObjectModel.FieldsImpl.ReaderExtensionsSupported = recordSetInfo.ReaderExtensionsSupported;
					odpContext.ReportObjectModel.FieldsImpl.ReaderFieldProperties = recordSetInfo != null && recordSetInfo.FieldPropertyNames != null;
					dataSetInstance.CommandText = recordSetInfo.CommandText;
					dataSetInstance.RewrittenCommandText = recordSetInfo.RewrittenCommandText;
					dataSetInstance.SetQueryExecutionTime(recordSetInfo.ExecutionTime);
				}
			}

			// Token: 0x060080EA RID: 33002 RVA: 0x00212C78 File Offset: 0x00210E78
			internal static void CreateDataChunkFieldMapping(DataSetInstance currentDataSetInstance, RecordSetInfo recordSetInfo, bool isSharedDataSetExecutionReader, out bool mappingIdentical, out int[] mappingDataSetFieldIndexesToDataChunk)
			{
				mappingDataSetFieldIndexesToDataChunk = null;
				mappingIdentical = true;
				string[] fieldNames = recordSetInfo.FieldNames;
				RecordSetPropertyNamesList fieldPropertyNames = recordSetInfo.FieldPropertyNames;
				List<Field> fields = currentDataSetInstance.DataSetDef.Fields;
				if (fieldNames != null && fields != null)
				{
					int num = (isSharedDataSetExecutionReader ? currentDataSetInstance.DataSetDef.Fields.Count : currentDataSetInstance.DataSetDef.NonCalculatedFieldCount);
					if (fieldPropertyNames != null && fieldPropertyNames.Count > 0)
					{
						currentDataSetInstance.FieldInfos = new FieldInfo[num];
					}
					mappingIdentical = fieldNames.Length == num;
					Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.Ordinal);
					for (int i = 0; i < fieldNames.Length; i++)
					{
						dictionary.Add(fieldNames[i], i);
					}
					int count = fields.Count;
					int num2 = 0;
					mappingDataSetFieldIndexesToDataChunk = new int[num];
					for (int j = 0; j < count; j++)
					{
						if (!fields[j].IsCalculatedField || isSharedDataSetExecutionReader)
						{
							string text = fields[j].DataField;
							if (isSharedDataSetExecutionReader || fields[j].IsCalculatedField)
							{
								text = fields[j].Name;
							}
							int num3;
							if (dictionary.TryGetValue(text, out num3))
							{
								mappingDataSetFieldIndexesToDataChunk[num2] = num3;
								if (fieldPropertyNames != null && num3 < fieldPropertyNames.Count && fieldPropertyNames[num3] != null)
								{
									List<string> propertyNames = fieldPropertyNames.GetPropertyNames(num3);
									if (propertyNames != null)
									{
										currentDataSetInstance.FieldInfos[num2] = new FieldInfo(ChunkManager.DataChunkReader.CreateSequentialIndexList(propertyNames.Count), propertyNames);
									}
								}
								if (num2 != num3)
								{
									mappingIdentical = false;
								}
							}
							else
							{
								mappingDataSetFieldIndexesToDataChunk[num2] = -1;
								mappingIdentical = false;
							}
							num2++;
						}
					}
				}
			}

			// Token: 0x060080EB RID: 33003 RVA: 0x00212E08 File Offset: 0x00211008
			private static List<int> CreateSequentialIndexList(int capacity)
			{
				List<int> list = new List<int>(capacity);
				for (int i = 0; i < capacity; i++)
				{
					list.Add(i);
				}
				return list;
			}

			// Token: 0x060080EC RID: 33004 RVA: 0x00212E30 File Offset: 0x00211030
			public bool MoveToFirstRow()
			{
				if (this.m_chunkStream == null || !this.m_chunkStream.CanSeek)
				{
					return false;
				}
				this.m_chunkReader.Value.Seek(this.m_firstRow, SeekOrigin.Begin);
				this.m_currentRow = -1;
				this.m_previousStreamOffset = -1L;
				this.m_recordRow = null;
				return true;
			}

			// Token: 0x060080ED RID: 33005 RVA: 0x00212E85 File Offset: 0x00211085
			internal void ResetCachedStreamOffset()
			{
				this.m_previousStreamOffset = -1L;
			}

			// Token: 0x060080EE RID: 33006 RVA: 0x00212E90 File Offset: 0x00211090
			public bool GetNextRow()
			{
				if (this.m_chunkStream == null)
				{
					return false;
				}
				bool flag = false;
				if (-1 == this.m_recordSetSize)
				{
					if (this.m_chunkStream.Position < this.m_streamLength - 1L)
					{
						flag = true;
					}
				}
				else if (this.m_currentRow < this.m_recordSetSize - 1)
				{
					flag = true;
				}
				if (flag)
				{
					this.m_previousStreamOffset = this.m_chunkStream.Position;
					this.m_currentRow++;
					this.ReadNextRow();
				}
				return flag;
			}

			// Token: 0x060080EF RID: 33007 RVA: 0x00212F08 File Offset: 0x00211108
			internal bool ReadOneRowAtPosition(long offset)
			{
				if (this.m_chunkStream == null)
				{
					return false;
				}
				if (this.m_previousStreamOffset == offset)
				{
					return false;
				}
				this.m_previousStreamOffset = offset;
				this.m_chunkReader.Value.Seek(offset, SeekOrigin.Begin);
				this.ReadNextRow();
				return true;
			}

			// Token: 0x060080F0 RID: 33008 RVA: 0x00212F50 File Offset: 0x00211150
			internal object GetFieldValue(int aliasIndex)
			{
				if (this.m_recordRow.RecordFields[aliasIndex] == null)
				{
					throw new ReportProcessingException_FieldError(DataFieldStatus.IsMissing, null);
				}
				return this.m_recordRow.GetFieldValue(aliasIndex);
			}

			// Token: 0x060080F1 RID: 33009 RVA: 0x00212F84 File Offset: 0x00211184
			internal bool IsAggregationField(int aliasIndex)
			{
				return this.m_recordRow.IsAggregationField(aliasIndex);
			}

			// Token: 0x060080F2 RID: 33010 RVA: 0x00212F94 File Offset: 0x00211194
			internal object GetPropertyValue(int aliasIndex, int propertyIndex)
			{
				if (this.m_recordSetInfo.FieldPropertyNames != null && this.m_recordRow.RecordFields[aliasIndex] != null)
				{
					List<object> fieldPropertyValues = this.m_recordRow.RecordFields[aliasIndex].FieldPropertyValues;
					if (fieldPropertyValues != null && propertyIndex >= 0 && propertyIndex < fieldPropertyValues.Count)
					{
						return fieldPropertyValues[propertyIndex];
					}
				}
				return null;
			}

			// Token: 0x060080F3 RID: 33011 RVA: 0x00212FEC File Offset: 0x002111EC
			internal int GetPropertyCount(int aliasIndex)
			{
				if (this.m_recordSetInfo.FieldPropertyNames != null && this.m_recordRow.RecordFields[aliasIndex] != null && this.m_recordRow.RecordFields[aliasIndex].FieldPropertyValues != null)
				{
					return this.m_recordRow.RecordFields[aliasIndex].FieldPropertyValues.Count;
				}
				return 0;
			}

			// Token: 0x060080F4 RID: 33012 RVA: 0x00213042 File Offset: 0x00211242
			internal string GetPropertyName(int aliasIndex, int propertyIndex)
			{
				if (this.m_recordSetInfo.FieldPropertyNames != null && this.m_recordSetInfo.FieldPropertyNames[aliasIndex] != null)
				{
					return this.m_recordSetInfo.FieldPropertyNames[aliasIndex].PropertyNames[propertyIndex];
				}
				return null;
			}

			// Token: 0x060080F5 RID: 33013 RVA: 0x00213082 File Offset: 0x00211282
			public void Close()
			{
				this.Dispose(true);
			}

			// Token: 0x060080F6 RID: 33014 RVA: 0x0021308B File Offset: 0x0021128B
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x060080F7 RID: 33015 RVA: 0x00213094 File Offset: 0x00211294
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this.m_chunkReader != null)
					{
						this.m_chunkReader = null;
					}
					if (this.m_chunkStream != null)
					{
						this.m_chunkStream.Close();
						this.m_chunkStream = null;
					}
				}
				this.m_recordRow = null;
				this.m_recordSetInfo = null;
			}

			// Token: 0x060080F8 RID: 33016 RVA: 0x002130E8 File Offset: 0x002112E8
			private void ReadNextRow()
			{
				this.m_recordRow = (RecordRow)this.m_chunkReader.Value.ReadRIFObject();
				if (!this.m_mappingIdentical)
				{
					this.m_recordRow.ApplyFieldMapping(this.m_mappingDataSetFieldIndexesToDataChunk);
				}
			}

			// Token: 0x04004198 RID: 16792
			private Stream m_chunkStream;

			// Token: 0x04004199 RID: 16793
			private IntermediateFormatReader? m_chunkReader;

			// Token: 0x0400419A RID: 16794
			private RecordSetInfo m_recordSetInfo;

			// Token: 0x0400419B RID: 16795
			private RecordRow m_recordRow;

			// Token: 0x0400419C RID: 16796
			private int m_recordSetSize = -1;

			// Token: 0x0400419D RID: 16797
			private int m_currentRow = -1;

			// Token: 0x0400419E RID: 16798
			private long m_streamLength = -1L;

			// Token: 0x0400419F RID: 16799
			private long m_previousStreamOffset = -1L;

			// Token: 0x040041A0 RID: 16800
			private long m_firstRow = -1L;

			// Token: 0x040041A1 RID: 16801
			private int[] m_mappingDataSetFieldIndexesToDataChunk;

			// Token: 0x040041A2 RID: 16802
			private bool m_mappingIdentical;
		}

		// Token: 0x02000997 RID: 2455
		internal sealed class OnDemandProcessingManager
		{
			// Token: 0x060080F9 RID: 33017 RVA: 0x0021312C File Offset: 0x0021132C
			internal OnDemandProcessingManager()
			{
			}

			// Token: 0x060080FA RID: 33018 RVA: 0x00213134 File Offset: 0x00211334
			internal void SetOdpContext(OnDemandProcessingContext odpContext)
			{
				this.m_odpContext = odpContext;
			}

			// Token: 0x060080FB RID: 33019 RVA: 0x00213140 File Offset: 0x00211340
			internal static GlobalIDOwnerCollection DeserializeOdpReportSnapshot(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, IChunkFactory originalSnapshotChunks, ProcessingErrorContext errorContext, bool fetchSubreports, bool deserializeGroupTree, IConfiguration configuration, ref OnDemandMetadata odpMetadata, out Report report)
			{
				GlobalIDOwnerCollection globalIDOwnerCollection = new GlobalIDOwnerCollection();
				report = ReportProcessing.DeserializeKatmaiReport(pc.ChunkFactory, true, globalIDOwnerCollection);
				IChunkFactory chunkFactory = originalSnapshotChunks ?? pc.ChunkFactory;
				if (odpMetadata == null)
				{
					odpMetadata = ChunkManager.OnDemandProcessingManager.DeserializeOnDemandMetadata(chunkFactory, globalIDOwnerCollection);
				}
				if (pc.Parameters != null)
				{
					pc.Parameters.StoreLabels();
				}
				if (fetchSubreports)
				{
					ReportProcessing.FetchSubReports(report, pc.ChunkFactory, errorContext, odpMetadata, pc.ReportContext, pc.OnDemandSubReportCallback, 0, true, false, globalIDOwnerCollection, pc.QueryParameters);
					if (deserializeGroupTree)
					{
						ChunkManager.OnDemandProcessingManager.DeserializeGroupTree(report, chunkFactory, globalIDOwnerCollection, configuration, ref odpMetadata);
					}
				}
				odpMetadata.GlobalIDOwnerCollection = globalIDOwnerCollection;
				return globalIDOwnerCollection;
			}

			// Token: 0x060080FC RID: 33020 RVA: 0x002131D8 File Offset: 0x002113D8
			internal static void DeserializeGroupTree(Report report, IChunkFactory chunkFactory, GlobalIDOwnerCollection globalIDOwnerCollection, IConfiguration configuration, ref OnDemandMetadata odpMetadata)
			{
				bool flag = configuration != null && configuration.ProhibitSerializableValues;
				ChunkManager.OnDemandProcessingManager.EnsureGroupTreeStorageSetup(odpMetadata, chunkFactory, globalIDOwnerCollection, true, ReportProcessingCompatibilityVersion.GetCompatibilityVersion(configuration), flag);
				GroupTreePartition groupTreePartition = (GroupTreePartition)odpMetadata.GroupTreeScalabilityCache.Storage.Retrieve(odpMetadata.GroupTreeRootOffset);
				Global.Tracer.Assert(groupTreePartition.TopLevelScopeInstances[0].GetObjectType() == ObjectType.ReportInstanceReference, "GroupTree root partition did not contain a ReportInstance");
				odpMetadata.ReportInstance = groupTreePartition.TopLevelScopeInstances[0] as IReference<ReportInstance>;
				odpMetadata.Report = report;
				odpMetadata.ReportSnapshot.Report = report;
			}

			// Token: 0x060080FD RID: 33021 RVA: 0x00213278 File Offset: 0x00211478
			internal static void EnsureGroupTreeStorageSetup(OnDemandMetadata odpMetadata, IChunkFactory chunkFactory, GlobalIDOwnerCollection globalIDOwnerCollection, bool openExisting, int rifCompatVersion, bool prohibitSerializableValues)
			{
				if (odpMetadata.GroupTreeScalabilityCache == null)
				{
					IStorage storage = new RIFAppendOnlyStorage(ChunkManager.OnDemandProcessingManager.BuildChunkStreamHandler("GroupTree", ReportProcessing.ReportChunkTypes.Main, chunkFactory, openExisting), default(GroupTreeRIFObjectCreator), GroupTreeReferenceCreator.Instance, globalIDOwnerCollection, openExisting, rifCompatVersion, prohibitSerializableValues);
					odpMetadata.GroupTreeScalabilityCache = new GroupTreeScalabilityCache(odpMetadata.GroupTreePartitionManager, storage);
				}
			}

			// Token: 0x060080FE RID: 33022 RVA: 0x002132CC File Offset: 0x002114CC
			internal static void EnsureLookupStorageSetup(OnDemandMetadata odpMetadata, IChunkFactory chunkFactory, bool openExisting, int rifCompatVersion, bool prohibitSerializableValues)
			{
				if (odpMetadata.LookupScalabilityCache == null)
				{
					new AppendOnlySpaceManager();
					IStorage storage = new RIFAppendOnlyStorage(ChunkManager.OnDemandProcessingManager.BuildChunkStreamHandler("LookupInfo", ReportProcessing.ReportChunkTypes.LookupInfo, chunkFactory, openExisting), default(LookupRIFObjectCreator), LookupReferenceCreator.Instance, null, openExisting, rifCompatVersion, prohibitSerializableValues);
					odpMetadata.LookupScalabilityCache = new LookupScalabilityCache(odpMetadata.LookupPartitionManager, storage);
				}
			}

			// Token: 0x060080FF RID: 33023 RVA: 0x00213325 File Offset: 0x00211525
			private static IStreamHandler BuildChunkStreamHandler(string chunkName, ReportProcessing.ReportChunkTypes chunkType, IChunkFactory chunkFactory, bool openExisting)
			{
				return new ChunkFactoryStreamHandler(chunkName, chunkType, chunkFactory, openExisting);
			}

			// Token: 0x06008100 RID: 33024 RVA: 0x00213330 File Offset: 0x00211530
			internal static void PreparePartitionedTreesForAsyncSerialization(OnDemandProcessingContext odpContext)
			{
				ChunkManager.OnDemandProcessingManager.PreparePartitionedTreeForAsyncSerialization(odpContext.OdpMetadata.GroupTreeScalabilityCache, odpContext, "GroupTree", ReportProcessing.ReportChunkTypes.Main);
				ChunkManager.OnDemandProcessingManager.PreparePartitionedTreeForAsyncSerialization(odpContext.OdpMetadata.LookupScalabilityCache, odpContext, "LookupInfo", ReportProcessing.ReportChunkTypes.LookupInfo);
			}

			// Token: 0x06008101 RID: 33025 RVA: 0x00213364 File Offset: 0x00211564
			private static void PreparePartitionedTreeForAsyncSerialization(PartitionedTreeScalabilityCache scaleCache, OnDemandProcessingContext odpContext, string chunkName, ReportProcessing.ReportChunkTypes chunkType)
			{
				if (scaleCache != null)
				{
					RIFAppendOnlyStorage rifappendOnlyStorage = scaleCache.Storage as RIFAppendOnlyStorage;
					if (rifappendOnlyStorage != null)
					{
						IStreamHandler streamHandler = ChunkManager.OnDemandProcessingManager.BuildChunkStreamHandler(chunkName, chunkType, odpContext.ChunkFactory, rifappendOnlyStorage.FromExistingStream);
						rifappendOnlyStorage.Reset(streamHandler);
					}
					scaleCache.PrepareForFlush();
				}
			}

			// Token: 0x06008102 RID: 33026 RVA: 0x002133A4 File Offset: 0x002115A4
			internal static void PreparePartitionedTreesForSyncSerialization(OnDemandProcessingContext odpContext)
			{
				OnDemandMetadata odpMetadata = odpContext.OdpMetadata;
				if (odpMetadata.GroupTreeScalabilityCache != null)
				{
					odpMetadata.GroupTreeScalabilityCache.PrepareForFlush();
				}
				if (odpMetadata.LookupScalabilityCache != null)
				{
					odpMetadata.LookupScalabilityCache.PrepareForFlush();
				}
			}

			// Token: 0x06008103 RID: 33027 RVA: 0x002133E0 File Offset: 0x002115E0
			internal static OnDemandMetadata DeserializeOnDemandMetadata(IChunkFactory chunkFactory, GlobalIDOwnerCollection globalIDOwnerCollection)
			{
				Stream stream = null;
				OnDemandMetadata onDemandMetadata2;
				try
				{
					string text;
					stream = chunkFactory.GetChunk("Metadata", ReportProcessing.ReportChunkTypes.Main, ChunkMode.Open, out text);
					IntermediateFormatReader intermediateFormatReader = new IntermediateFormatReader(stream, default(GroupTreeRIFObjectCreator), globalIDOwnerCollection);
					OnDemandMetadata onDemandMetadata = (OnDemandMetadata)intermediateFormatReader.ReadRIFObject();
					Global.Tracer.Assert(onDemandMetadata != null, "(null != odpMetadata)");
					stream.Close();
					stream = null;
					onDemandMetadata.OdpChunkManager = new ChunkManager.OnDemandProcessingManager();
					onDemandMetadata2 = onDemandMetadata;
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
				return onDemandMetadata2;
			}

			// Token: 0x06008104 RID: 33028 RVA: 0x0021346C File Offset: 0x0021166C
			internal void SerializeSnapshot()
			{
				Global.Tracer.Assert(this.m_odpContext != null, "OnDemandProcessingContext is unavailable");
				OnDemandMetadata odpMetadata = this.m_odpContext.OdpMetadata;
				if (!odpMetadata.SnapshotHasChanged)
				{
					return;
				}
				try
				{
					IReference<ReportInstance> reportInstance = odpMetadata.ReportInstance;
					Global.Tracer.Assert(reportInstance != null, "Missing GroupTreeRoot");
					if (odpMetadata.IsInitialProcessingRequest)
					{
						reportInstance.UnPinValue();
					}
					if (odpMetadata.GroupTreeHasChanged || odpMetadata.IsInitialProcessingRequest)
					{
						GroupTreeScalabilityCache groupTreeScalabilityCache = this.m_odpContext.OdpMetadata.GroupTreeScalabilityCache;
						groupTreeScalabilityCache.Flush();
						if (odpMetadata.IsInitialProcessingRequest)
						{
							GroupTreePartition groupTreePartition = new GroupTreePartition();
							groupTreePartition.AddTopLevelScopeInstance((IReference<ScopeInstance>)reportInstance);
							long num = groupTreeScalabilityCache.Storage.Allocate(groupTreePartition);
							groupTreeScalabilityCache.Storage.Flush();
							odpMetadata.GroupTreeRootOffset = num;
						}
					}
					if (odpMetadata.LookupInfoHasChanged)
					{
						this.m_odpContext.OdpMetadata.LookupScalabilityCache.Flush();
					}
					ChunkManager.OnDemandProcessingManager.SerializeMetadata(this.m_odpContext.ChunkFactory, this.m_odpContext.OdpMetadata, this.m_odpContext.GetActiveCompatibilityVersion(), this.m_odpContext.ProhibitSerializableValues);
					ChunkManager.OnDemandProcessingManager.SerializeSortFilterEventInfo(this.m_odpContext);
				}
				finally
				{
					if (odpMetadata != null)
					{
						odpMetadata.DisposePersistedTreeScalability();
					}
				}
			}

			// Token: 0x06008105 RID: 33029 RVA: 0x002135A8 File Offset: 0x002117A8
			internal static void SerializeMetadata(IChunkFactory chunkFactory, OnDemandMetadata odpMetadata, int compatVersion, bool prohibitSerializableValues)
			{
				odpMetadata.UpdateLastAssignedGlobalID();
				using (Stream stream = chunkFactory.CreateChunk("Metadata", ReportProcessing.ReportChunkTypes.Main, null))
				{
					IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, compatVersion, prohibitSerializableValues);
					intermediateFormatWriter.Write(odpMetadata);
				}
			}

			// Token: 0x06008106 RID: 33030 RVA: 0x002135F8 File Offset: 0x002117F8
			private static void SerializeSortFilterEventInfo(OnDemandProcessingContext odpContext)
			{
				ReportSnapshot reportSnapshot = odpContext.OdpMetadata.ReportSnapshot;
				if (reportSnapshot == null || reportSnapshot.SortFilterEventInfo == null)
				{
					return;
				}
				Stream stream = null;
				try
				{
					string text;
					stream = odpContext.ChunkFactory.GetChunk("SortFilterEventInfo", ReportProcessing.ReportChunkTypes.Interactivity, ChunkMode.OpenOrCreate, out text);
					stream.Seek(0L, SeekOrigin.End);
					IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, odpContext.GetActiveCompatibilityVersion(), odpContext.ProhibitSerializableValues);
					intermediateFormatWriter.Write(reportSnapshot.SortFilterEventInfo);
					reportSnapshot.SortFilterEventInfo = null;
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}

			// Token: 0x06008107 RID: 33031 RVA: 0x00213684 File Offset: 0x00211884
			internal static SortFilterEventInfoMap DeserializeSortFilterEventInfo(IChunkFactory originalSnapshotChunks, GlobalIDOwnerCollection globalIDOwnerCollection)
			{
				Stream stream = null;
				SortFilterEventInfoMap sortFilterEventInfoMap = null;
				try
				{
					string text;
					stream = originalSnapshotChunks.GetChunk("SortFilterEventInfo", ReportProcessing.ReportChunkTypes.Interactivity, ChunkMode.Open, out text);
					if (stream != null)
					{
						IntermediateFormatReader intermediateFormatReader;
						do
						{
							intermediateFormatReader = new IntermediateFormatReader(stream, new ProcessingRIFObjectCreator(null, null), globalIDOwnerCollection);
							SortFilterEventInfoMap sortFilterEventInfoMap2 = (SortFilterEventInfoMap)intermediateFormatReader.ReadRIFObject();
							Global.Tracer.Assert(sortFilterEventInfoMap2 != null, "(null != newInfo)");
							if (sortFilterEventInfoMap == null)
							{
								sortFilterEventInfoMap = sortFilterEventInfoMap2;
							}
							else
							{
								sortFilterEventInfoMap.Merge(sortFilterEventInfoMap2);
							}
						}
						while (!intermediateFormatReader.EOS);
					}
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
				return sortFilterEventInfoMap;
			}

			// Token: 0x06008108 RID: 33032 RVA: 0x00213710 File Offset: 0x00211910
			internal static List<Declaration> GetChunkDeclarations()
			{
				if (ChunkManager.OnDemandProcessingManager.m_ChunkDeclarations == null)
				{
					return new List<Declaration>(21)
					{
						ScopeInstance.GetDeclaration(),
						ReportInstance.GetDeclaration(),
						DataSetInstance.GetDeclaration(),
						DataRegionInstance.GetDeclaration(),
						DataRegionMemberInstance.GetDeclaration(),
						DataCellInstance.GetDeclaration(),
						DataAggregateObjResult.GetDeclaration(),
						SubReportInstance.GetDeclaration(),
						GroupTreePartition.GetDeclaration(),
						ReportSnapshot.GetDeclaration(),
						ParametersImplWrapper.GetDeclaration(),
						ParameterImplWrapper.GetDeclaration(),
						SubReportInfo.GetDeclaration(),
						ParameterInfo.GetNewDeclaration(),
						ParameterInfoCollection.GetDeclaration(),
						ParameterBase.GetNewDeclaration(),
						ValidValue.GetNewDeclaration(),
						FieldInfo.GetDeclaration(),
						TreePartitionManager.GetDeclaration(),
						LookupObjResult.GetDeclaration(),
						DataCellInstanceList.GetDeclaration()
					};
				}
				return ChunkManager.OnDemandProcessingManager.m_ChunkDeclarations;
			}

			// Token: 0x06008109 RID: 33033 RVA: 0x0021381C File Offset: 0x00211A1C
			internal static Stream OpenExistingDocumentMapStream(OnDemandMetadata odpMetadata, ICatalogItemContext reportContext, IChunkFactory chunkFactory)
			{
				Stream stream;
				if (!odpMetadata.ReportSnapshot.CanUseExistingDocumentMapChunk(reportContext))
				{
					stream = null;
				}
				else
				{
					string text;
					stream = chunkFactory.GetChunk("DocumentMap", ReportProcessing.ReportChunkTypes.Interactivity, ChunkMode.Open, out text);
				}
				return stream;
			}

			// Token: 0x040041A3 RID: 16803
			private static List<Declaration> m_ChunkDeclarations;

			// Token: 0x040041A4 RID: 16804
			private OnDemandProcessingContext m_odpContext;
		}
	}
}
