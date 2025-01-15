using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B55 RID: 2901
	internal class FuzzyNestedJoinQuery : DataSourceQuery
	{
		// Token: 0x0600503F RID: 20543 RVA: 0x0010CD50 File Offset: 0x0010AF50
		public FuzzyNestedJoinQuery(IEngineHost host, Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, TypeValue columnType, FuzzyJoinOptions fuzzyJoinOptions)
		{
			this.host = host;
			this.leftQuery = leftQuery;
			this.leftKeyColumns = leftKeyColumns;
			this.rightTable = rightTable;
			this.rightKey = rightKey;
			this.joinKind = joinKind;
			this.newColumnName = newColumnName;
			this.joinKeys = joinKeys;
			this.columnType = columnType;
			this.fuzzyJoinOptions = fuzzyJoinOptions;
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x0010CDB0 File Offset: 0x0010AFB0
		public Query LeftQuery
		{
			get
			{
				return this.leftQuery;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x0010CDB8 File Offset: 0x0010AFB8
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeyColumns;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x0010CDC0 File Offset: 0x0010AFC0
		public Value DelayedRightTable
		{
			get
			{
				return this.rightTable;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x0010CDC8 File Offset: 0x0010AFC8
		public Keys RightKey
		{
			get
			{
				return this.rightKey;
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x0010CDD0 File Offset: 0x0010AFD0
		public TableTypeAlgebra.JoinKind JoinKind
		{
			get
			{
				return this.joinKind;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06005045 RID: 20549 RVA: 0x0010CDD8 File Offset: 0x0010AFD8
		public Keys JoinKeys
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x0010CDE0 File Offset: 0x0010AFE0
		public string NewColumnName
		{
			get
			{
				return this.newColumnName;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06005047 RID: 20551 RVA: 0x0010CDE8 File Offset: 0x0010AFE8
		public TypeValue ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x0010CDF0 File Offset: 0x0010AFF0
		public FuzzyJoinOptions JoinOptions
		{
			get
			{
				return this.fuzzyJoinOptions;
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06005049 RID: 20553 RVA: 0x0010CDF8 File Offset: 0x0010AFF8
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x0010CE00 File Offset: 0x0010B000
		public TableValue RightTable
		{
			get
			{
				if (this.rightTable.IsFunction)
				{
					this.rightTable = this.rightTable.AsFunction.Invoke();
				}
				return this.rightTable.AsTable;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x0010CE30 File Offset: 0x0010B030
		public override Keys Columns
		{
			get
			{
				return this.JoinKeys;
			}
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0010CE38 File Offset: 0x0010B038
		public override TypeValue GetColumnType(int index)
		{
			if (index < this.leftQuery.Columns.Length)
			{
				return this.leftQuery.GetColumnType(index);
			}
			if (this.columnType == null)
			{
				this.columnType = TypeServices.ConvertToLimitedPreview(new FuzzyNestedJoinQuery.FuzzyNestedJoinTableType(this)).AsType;
			}
			return this.columnType;
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x0600504D RID: 20557 RVA: 0x0010CE8C File Offset: 0x0010B08C
		public override IList<TableKey> TableKeys
		{
			get
			{
				TableTypeAlgebra.JoinKind joinKind = this.joinKind;
				if (joinKind <= TableTypeAlgebra.JoinKind.LeftOuter)
				{
					return this.leftQuery.TableKeys;
				}
				return Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x0010CEB5 File Offset: 0x0010B0B5
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.leftQuery.ComputedColumns;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x0600504F RID: 20559 RVA: 0x0010CEC2 File Offset: 0x0010B0C2
		public override RowCount RowCount
		{
			get
			{
				if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter)
				{
					return this.leftQuery.RowCount;
				}
				return base.RowCount;
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06005050 RID: 20560 RVA: 0x0010CEDF File Offset: 0x0010B0DF
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.leftQuery.SortOrder;
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0010CEEC File Offset: 0x0010B0EC
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			if (this.TrySelectColumns(new FuzzyNestedJoinQuery.CreateFuzzyNestedJoin(FuzzyNestedJoinQuery.DefaultCreateFuzzyNestedJoin), columnSelection, out query))
			{
				return query;
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0010CF1C File Offset: 0x0010B11C
		public override Query Unordered()
		{
			Query query;
			if (this.TryUnordered(new FuzzyNestedJoinQuery.CreateFuzzyNestedJoin(FuzzyNestedJoinQuery.DefaultCreateFuzzyNestedJoin), out query))
			{
				return query;
			}
			return this;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0010CF44 File Offset: 0x0010B144
		public override IEnumerable<IValueReference> GetRows()
		{
			FuzzyNestedJoinParameters fuzzyNestedJoinParameters = new FuzzyNestedJoinParameters(this.leftQuery, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.fuzzyJoinOptions);
			return FuzzyNestedJoinAlgorithm.GetFuzzyNestedJoinAlgorithm(this.host, this.joinKind).NestedJoin(fuzzyNestedJoinParameters);
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x000BF254 File Offset: 0x000BD454
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			result = null;
			return false;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x0010CF9E File Offset: 0x0010B19E
		public int SelectJoinColumn(ColumnSelection columnSelection)
		{
			return columnSelection.CreateSelectMap(this.Columns).MapColumn(this.LeftQuery.Columns.Length);
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x0010CFC4 File Offset: 0x0010B1C4
		public override bool TryGetExpression(out IExpression expression)
		{
			ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
			for (int i = 0; i < this.LeftKeyColumns.Length; i++)
			{
				string text = this.JoinKeys[this.LeftKeyColumns[i]];
				arrayBuilder.Add(TextValue.New(text));
			}
			ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
			for (int j = 0; j < this.RightKey.Length; j++)
			{
				string text2 = this.RightKey[j];
				arrayBuilder2.Add(TextValue.New(text2));
			}
			expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(new FuzzyMatchingModule.Table.FuzzyNestedJoinFunctionValue(Microsoft.Mashup.Engine.Host.EngineHost.Empty)), new IExpression[]
			{
				QueryToExpressionVisitor.ToExpression(this.LeftQuery),
				new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray())),
				new ConstantExpressionSyntaxNode(this.RightTable),
				new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder2.ToArray())),
				new ConstantExpressionSyntaxNode(TextValue.New(this.NewColumnName)),
				new ConstantExpressionSyntaxNode(TableTypeAlgebra.GetValue(this.JoinKind)),
				new ConstantExpressionSyntaxNode(this.JoinOptions.AsRecord())
			});
			return true;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x0010D0E8 File Offset: 0x0010B2E8
		private bool TrySelectColumns(FuzzyNestedJoinQuery.CreateFuzzyNestedJoin createFuzzyNestedJoin, ColumnSelection columnSelection, out Query query)
		{
			int num = this.SelectJoinColumn(columnSelection);
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter && num == -1)
			{
				query = this.leftQuery.SelectColumns(columnSelection);
				return true;
			}
			ColumnSelection columnSelection2;
			ColumnSelectionBuilder columnSelectionBuilder;
			int[] array;
			string text;
			KeysBuilder keysBuilder;
			if (NestedJoinQuery.TrySelectColumns(num, this.leftQuery, this.leftKeyColumns, this.joinKind, this.joinKeys, this.newColumnName, columnSelection, out columnSelection2, out columnSelectionBuilder, out array, out text, out keysBuilder))
			{
				query = createFuzzyNestedJoin(this.host, this.leftQuery.SelectColumns(columnSelection2), array, this.rightTable, this.rightKey, this.joinKind, text, keysBuilder.ToKeys(), this.fuzzyJoinOptions);
				query = FloatingSelectColumnsQuery.New(columnSelectionBuilder.ToColumnSelection(), query);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0010D19C File Offset: 0x0010B39C
		private bool TryUnordered(FuzzyNestedJoinQuery.CreateFuzzyNestedJoin createFuzzyNestedJoin, out Query query)
		{
			query = this.LeftQuery.Unordered();
			query = createFuzzyNestedJoin(this.host, query, this.LeftKeyColumns, this.DelayedRightTable, this.RightKey, this.JoinKind, this.NewColumnName, this.Columns, this.fuzzyJoinOptions);
			return true;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0010D1F4 File Offset: 0x0010B3F4
		public static Query FuzzyNestedJoin(IEngineHost host, Query innerQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers, FuzzyJoinOptions fuzzyJoinOptions)
		{
			return new FuzzyNestedJoinQuery(host, innerQuery, leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, null, fuzzyJoinOptions);
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0010D218 File Offset: 0x0010B418
		private static Query DefaultCreateFuzzyNestedJoin(IEngineHost host, Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FuzzyJoinOptions fuzzyJoinOptions)
		{
			return FuzzyNestedJoinQuery.FuzzyNestedJoin(host, leftQuery, leftKeyColumns, rightTable, rightKey, joinKind, newColumnName, joinKeys, null, fuzzyJoinOptions);
		}

		// Token: 0x04002B0F RID: 11023
		private readonly IEngineHost host;

		// Token: 0x04002B10 RID: 11024
		private readonly Query leftQuery;

		// Token: 0x04002B11 RID: 11025
		private readonly int[] leftKeyColumns;

		// Token: 0x04002B12 RID: 11026
		private Value rightTable;

		// Token: 0x04002B13 RID: 11027
		private readonly Keys rightKey;

		// Token: 0x04002B14 RID: 11028
		private readonly TableTypeAlgebra.JoinKind joinKind;

		// Token: 0x04002B15 RID: 11029
		private readonly Keys joinKeys;

		// Token: 0x04002B16 RID: 11030
		private readonly string newColumnName;

		// Token: 0x04002B17 RID: 11031
		private TypeValue columnType;

		// Token: 0x04002B18 RID: 11032
		private readonly FuzzyJoinOptions fuzzyJoinOptions;

		// Token: 0x02000B56 RID: 2902
		// (Invoke) Token: 0x0600505C RID: 20572
		private delegate Query CreateFuzzyNestedJoin(IEngineHost host, Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FuzzyJoinOptions fuzzyJoinOptions);

		// Token: 0x02000B57 RID: 2903
		private sealed class FuzzyNestedJoinTableType : TableTypeValue
		{
			// Token: 0x0600505F RID: 20575 RVA: 0x0010D239 File Offset: 0x0010B439
			public FuzzyNestedJoinTableType(FuzzyNestedJoinQuery query)
			{
				this.query = query;
				this.tableType = null;
			}

			// Token: 0x17001908 RID: 6408
			// (get) Token: 0x06005060 RID: 20576 RVA: 0x0010D24F File Offset: 0x0010B44F
			public override RecordTypeValue ItemType
			{
				get
				{
					return this.TableType.ItemType;
				}
			}

			// Token: 0x17001909 RID: 6409
			// (get) Token: 0x06005061 RID: 20577 RVA: 0x0010D25C File Offset: 0x0010B45C
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.TableType.TableKeys;
				}
			}

			// Token: 0x1700190A RID: 6410
			// (get) Token: 0x06005062 RID: 20578 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700190B RID: 6411
			// (get) Token: 0x06005063 RID: 20579 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700190C RID: 6412
			// (get) Token: 0x06005064 RID: 20580 RVA: 0x0010D269 File Offset: 0x0010B469
			public override TypeValue NonNullable
			{
				get
				{
					return this.TableType.NonNullable;
				}
			}

			// Token: 0x1700190D RID: 6413
			// (get) Token: 0x06005065 RID: 20581 RVA: 0x0010D278 File Offset: 0x0010B478
			private TableTypeValue TableType
			{
				get
				{
					if (this.tableType == null)
					{
						RecordTypeValue asRecordType = NavigationTableServices.ConvertToLink(new FuzzyNestedJoinQuery.FuzzyNestedJoinRecordType(this.query)).AsRecordType;
						this.tableType = TableTypeValue.New(asRecordType).AsTableType;
					}
					return this.tableType;
				}
			}

			// Token: 0x04002B19 RID: 11033
			private readonly FuzzyNestedJoinQuery query;

			// Token: 0x04002B1A RID: 11034
			private TableTypeValue tableType;
		}

		// Token: 0x02000B58 RID: 2904
		private sealed class FuzzyNestedJoinRecordType : RecordTypeValue
		{
			// Token: 0x06005066 RID: 20582 RVA: 0x0010D2BA File Offset: 0x0010B4BA
			public FuzzyNestedJoinRecordType(FuzzyNestedJoinQuery query)
			{
				this.query = query;
				this.recordType = null;
			}

			// Token: 0x1700190E RID: 6414
			// (get) Token: 0x06005067 RID: 20583 RVA: 0x0010D2D0 File Offset: 0x0010B4D0
			public override RecordValue Fields
			{
				get
				{
					return this.RecordType.Fields;
				}
			}

			// Token: 0x1700190F RID: 6415
			// (get) Token: 0x06005068 RID: 20584 RVA: 0x00002105 File Offset: 0x00000305
			public override bool Open
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001910 RID: 6416
			// (get) Token: 0x06005069 RID: 20585 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001911 RID: 6417
			// (get) Token: 0x0600506A RID: 20586 RVA: 0x0010D2DD File Offset: 0x0010B4DD
			public override TypeValue Nullable
			{
				get
				{
					return this.RecordType.Nullable;
				}
			}

			// Token: 0x17001912 RID: 6418
			// (get) Token: 0x0600506B RID: 20587 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001913 RID: 6419
			// (get) Token: 0x0600506C RID: 20588 RVA: 0x0010D2EC File Offset: 0x0010B4EC
			private RecordTypeValue RecordType
			{
				get
				{
					if (this.recordType == null)
					{
						KeysBuilder keysBuilder = new KeysBuilder(this.query.RightTable.Columns.Length + 1);
						keysBuilder.Union(this.query.RightTable.Columns);
						List<Value> list = new List<Value>();
						for (int i = 0; i < this.query.RightTable.Columns.Length; i++)
						{
							list.Add(RecordTypeValue.NewField(this.query.RightTable.GetColumnType(i), null));
						}
						if (this.query.fuzzyJoinOptions.SimilarityColumnName != null)
						{
							keysBuilder.Add(this.query.fuzzyJoinOptions.SimilarityColumnName);
							list.Add(RecordTypeValue.NewField(TypeValue.Number, null));
						}
						this.recordType = RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), list.ToArray()));
					}
					return this.recordType;
				}
			}

			// Token: 0x04002B1B RID: 11035
			private readonly FuzzyNestedJoinQuery query;

			// Token: 0x04002B1C RID: 11036
			private RecordTypeValue recordType;
		}
	}
}
