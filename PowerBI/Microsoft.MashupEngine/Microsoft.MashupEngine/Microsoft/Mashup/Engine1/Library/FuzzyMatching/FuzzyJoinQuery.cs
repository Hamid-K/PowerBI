using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B3E RID: 2878
	internal class FuzzyJoinQuery : DataSourceQuery
	{
		// Token: 0x06004FDF RID: 20447 RVA: 0x0010B81C File Offset: 0x00109A1C
		public FuzzyJoinQuery(IEngineHost host, RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, FuzzyJoinAlgorithm fuzzyJoinAlgorithm, FuzzyJoinOptions fuzzyJoinOptions)
		{
			this.host = host;
			this.leftQuery = leftQuery;
			this.leftKeyColumns = leftKeyColumns;
			this.rightQuery = rightQuery;
			this.rightKeyColumns = rightKeyColumns;
			this.joinKind = joinKind;
			this.take = take;
			this.joinKeys = joinKeys;
			this.joinColumns = joinColumns;
			this.fuzzyJoinAlgorithm = fuzzyJoinAlgorithm;
			this.fuzzyJoinOptions = fuzzyJoinOptions;
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x0010B884 File Offset: 0x00109A84
		public Query LeftQuery
		{
			get
			{
				return this.leftQuery;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x0010B88C File Offset: 0x00109A8C
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeyColumns;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x0010B894 File Offset: 0x00109A94
		public Query RightQuery
		{
			get
			{
				return this.rightQuery;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x0010B89C File Offset: 0x00109A9C
		public int[] RightKeyColumns
		{
			get
			{
				return this.rightKeyColumns;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x0010B8A4 File Offset: 0x00109AA4
		public TableTypeAlgebra.JoinKind JoinKind
		{
			get
			{
				return this.joinKind;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x0010B8AC File Offset: 0x00109AAC
		public RowCount TakeCount
		{
			get
			{
				return this.take;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x0010B8B4 File Offset: 0x00109AB4
		public Keys JoinKeys
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x0010B8BC File Offset: 0x00109ABC
		public JoinColumn[] JoinColumns
		{
			get
			{
				return this.joinColumns;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x0010B8C4 File Offset: 0x00109AC4
		public FuzzyJoinAlgorithm FuzzyJoinAlgorithm
		{
			get
			{
				return this.fuzzyJoinAlgorithm;
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x0010B8CC File Offset: 0x00109ACC
		public FuzzyJoinOptions JoinOptions
		{
			get
			{
				return this.fuzzyJoinOptions;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x0010B8D4 File Offset: 0x00109AD4
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x0010B8DC File Offset: 0x00109ADC
		public override Keys Columns
		{
			get
			{
				KeysBuilder keysBuilder = new KeysBuilder(this.JoinKeys.Length + 1);
				keysBuilder.Union(this.JoinKeys);
				if (this.JoinOptions.SimilarityColumnName != null)
				{
					keysBuilder.Add(this.JoinOptions.SimilarityColumnName);
				}
				return keysBuilder.ToKeys();
			}
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0010B930 File Offset: 0x00109B30
		public override TypeValue GetColumnType(int index)
		{
			if (index >= this.joinKeys.Length)
			{
				return TypeValue.Number;
			}
			JoinColumn joinColumn = this.joinColumns[index];
			TypeValue typeValue = (joinColumn.Left ? this.leftQuery.GetColumnType(joinColumn.LeftColumn) : this.rightQuery.GetColumnType(joinColumn.RightColumn));
			if (JoinQuery.NullableColumn(joinColumn.Left, this.joinKind))
			{
				return typeValue.Nullable;
			}
			return typeValue;
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x0010B9AA File Offset: 0x00109BAA
		public override IList<TableKey> TableKeys
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x0010B9B4 File Offset: 0x00109BB4
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.Join(this.leftQuery.ComputedColumns, this.rightQuery.ComputedColumns, QueryTableValue.NewRowType(this.leftQuery), QueryTableValue.NewRowType(this.rightQuery), this.joinKeys, this.joinColumns);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x00049E54 File Offset: 0x00048054
		public override TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0010BA14 File Offset: 0x00109C14
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			int[] array;
			Query query2;
			int[] array2;
			Keys keys;
			JoinColumn[] array3;
			if (FuzzyJoinQuery.SelectColumns(this.leftQuery, this.rightQuery, this.rightKeyColumns, this.joinKeys, this.joinColumns, this.leftKeyColumns, this.fuzzyJoinOptions, columnSelection, out query, out array, out query2, out array2, out keys, out array3))
			{
				return FuzzyJoinQuery.FuzzyJoin(this.host, this.take, query, array, query2, array2, this.joinKind, keys, array3, this.fuzzyJoinAlgorithm, this.fuzzyJoinOptions);
			}
			return SelectColumnsQuery.New(columnSelection, this);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0010BA94 File Offset: 0x00109C94
		public static bool SelectColumns(Query leftQuery, Query rightQuery, int[] rightKeyColumns, Keys joinKeys, JoinColumn[] joinColumns, int[] leftKeyColumns, FuzzyJoinOptions fuzzyOptions, ColumnSelection columnSelection, out Query newLeftQuery, out int[] newLeftKeyColumns, out Query newRightQuery, out int[] newRightKeyColumns, out Keys newJoinKeys, out JoinColumn[] newJoinColumns)
		{
			ColumnSelection columnSelection2;
			if (fuzzyOptions.SimilarityColumnName != null && columnSelection.Keys.Contains(fuzzyOptions.SimilarityColumnName))
			{
				columnSelection2 = columnSelection.Remove(columnSelection.Keys.IndexOfKey(fuzzyOptions.SimilarityColumnName));
			}
			else
			{
				columnSelection2 = columnSelection;
			}
			return JoinQuery.SelectColumns(leftQuery, rightQuery, rightKeyColumns, joinKeys, joinColumns, leftKeyColumns, columnSelection2, out newLeftQuery, out newLeftKeyColumns, out newRightQuery, out newRightKeyColumns, out newJoinKeys, out newJoinColumns);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0010BAFC File Offset: 0x00109CFC
		public override IEnumerable<IValueReference> GetRows()
		{
			IEnumerable<IValueReference> enumerable = this.fuzzyJoinAlgorithm.Join(this.host, new FuzzyJoinParameters(this.take, this.leftQuery, this.leftKeyColumns, this.rightQuery, this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns, this.fuzzyJoinOptions));
			if (!this.take.IsInfinite)
			{
				enumerable = new SkipTakeEnumerable(enumerable, RowRange.All.Take(this.take));
			}
			return enumerable;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0010BB80 File Offset: 0x00109D80
		public override bool TryGetExpression(out IExpression expression)
		{
			ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
			for (int i = 0; i < this.LeftKeyColumns.Length; i++)
			{
				string text = this.JoinKeys[this.LeftKeyColumns[i]];
				arrayBuilder.Add(TextValue.New(text));
			}
			ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
			for (int j = 0; j < this.RightKeyColumns.Length; j++)
			{
				string text2 = this.JoinKeys[this.RightKeyColumns[j]];
				arrayBuilder2.Add(TextValue.New(text2));
			}
			expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(new FuzzyMatchingModule.Table.FuzzyJoinFunctionValue(Microsoft.Mashup.Engine.Host.EngineHost.Empty)), new IExpression[]
			{
				QueryToExpressionVisitor.ToExpression(this.LeftQuery),
				new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray())),
				QueryToExpressionVisitor.ToExpression(this.RightQuery),
				new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder2.ToArray())),
				new ConstantExpressionSyntaxNode(TableTypeAlgebra.GetValue(this.JoinKind)),
				new ConstantExpressionSyntaxNode(this.JoinOptions.AsRecord())
			});
			return true;
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0010BC94 File Offset: 0x00109E94
		public static Query FuzzyJoin(IEngineHost host, RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, FuzzyJoinAlgorithm fuzzyJoinAlgorithm, FuzzyJoinOptions fuzzyJoinOptions)
		{
			return new FuzzyJoinQuery(host, take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, fuzzyJoinAlgorithm, fuzzyJoinOptions);
		}

		// Token: 0x04002ADA RID: 10970
		private IEngineHost host;

		// Token: 0x04002ADB RID: 10971
		private Query leftQuery;

		// Token: 0x04002ADC RID: 10972
		private int[] leftKeyColumns;

		// Token: 0x04002ADD RID: 10973
		private Query rightQuery;

		// Token: 0x04002ADE RID: 10974
		private int[] rightKeyColumns;

		// Token: 0x04002ADF RID: 10975
		private TableTypeAlgebra.JoinKind joinKind;

		// Token: 0x04002AE0 RID: 10976
		private RowCount take;

		// Token: 0x04002AE1 RID: 10977
		private Keys joinKeys;

		// Token: 0x04002AE2 RID: 10978
		private JoinColumn[] joinColumns;

		// Token: 0x04002AE3 RID: 10979
		private FuzzyJoinAlgorithm fuzzyJoinAlgorithm;

		// Token: 0x04002AE4 RID: 10980
		private FuzzyJoinOptions fuzzyJoinOptions;

		// Token: 0x04002AE5 RID: 10981
		private IList<ComputedColumn> computedColumns;
	}
}
