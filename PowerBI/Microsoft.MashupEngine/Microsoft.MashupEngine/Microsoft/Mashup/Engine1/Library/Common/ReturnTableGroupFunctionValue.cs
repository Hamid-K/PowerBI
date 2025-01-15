using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001120 RID: 4384
	internal sealed class ReturnTableGroupFunctionValue : NativeFunctionValue1
	{
		// Token: 0x060072A9 RID: 29353 RVA: 0x00189FCF File Offset: 0x001881CF
		public ReturnTableGroupFunctionValue(Value aggregatedColumns)
			: base("result")
		{
			this.aggregatedColumns = aggregatedColumns;
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x060072AA RID: 29354 RVA: 0x00189FE4 File Offset: 0x001881E4
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Group), new IExpression[]
					{
						new InclusiveIdentifierExpressionSyntaxNode(identifier),
						new ConstantExpressionSyntaxNode(ListValue.Empty),
						new ConstantExpressionSyntaxNode(this.aggregatedColumns)
					})));
				}
				return this.expression;
			}
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x0018A06C File Offset: 0x0018826C
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.Return.Invoke(TableModule.Table.Group.Invoke(result, ListValue.Empty, this.aggregatedColumns));
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x0018A08E File Offset: 0x0018828E
		public IAccumulator CreateAccumulator(TableTypeValue tableType)
		{
			return new ReturnTableGroupFunctionValue.Accumulator(tableType, this.aggregatedColumns);
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x0018A09C File Offset: 0x0018829C
		public Value CreateResult(IAccumulator accumulator)
		{
			return ((ReturnTableGroupFunctionValue.Accumulator)accumulator).CreateResult();
		}

		// Token: 0x04003F31 RID: 16177
		private readonly Value aggregatedColumns;

		// Token: 0x04003F32 RID: 16178
		private IExpression expression;

		// Token: 0x02001121 RID: 4385
		private sealed class Accumulator : IAccumulator
		{
			// Token: 0x060072AE RID: 29358 RVA: 0x0018A0AC File Offset: 0x001882AC
			public Accumulator(TableTypeValue tableType, Value aggregatedColumns)
			{
				ColumnConstructor[] columnConstructors = TableValue.GetColumnConstructors(aggregatedColumns.AsList);
				KeysBuilder keysBuilder = new KeysBuilder(columnConstructors.Length);
				for (int i = 0; i < columnConstructors.Length; i++)
				{
					keysBuilder.Add(columnConstructors[i].Name);
				}
				this.grouping = new Grouping(false, keysBuilder.ToKeys(), Keys.Empty, EmptyArray<int>.Instance, columnConstructors, true, null, tableType);
				this.accumulator = AccumulableHelper.CreateAccumulable(tableType.ItemType, ref this.grouping).CreateAccumulator();
				this.tableType = tableType;
				this.aggregatedColumns = aggregatedColumns;
			}

			// Token: 0x1700201A RID: 8218
			// (get) Token: 0x060072AF RID: 29359 RVA: 0x0018A13D File Offset: 0x0018833D
			public IValueReference Current
			{
				get
				{
					return this.accumulator.Current;
				}
			}

			// Token: 0x060072B0 RID: 29360 RVA: 0x0018A14A File Offset: 0x0018834A
			public void AccumulateNext(IValueReference next)
			{
				this.accumulator.AccumulateNext(next);
				this.rowCount++;
			}

			// Token: 0x060072B1 RID: 29361 RVA: 0x0018A168 File Offset: 0x00188368
			public Value CreateResult()
			{
				ListValue listValue = ((this.rowCount == 0) ? ListValue.Empty : ListValue.New(new Value[] { AccumulableHelper.CreateGroupResult(this.grouping, Value.Null, this.accumulator.Current) }));
				TableValue tableValue = new ReturnTableGroupFunctionValue.TableGroupResultValue(this.tableType, this.aggregatedColumns, TableTypeValue.New(this.grouping.ResultKeys, null), listValue);
				return new TableFromGroupFunctionValue(this.aggregatedColumns).Invoke(tableValue);
			}

			// Token: 0x04003F33 RID: 16179
			private readonly TableTypeValue tableType;

			// Token: 0x04003F34 RID: 16180
			private readonly Value aggregatedColumns;

			// Token: 0x04003F35 RID: 16181
			private readonly IAccumulator accumulator;

			// Token: 0x04003F36 RID: 16182
			private readonly Grouping grouping;

			// Token: 0x04003F37 RID: 16183
			private int rowCount;
		}

		// Token: 0x02001122 RID: 4386
		private sealed class TableGroupResultValue : TableValue
		{
			// Token: 0x060072B2 RID: 29362 RVA: 0x0018A1E3 File Offset: 0x001883E3
			public TableGroupResultValue(TableTypeValue baseTableType, Value aggregatedColumns, TableTypeValue groupTableType, ListValue aggregatedRows)
			{
				this.baseTableType = baseTableType;
				this.aggregatedColumns = aggregatedColumns;
				this.groupTableType = groupTableType;
				this.aggregatedRows = aggregatedRows;
			}

			// Token: 0x1700201B RID: 8219
			// (get) Token: 0x060072B3 RID: 29363 RVA: 0x0018A208 File Offset: 0x00188408
			public override TypeValue Type
			{
				get
				{
					return this.groupTableType;
				}
			}

			// Token: 0x1700201C RID: 8220
			// (get) Token: 0x060072B4 RID: 29364 RVA: 0x0018A210 File Offset: 0x00188410
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Group), new IExpression[]
						{
							new ConstantExpressionSyntaxNode(new CountAndTypeTableValue(this.aggregatedRows.LargeCount, this.baseTableType)),
							new ConstantExpressionSyntaxNode(ListValue.Empty),
							new ConstantExpressionSyntaxNode(this.aggregatedColumns)
						});
					}
					return this.expression;
				}
			}

			// Token: 0x060072B5 RID: 29365 RVA: 0x0018A27F File Offset: 0x0018847F
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.aggregatedRows.GetEnumerator();
			}

			// Token: 0x04003F38 RID: 16184
			private readonly TableTypeValue baseTableType;

			// Token: 0x04003F39 RID: 16185
			private readonly Value aggregatedColumns;

			// Token: 0x04003F3A RID: 16186
			private readonly TableTypeValue groupTableType;

			// Token: 0x04003F3B RID: 16187
			private readonly ListValue aggregatedRows;

			// Token: 0x04003F3C RID: 16188
			private IExpression expression;
		}
	}
}
