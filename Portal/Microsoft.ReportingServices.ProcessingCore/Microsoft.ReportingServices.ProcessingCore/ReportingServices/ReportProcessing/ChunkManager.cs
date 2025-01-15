using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200067B RID: 1659
	internal sealed class ChunkManager
	{
		// Token: 0x06005B11 RID: 23313 RVA: 0x00176754 File Offset: 0x00174954
		private static string GenerateDataChunkName(string dataSetName, string subReportName, bool isShareable, int reportUniqueName)
		{
			if (-1 == reportUniqueName)
			{
				return "DataChunk_" + dataSetName;
			}
			if (isShareable && subReportName != null)
			{
				return "DataChunk" + subReportName + "_" + dataSetName;
			}
			return "DataChunk" + reportUniqueName.ToString() + "_" + dataSetName;
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x001767A0 File Offset: 0x001749A0
		private static string GenerateDataChunkName(DataSet dataSet, ReportProcessing.ProcessingContext context, bool writeOperation)
		{
			string text = null;
			if (context.SubReportLevel != 0U)
			{
				text = context.ReportContext.StableItemPath;
			}
			string text2;
			if (dataSet.IsShareable())
			{
				text2 = context.CachedDataChunkMapping[dataSet.ID] as string;
				if (text2 == null)
				{
					text2 = ChunkManager.GenerateDataChunkName(dataSet.Name, text, true, context.DataSetUniqueName);
					if (writeOperation)
					{
						context.CachedDataChunkMapping.Add(dataSet.ID, text2);
					}
				}
			}
			else
			{
				text2 = ChunkManager.GenerateDataChunkName(dataSet.Name, text, false, context.DataSetUniqueName);
			}
			return text2;
		}

		// Token: 0x04002F56 RID: 12118
		internal const string Definition = "CompiledDefinition";

		// Token: 0x04002F57 RID: 12119
		internal const string MainChunk = "Main";

		// Token: 0x04002F58 RID: 12120
		internal const string FirstPageChunk = "FirstPage";

		// Token: 0x04002F59 RID: 12121
		internal const string OtherPageChunk = "OtherPages";

		// Token: 0x04002F5A RID: 12122
		internal const string SpecialChunk = "Special";

		// Token: 0x04002F5B RID: 12123
		internal const string DocumentMap = "DocumentMap";

		// Token: 0x04002F5C RID: 12124
		internal const string ShowHideInfo = "ShowHideInfo";

		// Token: 0x04002F5D RID: 12125
		internal const string Bookmarks = "Bookmarks";

		// Token: 0x04002F5E RID: 12126
		internal const string Drillthrough = "Drillthrough";

		// Token: 0x04002F5F RID: 12127
		internal const string QuickFind = "QuickFind";

		// Token: 0x04002F60 RID: 12128
		internal const string SortFilterEventInfo = "SortFilterEventInfo";

		// Token: 0x04002F61 RID: 12129
		internal const string DataChunkPrefix = "DataChunk";

		// Token: 0x04002F62 RID: 12130
		internal const string PageSections = "PageSections";

		// Token: 0x04002F63 RID: 12131
		internal const string PageSectionInstances = "PageSectionInstances";

		// Token: 0x04002F64 RID: 12132
		internal const string Delimiter = "_";

		// Token: 0x04002F65 RID: 12133
		private const int InstancePerChunk = 4096;

		// Token: 0x04002F66 RID: 12134
		private const int RecordRowPerChunk = 4096;

		// Token: 0x02000C92 RID: 3218
		internal enum SpecialChunkName
		{
			// Token: 0x04004CF9 RID: 19705
			DocumentMap,
			// Token: 0x04004CFA RID: 19706
			ShowHideInfo,
			// Token: 0x04004CFB RID: 19707
			Bookmark,
			// Token: 0x04004CFC RID: 19708
			QuickFind,
			// Token: 0x04004CFD RID: 19709
			SortFilterEventInfo
		}

		// Token: 0x02000C93 RID: 3219
		internal sealed class InstanceInfoOwnerList : ArrayList
		{
			// Token: 0x17002B27 RID: 11047
			internal InstanceInfoOwner this[int index]
			{
				get
				{
					return (InstanceInfoOwner)base[index];
				}
			}
		}

		// Token: 0x02000C94 RID: 3220
		internal sealed class InstanceInfoList : ArrayList
		{
			// Token: 0x17002B28 RID: 11048
			internal InstanceInfo this[int index]
			{
				get
				{
					return (InstanceInfo)base[index];
				}
			}
		}

		// Token: 0x02000C95 RID: 3221
		internal sealed class DataChunkWriter
		{
			// Token: 0x06008C4B RID: 35915 RVA: 0x0023A878 File Offset: 0x00238A78
			internal DataChunkWriter(DataSet dataSet, ReportProcessing.ProcessingContext context, bool readerExtensionsSupported, bool stopSaveOnError)
			{
				Global.Tracer.Assert(context.CreateReportChunkCallback != null, "(null != context.CreateReportChunkCallback)");
				this.m_dataSetChunkName = ChunkManager.GenerateDataChunkName(dataSet, context, true);
				this.m_createChunkCallback = context.CreateReportChunkCallback;
				this.m_createChunkFactory = context.CreateReportChunkFactory;
				this.m_recordSetInfo = new RecordSetInfo(readerExtensionsSupported, dataSet.GetCLRCompareOptions());
				this.m_recordRows = new ChunkManager.DataChunkWriter.RecordRowList();
				this.m_cacheDataCallback = context.CacheDataCallback;
				this.m_stopSaveOnError = stopSaveOnError;
			}

			// Token: 0x06008C4C RID: 35916 RVA: 0x0023A8FC File Offset: 0x00238AFC
			internal DataChunkWriter(DataSet dataSet, ReportProcessing.ProcessingContext context)
			{
				Global.Tracer.Assert(context.CreateReportChunkCallback != null, "(null != context.CreateReportChunkCallback)");
				this.m_dataSetChunkName = ChunkManager.GenerateDataChunkName(dataSet, context, false);
				this.m_createChunkCallback = context.CreateReportChunkCallback;
				this.m_createChunkFactory = context.CreateReportChunkFactory;
			}

			// Token: 0x17002B29 RID: 11049
			// (set) Token: 0x06008C4D RID: 35917 RVA: 0x0023A94D File Offset: 0x00238B4D
			internal Hashtable[] FieldAliasPropertyNames
			{
				set
				{
					this.m_fieldAliasPropertyNames = value;
				}
			}

			// Token: 0x17002B2A RID: 11050
			// (get) Token: 0x06008C4E RID: 35918 RVA: 0x0023A956 File Offset: 0x00238B56
			internal RecordSetInfo RecordSetInfo
			{
				get
				{
					return this.m_recordSetInfo;
				}
			}

			// Token: 0x17002B2B RID: 11051
			// (get) Token: 0x06008C4F RID: 35919 RVA: 0x0023A95E File Offset: 0x00238B5E
			// (set) Token: 0x06008C50 RID: 35920 RVA: 0x0023A966 File Offset: 0x00238B66
			internal bool RecordSetInfoPopulated
			{
				get
				{
					return this.m_recordSetPopulated;
				}
				set
				{
					this.m_recordSetPopulated = value;
				}
			}

			// Token: 0x06008C51 RID: 35921 RVA: 0x0023A96F File Offset: 0x00238B6F
			internal bool AddRecordRow(FieldsImpl fields, int fieldCount)
			{
				return this.AddRecordRow(new RecordRow(fields, fieldCount));
			}

			// Token: 0x06008C52 RID: 35922 RVA: 0x0023A97E File Offset: 0x00238B7E
			internal bool AddRecordRow(RecordRow aRow)
			{
				this.CheckChunkLimit();
				if (!this.m_errorOccurred || !this.m_stopSaveOnError)
				{
					this.m_recordRows.Add(aRow);
				}
				return !this.m_errorOccurred;
			}

			// Token: 0x06008C53 RID: 35923 RVA: 0x0023A9AC File Offset: 0x00238BAC
			internal bool FinalFlush()
			{
				this.Flush();
				if (!this.m_errorOccurred || !this.m_stopSaveOnError)
				{
					this.Close();
				}
				return !this.m_errorOccurred;
			}

			// Token: 0x06008C54 RID: 35924 RVA: 0x0023A9D4 File Offset: 0x00238BD4
			internal void Close()
			{
				if (this.m_chunkWriter != null)
				{
					this.m_chunkWriter = null;
				}
				if (this.m_chunkStream != null)
				{
					this.m_chunkStream.Close();
					this.m_chunkStream = null;
				}
				if (this.m_cacheWriter != null)
				{
					this.m_cacheWriter = null;
				}
				if (this.m_cacheStream != null)
				{
					this.m_cacheStream.Close();
					this.m_cacheStream = null;
				}
			}

			// Token: 0x06008C55 RID: 35925 RVA: 0x0023AA34 File Offset: 0x00238C34
			internal void CloseAndEraseChunk()
			{
				if (this.m_chunkWriter != null)
				{
					this.m_chunkWriter = null;
				}
				if (this.m_cacheWriter != null)
				{
					this.m_cacheWriter = null;
				}
				if (this.m_cacheStream != null)
				{
					this.m_cacheStream.Close();
					this.m_cacheStream = null;
				}
				if (this.m_createChunkFactory == null)
				{
					return;
				}
				try
				{
					if (this.m_chunkStream != null)
					{
						this.m_chunkStream.Close();
						this.m_chunkStream = null;
					}
					this.m_createChunkFactory.Erase(this.m_dataSetChunkName, ReportProcessing.ReportChunkTypes.Other);
				}
				catch
				{
				}
			}

			// Token: 0x06008C56 RID: 35926 RVA: 0x0023AAC8 File Offset: 0x00238CC8
			private void CheckChunkLimit()
			{
				if (this.m_recordRows.Count >= 4096)
				{
					this.Flush();
					if (!this.m_errorOccurred || !this.m_stopSaveOnError)
					{
						this.m_recordRows = new ChunkManager.DataChunkWriter.RecordRowList();
					}
				}
			}

			// Token: 0x06008C57 RID: 35927 RVA: 0x0023AB00 File Offset: 0x00238D00
			private void Flush()
			{
				if (this.m_recordRows == null || (this.m_createChunkCallback == null && this.m_cacheDataCallback == null))
				{
					return;
				}
				try
				{
					if (this.m_fieldAliasPropertyNames != null && !this.m_recordSetPopulated)
					{
						this.m_recordSetInfo.FieldPropertyNames = new RecordSetPropertyNamesList(this.m_fieldAliasPropertyNames.Length);
						for (int i = 0; i < this.m_fieldAliasPropertyNames.Length; i++)
						{
							RecordSetPropertyNames recordSetPropertyNames = null;
							if (this.m_fieldAliasPropertyNames[i] != null && this.m_fieldAliasPropertyNames[i].Count != 0)
							{
								recordSetPropertyNames = new RecordSetPropertyNames();
								recordSetPropertyNames.PropertyNames = new StringList(this.m_fieldAliasPropertyNames[i].Count);
								recordSetPropertyNames.PropertyNames.AddRange(this.m_fieldAliasPropertyNames[i].Values);
							}
							this.m_recordSetInfo.FieldPropertyNames.Add(recordSetPropertyNames);
						}
					}
					if (this.m_chunkStream == null && this.m_createChunkCallback != null)
					{
						this.m_chunkStream = this.m_createChunkCallback(this.m_dataSetChunkName, ReportProcessing.ReportChunkTypes.Other, null);
						this.m_chunkWriter = new IntermediateFormatWriter(this.m_chunkStream, true);
						this.m_chunkWriter.WriteRecordSetInfo(this.m_recordSetInfo);
					}
					if (this.m_cacheStream == null && this.m_cacheDataCallback != null)
					{
						this.m_cacheStream = this.m_cacheDataCallback(this.m_dataSetChunkName, ReportProcessing.ReportChunkTypes.Other, null);
						this.m_cacheWriter = new IntermediateFormatWriter(this.m_cacheStream, true);
						this.m_cacheWriter.WriteRecordSetInfo(this.m_recordSetInfo);
					}
					Global.Tracer.Assert(this.m_chunkWriter != null || this.m_cacheWriter != null, "(null != m_chunkWriter || null != m_cacheWriter)");
					for (int j = 0; j < this.m_recordRows.Count; j++)
					{
						if (this.m_chunkWriter != null && !this.m_chunkWriter.WriteRecordRow(this.m_recordRows[j], this.m_recordSetInfo.FieldPropertyNames))
						{
							this.m_errorOccurred = true;
						}
						if (this.m_errorOccurred && this.m_stopSaveOnError)
						{
							this.CloseAndEraseChunk();
							break;
						}
						if (this.m_cacheWriter != null)
						{
							this.m_cacheWriter.WriteRecordRow(this.m_recordRows[j], this.m_recordSetInfo.FieldPropertyNames);
						}
					}
					this.m_recordRows = null;
				}
				catch
				{
					this.m_chunkWriter = null;
					if (this.m_chunkStream != null)
					{
						this.m_chunkStream.Close();
						this.m_chunkStream = null;
					}
					this.m_cacheWriter = null;
					if (this.m_cacheStream != null)
					{
						this.m_cacheStream.Close();
						this.m_cacheStream = null;
					}
					throw;
				}
			}

			// Token: 0x04004CFE RID: 19710
			private ReportProcessing.CreateReportChunk m_createChunkCallback;

			// Token: 0x04004CFF RID: 19711
			private IChunkFactory m_createChunkFactory;

			// Token: 0x04004D00 RID: 19712
			private string m_dataSetChunkName;

			// Token: 0x04004D01 RID: 19713
			private RecordSetInfo m_recordSetInfo;

			// Token: 0x04004D02 RID: 19714
			private bool m_recordSetPopulated;

			// Token: 0x04004D03 RID: 19715
			private ChunkManager.DataChunkWriter.RecordRowList m_recordRows;

			// Token: 0x04004D04 RID: 19716
			private Stream m_chunkStream;

			// Token: 0x04004D05 RID: 19717
			private IntermediateFormatWriter m_chunkWriter;

			// Token: 0x04004D06 RID: 19718
			private ReportProcessing.CreateReportChunk m_cacheDataCallback;

			// Token: 0x04004D07 RID: 19719
			private Stream m_cacheStream;

			// Token: 0x04004D08 RID: 19720
			private IntermediateFormatWriter m_cacheWriter;

			// Token: 0x04004D09 RID: 19721
			private bool m_stopSaveOnError;

			// Token: 0x04004D0A RID: 19722
			private bool m_errorOccurred;

			// Token: 0x04004D0B RID: 19723
			private Hashtable[] m_fieldAliasPropertyNames;

			// Token: 0x02000D47 RID: 3399
			internal sealed class RecordRowList : ArrayList
			{
				// Token: 0x06008FD1 RID: 36817 RVA: 0x00247E56 File Offset: 0x00246056
				internal RecordRowList()
				{
				}

				// Token: 0x06008FD2 RID: 36818 RVA: 0x00247E5E File Offset: 0x0024605E
				internal RecordRowList(int capacity)
					: base(capacity)
				{
				}

				// Token: 0x17002C1E RID: 11294
				internal RecordRow this[int index]
				{
					get
					{
						return (RecordRow)base[index];
					}
				}
			}
		}

		// Token: 0x02000C96 RID: 3222
		internal sealed class DataChunkReader : IDisposable
		{
			// Token: 0x06008C58 RID: 35928 RVA: 0x0023AD80 File Offset: 0x00238F80
			internal DataChunkReader(DataSet dataSet, ReportProcessing.ProcessingContext context)
			{
				this.m_currentRow = -1;
				if (context.SubReportLevel == 0U)
				{
					this.m_recordSetSize = dataSet.RecordSetSize;
				}
				Global.Tracer.Assert(context.GetReportChunkCallback != null, "(null != context.GetReportChunkCallback)");
				string text;
				this.m_chunkStream = context.GetReportChunkCallback(ChunkManager.GenerateDataChunkName(dataSet, context, false), ReportProcessing.ReportChunkTypes.Other, out text);
				this.m_chunkReader = new IntermediateFormatReader(this.m_chunkStream);
				this.m_recordSetInfo = this.m_chunkReader.ReadRecordSetInfo();
				if (-1 == this.m_recordSetSize)
				{
					this.m_streamLength = this.m_chunkStream.Length;
				}
			}

			// Token: 0x17002B2C RID: 11052
			// (get) Token: 0x06008C59 RID: 35929 RVA: 0x0023AE2D File Offset: 0x0023902D
			internal bool ReaderExtensionsSupported
			{
				get
				{
					return this.m_recordSetInfo.ReaderExtensionsSupported;
				}
			}

			// Token: 0x17002B2D RID: 11053
			// (get) Token: 0x06008C5A RID: 35930 RVA: 0x0023AE3A File Offset: 0x0023903A
			internal bool ReaderFieldProperties
			{
				get
				{
					return this.m_recordSetInfo != null && this.m_recordSetInfo.FieldPropertyNames != null;
				}
			}

			// Token: 0x17002B2E RID: 11054
			// (get) Token: 0x06008C5B RID: 35931 RVA: 0x0023AE54 File Offset: 0x00239054
			internal bool ValidCompareOptions
			{
				get
				{
					return this.m_recordSetInfo.ValidCompareOptions;
				}
			}

			// Token: 0x17002B2F RID: 11055
			// (get) Token: 0x06008C5C RID: 35932 RVA: 0x0023AE61 File Offset: 0x00239061
			internal CompareOptions CompareOptions
			{
				get
				{
					return this.m_recordSetInfo.CompareOptions;
				}
			}

			// Token: 0x17002B30 RID: 11056
			// (get) Token: 0x06008C5D RID: 35933 RVA: 0x0023AE6E File Offset: 0x0023906E
			public bool IsAggregateRow
			{
				get
				{
					return this.m_recordRow.IsAggregateRow;
				}
			}

			// Token: 0x17002B31 RID: 11057
			// (get) Token: 0x06008C5E RID: 35934 RVA: 0x0023AE7B File Offset: 0x0023907B
			public int AggregationFieldCount
			{
				get
				{
					return this.m_recordRow.AggregationFieldCount;
				}
			}

			// Token: 0x17002B32 RID: 11058
			// (get) Token: 0x06008C5F RID: 35935 RVA: 0x0023AE88 File Offset: 0x00239088
			internal RecordSetPropertyNamesList FieldPropertyNames
			{
				get
				{
					return this.m_recordSetInfo.FieldPropertyNames;
				}
			}

			// Token: 0x06008C60 RID: 35936 RVA: 0x0023AE98 File Offset: 0x00239098
			internal bool GetNextRow()
			{
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
					this.m_currentRow++;
					this.ReadNextRow();
				}
				return flag;
			}

			// Token: 0x06008C61 RID: 35937 RVA: 0x0023AEF4 File Offset: 0x002390F4
			internal object GetFieldValue(int aliasIndex)
			{
				if (this.m_recordRow.RecordFields[aliasIndex] == null)
				{
					throw new ReportProcessingException_FieldError(DataFieldStatus.IsMissing, null);
				}
				return this.m_recordRow.GetFieldValue(aliasIndex);
			}

			// Token: 0x06008C62 RID: 35938 RVA: 0x0023AF19 File Offset: 0x00239119
			internal bool IsAggregationField(int aliasIndex)
			{
				return this.m_recordRow.IsAggregationField(aliasIndex);
			}

			// Token: 0x06008C63 RID: 35939 RVA: 0x0023AF28 File Offset: 0x00239128
			internal object GetPropertyValue(int aliasIndex, int propertyIndex)
			{
				if (this.m_recordSetInfo.FieldPropertyNames != null && this.m_recordRow.RecordFields[aliasIndex] != null)
				{
					VariantList fieldPropertyValues = this.m_recordRow.RecordFields[aliasIndex].FieldPropertyValues;
					if (fieldPropertyValues != null && propertyIndex >= 0 && propertyIndex < fieldPropertyValues.Count)
					{
						return fieldPropertyValues[propertyIndex];
					}
				}
				return null;
			}

			// Token: 0x06008C64 RID: 35940 RVA: 0x0023AF7D File Offset: 0x0023917D
			void IDisposable.Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06008C65 RID: 35941 RVA: 0x0023AF8C File Offset: 0x0023918C
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

			// Token: 0x06008C66 RID: 35942 RVA: 0x0023AFC8 File Offset: 0x002391C8
			private void ReadNextRow()
			{
				this.m_recordRow = this.m_chunkReader.ReadRecordRow();
			}

			// Token: 0x04004D0C RID: 19724
			private Stream m_chunkStream;

			// Token: 0x04004D0D RID: 19725
			private IntermediateFormatReader m_chunkReader;

			// Token: 0x04004D0E RID: 19726
			private RecordSetInfo m_recordSetInfo;

			// Token: 0x04004D0F RID: 19727
			private RecordRow m_recordRow;

			// Token: 0x04004D10 RID: 19728
			private int m_recordSetSize = -1;

			// Token: 0x04004D11 RID: 19729
			private int m_currentRow;

			// Token: 0x04004D12 RID: 19730
			private long m_streamLength = -1L;
		}

		// Token: 0x02000C97 RID: 3223
		internal sealed class PageSectionManager
		{
			// Token: 0x06008C67 RID: 35943 RVA: 0x0023AFDB File Offset: 0x002391DB
			internal void AddPageSectionInstance(InstanceInfo instanceInfo, InstanceInfoOwner owner)
			{
				if (this.m_pageSectionInstances == null)
				{
					this.m_pageSectionInstances = new ChunkManager.InstanceInfoList();
					this.m_pageSectionInstanceOwners = new ChunkManager.InstanceInfoOwnerList();
				}
				this.m_pageSectionInstances.Add(instanceInfo);
				this.m_pageSectionInstanceOwners.Add(owner);
			}

			// Token: 0x06008C68 RID: 35944 RVA: 0x0023B018 File Offset: 0x00239218
			internal void Flush(ReportSnapshot reportSnapshot, ReportProcessing.CreateReportChunk createChunkCallback)
			{
				if (this.m_pageSectionInstances == null || createChunkCallback == null || reportSnapshot == null)
				{
					return;
				}
				Stream stream = null;
				try
				{
					stream = createChunkCallback("PageSectionInstances", ReportProcessing.ReportChunkTypes.Other, null);
					IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, false);
					for (int i = 0; i < this.m_pageSectionInstances.Count; i++)
					{
						long position = stream.Position;
						intermediateFormatWriter.WriteInstanceInfo(this.m_pageSectionInstances[i]);
						this.m_pageSectionInstanceOwners[i].SetOffset(position);
					}
					stream.Close();
					bool[] declarationsToWrite = intermediateFormatWriter.DeclarationsToWrite;
					stream = createChunkCallback("PageSections", ReportProcessing.ReportChunkTypes.Main, null);
					intermediateFormatWriter = new IntermediateFormatWriter(stream, declarationsToWrite, null);
					reportSnapshot.PageSectionOffsets = intermediateFormatWriter.WritePageSections(stream, reportSnapshot.PageSections);
					Global.Tracer.Assert(2 * reportSnapshot.PageSectionOffsets.Count == reportSnapshot.PageSections.Count);
					reportSnapshot.PageSections = null;
					stream.Close();
					stream = null;
				}
				finally
				{
					this.m_pageSectionInstances = null;
					this.m_pageSectionInstanceOwners = null;
					if (stream != null)
					{
						stream.Close();
					}
				}
			}

			// Token: 0x04004D13 RID: 19731
			private ChunkManager.InstanceInfoList m_pageSectionInstances;

			// Token: 0x04004D14 RID: 19732
			private ChunkManager.InstanceInfoOwnerList m_pageSectionInstanceOwners;
		}

		// Token: 0x02000C98 RID: 3224
		internal abstract class SnapshotChunkManager
		{
			// Token: 0x06008C6A RID: 35946 RVA: 0x0023B134 File Offset: 0x00239334
			protected internal SnapshotChunkManager(ReportProcessing.CreateReportChunk createChunkCallback)
			{
				this.m_createChunkCallback = createChunkCallback;
				this.m_firstPageChunkInstances = new ChunkManager.InstanceInfoList();
				this.m_firstPageChunkInstanceOwners = new ChunkManager.InstanceInfoOwnerList();
			}

			// Token: 0x06008C6B RID: 35947 RVA: 0x0023B15C File Offset: 0x0023935C
			protected void Flush()
			{
				if (this.m_chunkInstances == null || this.m_createChunkCallback == null)
				{
					return;
				}
				try
				{
					if (this.m_chunkStream == null)
					{
						this.m_chunkStream = this.m_createChunkCallback("OtherPages", ReportProcessing.ReportChunkTypes.Other, null);
					}
					if (this.m_chunkWriter == null)
					{
						this.m_chunkWriter = new IntermediateFormatWriter(this.m_chunkStream, false);
					}
					for (int i = 0; i < this.m_chunkInstances.Count; i++)
					{
						long position = this.m_chunkStream.Position;
						this.m_chunkWriter.WriteInstanceInfo(this.m_chunkInstances[i]);
						this.m_chunkInstanceOwners[i].SetOffset(position);
					}
					this.m_chunkInstances = null;
					this.m_chunkInstanceOwners = null;
				}
				catch
				{
					this.m_chunkWriter = null;
					if (this.m_chunkStream != null)
					{
						this.m_chunkStream.Close();
						this.m_chunkStream = null;
					}
					throw;
				}
			}

			// Token: 0x06008C6C RID: 35948 RVA: 0x0023B244 File Offset: 0x00239444
			internal void FinalFlush()
			{
				this.Flush();
				if (this.m_chunkWriter != null)
				{
					this.m_otherPageDeclarationsToWrite = this.m_chunkWriter.DeclarationsToWrite;
					this.m_chunkWriter = null;
				}
				if (this.m_chunkStream != null)
				{
					this.m_chunkStream.Close();
					this.m_chunkStream = null;
				}
			}

			// Token: 0x06008C6D RID: 35949 RVA: 0x0023B294 File Offset: 0x00239494
			internal void SaveFirstPage()
			{
				if (this.m_firstPageChunkInstances == null || this.m_firstPageChunkInstances.Count == 0 || this.m_createChunkCallback == null)
				{
					return;
				}
				Stream stream = null;
				try
				{
					stream = this.m_createChunkCallback("FirstPage", ReportProcessing.ReportChunkTypes.Main, null);
					IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, false);
					for (int i = 0; i < this.m_firstPageChunkInstances.Count; i++)
					{
						long num = stream.Position;
						intermediateFormatWriter.WriteInstanceInfo(this.m_firstPageChunkInstances[i]);
						Global.Tracer.Assert(num != 0L, "(0 != offset)");
						num = 0L - num;
						this.m_firstPageChunkInstanceOwners[i].SetOffset(num);
					}
					this.m_firstPageChunkInstances = null;
					this.m_firstPageChunkInstanceOwners = null;
					this.m_firstPageDeclarationsToWrite = intermediateFormatWriter.DeclarationsToWrite;
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}

			// Token: 0x06008C6E RID: 35950 RVA: 0x0023B370 File Offset: 0x00239570
			internal void SaveReportSnapshot(ReportSnapshot reportSnapshot)
			{
				if (this.m_createChunkCallback == null)
				{
					return;
				}
				Stream stream = null;
				try
				{
					if (reportSnapshot.HasDocumentMap)
					{
						stream = this.m_createChunkCallback("DocumentMap", ReportProcessing.ReportChunkTypes.Main, null);
						new IntermediateFormatWriter(stream, true).WriteDocumentMapNode(reportSnapshot.DocumentMap);
						reportSnapshot.DocumentMap = null;
						stream.Close();
						stream = null;
					}
					if (reportSnapshot.HasBookmarks)
					{
						stream = this.m_createChunkCallback("Bookmarks", ReportProcessing.ReportChunkTypes.Main, null);
						new IntermediateFormatWriter(stream, true).WriteBookmarksHashtable(reportSnapshot.BookmarksInfo);
						reportSnapshot.BookmarksInfo = null;
						stream.Close();
						stream = null;
					}
					if (reportSnapshot.DrillthroughInfo != null)
					{
						stream = this.m_createChunkCallback("Drillthrough", ReportProcessing.ReportChunkTypes.Main, null);
						new IntermediateFormatWriter(stream, true).WriteDrillthroughInfo(reportSnapshot.DrillthroughInfo);
						reportSnapshot.DrillthroughInfo = null;
						stream.Close();
						stream = null;
					}
					if (reportSnapshot.ShowHideSenderInfo != null || reportSnapshot.ShowHideReceiverInfo != null)
					{
						stream = this.m_createChunkCallback("ShowHideInfo", ReportProcessing.ReportChunkTypes.Main, null);
						IntermediateFormatWriter intermediateFormatWriter = new IntermediateFormatWriter(stream, true);
						intermediateFormatWriter.WriteSenderInformationHashtable(reportSnapshot.ShowHideSenderInfo);
						reportSnapshot.ShowHideSenderInfo = null;
						intermediateFormatWriter.WriteReceiverInformationHashtable(reportSnapshot.ShowHideReceiverInfo);
						reportSnapshot.ShowHideReceiverInfo = null;
						stream.Close();
						stream = null;
					}
					if (reportSnapshot.QuickFind != null)
					{
						stream = this.m_createChunkCallback("QuickFind", ReportProcessing.ReportChunkTypes.Main, null);
						new IntermediateFormatWriter(stream, true).WriteQuickFindHashtable(reportSnapshot.QuickFind);
						reportSnapshot.QuickFind = null;
						stream.Close();
						stream = null;
					}
					if (reportSnapshot.SortFilterEventInfo != null)
					{
						stream = this.m_createChunkCallback("SortFilterEventInfo", ReportProcessing.ReportChunkTypes.Main, null);
						new IntermediateFormatWriter(stream, true).WriteSortFilterEventInfoHashtable(reportSnapshot.SortFilterEventInfo);
						reportSnapshot.SortFilterEventInfo = null;
						stream.Close();
						stream = null;
					}
					stream = this.m_createChunkCallback("Main", ReportProcessing.ReportChunkTypes.Main, null);
					new IntermediateFormatWriter(stream, this.m_firstPageDeclarationsToWrite, this.m_otherPageDeclarationsToWrite).WriteReportSnapshot(reportSnapshot);
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}

			// Token: 0x06008C6F RID: 35951 RVA: 0x0023B560 File Offset: 0x00239760
			internal void Close()
			{
				this.m_chunkWriter = null;
				if (this.m_chunkStream != null)
				{
					this.m_chunkStream.Close();
					this.m_chunkStream = null;
				}
			}

			// Token: 0x04004D15 RID: 19733
			protected ReportProcessing.CreateReportChunk m_createChunkCallback;

			// Token: 0x04004D16 RID: 19734
			protected ChunkManager.InstanceInfoList m_firstPageChunkInstances;

			// Token: 0x04004D17 RID: 19735
			protected ChunkManager.InstanceInfoOwnerList m_firstPageChunkInstanceOwners;

			// Token: 0x04004D18 RID: 19736
			protected ChunkManager.InstanceInfoList m_chunkInstances;

			// Token: 0x04004D19 RID: 19737
			protected ChunkManager.InstanceInfoOwnerList m_chunkInstanceOwners;

			// Token: 0x04004D1A RID: 19738
			protected IntermediateFormatWriter m_chunkWriter;

			// Token: 0x04004D1B RID: 19739
			protected Stream m_chunkStream;

			// Token: 0x04004D1C RID: 19740
			private bool[] m_firstPageDeclarationsToWrite;

			// Token: 0x04004D1D RID: 19741
			private bool[] m_otherPageDeclarationsToWrite;
		}

		// Token: 0x02000C99 RID: 3225
		internal sealed class UpgradeManager : ChunkManager.SnapshotChunkManager
		{
			// Token: 0x06008C70 RID: 35952 RVA: 0x0023B583 File Offset: 0x00239783
			internal UpgradeManager(ReportProcessing.CreateReportChunk createChunkCallback)
				: base(createChunkCallback)
			{
			}

			// Token: 0x06008C71 RID: 35953 RVA: 0x0023B58C File Offset: 0x0023978C
			internal void AddInstance(InstanceInfo instanceInfo, InstanceInfoOwner owner, long offset)
			{
				Global.Tracer.Assert(offset != 0L, "(0 != offset)");
				if (offset < 0L)
				{
					this.m_firstPageChunkInstances.Add(instanceInfo);
					this.m_firstPageChunkInstanceOwners.Add(owner);
					return;
				}
				if (this.m_chunkInstances == null)
				{
					this.m_chunkInstances = new ChunkManager.InstanceInfoList();
					this.m_chunkInstanceOwners = new ChunkManager.InstanceInfoOwnerList();
				}
				this.m_chunkInstances.Add(instanceInfo);
				this.m_chunkInstanceOwners.Add(owner);
			}
		}

		// Token: 0x02000C9A RID: 3226
		internal sealed class ProcessingChunkManager : ChunkManager.SnapshotChunkManager
		{
			// Token: 0x06008C72 RID: 35954 RVA: 0x0023B605 File Offset: 0x00239805
			internal ProcessingChunkManager(ReportProcessing.CreateReportChunk createChunkCallback, bool isOnePass)
				: base(createChunkCallback)
			{
				this.m_isOnePass = isOnePass;
				this.m_pageSectionManager = new ChunkManager.PageSectionManager();
			}

			// Token: 0x17002B33 RID: 11059
			// (get) Token: 0x06008C73 RID: 35955 RVA: 0x0023B627 File Offset: 0x00239827
			internal bool InFirstPage
			{
				get
				{
					return this.m_inFirstPage;
				}
			}

			// Token: 0x17002B34 RID: 11060
			// (get) Token: 0x06008C74 RID: 35956 RVA: 0x0023B62F File Offset: 0x0023982F
			internal long TotalCount
			{
				get
				{
					return this.m_totalInstanceCount + (long)this.m_instanceCount;
				}
			}

			// Token: 0x06008C75 RID: 35957 RVA: 0x0023B63F File Offset: 0x0023983F
			internal void PageSectionFlush(ReportSnapshot reportSnapshot)
			{
				this.m_pageSectionManager.Flush(reportSnapshot, this.m_createChunkCallback);
			}

			// Token: 0x06008C76 RID: 35958 RVA: 0x0023B653 File Offset: 0x00239853
			internal void EnterIgnorePageBreakItem()
			{
				if (!this.m_isOnePass)
				{
					this.m_ignorePageBreaks++;
				}
			}

			// Token: 0x06008C77 RID: 35959 RVA: 0x0023B66B File Offset: 0x0023986B
			internal void LeaveIgnorePageBreakItem()
			{
				if (!this.m_isOnePass)
				{
					this.m_ignorePageBreaks--;
					Global.Tracer.Assert(0 <= this.m_ignorePageBreaks, "(0 <= m_ignorePageBreaks)");
				}
			}

			// Token: 0x06008C78 RID: 35960 RVA: 0x0023B69E File Offset: 0x0023989E
			internal void EnterIgnoreInstances()
			{
				if (!this.m_isOnePass)
				{
					this.m_ignoreInstances++;
				}
			}

			// Token: 0x06008C79 RID: 35961 RVA: 0x0023B6B6 File Offset: 0x002398B6
			internal void LeaveIgnoreInstances()
			{
				if (!this.m_isOnePass)
				{
					this.m_ignoreInstances--;
					Global.Tracer.Assert(0 <= this.m_ignoreInstances, "(0 <= m_ignoreInstances)");
				}
			}

			// Token: 0x06008C7A RID: 35962 RVA: 0x0023B6E9 File Offset: 0x002398E9
			internal void EnterReportItemCollection()
			{
				if (!this.m_isOnePass)
				{
					this.m_reportItemCollectionLevel++;
				}
			}

			// Token: 0x06008C7B RID: 35963 RVA: 0x0023B704 File Offset: 0x00239904
			internal void LeaveReportItemCollection()
			{
				if (!this.m_isOnePass)
				{
					if (this.m_repeatSiblingLists != null)
					{
						this.m_repeatSiblingLists.Remove(this.m_reportItemCollectionLevel);
					}
					this.m_reportItemCollectionLevel--;
					Global.Tracer.Assert(0 <= this.m_reportItemCollectionLevel, "(0 <= m_reportItemCollectionLevel)");
				}
			}

			// Token: 0x06008C7C RID: 35964 RVA: 0x0023B760 File Offset: 0x00239960
			internal void AddRepeatSiblings(DataRegion dataRegion, int index)
			{
				if (this.m_isOnePass || !this.m_inFirstPage || dataRegion.RepeatSiblings == null)
				{
					return;
				}
				Hashtable hashtable = null;
				for (int i = 0; i < dataRegion.RepeatSiblings.Count; i++)
				{
					int num = dataRegion.RepeatSiblings[i];
					if (index <= num)
					{
						if (hashtable == null)
						{
							if (this.m_repeatSiblingLists == null)
							{
								this.m_repeatSiblingLists = new Hashtable();
							}
							else
							{
								hashtable = (Hashtable)this.m_repeatSiblingLists[this.m_reportItemCollectionLevel];
							}
							if (hashtable == null)
							{
								hashtable = new Hashtable();
								this.m_repeatSiblingLists.Add(this.m_reportItemCollectionLevel, hashtable);
							}
						}
						hashtable.Add(num, true);
					}
				}
			}

			// Token: 0x06008C7D RID: 35965 RVA: 0x0023B818 File Offset: 0x00239A18
			internal void CheckPageBreak(IPageBreakItem item, bool atStart)
			{
				if (this.m_isOnePass || !this.m_inFirstPage || 0 < this.m_ignorePageBreaks || this.m_createChunkCallback == null)
				{
					return;
				}
				if (item.IgnorePageBreaks())
				{
					if (atStart)
					{
						this.EnterIgnorePageBreakItem();
						if (item is Rectangle && ((Rectangle)item).RepeatedSibling)
						{
							this.EnterIgnoreInstances();
							return;
						}
					}
					else
					{
						this.LeaveIgnorePageBreakItem();
						if (item is Rectangle && ((Rectangle)item).RepeatedSibling)
						{
							this.LeaveIgnoreInstances();
							return;
						}
					}
				}
				else if ((atStart && this.m_hasLeafNode && item.HasPageBreaks(atStart)) || (!atStart && item.HasPageBreaks(atStart)))
				{
					this.m_inFirstPage = false;
				}
			}

			// Token: 0x06008C7E RID: 35966 RVA: 0x0023B8C0 File Offset: 0x00239AC0
			internal void AddInstance(InstanceInfo newInstance, ReportItem reportItemDef, InstanceInfoOwner owner, int index, bool isPageSection)
			{
				if (isPageSection)
				{
					this.m_pageSectionManager.AddPageSectionInstance(newInstance, owner);
					return;
				}
				if (!this.m_isOnePass && reportItemDef.RepeatedSibling && !this.m_inFirstPage)
				{
					Hashtable hashtable = null;
					if (this.m_repeatSiblingLists != null)
					{
						hashtable = (Hashtable)this.m_repeatSiblingLists[this.m_reportItemCollectionLevel];
					}
					if (hashtable != null && hashtable[index] != null)
					{
						this.SyncAddInstanceToFirstPage(newInstance, owner);
						return;
					}
				}
				this.AddInstance(newInstance, owner, isPageSection);
			}

			// Token: 0x06008C7F RID: 35967 RVA: 0x0023B944 File Offset: 0x00239B44
			internal void AddInstance(InstanceInfo newInstance, InstanceInfoOwner owner, bool isPageSection)
			{
				if (isPageSection)
				{
					this.m_pageSectionManager.AddPageSectionInstance(newInstance, owner);
					return;
				}
				if (this.m_isOnePass)
				{
					lock (this)
					{
						this.SyncAddInstance(newInstance, owner);
						return;
					}
				}
				this.SyncAddInstance(newInstance, owner);
			}

			// Token: 0x06008C80 RID: 35968 RVA: 0x0023B9A4 File Offset: 0x00239BA4
			internal void AddInstance(InstanceInfo newInstance, InstanceInfoOwner owner, bool addToFirstPage, bool isPageSection)
			{
				if (isPageSection)
				{
					this.m_pageSectionManager.AddPageSectionInstance(newInstance, owner);
					return;
				}
				if (addToFirstPage)
				{
					this.AddInstanceToFirstPage(newInstance, owner, false);
					return;
				}
				this.AddInstance(newInstance, owner, false);
			}

			// Token: 0x06008C81 RID: 35969 RVA: 0x0023B9D0 File Offset: 0x00239BD0
			internal void AddInstanceToFirstPage(InstanceInfo newInstance, InstanceInfoOwner owner, bool isPageSection)
			{
				if (isPageSection)
				{
					this.m_pageSectionManager.AddPageSectionInstance(newInstance, owner);
					return;
				}
				if (this.m_isOnePass)
				{
					lock (this)
					{
						this.SyncAddInstanceToFirstPage(newInstance, owner);
						return;
					}
				}
				this.SyncAddInstanceToFirstPage(newInstance, owner);
			}

			// Token: 0x06008C82 RID: 35970 RVA: 0x0023BA30 File Offset: 0x00239C30
			private void SyncAddInstanceToFirstPage(InstanceInfo newInstance, InstanceInfoOwner owner)
			{
				this.m_firstPageChunkInstances.Add(newInstance);
				this.m_firstPageChunkInstanceOwners.Add(owner);
			}

			// Token: 0x06008C83 RID: 35971 RVA: 0x0023BA4C File Offset: 0x00239C4C
			private void SyncAddInstance(InstanceInfo newInstance, InstanceInfoOwner owner)
			{
				this.CheckChunkLimit();
				if (this.m_inFirstPage)
				{
					this.SetHasLeafNodes(owner);
					this.SyncAddInstanceToFirstPage(newInstance, owner);
				}
				else
				{
					this.m_chunkInstances.Add(newInstance);
					this.m_chunkInstanceOwners.Add(owner);
				}
				if (newInstance is OWCChartInstanceInfo)
				{
					this.m_instanceCount += ((OWCChartInstanceInfo)newInstance).Size;
					return;
				}
				this.m_instanceCount++;
			}

			// Token: 0x06008C84 RID: 35972 RVA: 0x0023BAC4 File Offset: 0x00239CC4
			private void SetHasLeafNodes(InstanceInfoOwner owner)
			{
				if (this.m_isOnePass || this.m_hasLeafNode || 0 < this.m_ignoreInstances)
				{
					return;
				}
				if (owner is TextBoxInstance || owner is LineInstance || owner is CheckBoxInstance || owner is ImageInstance || owner is ActiveXControlInstance || owner is OWCChartInstance)
				{
					this.m_hasLeafNode = true;
				}
			}

			// Token: 0x06008C85 RID: 35973 RVA: 0x0023BB24 File Offset: 0x00239D24
			private void CheckChunkLimit()
			{
				if (this.m_createChunkCallback == null)
				{
					return;
				}
				if (this.m_inFirstPage)
				{
					if (this.m_instanceCount < 4096)
					{
						return;
					}
					this.m_inFirstPage = false;
				}
				bool flag = false;
				if (this.m_chunkInstances == null)
				{
					flag = true;
				}
				else if (this.m_instanceCount >= 4096)
				{
					base.Flush();
					flag = true;
				}
				if (flag)
				{
					this.m_totalInstanceCount += (long)this.m_instanceCount;
					this.m_instanceCount = 0;
					this.m_chunkInstances = new ChunkManager.InstanceInfoList();
					this.m_chunkInstanceOwners = new ChunkManager.InstanceInfoOwnerList();
				}
			}

			// Token: 0x04004D1E RID: 19742
			private bool m_inFirstPage = true;

			// Token: 0x04004D1F RID: 19743
			private bool m_hasLeafNode;

			// Token: 0x04004D20 RID: 19744
			private int m_ignorePageBreaks;

			// Token: 0x04004D21 RID: 19745
			private int m_ignoreInstances;

			// Token: 0x04004D22 RID: 19746
			private int m_reportItemCollectionLevel;

			// Token: 0x04004D23 RID: 19747
			private int m_instanceCount;

			// Token: 0x04004D24 RID: 19748
			private Hashtable m_repeatSiblingLists;

			// Token: 0x04004D25 RID: 19749
			private bool m_isOnePass;

			// Token: 0x04004D26 RID: 19750
			private ChunkManager.PageSectionManager m_pageSectionManager;

			// Token: 0x04004D27 RID: 19751
			private long m_totalInstanceCount;
		}

		// Token: 0x02000C9B RID: 3227
		internal sealed class EventsChunkManager
		{
			// Token: 0x06008C86 RID: 35974 RVA: 0x0023BBB0 File Offset: 0x00239DB0
			internal EventsChunkManager(ReportProcessing.GetReportChunk getChunkCallback)
			{
				this.m_specialChunkManager = new ChunkManager.SpecialChunkManager(getChunkCallback, null, null, null);
			}

			// Token: 0x06008C87 RID: 35975 RVA: 0x0023BBC7 File Offset: 0x00239DC7
			internal EventsChunkManager(ReportProcessing.GetReportChunk getChunkCallback, Hashtable definitionObjects, IntermediateFormatVersion intermediateFormatVersion)
			{
				this.m_specialChunkManager = new ChunkManager.SpecialChunkManager(getChunkCallback, null, definitionObjects, intermediateFormatVersion);
			}

			// Token: 0x06008C88 RID: 35976 RVA: 0x0023BBDE File Offset: 0x00239DDE
			internal void Close()
			{
				this.m_specialChunkManager.Close();
			}

			// Token: 0x06008C89 RID: 35977 RVA: 0x0023BBEC File Offset: 0x00239DEC
			internal void GetShowHideInfo(out SenderInformationHashtable senderInfo, out ReceiverInformationHashtable receiverInfo)
			{
				senderInfo = null;
				receiverInfo = null;
				IntermediateFormatReader showHideInfoReader = this.m_specialChunkManager.ShowHideInfoReader;
				if (showHideInfoReader != null)
				{
					senderInfo = showHideInfoReader.ReadSenderInformationHashtable();
					receiverInfo = showHideInfoReader.ReadReceiverInformationHashtable();
				}
			}

			// Token: 0x06008C8A RID: 35978 RVA: 0x0023BC20 File Offset: 0x00239E20
			internal BookmarkInformation GetBookmarkIdInfo(string bookmarkId)
			{
				if (bookmarkId == null)
				{
					return null;
				}
				IntermediateFormatReader bookmarkReader = this.m_specialChunkManager.BookmarkReader;
				if (bookmarkReader != null)
				{
					return bookmarkReader.FindBookmarkIdInfo(bookmarkId);
				}
				return null;
			}

			// Token: 0x06008C8B RID: 35979 RVA: 0x0023BC4C File Offset: 0x00239E4C
			internal DrillthroughInformation GetDrillthroughIdInfo(string drillthroughId)
			{
				if (drillthroughId == null)
				{
					return null;
				}
				IntermediateFormatReader drillthroughReader = this.m_specialChunkManager.DrillthroughReader;
				if (drillthroughReader != null)
				{
					return drillthroughReader.FindDrillthroughIdInfo(drillthroughId);
				}
				return null;
			}

			// Token: 0x06008C8C RID: 35980 RVA: 0x0023BC78 File Offset: 0x00239E78
			internal int GetDocumentMapNodePage(string documentMapId)
			{
				if (documentMapId == null)
				{
					return 0;
				}
				int num = 0;
				IntermediateFormatReader documentMapReader = this.m_specialChunkManager.DocumentMapReader;
				if (documentMapReader != null)
				{
					documentMapReader.FindDocumentMapNodePage(documentMapId, ref num);
				}
				return num;
			}

			// Token: 0x06008C8D RID: 35981 RVA: 0x0023BCA8 File Offset: 0x00239EA8
			internal DocumentMapNodeInfo GetDocumentMapInfo()
			{
				IntermediateFormatReader documentMapReader = this.m_specialChunkManager.DocumentMapReader;
				if (documentMapReader != null)
				{
					return documentMapReader.ReadDocumentMapNodeInfo();
				}
				return null;
			}

			// Token: 0x06008C8E RID: 35982 RVA: 0x0023BCCC File Offset: 0x00239ECC
			internal DocumentMapNode GetDocumentMapNode()
			{
				IntermediateFormatReader documentMapReader = this.m_specialChunkManager.DocumentMapReader;
				if (documentMapReader != null)
				{
					return documentMapReader.ReadDocumentMapNode();
				}
				return null;
			}

			// Token: 0x06008C8F RID: 35983 RVA: 0x0023BCF0 File Offset: 0x00239EF0
			internal SortFilterEventInfoHashtable GetSortFilterEventInfo()
			{
				IntermediateFormatReader sortFilterEventInfoReader = this.m_specialChunkManager.SortFilterEventInfoReader;
				if (sortFilterEventInfoReader != null)
				{
					return sortFilterEventInfoReader.ReadSortFilterEventInfoHashtable();
				}
				return null;
			}

			// Token: 0x04004D28 RID: 19752
			private ChunkManager.SpecialChunkManager m_specialChunkManager;
		}

		// Token: 0x02000C9C RID: 3228
		internal sealed class SpecialChunkManager
		{
			// Token: 0x06008C90 RID: 35984 RVA: 0x0023BD14 File Offset: 0x00239F14
			internal SpecialChunkManager(ReportProcessing.GetReportChunk getChunkCallback, Hashtable instanceObjects, Hashtable definitionObjects, IntermediateFormatVersion intermediateFormatVersion)
			{
				this.m_definitionObjects = definitionObjects;
				this.m_instanceObjects = instanceObjects;
				this.m_getChunkCallback = getChunkCallback;
				this.m_intermediateFormatVersion = intermediateFormatVersion;
			}

			// Token: 0x06008C91 RID: 35985 RVA: 0x0023BD70 File Offset: 0x00239F70
			private Stream GetSpecialChunkInfo(string chunkName, ref bool hasChunk)
			{
				string text;
				Stream stream = this.m_getChunkCallback(chunkName, ReportProcessing.ReportChunkTypes.Main, out text);
				if (stream == null)
				{
					hasChunk = false;
				}
				return stream;
			}

			// Token: 0x06008C92 RID: 35986 RVA: 0x0023BD94 File Offset: 0x00239F94
			internal void Close()
			{
				if (this.m_docMap != null)
				{
					this.m_docMap.Close();
					this.m_docMap = null;
					this.m_hasDocMap = true;
				}
				if (this.m_showHideInfo != null)
				{
					this.m_showHideInfo.Close();
					this.m_showHideInfo = null;
					this.m_hasShowHideInfo = true;
				}
				if (this.m_bookmarks != null)
				{
					this.m_bookmarks.Close();
					this.m_bookmarks = null;
					this.m_hasBookmarks = true;
				}
				if (this.m_drillthrough != null)
				{
					this.m_drillthrough.Close();
					this.m_drillthrough = null;
					this.m_hasDrillthrough = true;
				}
				if (this.m_quickFind != null)
				{
					this.m_quickFind.Close();
					this.m_quickFind = null;
					this.m_hasQuickFind = true;
				}
				if (this.m_sortFilterEventInfo != null)
				{
					this.m_sortFilterEventInfo.Close();
					this.m_sortFilterEventInfo = null;
					this.m_hasSortFilterEventInfo = true;
				}
			}

			// Token: 0x17002B35 RID: 11061
			// (get) Token: 0x06008C93 RID: 35987 RVA: 0x0023BE68 File Offset: 0x0023A068
			internal IntermediateFormatReader DocumentMapReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasDocMap && this.m_docMap == null)
					{
						this.m_docMap = this.GetSpecialChunkInfo("DocumentMap", ref this.m_hasDocMap);
					}
					if (this.m_docMap != null)
					{
						this.m_docMap.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_docMap, this.m_instanceObjects);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x17002B36 RID: 11062
			// (get) Token: 0x06008C94 RID: 35988 RVA: 0x0023BEC8 File Offset: 0x0023A0C8
			internal IntermediateFormatReader ShowHideInfoReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasShowHideInfo && this.m_showHideInfo == null)
					{
						this.m_showHideInfo = this.GetSpecialChunkInfo("ShowHideInfo", ref this.m_hasShowHideInfo);
					}
					if (this.m_showHideInfo != null)
					{
						this.m_showHideInfo.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_showHideInfo, this.m_instanceObjects);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x17002B37 RID: 11063
			// (get) Token: 0x06008C95 RID: 35989 RVA: 0x0023BF28 File Offset: 0x0023A128
			internal IntermediateFormatReader BookmarkReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasBookmarks && this.m_bookmarks == null)
					{
						this.m_bookmarks = this.GetSpecialChunkInfo("Bookmarks", ref this.m_hasBookmarks);
					}
					if (this.m_bookmarks != null)
					{
						this.m_bookmarks.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_bookmarks, this.m_instanceObjects);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x17002B38 RID: 11064
			// (get) Token: 0x06008C96 RID: 35990 RVA: 0x0023BF88 File Offset: 0x0023A188
			internal IntermediateFormatReader DrillthroughReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasDrillthrough && this.m_drillthrough == null)
					{
						this.m_drillthrough = this.GetSpecialChunkInfo("Drillthrough", ref this.m_hasDrillthrough);
					}
					if (this.m_drillthrough != null)
					{
						this.m_drillthrough.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_drillthrough, this.m_instanceObjects);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x17002B39 RID: 11065
			// (get) Token: 0x06008C97 RID: 35991 RVA: 0x0023BFE8 File Offset: 0x0023A1E8
			internal IntermediateFormatReader QuickFindReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasQuickFind && this.m_quickFind == null)
					{
						this.m_quickFind = this.GetSpecialChunkInfo("QuickFind", ref this.m_hasQuickFind);
					}
					if (this.m_quickFind != null)
					{
						this.m_quickFind.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_quickFind, this.m_instanceObjects);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x17002B3A RID: 11066
			// (get) Token: 0x06008C98 RID: 35992 RVA: 0x0023C048 File Offset: 0x0023A248
			internal IntermediateFormatReader SortFilterEventInfoReader
			{
				get
				{
					IntermediateFormatReader intermediateFormatReader = null;
					if (this.m_hasSortFilterEventInfo && this.m_sortFilterEventInfo == null)
					{
						this.m_sortFilterEventInfo = this.GetSpecialChunkInfo("SortFilterEventInfo", ref this.m_hasSortFilterEventInfo);
					}
					if (this.m_sortFilterEventInfo != null)
					{
						this.m_sortFilterEventInfo.Position = 0L;
						intermediateFormatReader = new IntermediateFormatReader(this.m_sortFilterEventInfo, this.m_instanceObjects, this.m_definitionObjects, this.m_intermediateFormatVersion);
					}
					return intermediateFormatReader;
				}
			}

			// Token: 0x04004D29 RID: 19753
			private ReportProcessing.GetReportChunk m_getChunkCallback;

			// Token: 0x04004D2A RID: 19754
			private Hashtable m_definitionObjects;

			// Token: 0x04004D2B RID: 19755
			private Hashtable m_instanceObjects;

			// Token: 0x04004D2C RID: 19756
			private IntermediateFormatVersion m_intermediateFormatVersion;

			// Token: 0x04004D2D RID: 19757
			private Stream m_docMap;

			// Token: 0x04004D2E RID: 19758
			private bool m_hasDocMap = true;

			// Token: 0x04004D2F RID: 19759
			private Stream m_showHideInfo;

			// Token: 0x04004D30 RID: 19760
			private bool m_hasShowHideInfo = true;

			// Token: 0x04004D31 RID: 19761
			private Stream m_bookmarks;

			// Token: 0x04004D32 RID: 19762
			private bool m_hasBookmarks = true;

			// Token: 0x04004D33 RID: 19763
			private Stream m_drillthrough;

			// Token: 0x04004D34 RID: 19764
			private bool m_hasDrillthrough = true;

			// Token: 0x04004D35 RID: 19765
			private Stream m_quickFind;

			// Token: 0x04004D36 RID: 19766
			private bool m_hasQuickFind = true;

			// Token: 0x04004D37 RID: 19767
			private Stream m_sortFilterEventInfo;

			// Token: 0x04004D38 RID: 19768
			private bool m_hasSortFilterEventInfo = true;
		}

		// Token: 0x02000C9D RID: 3229
		internal sealed class RenderingChunkManager
		{
			// Token: 0x06008C99 RID: 35993 RVA: 0x0023C0B4 File Offset: 0x0023A2B4
			internal RenderingChunkManager(ReportProcessing.GetReportChunk getChunkCallback, Hashtable instanceObjects, Hashtable definitionObjects, IntermediateFormatReader.State declarationsRead, IntermediateFormatVersion intermediateFormatVersion)
			{
				this.m_getChunkCallback = getChunkCallback;
				this.m_instanceObjects = instanceObjects;
				this.m_definitionObjects = definitionObjects;
				this.m_declarationsRead = declarationsRead;
				this.m_intermediateFormatVersion = intermediateFormatVersion;
				if (intermediateFormatVersion != null && intermediateFormatVersion.IsRS2005_WithPHFChunks)
				{
					this.m_pageSectionIntermediateFormatVersion = intermediateFormatVersion;
					return;
				}
				this.m_pageSectionIntermediateFormatVersion = new IntermediateFormatVersion();
			}

			// Token: 0x06008C9A RID: 35994 RVA: 0x0023C114 File Offset: 0x0023A314
			internal IntermediateFormatReader GetReaderForSpecialChunk(long offset)
			{
				if (this.m_specialChunkReader == null)
				{
					if (this.m_specialChunk == null)
					{
						string text;
						this.m_specialChunk = this.m_getChunkCallback("Special", ReportProcessing.ReportChunkTypes.Main, out text);
					}
					this.m_specialChunkReader = new IntermediateFormatReader(this.m_specialChunk, this.m_instanceObjects, this.m_intermediateFormatVersion);
				}
				this.m_specialChunk.Position = offset;
				return this.m_specialChunkReader;
			}

			// Token: 0x06008C9B RID: 35995 RVA: 0x0023C17C File Offset: 0x0023A37C
			internal IntermediateFormatReader GetSpecialChunkReader(ChunkManager.SpecialChunkName chunkName)
			{
				if (this.m_specialChunkManager == null)
				{
					this.m_specialChunkManager = new ChunkManager.SpecialChunkManager(this.m_getChunkCallback, this.m_instanceObjects, this.m_definitionObjects, this.m_intermediateFormatVersion);
				}
				if (chunkName == ChunkManager.SpecialChunkName.DocumentMap)
				{
					return this.m_specialChunkManager.DocumentMapReader;
				}
				if (chunkName == ChunkManager.SpecialChunkName.Bookmark)
				{
					return this.m_specialChunkManager.BookmarkReader;
				}
				if (chunkName == ChunkManager.SpecialChunkName.ShowHideInfo)
				{
					return this.m_specialChunkManager.ShowHideInfoReader;
				}
				if (chunkName == ChunkManager.SpecialChunkName.QuickFind)
				{
					return this.m_specialChunkManager.QuickFindReader;
				}
				if (chunkName == ChunkManager.SpecialChunkName.SortFilterEventInfo)
				{
					return this.m_specialChunkManager.SortFilterEventInfoReader;
				}
				return null;
			}

			// Token: 0x06008C9C RID: 35996 RVA: 0x0023C204 File Offset: 0x0023A404
			internal bool PageSectionChunkExists()
			{
				string text;
				this.m_pageSectionChunk = this.m_getChunkCallback("PageSections", ReportProcessing.ReportChunkTypes.Main, out text);
				if (this.m_pageSectionChunk != null)
				{
					this.m_pageSectionChunk.Close();
					this.m_pageSectionChunk = null;
					return true;
				}
				return false;
			}

			// Token: 0x06008C9D RID: 35997 RVA: 0x0023C248 File Offset: 0x0023A448
			internal IntermediateFormatReader GetPageSectionReader(int requestedPageNumber, out int currentPageNumber)
			{
				currentPageNumber = this.m_pageSectionLastReadPage + 1;
				bool flag = this.m_pageSectionReader == null;
				if (this.m_pageSectionChunk == null)
				{
					string text;
					this.m_pageSectionChunk = this.m_getChunkCallback("PageSections", ReportProcessing.ReportChunkTypes.Main, out text);
				}
				else if (requestedPageNumber < 0 || requestedPageNumber <= this.m_pageSectionLastReadPage)
				{
					this.m_pageSectionChunk.Position = 0L;
					currentPageNumber = 0;
					flag = true;
				}
				if (flag)
				{
					this.m_pageSectionReader = new IntermediateFormatReader(this.m_pageSectionChunk, this.m_pageSectionIntermediateFormatVersion);
				}
				return this.m_pageSectionReader;
			}

			// Token: 0x06008C9E RID: 35998 RVA: 0x0023C2CB File Offset: 0x0023A4CB
			internal void SetPageSectionReaderState(IntermediateFormatReader.State declarations, int pageSectionLastReadPage)
			{
				this.m_pageSectionDeclarationsRead = declarations;
				this.m_pageSectionLastReadPage = pageSectionLastReadPage;
			}

			// Token: 0x06008C9F RID: 35999 RVA: 0x0023C2DC File Offset: 0x0023A4DC
			internal IntermediateFormatReader GetPageSectionInstanceReader(long offset)
			{
				if (this.m_pageSectionInstanceReader == null)
				{
					if (this.m_pageSectionInstanceChunk == null)
					{
						string text;
						this.m_pageSectionInstanceChunk = this.m_getChunkCallback("PageSectionInstances", ReportProcessing.ReportChunkTypes.Other, out text);
					}
					if (this.m_pageSectionInstanceChunk != null)
					{
						this.m_pageSectionInstanceReader = new IntermediateFormatReader(this.m_pageSectionInstanceChunk, this.m_pageSectionDeclarationsRead, this.m_pageSectionIntermediateFormatVersion);
					}
				}
				if (this.m_pageSectionInstanceChunk != null)
				{
					this.m_pageSectionInstanceChunk.Position = offset;
				}
				return this.m_pageSectionInstanceReader;
			}

			// Token: 0x06008CA0 RID: 36000 RVA: 0x0023C354 File Offset: 0x0023A554
			internal IntermediateFormatReader GetReader(long offset)
			{
				if (offset < 0L)
				{
					if (this.m_firstPageReader == null)
					{
						if (this.m_firstPageChunk == null)
						{
							string text;
							this.m_firstPageChunk = this.m_getChunkCallback("FirstPage", ReportProcessing.ReportChunkTypes.Main, out text);
						}
						this.m_firstPageReader = new IntermediateFormatReader(this.m_firstPageChunk, this.m_declarationsRead, this.m_definitionObjects, this.m_intermediateFormatVersion);
					}
					this.m_firstPageChunk.Position = 0L - offset;
					return this.m_firstPageReader;
				}
				if (this.m_otherPageReader == null)
				{
					if (this.m_otherPageChunk == null)
					{
						string text2;
						this.m_otherPageChunk = this.m_getChunkCallback("OtherPages", ReportProcessing.ReportChunkTypes.Other, out text2);
					}
					this.m_otherPageReader = new IntermediateFormatReader(this.m_otherPageChunk, this.m_declarationsRead, this.m_definitionObjects, this.m_intermediateFormatVersion);
				}
				this.m_otherPageChunk.Position = offset;
				return this.m_otherPageReader;
			}

			// Token: 0x06008CA1 RID: 36001 RVA: 0x0023C428 File Offset: 0x0023A628
			internal void Close()
			{
				this.m_firstPageReader = null;
				if (this.m_firstPageChunk != null)
				{
					this.m_firstPageChunk.Close();
					this.m_firstPageChunk = null;
				}
				this.m_otherPageReader = null;
				if (this.m_otherPageChunk != null)
				{
					this.m_otherPageChunk.Close();
					this.m_otherPageChunk = null;
				}
				if (this.m_specialChunkManager != null)
				{
					this.m_specialChunkManager.Close();
					this.m_specialChunkManager = null;
				}
				this.m_specialChunkReader = null;
				if (this.m_specialChunk != null)
				{
					this.m_specialChunk.Close();
					this.m_specialChunk = null;
				}
				this.m_pageSectionReader = null;
				if (this.m_pageSectionChunk != null)
				{
					this.m_pageSectionChunk.Close();
					this.m_pageSectionChunk = null;
				}
				this.m_pageSectionInstanceReader = null;
				if (this.m_pageSectionInstanceChunk != null)
				{
					this.m_pageSectionInstanceChunk.Close();
					this.m_pageSectionInstanceChunk = null;
				}
			}

			// Token: 0x04004D39 RID: 19769
			private ReportProcessing.GetReportChunk m_getChunkCallback;

			// Token: 0x04004D3A RID: 19770
			private ChunkManager.SpecialChunkManager m_specialChunkManager;

			// Token: 0x04004D3B RID: 19771
			private Stream m_firstPageChunk;

			// Token: 0x04004D3C RID: 19772
			private IntermediateFormatReader m_firstPageReader;

			// Token: 0x04004D3D RID: 19773
			private Stream m_otherPageChunk;

			// Token: 0x04004D3E RID: 19774
			private IntermediateFormatReader m_otherPageReader;

			// Token: 0x04004D3F RID: 19775
			private Hashtable m_instanceObjects;

			// Token: 0x04004D40 RID: 19776
			private Hashtable m_definitionObjects;

			// Token: 0x04004D41 RID: 19777
			private IntermediateFormatReader.State m_declarationsRead;

			// Token: 0x04004D42 RID: 19778
			private IntermediateFormatVersion m_intermediateFormatVersion;

			// Token: 0x04004D43 RID: 19779
			private Stream m_specialChunk;

			// Token: 0x04004D44 RID: 19780
			private IntermediateFormatReader m_specialChunkReader;

			// Token: 0x04004D45 RID: 19781
			private Stream m_pageSectionChunk;

			// Token: 0x04004D46 RID: 19782
			private IntermediateFormatReader m_pageSectionReader;

			// Token: 0x04004D47 RID: 19783
			private Stream m_pageSectionInstanceChunk;

			// Token: 0x04004D48 RID: 19784
			private IntermediateFormatReader m_pageSectionInstanceReader;

			// Token: 0x04004D49 RID: 19785
			private IntermediateFormatVersion m_pageSectionIntermediateFormatVersion;

			// Token: 0x04004D4A RID: 19786
			private IntermediateFormatReader.State m_pageSectionDeclarationsRead;

			// Token: 0x04004D4B RID: 19787
			private int m_pageSectionLastReadPage = -1;
		}
	}
}
