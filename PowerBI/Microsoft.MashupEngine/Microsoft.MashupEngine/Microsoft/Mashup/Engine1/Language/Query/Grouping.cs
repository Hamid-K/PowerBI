using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001834 RID: 6196
	public class Grouping
	{
		// Token: 0x06009CFE RID: 40190 RVA: 0x00207218 File Offset: 0x00205418
		public Grouping(bool adjacent, Keys resultKeys, Keys keyKeys, int[] keyColumns, ColumnConstructor[] constructors, bool compareRecords, FunctionValue comparer, TableTypeValue groupTableType)
		{
			this.adjacent = adjacent;
			this.keys = resultKeys;
			this.keyKeys = keyKeys;
			this.keyColumns = keyColumns;
			this.constructors = constructors;
			this.compareRecords = compareRecords;
			this.comparer = comparer;
			this.groupTableType = groupTableType;
		}

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x06009CFF RID: 40191 RVA: 0x00207268 File Offset: 0x00205468
		public bool Adjacent
		{
			get
			{
				return this.adjacent;
			}
		}

		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x06009D00 RID: 40192 RVA: 0x00207270 File Offset: 0x00205470
		public Keys ResultKeys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x06009D01 RID: 40193 RVA: 0x00207278 File Offset: 0x00205478
		public Keys KeyKeys
		{
			get
			{
				return this.keyKeys;
			}
		}

		// Token: 0x1700287B RID: 10363
		// (get) Token: 0x06009D02 RID: 40194 RVA: 0x00207280 File Offset: 0x00205480
		public int[] KeyColumns
		{
			get
			{
				return this.keyColumns;
			}
		}

		// Token: 0x1700287C RID: 10364
		// (get) Token: 0x06009D03 RID: 40195 RVA: 0x00207288 File Offset: 0x00205488
		public ColumnConstructor[] Constructors
		{
			get
			{
				return this.constructors;
			}
		}

		// Token: 0x1700287D RID: 10365
		// (get) Token: 0x06009D04 RID: 40196 RVA: 0x00207290 File Offset: 0x00205490
		public FunctionValue Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x1700287E RID: 10366
		// (get) Token: 0x06009D05 RID: 40197 RVA: 0x00207298 File Offset: 0x00205498
		public bool CompareRecords
		{
			get
			{
				return this.compareRecords;
			}
		}

		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x06009D06 RID: 40198 RVA: 0x002072A0 File Offset: 0x002054A0
		public TableTypeValue GroupTableType
		{
			get
			{
				return this.groupTableType;
			}
		}

		// Token: 0x06009D07 RID: 40199 RVA: 0x002072A8 File Offset: 0x002054A8
		public Grouping.GroupMap CreateGroupMap(RecordTypeValue rowType)
		{
			return new Grouping.GroupMap(rowType, this);
		}

		// Token: 0x06009D08 RID: 40200 RVA: 0x002072B4 File Offset: 0x002054B4
		public static FunctionValue NewAliasedColumn(string columnName)
		{
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Table.SingleRow), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Table.Distinct), new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(columnName)))));
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}

		// Token: 0x06009D09 RID: 40201 RVA: 0x0020730C File Offset: 0x0020550C
		public static FunctionValue NewCollapsedColumns(int[] columns)
		{
			Value[] array = new Value[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = NumberValue.New(columns[i]);
			}
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library.Record.SelectFieldsByIndex), new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), new ConstantExpressionSyntaxNode(ListValue.New(array))));
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}

		// Token: 0x06009D0A RID: 40202 RVA: 0x00207376 File Offset: 0x00205576
		public static bool TryGetAliasedColumn(RecordTypeValue rowType, FunctionValue function, out int aliasedColumn)
		{
			if (Grouping.TryGetSingleDistinct(QueryExpressionBuilder.ToQueryExpression(rowType, function), TableModule.Table.SingleRow, TableModule.Table.Distinct, out aliasedColumn))
			{
				return true;
			}
			aliasedColumn = -1;
			return false;
		}

		// Token: 0x06009D0B RID: 40203 RVA: 0x00207398 File Offset: 0x00205598
		public static bool TryGetCollapsedColumns(RecordTypeValue rowType, FunctionValue function, out int[] collapsedColumns)
		{
			ColumnAccessQueryExpression[] array;
			if (QueryExpressionBuilder.ToQueryExpression(rowType, function).TryGetListOfColumnAccesses(out array))
			{
				collapsedColumns = new int[array.Length];
				for (int i = 0; i < collapsedColumns.Length; i++)
				{
					collapsedColumns[i] = array[i].Column;
				}
				return true;
			}
			collapsedColumns = null;
			return false;
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x002073E0 File Offset: 0x002055E0
		private static bool TryGetSingleDistinct(QueryExpression expression, FunctionValue single, FunctionValue distinct, out int column)
		{
			IList<QueryExpression> list;
			if (expression.TryGetInvocation(single, 1, out list) && list[0].TryGetInvocation(distinct, 1, out list) && list[0].TryGetColumnAccess(out column))
			{
				return true;
			}
			column = -1;
			return false;
		}

		// Token: 0x0400528E RID: 21134
		private bool adjacent;

		// Token: 0x0400528F RID: 21135
		private Keys keys;

		// Token: 0x04005290 RID: 21136
		private Keys keyKeys;

		// Token: 0x04005291 RID: 21137
		private int[] keyColumns;

		// Token: 0x04005292 RID: 21138
		private ColumnConstructor[] constructors;

		// Token: 0x04005293 RID: 21139
		private bool compareRecords;

		// Token: 0x04005294 RID: 21140
		private FunctionValue comparer;

		// Token: 0x04005295 RID: 21141
		private TableTypeValue groupTableType;

		// Token: 0x02001835 RID: 6197
		public class GroupMap
		{
			// Token: 0x06009D0D RID: 40205 RVA: 0x00207420 File Offset: 0x00205620
			public GroupMap(RecordTypeValue rowType, Grouping grouping)
			{
				this.rowType = rowType;
				this.grouping = grouping;
			}

			// Token: 0x06009D0E RID: 40206 RVA: 0x00207438 File Offset: 0x00205638
			public int MapColumn(int column)
			{
				int num = Array.IndexOf<int>(this.grouping.KeyColumns, column);
				if (num != -1)
				{
					return num;
				}
				for (int i = 0; i < this.grouping.Constructors.Length; i++)
				{
					FunctionValue function = this.grouping.Constructors[i].Function;
					int num2;
					if (Grouping.TryGetAliasedColumn(this.rowType, function, out num2) && num2 == column)
					{
						return this.grouping.KeyColumns.Length + i;
					}
				}
				return -1;
			}

			// Token: 0x04005296 RID: 21142
			private readonly RecordTypeValue rowType;

			// Token: 0x04005297 RID: 21143
			private readonly Grouping grouping;
		}
	}
}
