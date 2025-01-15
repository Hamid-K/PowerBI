using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F3F RID: 7999
	internal sealed class ParquetQuery : DataSourceQuery, INestedOperationQuery
	{
		// Token: 0x06010CE7 RID: 68839 RVA: 0x0039DFBC File Offset: 0x0039C1BC
		private ParquetQuery(IEngineHost host, BinaryValue binaryValue, OptionsRecord options, GroupSchemaElement schema, bool hasColumnSelection, RowCount skipCount, StreamOwningParquetFileReader fileReader)
		{
			this.host = host;
			this.schema = schema;
			this.binaryValue = binaryValue;
			this.options = options;
			this.skipCount = skipCount;
			this.fileReader = ((fileReader == null) ? new StreamOwningParquetFileReader() : new StreamOwningParquetFileReader(fileReader));
			this.hasColumnSelection = hasColumnSelection;
		}

		// Token: 0x17002C7F RID: 11391
		// (get) Token: 0x06010CE8 RID: 68840 RVA: 0x0039E014 File Offset: 0x0039C214
		public override Keys Columns
		{
			get
			{
				return this.Schema.ItemTypeValue.AsRecordType.FieldKeys;
			}
		}

		// Token: 0x17002C80 RID: 11392
		// (get) Token: 0x06010CE9 RID: 68841 RVA: 0x0039E02B File Offset: 0x0039C22B
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002C81 RID: 11393
		// (get) Token: 0x06010CEA RID: 68842 RVA: 0x00004FAE File Offset: 0x000031AE
		Query INestedOperationQuery.AsQuery
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002C82 RID: 11394
		// (get) Token: 0x06010CEB RID: 68843 RVA: 0x0039E033 File Offset: 0x0039C233
		private bool UseStatistics
		{
			get
			{
				return this.options.GetBool("UseStatistics", false);
			}
		}

		// Token: 0x06010CEC RID: 68844 RVA: 0x0039E048 File Offset: 0x0039C248
		public override TypeValue GetColumnType(int column)
		{
			bool flag;
			return this.Schema.ItemTypeValue.AsRecordType.GetFieldType(column, out flag);
		}

		// Token: 0x17002C83 RID: 11395
		// (get) Token: 0x06010CED RID: 68845 RVA: 0x0039E070 File Offset: 0x0039C270
		private GroupSchemaElement Schema
		{
			get
			{
				if (this.schema == null)
				{
					using (StreamOwningParquetFileReader streamOwningParquetFileReader = this.GetFileReader())
					{
						this.schema = this.CreateSchema(streamOwningParquetFileReader);
						this.CacheFileReader(streamOwningParquetFileReader);
					}
				}
				return this.schema;
			}
		}

		// Token: 0x06010CEE RID: 68846 RVA: 0x0039E0C4 File Offset: 0x0039C2C4
		private GroupSchemaElement GetSchema(StreamOwningParquetFileReader fileReader)
		{
			if (this.schema == null)
			{
				this.schema = this.CreateSchema(fileReader);
			}
			return this.schema;
		}

		// Token: 0x06010CEF RID: 68847 RVA: 0x0039E0E1 File Offset: 0x0039C2E1
		public static ParquetQuery New(IEngineHost host, BinaryValue binaryValue, OptionsRecord options)
		{
			return new ParquetQuery(host, binaryValue, options, null, false, RowCount.Zero, null);
		}

		// Token: 0x06010CF0 RID: 68848 RVA: 0x0039E0F3 File Offset: 0x0039C2F3
		private GroupSchemaElement CreateSchema(StreamOwningParquetFileReader fileReader)
		{
			return ParquetExceptionHandler.Invoke<GroupSchemaElement>(delegate
			{
				SchemaConfig schemaConfig = new SchemaConfig
				{
					DefaultTypeMapping = ParquetOptions.GetTypeMapping(this.options)
				};
				GroupSchemaElement groupSchemaElement;
				using (Node schemaRoot = fileReader.ParquetFileReader.FileMetaData.Schema.SchemaRoot)
				{
					groupSchemaElement = SchemaElement.CreateSchema(schemaRoot, null, schemaConfig);
				}
				return groupSchemaElement;
			});
		}

		// Token: 0x17002C84 RID: 11396
		// (get) Token: 0x06010CF1 RID: 68849 RVA: 0x0039E118 File Offset: 0x0039C318
		public override RowCount RowCount
		{
			get
			{
				RowCount rowCount;
				using (StreamOwningParquetFileReader streamOwningParquetFileReader = this.GetFileReader())
				{
					long numRows = streamOwningParquetFileReader.ParquetFileReader.FileMetaData.NumRows;
					this.CacheFileReader(streamOwningParquetFileReader);
					long num = numRows;
					rowCount = this.skipCount;
					RowCount rowCount2;
					if (num <= rowCount.Value)
					{
						rowCount2 = RowCount.Zero;
					}
					else
					{
						long num2 = numRows;
						rowCount = this.skipCount;
						rowCount2 = new RowCount(num2 - rowCount.Value);
					}
					rowCount = rowCount2;
				}
				return rowCount;
			}
		}

		// Token: 0x06010CF2 RID: 68850 RVA: 0x0039E190 File Offset: 0x0039C390
		public List<KeyValuePair<string, ListStatistics>> GetStatistics(out long rowCount)
		{
			rowCount = 0L;
			List<KeyValuePair<string, ListStatistics>> list2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "ParquetQuery/GetStatistics", TraceEventType.Information, null))
			{
				using (StreamOwningParquetFileReader streamOwningParquetFileReader = this.GetFileReader())
				{
					int[] columnSelection = this.GetColumnSelection(streamOwningParquetFileReader);
					ListStatistics[] array = new ListStatistics[columnSelection.Length];
					ParquetPrimitiveTypeMap[] array2;
					this.schema.CreateTableSchema(out array2);
					using (IEnumerator<RowGroupReader> enumerator = new ParquetRowGroupsEnumerator(streamOwningParquetFileReader, columnSelection))
					{
						bool flag = true;
						while (enumerator.MoveNext())
						{
							RowGroupReader rowGroupReader = enumerator.Current;
							rowCount += rowGroupReader.MetaData.NumRows;
							foreach (int num in columnSelection)
							{
								using (ColumnChunkMetaData columnChunkMetaData = rowGroupReader.MetaData.GetColumnChunkMetaData(num))
								{
									using (Statistics statistics = columnChunkMetaData.Statistics)
									{
										if ((flag || array[num] != null) && statistics != null && (statistics.HasMinMax || statistics.NullCount == rowGroupReader.MetaData.NumRows))
										{
											ListStatistics statistics2 = array2[num].GetStatistics(statistics);
											array[num] = (flag ? statistics2 : array[num].Combine(statistics2));
										}
										else
										{
											array[num] = null;
										}
									}
								}
							}
							flag = false;
						}
					}
					List<KeyValuePair<string, ListStatistics>> list = new List<KeyValuePair<string, ListStatistics>>(columnSelection.Length);
					foreach (int num2 in columnSelection)
					{
						if (array[num2] != null)
						{
							list.Add(new KeyValuePair<string, ListStatistics>(this.Columns[num2], array[num2]));
						}
						else
						{
							hostTrace.Add("Column" + num2.ToString(CultureInfo.InvariantCulture), array2[num2].TypeValue.TypeKind.ToString(), false);
						}
					}
					list2 = list;
				}
			}
			return list2;
		}

		// Token: 0x06010CF3 RID: 68851 RVA: 0x0039E3F0 File Offset: 0x0039C5F0
		public override IEnumerable<IValueReference> GetRows()
		{
			if (this.Columns.Length == 0)
			{
				return ParquetQuery.Empty(this.RowCount.Value);
			}
			return DeferredEnumerable.From<IValueReference>(delegate
			{
				IEnumerator<IValueReference> enumerator;
				using (StreamOwningParquetFileReader streamOwningParquetFileReader = this.GetFileReader())
				{
					int[] columnSelection = this.GetColumnSelection(streamOwningParquetFileReader);
					IPageReader pageReader;
					if (this.TryGetReader(streamOwningParquetFileReader, columnSelection, out pageReader))
					{
						enumerator = new DbDataReaderEnumerator(new DataReaderDbDataReader(new PageReaderDataReader(pageReader, new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties))), true, QueryTableValue.NewRowType(this), "Parquet", null);
					}
					else
					{
						try
						{
							enumerator = ParquetRecordEnumerator.New(streamOwningParquetFileReader, this.GetSchema(streamOwningParquetFileReader), columnSelection, this.skipCount);
						}
						catch (ParquetException ex)
						{
							throw ParquetExceptionHandler.GetParquetValueException(ex);
						}
					}
				}
				return enumerator;
			});
		}

		// Token: 0x06010CF4 RID: 68852 RVA: 0x0039E430 File Offset: 0x0039C630
		public override bool TryGetReader(out IPageReader reader)
		{
			bool flag2;
			using (StreamOwningParquetFileReader streamOwningParquetFileReader = this.GetFileReader())
			{
				int[] columnSelection = this.GetColumnSelection(streamOwningParquetFileReader);
				bool flag = this.TryGetReader(streamOwningParquetFileReader, columnSelection, out reader);
				this.CacheFileReader(streamOwningParquetFileReader);
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06010CF5 RID: 68853 RVA: 0x0039E47C File Offset: 0x0039C67C
		private bool TryGetReader(StreamOwningParquetFileReader fileReader, int[] columnSelection, out IPageReader reader)
		{
			bool flag;
			try
			{
				reader = ParquetPageReader.New(fileReader, this.GetSchema(fileReader), columnSelection, this.skipCount);
				flag = true;
			}
			catch (ParquetException ex)
			{
				throw ParquetExceptionHandler.GetParquetValueException(ex);
			}
			catch (NotSupportedException)
			{
				reader = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06010CF6 RID: 68854 RVA: 0x0039E4D0 File Offset: 0x0039C6D0
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			INestedOperationQuery nestedOperationQuery;
			if (!this.TrySelectColumns(new NestedColumnSelection(columnSelection, null), out nestedOperationQuery))
			{
				return base.SelectColumns(columnSelection);
			}
			return nestedOperationQuery.AsQuery;
		}

		// Token: 0x06010CF7 RID: 68855 RVA: 0x0039E4FC File Offset: 0x0039C6FC
		public override Query Skip(RowCount count)
		{
			return new ParquetQuery(this.host, this.binaryValue, this.options, this.schema, this.hasColumnSelection, RowRange.All.Skip(this.skipCount).Skip(count).SkipCount, this.fileReader);
		}

		// Token: 0x06010CF8 RID: 68856 RVA: 0x0039E558 File Offset: 0x0039C758
		public override Query Group(Grouping grouping)
		{
			if (this.UseStatistics && this.skipCount.IsZero)
			{
				Query query;
				if (ListStatisticsOperations.TryGetAggregation(this, grouping, new ParquetQuery.RowGroupStatisticsEnumerable(this), out query))
				{
					return query;
				}
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "ParquetQuery/Group/TryGetAggregation/Fail", TraceEventType.Information, null))
				{
					RecordValue fields = QueryTableValue.NewRowType(this).Fields;
					for (int i = 0; i < fields.Count; i++)
					{
						hostTrace.Add("Column" + i.ToString(CultureInfo.InvariantCulture), fields[i]["Type"].AsType.TypeKind.ToString(), false);
					}
				}
			}
			return base.Group(grouping);
		}

		// Token: 0x06010CF9 RID: 68857 RVA: 0x0039BAB0 File Offset: 0x00399CB0
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			query = NestingExpandRecordColumnQuery.New(columnToExpand, fieldsToProject, newColumns, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x06010CFA RID: 68858 RVA: 0x0039BAC9 File Offset: 0x00399CC9
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			query = NestingExpandListColumnQuery.New(columnIndex, singleOrDefault, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x06010CFB RID: 68859 RVA: 0x0039E638 File Offset: 0x0039C838
		public bool TrySelectColumns(NestedColumnSelection columnSelection, out INestedOperationQuery query)
		{
			SchemaElement schemaElement;
			if (!this.Schema.TrySelectColumns(columnSelection, out schemaElement))
			{
				query = null;
				return false;
			}
			query = new ParquetQuery(this.host, this.binaryValue, this.options, (GroupSchemaElement)schemaElement, true, this.skipCount, this.fileReader);
			return true;
		}

		// Token: 0x06010CFC RID: 68860 RVA: 0x0039E688 File Offset: 0x0039C888
		public override bool TryGetExpression(out IExpression expression)
		{
			IExpression expression2 = this.binaryValue.Expression;
			if (expression2 != null)
			{
				expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new ParquetModule.Parquet.DocumentFunctionValue(this.host)), expression2, new ConstantExpressionSyntaxNode(this.options.AsRecord));
				if (this.hasColumnSelection)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectColumns), expression, new ConstantExpressionSyntaxNode(ListValue.New(this.schema.FieldKeys.ToArray<string>())));
				}
				if (!this.skipCount.IsZero)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.Skip), expression, new ConstantExpressionSyntaxNode(NumberValue.New(this.skipCount.Value)));
				}
				return true;
			}
			return base.TryGetExpression(out expression);
		}

		// Token: 0x06010CFD RID: 68861 RVA: 0x0039E748 File Offset: 0x0039C948
		public ActionValue Replace(Value value)
		{
			return new SimpleBindingActionValue(delegate(FunctionValue binding)
			{
				if (!(binding is ReturnTableGroupFunctionValue) && binding != SimpleActionBinding.ReturnNull && binding != SimpleActionBinding.ReturnRowCount)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
				}
				Func<Value> <>9__2;
				return ActionValue.New(delegate
				{
					Func<Value> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = delegate
						{
							TableValue asTable = value.AsTable;
							IAccumulator accumulator = null;
							ReturnTableGroupFunctionValue returnTableGroupFunctionValue = binding as ReturnTableGroupFunctionValue;
							if (returnTableGroupFunctionValue != null)
							{
								accumulator = returnTableGroupFunctionValue.CreateAccumulator(asTable.Type.AsTableType);
							}
							SchemaConfig schemaConfig = new SchemaConfig
							{
								MaxDepth = ParquetOptions.GetMaxDepth(this.options),
								DefaultTypeMapping = ParquetOptions.GetTypeMapping(this.options)
							};
							GroupSchemaElement groupSchemaElement = SchemaElement.CreateSchema(null, asTable.Type.AsTableType, schemaConfig);
							long num;
							using (WriterPropertiesBuilder writerPropertiesBuilder = this.GetWriterPropertiesBuilder())
							{
								foreach (TableKey tableKey in asTable.TableKeys)
								{
									if (tableKey.Columns.Length == 1)
									{
										SchemaElement schemaElement = groupSchemaElement.Fields[tableKey.Columns[0]];
										if (schemaElement.ElementType == NodeType.Primitive)
										{
											writerPropertiesBuilder.Encoding(schemaElement.PathString, Encoding.Plain);
										}
									}
								}
								using (WriterProperties writerProperties = writerPropertiesBuilder.Build())
								{
									using (ParquetScalableTableWriter parquetScalableTableWriter = new ParquetScalableTableWriter(this.host, this.options))
									{
										num = parquetScalableTableWriter.Write(this.binaryValue, asTable, groupSchemaElement, writerProperties, accumulator);
									}
								}
							}
							if (returnTableGroupFunctionValue != null)
							{
								return returnTableGroupFunctionValue.CreateResult(accumulator);
							}
							if (binding == SimpleActionBinding.ReturnRowCount)
							{
								return new CountTableValue(num);
							}
							return Value.Null;
						});
					}
					return ParquetExceptionHandler.Invoke<Value>(func);
				}).Bind(binding);
			});
		}

		// Token: 0x06010CFE RID: 68862 RVA: 0x0039E770 File Offset: 0x0039C970
		private WriterPropertiesBuilder GetWriterPropertiesBuilder()
		{
			WriterPropertiesBuilder writerPropertiesBuilder = new WriterPropertiesBuilder();
			object obj;
			if (this.options.TryGetValue("Compression", out obj))
			{
				writerPropertiesBuilder.Compression((global::ParquetSharp.Compression)obj);
			}
			if (!this.options.GetBool("PreserveOrder", true))
			{
				writerPropertiesBuilder.EnableShuffle();
			}
			writerPropertiesBuilder.DictionaryPagesizeLimit(134217728L);
			return writerPropertiesBuilder;
		}

		// Token: 0x06010CFF RID: 68863 RVA: 0x0039E7D0 File Offset: 0x0039C9D0
		private int[] GetColumnSelection(StreamOwningParquetFileReader fileReader)
		{
			int[] array;
			if (!this.GetSchema(fileReader).TryGetPrimitiveColumnSelection(fileReader.ParquetFileReader.FileMetaData.Schema, out array, new SchemaConfig
			{
				DefaultTypeMapping = ParquetOptions.GetTypeMapping(this.options)
			}))
			{
				throw DataSourceException.NewDataSourceChanged(this.host, "Parquet", null);
			}
			return array;
		}

		// Token: 0x06010D00 RID: 68864 RVA: 0x0039E826 File Offset: 0x0039CA26
		private StreamOwningParquetFileReader GetFileReader()
		{
			if (this.fileReader.ParquetFileReader == null)
			{
				return StreamOwningParquetFileReader.Open(this.binaryValue);
			}
			return new StreamOwningParquetFileReader(this.fileReader);
		}

		// Token: 0x06010D01 RID: 68865 RVA: 0x0039E84C File Offset: 0x0039CA4C
		private void CacheFileReader(StreamOwningParquetFileReader fileReader)
		{
			if (this.fileReader.ParquetFileReader == null)
			{
				this.fileReader.Swap(fileReader);
				this.fileReader.RegisterForCleanup(this.host);
			}
		}

		// Token: 0x06010D02 RID: 68866 RVA: 0x0039E878 File Offset: 0x0039CA78
		private static IEnumerable<IValueReference> Empty(long rowCount)
		{
			while (rowCount > 0L)
			{
				yield return RecordValue.Empty;
				long num = rowCount;
				rowCount = num - 1L;
			}
			yield break;
		}

		// Token: 0x040064C8 RID: 25800
		public const string DataSourceName = "Parquet";

		// Token: 0x040064C9 RID: 25801
		private readonly IEngineHost host;

		// Token: 0x040064CA RID: 25802
		private readonly BinaryValue binaryValue;

		// Token: 0x040064CB RID: 25803
		private readonly OptionsRecord options;

		// Token: 0x040064CC RID: 25804
		private readonly RowCount skipCount;

		// Token: 0x040064CD RID: 25805
		private readonly StreamOwningParquetFileReader fileReader;

		// Token: 0x040064CE RID: 25806
		private readonly bool hasColumnSelection;

		// Token: 0x040064CF RID: 25807
		private GroupSchemaElement schema;

		// Token: 0x02001F40 RID: 8000
		private sealed class RowGroupStatisticsEnumerable : IEnumerable<ITableSegmentWithStatistics>, IEnumerable
		{
			// Token: 0x06010D04 RID: 68868 RVA: 0x0039E930 File Offset: 0x0039CB30
			public RowGroupStatisticsEnumerable(ParquetQuery query)
			{
				this.query = query;
			}

			// Token: 0x06010D05 RID: 68869 RVA: 0x0039E93F File Offset: 0x0039CB3F
			public IEnumerator<ITableSegmentWithStatistics> GetEnumerator()
			{
				using (StreamOwningParquetFileReader fileReader = this.query.GetFileReader())
				{
					int[] columnSelection = this.query.GetColumnSelection(fileReader);
					ParquetPrimitiveTypeMap[] typeMaps;
					this.query.schema.CreateTableSchema(out typeMaps);
					using (IEnumerator<RowGroupReader> rowGroupsEnumerator = new ParquetRowGroupsEnumerator(fileReader, columnSelection))
					{
						while (rowGroupsEnumerator.MoveNext())
						{
							RowGroupReader rowGroupReader = rowGroupsEnumerator.Current;
							yield return new ParquetQuery.RowGroupStatisticsEnumerable.RowGroupWithStatistics(this.query.host, rowGroupReader, typeMaps);
						}
					}
					IEnumerator<RowGroupReader> rowGroupsEnumerator = null;
					typeMaps = null;
				}
				StreamOwningParquetFileReader fileReader = null;
				yield break;
				yield break;
			}

			// Token: 0x06010D06 RID: 68870 RVA: 0x0039E94E File Offset: 0x0039CB4E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040064D0 RID: 25808
			private readonly ParquetQuery query;

			// Token: 0x02001F41 RID: 8001
			private class RowGroupWithStatistics : ITableSegmentWithStatistics
			{
				// Token: 0x06010D07 RID: 68871 RVA: 0x0039E956 File Offset: 0x0039CB56
				public RowGroupWithStatistics(IEngineHost engineHost, RowGroupReader rowGroupReader, ParquetPrimitiveTypeMap[] typeMaps)
				{
					this.engineHost = engineHost;
					this.rowGroupReader = rowGroupReader;
					this.typeMaps = typeMaps;
				}

				// Token: 0x06010D08 RID: 68872 RVA: 0x0039E974 File Offset: 0x0039CB74
				public bool TryGetStatistics(int column, out ListStatistics statistics)
				{
					statistics = null;
					using (ColumnChunkMetaData columnChunkMetaData = this.rowGroupReader.MetaData.GetColumnChunkMetaData(column))
					{
						using (Statistics statistics2 = columnChunkMetaData.Statistics)
						{
							if (statistics2 != null && (statistics2.HasMinMax || statistics2.NullCount == this.rowGroupReader.MetaData.NumRows))
							{
								statistics = this.typeMaps[column].GetStatistics(statistics2);
								return true;
							}
						}
					}
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, "ParquetQuery/TryGetStatistics/Fail", TraceEventType.Information, null))
					{
						hostTrace.Add("Column", column, false);
						hostTrace.Add("Type", this.typeMaps[column].TypeValue.TypeKind.ToString(), false);
					}
					return false;
				}

				// Token: 0x040064D1 RID: 25809
				private readonly IEngineHost engineHost;

				// Token: 0x040064D2 RID: 25810
				private readonly RowGroupReader rowGroupReader;

				// Token: 0x040064D3 RID: 25811
				private readonly ParquetPrimitiveTypeMap[] typeMaps;
			}
		}
	}
}
