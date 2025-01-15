using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200124F RID: 4687
	internal static class AccumulableHelper
	{
		// Token: 0x06007B95 RID: 31637 RVA: 0x001AA29C File Offset: 0x001A849C
		public static IAccumulable CreateAccumulable(RecordTypeValue rowType, ref Grouping grouping)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			List<IAccumulable> list = new List<IAccumulable>();
			int num = -1;
			ColumnConstructor[] array = new ColumnConstructor[grouping.Constructors.Length];
			TableTypeValue groupTableType = grouping.GroupTableType;
			for (int i = 0; i < grouping.Constructors.Length; i++)
			{
				ColumnConstructor columnConstructor = grouping.Constructors[i];
				IAccumulable accumulable;
				FunctionValue function;
				int num2;
				if (AggregationToAccumulationRewriter.TryRewrite(rowType, columnConstructor.Function, out accumulable, out function))
				{
					num2 = list.Count;
					keysBuilder.Add(Identifier.New());
					list.Add(accumulable);
				}
				else
				{
					if (num == -1)
					{
						num = list.Count;
						keysBuilder.Add(Identifier.New());
						list.Add(new AccumulableHelper.TableAccumulable(groupTableType));
					}
					num2 = num;
					function = columnConstructor.Function;
				}
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(keysBuilder.ToKeys(), new InvocationQueryExpression(new ConstantQueryExpression(function), new QueryExpression[]
				{
					new ColumnAccessQueryExpression(num2)
				}));
				array[i] = new ColumnConstructor(columnConstructor.Name, functionValue, columnConstructor.Type);
			}
			grouping = new Grouping(grouping.Adjacent, grouping.ResultKeys, grouping.KeyKeys, grouping.KeyColumns, array, grouping.CompareRecords, grouping.Comparer, grouping.GroupTableType);
			return new RecordAccumulable(keysBuilder.ToKeys(), list);
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x001AA3F0 File Offset: 0x001A85F0
		public static RecordValue CreateGroupResult(Grouping grouping, Value key, IValueReference state)
		{
			int[] keyColumns = grouping.KeyColumns;
			ColumnConstructor[] constructors = grouping.Constructors;
			IValueReference[] array = new IValueReference[grouping.ResultKeys.Length];
			if (grouping.CompareRecords)
			{
				for (int i = 0; i < keyColumns.Length; i++)
				{
					array[i] = key[i];
				}
			}
			else
			{
				array[0] = key;
			}
			for (int j = 0; j < constructors.Length; j++)
			{
				array[keyColumns.Length + j] = new TransformValueReference(state, constructors[j].Function);
			}
			return RecordValue.New(grouping.ResultKeys, array);
		}

		// Token: 0x02001250 RID: 4688
		private sealed class TableAccumulable : IAccumulable
		{
			// Token: 0x06007B97 RID: 31639 RVA: 0x001AA477 File Offset: 0x001A8677
			public TableAccumulable(TableTypeValue groupTableType)
			{
				this.groupTableType = groupTableType;
			}

			// Token: 0x06007B98 RID: 31640 RVA: 0x001AA486 File Offset: 0x001A8686
			public IAccumulator CreateAccumulator()
			{
				return new AccumulableHelper.TableAccumulable.TableAccumulator(this.groupTableType);
			}

			// Token: 0x04004485 RID: 17541
			private readonly TableTypeValue groupTableType;

			// Token: 0x02001251 RID: 4689
			private sealed class TableAccumulator : IAccumulator
			{
				// Token: 0x06007B99 RID: 31641 RVA: 0x001AA493 File Offset: 0x001A8693
				public TableAccumulator(TableTypeValue groupTableType)
				{
					this.groupTableType = groupTableType;
					this.group = new List<IValueReference>();
				}

				// Token: 0x170021BB RID: 8635
				// (get) Token: 0x06007B9A RID: 31642 RVA: 0x001AA4AD File Offset: 0x001A86AD
				public IValueReference Current
				{
					get
					{
						this.copyOnAccumulateNext = true;
						return ListValue.New(this.group).ToTable(this.groupTableType);
					}
				}

				// Token: 0x06007B9B RID: 31643 RVA: 0x001AA4CC File Offset: 0x001A86CC
				public void AccumulateNext(IValueReference next)
				{
					if (this.copyOnAccumulateNext)
					{
						this.group = new List<IValueReference>(this.group);
						this.copyOnAccumulateNext = false;
					}
					this.group.Add(next);
				}

				// Token: 0x04004486 RID: 17542
				private readonly TableTypeValue groupTableType;

				// Token: 0x04004487 RID: 17543
				private List<IValueReference> group;

				// Token: 0x04004488 RID: 17544
				private bool copyOnAccumulateNext;
			}
		}
	}
}
