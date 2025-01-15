using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.Data.DeltaLake.Serialization;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Data.DeltaLake.Utilities;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EE8 RID: 7912
	internal class DeltaQuery : DataSourceQuery
	{
		// Token: 0x06010AAE RID: 68270 RVA: 0x00396322 File Offset: 0x00394522
		public DeltaQuery(IEngineHost engineHost, DeltaSource source)
			: this(engineHost, source, null, null, EmptyArray<QueryExpression>.Instance, EmptyArray<QueryExpression>.Instance, null)
		{
		}

		// Token: 0x06010AAF RID: 68271 RVA: 0x00396339 File Offset: 0x00394539
		private DeltaQuery(IEngineHost engineHost, DeltaSource source, RecordValue fields, ColumnSelection columnSelection, IList<QueryExpression> partitionExprs, IList<QueryExpression> filterExprs, HashSet<string> columnAccess)
		{
			this.engineHost = engineHost;
			this.source = source;
			this.fields = fields;
			this.columnSelection = columnSelection;
			this.partitionExprs = partitionExprs;
			this.filterExprs = filterExprs;
			this.columnAccess = columnAccess;
		}

		// Token: 0x17002C18 RID: 11288
		// (get) Token: 0x06010AB0 RID: 68272 RVA: 0x00396376 File Offset: 0x00394576
		public override Keys Columns
		{
			get
			{
				ColumnSelection columnSelection = this.columnSelection;
				return ((columnSelection != null) ? columnSelection.Keys : null) ?? this.Fields.Keys;
			}
		}

		// Token: 0x17002C19 RID: 11289
		// (get) Token: 0x06010AB1 RID: 68273 RVA: 0x00396399 File Offset: 0x00394599
		public override IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17002C1A RID: 11290
		// (get) Token: 0x06010AB2 RID: 68274 RVA: 0x003963A1 File Offset: 0x003945A1
		private FunctionValue PartitionFunction
		{
			get
			{
				if (this.partitionFunction == null)
				{
					this.partitionFunction = this.MakeFunction(this.partitionExprs);
				}
				return this.partitionFunction;
			}
		}

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x06010AB3 RID: 68275 RVA: 0x003963C3 File Offset: 0x003945C3
		private FunctionValue FilterFunction
		{
			get
			{
				if (this.filterFunction == null)
				{
					this.filterFunction = this.MakeFunction(this.filterExprs);
				}
				return this.filterFunction;
			}
		}

		// Token: 0x06010AB4 RID: 68276 RVA: 0x003963E5 File Offset: 0x003945E5
		private FunctionValue MakeFunction(IList<QueryExpression> expressions)
		{
			if (expressions.Count > 0)
			{
				return QueryExpressionAssembler.Assemble(this.Fields.Keys, SelectRowsQuery.CreateConjunctiveNF(expressions));
			}
			return ConstantFunctionValue.EachTrue;
		}

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x06010AB5 RID: 68277 RVA: 0x0039640C File Offset: 0x0039460C
		private bool IsBaseTable
		{
			get
			{
				return this.IsCompletePartitions && this.partitionExprs.Count == 0;
			}
		}

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x06010AB6 RID: 68278 RVA: 0x00396426 File Offset: 0x00394626
		private bool IsCompletePartitions
		{
			get
			{
				return this.columnSelection == null && this.filterExprs.Count == 0;
			}
		}

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x06010AB7 RID: 68279 RVA: 0x00396440 File Offset: 0x00394640
		private RecordValue Fields
		{
			get
			{
				if (this.fields == null)
				{
					this.fields = DeltaTypeConversion.Convert(this.source.GetSchema()).AsRecordType.Fields;
				}
				return this.fields;
			}
		}

		// Token: 0x06010AB8 RID: 68280 RVA: 0x00396470 File Offset: 0x00394670
		public override bool TryGetExpression(out IExpression expression)
		{
			if (this.source.TryGetExpression(out expression))
			{
				if (this.partitionExprs.Count > 0)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), expression, ConstantExpressionSyntaxNode.New(this.PartitionFunction));
				}
				if (this.filterExprs.Count > 0)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), expression, ConstantExpressionSyntaxNode.New(this.FilterFunction));
				}
				if (this.columnSelection != null)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectColumns), expression, ConstantExpressionSyntaxNode.New(ListValue.New(this.columnSelection.Keys.ToArray<string>())));
				}
				return true;
			}
			return base.TryGetExpression(out expression);
		}

		// Token: 0x06010AB9 RID: 68281 RVA: 0x00396524 File Offset: 0x00394724
		public override TypeValue GetColumnType(int column)
		{
			if (this.columnSelection != null)
			{
				column = this.columnSelection.GetColumn(column);
			}
			return this.Fields[column][0].AsType;
		}

		// Token: 0x06010ABA RID: 68282 RVA: 0x00396554 File Offset: 0x00394754
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			if (this.columnSelection != null)
			{
				columnSelection = this.columnSelection.SelectColumns(columnSelection);
			}
			return new DeltaQuery(this.engineHost, this.source, this.Fields, columnSelection, this.partitionExprs, this.filterExprs, this.columnAccess);
		}

		// Token: 0x06010ABB RID: 68283 RVA: 0x003965A4 File Offset: 0x003947A4
		public override Query SelectRows(FunctionValue function)
		{
			QueryExpression queryExpression;
			if (!QueryExpressionBuilder.TryToQueryExpression(RecordTypeValue.New(this.Fields), function, out queryExpression))
			{
				return base.SelectRows(function);
			}
			List<QueryExpression> list = null;
			List<QueryExpression> list2 = null;
			HashSet<string> hashSet = ((this.columnAccess == null) ? new HashSet<string>() : new HashSet<string>(this.columnAccess));
			foreach (QueryExpression queryExpression2 in SelectRowsQuery.GetConjunctiveNF(queryExpression))
			{
				if (this.AccessesOnlyPartitionColumn(queryExpression2, hashSet))
				{
					list = list ?? new List<QueryExpression>(this.partitionExprs);
					list.Add(queryExpression2);
				}
				else
				{
					list2 = list2 ?? new List<QueryExpression>(this.filterExprs);
					list2.Add(queryExpression2);
				}
			}
			IEngineHost engineHost = this.engineHost;
			DeltaSource deltaSource = this.source;
			RecordValue recordValue = this.Fields;
			ColumnSelection columnSelection = this.columnSelection;
			IList<QueryExpression> list3 = list;
			IList<QueryExpression> list4 = list3 ?? this.partitionExprs;
			list3 = list2;
			return new DeltaQuery(engineHost, deltaSource, recordValue, columnSelection, list4, list3 ?? this.filterExprs, hashSet);
		}

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x06010ABC RID: 68284 RVA: 0x003966AC File Offset: 0x003948AC
		public override RowCount RowCount
		{
			get
			{
				long? num = new long?(0L);
				if (this.filterExprs.Count == 0 && this.source.UseStatistics)
				{
					Keys keys = Keys.New(this.source.GetMetadata().PartitionColumns);
					foreach (KeyValuePair<AddFile, Value[]> keyValuePair in this.GetFiles(keys))
					{
						Statistics statistics = StatisticsSerializer.Deserialize(this.source.GetSchema(), keyValuePair.Key.StatsString);
						if (statistics == null || statistics.NumRecords == null)
						{
							num = null;
							break;
						}
						num += statistics.NumRecords;
						if (keyValuePair.Key.DeletionVector != null)
						{
							num -= keyValuePair.Key.DeletionVector.Cardinality;
						}
					}
					if (num != null)
					{
						return new RowCount(num.Value);
					}
				}
				return base.RowCount;
			}
		}

		// Token: 0x06010ABD RID: 68285 RVA: 0x0039681C File Offset: 0x00394A1C
		public override Query Group(Grouping grouping)
		{
			Query query;
			if (this.filterExprs.Count == 0 && this.source.UseStatistics && ListStatisticsOperations.TryGetAggregation(this, grouping, new DeltaQuery.AddFileStatisticsEnumerable(this, false), out query))
			{
				return query;
			}
			return base.Group(grouping);
		}

		// Token: 0x06010ABE RID: 68286 RVA: 0x0039685E File Offset: 0x00394A5E
		public override IEnumerable<IValueReference> GetRows()
		{
			ListValue columns = ListValue.New(this.Columns.ToArray<string>());
			Metadata metadata = this.source.GetMetadata();
			string[] partitionColumns = metadata.PartitionColumns;
			Keys partitionKeys = Keys.New(partitionColumns);
			Value partitionTypes = ListValue.New(Enumerable.Repeat<IValueReference>(TypeValue.Any, partitionKeys.Length));
			HashSet<string> columnsToRemove = new HashSet<string>();
			columnsToRemove.AddRange(partitionColumns);
			foreach (string text in this.Fields.Keys)
			{
				ColumnSelection columnSelection = this.columnSelection;
				if (columnSelection != null && !columnSelection.Keys.Contains(text))
				{
					HashSet<string> hashSet = this.columnAccess;
					if (hashSet == null || !hashSet.Contains(text))
					{
						columnsToRemove.Add(text);
					}
				}
			}
			foreach (KeyValuePair<AddFile, Value[]> keyValuePair in this.GetFiles(partitionKeys))
			{
				ITableSegmentWithStatistics tableSegmentWithStatistics;
				if (!this.source.UseStatistics || !this.TryGetStatisticsForFilters(keyValuePair.Key, partitionKeys, keyValuePair.Value, this.filterExprs, out tableSegmentWithStatistics) || !ListStatisticsOperations.CanSkipSegment(tableSegmentWithStatistics, this.filterExprs))
				{
					FunctionValue functionValue = ConstantFunctionValue.Each(ListValue.New(keyValuePair.Value));
					IEnumerable<IValueReference> enumerable = this.source.GetFile(keyValuePair.Key.Path).RenameColumns(DeltaQuery.MakeRenames(metadata, false, new QueryTableValue(this)), MissingFieldMode.Ignore).RemoveColumns(ListValue.New(columnsToRemove.ToArray<string>()), MissingFieldMode.Ignore)
						.AddColumns(ListValue.New(partitionColumns), functionValue, partitionTypes)
						.SelectRows(this.FilterFunction)
						.SelectColumns(columns, MissingFieldMode.UseNull);
					if (keyValuePair.Key.DeletionVector != null)
					{
						enumerable = DeletionVectors.GetFilteredItems<IValueReference>(this.source, enumerable, keyValuePair.Key.DeletionVector);
					}
					foreach (IValueReference valueReference in enumerable)
					{
						yield return valueReference;
					}
					IEnumerator<IValueReference> enumerator3 = null;
				}
			}
			IEnumerator<KeyValuePair<AddFile, Value[]>> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06010ABF RID: 68287 RVA: 0x00396870 File Offset: 0x00394A70
		private bool TryGetStatisticsForFilters(AddFile addFile, Keys partitionKeys, Value[] partitionValues, IList<QueryExpression> filters, out ITableSegmentWithStatistics statistics)
		{
			if (filters.Count > 0)
			{
				DeltaQuery deltaQuery;
				if (this.columnSelection == null)
				{
					deltaQuery = this;
				}
				else
				{
					deltaQuery = new DeltaQuery(this.engineHost, this.source, this.Fields, null, this.partitionExprs, this.filterExprs, null);
				}
				statistics = new DeltaQuery.FileStatistics(deltaQuery, addFile, partitionKeys, partitionValues, true);
				return true;
			}
			statistics = null;
			return false;
		}

		// Token: 0x06010AC0 RID: 68288 RVA: 0x003968D0 File Offset: 0x00394AD0
		public override TableValue GetPartitionTable(int[] columns)
		{
			if (this.IsBaseTable)
			{
				string[] partitionColumns = this.source.GetMetadata().PartitionColumns;
				Keys keys = Keys.New(partitionColumns);
				if (partitionColumns.Length != 0)
				{
					RecordBuilder recordBuilder = new RecordBuilder(partitionColumns.Length);
					for (int i = 0; i < partitionColumns.Length; i++)
					{
						int num;
						if (this.Columns.TryGetKeyIndex(partitionColumns[i], out num))
						{
							RecordValue recordValue = RecordTypeAlgebra.NewField(this.GetColumnType(num), false);
							recordBuilder.Add(partitionColumns[i], recordValue, recordValue.Type);
						}
					}
					RecordValue recordValue2 = recordBuilder.ToRecord();
					if (recordValue2.Count == partitionColumns.Length)
					{
						TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(recordValue2, false));
						return ListValue.New(this.GetPartitionValues(keys)).ToTable(tableTypeValue).Distinct(Value.Null);
					}
				}
			}
			return base.GetPartitionTable(columns);
		}

		// Token: 0x06010AC1 RID: 68289 RVA: 0x003969A0 File Offset: 0x00394BA0
		public static ListValue MakeRenames(Metadata metadata, bool logicalToPhysical, Value errorDetail)
		{
			string text;
			if (!metadata.Configuration.TryGetValue("delta.columnMapping.mode", out text))
			{
				return ListValue.Empty;
			}
			if (!string.Equals(text, "name", StringComparison.OrdinalIgnoreCase))
			{
				throw ValueException.NewDataFormatError<Message1>(Resources.UnsupportedRead(PiiFree.New("delta.columnMapping.mode: " + text)), errorDetail, null);
			}
			List<Value> list = new List<Value>();
			foreach (StructField structField in metadata.Schema.Fields)
			{
				string physicalNameOrNull = structField.PhysicalNameOrNull;
				if (physicalNameOrNull != null)
				{
					new Value[2];
					if (logicalToPhysical)
					{
						list.Add(ListValue.New(new Value[]
						{
							TextValue.New(structField.Name),
							TextValue.New(physicalNameOrNull)
						}));
					}
					else
					{
						list.Add(ListValue.New(new Value[]
						{
							TextValue.New(physicalNameOrNull),
							TextValue.New(structField.Name)
						}));
					}
				}
			}
			return ListValue.New(list);
		}

		// Token: 0x06010AC2 RID: 68290 RVA: 0x00396A8A File Offset: 0x00394C8A
		private IEnumerable<IValueReference> GetPartitionValues(Keys partitionKeys)
		{
			foreach (AddFile addFile in this.source.GetFiles())
			{
				Value[] array2 = new Value[partitionKeys.Length];
				this.IncludeFile(addFile, partitionKeys, array2);
				yield return RecordValue.New(partitionKeys, array2);
			}
			AddFile[] array = null;
			yield break;
		}

		// Token: 0x06010AC3 RID: 68291 RVA: 0x00396AA1 File Offset: 0x00394CA1
		private IEnumerable<KeyValuePair<AddFile, Value[]>> GetFiles(Keys partitionKeys)
		{
			foreach (AddFile addFile in this.source.GetFiles())
			{
				Value[] array2 = ((partitionKeys.Length == 0) ? EmptyArray<Value>.Instance : new Value[partitionKeys.Length]);
				if (this.IncludeFile(addFile, partitionKeys, array2))
				{
					yield return new KeyValuePair<AddFile, Value[]>(addFile, array2);
				}
			}
			AddFile[] array = null;
			yield break;
		}

		// Token: 0x06010AC4 RID: 68292 RVA: 0x00396AB8 File Offset: 0x00394CB8
		private bool IncludeFile(AddFile file, Keys partitionKeys, Value[] partitionValues)
		{
			RecordValue recordValue;
			if (partitionKeys.Length > 0)
			{
				StructType schema = this.source.GetSchema();
				IReadOnlyDictionary<string, object> readOnlyDictionary = PartitionUtils.TranslateValues(schema, file.PartitionValues);
				for (int i = 0; i < partitionKeys.Length; i++)
				{
					string text = partitionKeys[i];
					object obj = readOnlyDictionary[text];
					if (schema.Field(text).DataType == DataTypes.Date)
					{
						obj = new Date((DateTime)obj);
					}
					partitionValues[i] = ValueMarshaller.MarshalFromClr(obj);
				}
				recordValue = RecordValue.New(partitionKeys, partitionValues);
			}
			else
			{
				recordValue = RecordValue.Empty;
			}
			return this.partitionExprs.Count == 0 || this.PartitionFunction.Invoke(recordValue).AsBoolean;
		}

		// Token: 0x06010AC5 RID: 68293 RVA: 0x00396B6C File Offset: 0x00394D6C
		public ActionValue Replace(TableValue rowsToReplace)
		{
			return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.Replace(countOnlyTable, rowsToReplace));
		}

		// Token: 0x06010AC6 RID: 68294 RVA: 0x00396B94 File Offset: 0x00394D94
		private ActionValue Replace(bool countOnlyTable, TableValue rowsToInsert)
		{
			if (countOnlyTable && this.IsBaseTable && !this.source.ReadOnly)
			{
				DeltaTransaction deltaTransaction = this.source.StartTransaction();
				deltaTransaction.DeleteRows((AddFile file) => true);
				long num = deltaTransaction.InsertRows(rowsToInsert, RecordValue.Empty, true);
				return this.Commit(deltaTransaction, num);
			}
			throw ValueException.NewDataSourceError<Message0>(Microsoft.Mashup.Engine1.Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
		}

		// Token: 0x06010AC7 RID: 68295 RVA: 0x00396C14 File Offset: 0x00394E14
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			this.source.CheckWriterFeatures();
			if (this.columnSelection == null && !this.source.ReadOnly)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(countOnlyTable, rowsToInsert));
			}
			return base.InsertRows(rowsToInsert);
		}

		// Token: 0x06010AC8 RID: 68296 RVA: 0x00396C74 File Offset: 0x00394E74
		private ActionValue InsertRows(bool countOnlyTable, Query rowsToInsert)
		{
			if (!countOnlyTable)
			{
				throw ValueException.NewDataSourceError<Message0>(Microsoft.Mashup.Engine1.Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
			}
			RecordValue recordValue;
			if (!this.IsSinglePartition(out recordValue))
			{
				throw ValueException.NewDataFormatError<Message0>(Resources.CantInsertPartitionedTable, Value.Null, null);
			}
			DeltaTransaction deltaTransaction = this.source.StartTransaction();
			long num = deltaTransaction.InsertRows(new QueryTableValue(rowsToInsert), recordValue, false);
			return this.Commit(deltaTransaction, num);
		}

		// Token: 0x06010AC9 RID: 68297 RVA: 0x00396CD4 File Offset: 0x00394ED4
		public override ActionValue DeleteRows()
		{
			this.source.CheckWriterFeatures();
			if (this.IsCompletePartitions && !this.source.ReadOnly)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(countOnlyTable));
			}
			return base.DeleteRows();
		}

		// Token: 0x06010ACA RID: 68298 RVA: 0x00396D10 File Offset: 0x00394F10
		private ActionValue DeleteRows(bool countOnlyTable)
		{
			if (countOnlyTable)
			{
				string[] partitionColumns = this.source.GetMetadata().PartitionColumns;
				Keys partitionKeys = Keys.New(partitionColumns);
				Value[] partitionValues = new Value[partitionKeys.Length];
				Func<AddFile, bool> func = (AddFile file) => this.IncludeFile(file, partitionKeys, partitionValues);
				DeltaTransaction deltaTransaction = this.source.StartTransaction();
				long num = deltaTransaction.DeleteRows(func);
				return this.Commit(deltaTransaction, num);
			}
			throw ValueException.NewDataSourceError<Message0>(Microsoft.Mashup.Engine1.Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
		}

		// Token: 0x06010ACB RID: 68299 RVA: 0x00396DA0 File Offset: 0x00394FA0
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			if (index == 0 && arguments.Length == 1)
			{
				if (function.Equals(Library._Value.Versions) && this.IsBaseTable)
				{
					TableValue tableValue;
					if (new DeltaValueVersions(this.source).TryCreateTable(out tableValue))
					{
						result = tableValue;
						return true;
					}
				}
				else if (function.Equals(Library._Value.VersionIdentity))
				{
					result = TextValue.NewOrNull(this.source.VersionIdentity);
					return true;
				}
			}
			return base.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06010ACC RID: 68300 RVA: 0x00396E14 File Offset: 0x00395014
		public bool IsSinglePartition(out RecordValue partitionKey)
		{
			string[] partitionColumns = this.source.GetMetadata().PartitionColumns;
			if (partitionColumns.Length == 0)
			{
				partitionKey = RecordValue.Empty;
				return true;
			}
			List<QueryExpression> list = new List<QueryExpression>(partitionColumns.Length);
			for (int i = 0; i < this.partitionExprs.Count; i++)
			{
				SelectRowsQuery.AddNormalForm(list, this.partitionExprs[i], BinaryOperator2.And);
			}
			if (list.Count == partitionColumns.Length)
			{
				RecordBuilder recordBuilder = new RecordBuilder(partitionColumns.Length);
				int num = 0;
				int num2;
				BinaryOperator2 binaryOperator;
				Value value;
				while (num < list.Count && list[num].TryGetColumnComparison(out num2, out binaryOperator, out value) && binaryOperator == BinaryOperator2.Equals)
				{
					recordBuilder.Add(this.Fields.Keys[num2], value, value.Type);
					num++;
				}
				RecordValue recordValue = recordBuilder.ToRecord();
				if (recordValue.Count == partitionColumns.Length)
				{
					partitionKey = recordValue;
					return true;
				}
			}
			partitionKey = null;
			return false;
		}

		// Token: 0x06010ACD RID: 68301 RVA: 0x00396EF8 File Offset: 0x003950F8
		private bool AccessesOnlyPartitionColumn(QueryExpression expression, HashSet<string> columnAccess)
		{
			return expression.AllAccess((InvocationQueryExpression i) => true, delegate(int i)
			{
				columnAccess.Add(this.Fields.Keys[i]);
				return this.IsPartitionColumn(i);
			});
		}

		// Token: 0x06010ACE RID: 68302 RVA: 0x00396F4A File Offset: 0x0039514A
		private bool IsPartitionColumn(int i)
		{
			return this.source.GetMetadata().PartitionColumns.Contains(this.Fields.Keys[i]);
		}

		// Token: 0x06010ACF RID: 68303 RVA: 0x00396F74 File Offset: 0x00395174
		private ActionValue Commit(DeltaTransaction transaction, long rowsAffected)
		{
			return ActionValue.New(ListValue.New(new IValueReference[]
			{
				transaction.CommitAction,
				ActionModule.Action.Return.Invoke(NumberValue.New(rowsAffected)),
				new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType)
			}));
		}

		// Token: 0x040063BE RID: 25534
		private readonly IEngineHost engineHost;

		// Token: 0x040063BF RID: 25535
		private readonly DeltaSource source;

		// Token: 0x040063C0 RID: 25536
		private readonly ColumnSelection columnSelection;

		// Token: 0x040063C1 RID: 25537
		private readonly IList<QueryExpression> partitionExprs;

		// Token: 0x040063C2 RID: 25538
		private readonly IList<QueryExpression> filterExprs;

		// Token: 0x040063C3 RID: 25539
		private readonly HashSet<string> columnAccess;

		// Token: 0x040063C4 RID: 25540
		private RecordValue fields;

		// Token: 0x040063C5 RID: 25541
		private FunctionValue partitionFunction;

		// Token: 0x040063C6 RID: 25542
		private FunctionValue filterFunction;

		// Token: 0x02001EE9 RID: 7913
		private sealed class AddFileStatisticsEnumerable : IEnumerable<ITableSegmentWithStatistics>, IEnumerable
		{
			// Token: 0x06010AD1 RID: 68305 RVA: 0x00396FCE File Offset: 0x003951CE
			public AddFileStatisticsEnumerable(DeltaQuery deltaQuery, bool allowLooseBounds)
			{
				this.deltaQuery = deltaQuery;
				this.allowLooseBounds = allowLooseBounds;
			}

			// Token: 0x06010AD2 RID: 68306 RVA: 0x00396FE4 File Offset: 0x003951E4
			public IEnumerator<ITableSegmentWithStatistics> GetEnumerator()
			{
				Keys partitionKeys = Keys.New(this.deltaQuery.source.GetMetadata().PartitionColumns);
				foreach (KeyValuePair<AddFile, Value[]> keyValuePair in this.deltaQuery.GetFiles(partitionKeys))
				{
					yield return new DeltaQuery.FileStatistics(this.deltaQuery, keyValuePair.Key, partitionKeys, keyValuePair.Value, this.allowLooseBounds);
				}
				IEnumerator<KeyValuePair<AddFile, Value[]>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06010AD3 RID: 68307 RVA: 0x00396FF3 File Offset: 0x003951F3
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040063C7 RID: 25543
			private readonly DeltaQuery deltaQuery;

			// Token: 0x040063C8 RID: 25544
			private readonly bool allowLooseBounds;
		}

		// Token: 0x02001EEB RID: 7915
		private sealed class FileStatistics : ITableSegmentWithStatistics
		{
			// Token: 0x06010ADB RID: 68315 RVA: 0x00397164 File Offset: 0x00395364
			public FileStatistics(DeltaQuery deltaQuery, AddFile addFile, Keys partitionKeys, Value[] partitionValues, bool allowLooseBounds)
			{
				this.deltaQuery = deltaQuery;
				this.statistics = StatisticsSerializer.Deserialize(deltaQuery.source.GetSchema(), addFile.StatsString);
				this.partitionKeys = partitionKeys;
				this.partitionValues = partitionValues;
				this.deletionVector = addFile.DeletionVector;
				this.allowLooseBounds = allowLooseBounds;
			}

			// Token: 0x06010ADC RID: 68316 RVA: 0x003971C0 File Offset: 0x003953C0
			public bool TryGetStatistics(int column, out ListStatistics statistics)
			{
				if (this.statistics != null && this.statistics.NumRecords != null)
				{
					DeletionVector deletionVector = this.deletionVector;
					long num = ((deletionVector != null) ? deletionVector.Cardinality : 0L);
					string text = this.deltaQuery.Columns[column];
					int num2;
					if (this.partitionKeys.TryGetKeyIndex(text, out num2))
					{
						statistics = new ListStatistics(this.deltaQuery.GetColumnType(column), this.partitionValues[num2], this.partitionValues[num2], this.statistics.NumRecords.Value - num, this.partitionValues[num2].IsNull ? (this.statistics.NumRecords.Value - num) : 0L);
						return true;
					}
					if (!this.allowLooseBounds)
					{
						bool? tightBounds = this.statistics.TightBounds;
						bool flag = true;
						if (!((tightBounds.GetValueOrDefault() == flag) & (tightBounds != null)) && (this.statistics.TightBounds != null || this.deletionVector != null))
						{
							goto IL_017D;
						}
					}
					object obj;
					object obj2;
					long num3;
					if (this.statistics.MinValues.TryGetValue(text, out obj) && this.statistics.MaxValues.TryGetValue(text, out obj2) && this.statistics.NullCount.TryGetValue(text, out num3))
					{
						statistics = new ListStatistics(this.deltaQuery.GetColumnType(column), ValueMarshaller.MarshalFromClr(obj), ValueMarshaller.MarshalFromClr(obj2), this.statistics.NumRecords.Value, num3);
						return true;
					}
				}
				IL_017D:
				statistics = null;
				return false;
			}

			// Token: 0x040063CE RID: 25550
			private readonly DeltaQuery deltaQuery;

			// Token: 0x040063CF RID: 25551
			private readonly Statistics statistics;

			// Token: 0x040063D0 RID: 25552
			private readonly Keys partitionKeys;

			// Token: 0x040063D1 RID: 25553
			private readonly Value[] partitionValues;

			// Token: 0x040063D2 RID: 25554
			private readonly DeletionVector deletionVector;

			// Token: 0x040063D3 RID: 25555
			private readonly bool allowLooseBounds;
		}
	}
}
