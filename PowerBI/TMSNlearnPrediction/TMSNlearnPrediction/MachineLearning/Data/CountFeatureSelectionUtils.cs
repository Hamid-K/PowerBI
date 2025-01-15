using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200009F RID: 159
	public static class CountFeatureSelectionUtils
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x000120E0 File Offset: 0x000102E0
		public static long[][] Train(IHostEnvironment env, IDataView input, string[] columns, out int[] colSizes)
		{
			CountFeatureSelectionUtils.<>c__DisplayClass6 CS$<>8__locals1 = new CountFeatureSelectionUtils.<>c__DisplayClass6();
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.Check(env, Utils.Size<string>(columns) > 0, "columns");
			ISchema schema = input.Schema;
			int num = columns.Length;
			CS$<>8__locals1.activeInput = new bool[schema.ColumnCount];
			int[] array = new int[num];
			ColumnType[] array2 = new ColumnType[num];
			colSizes = new int[num];
			for (int i = 0; i < num; i++)
			{
				string text = columns[i];
				int num2;
				if (!schema.TryGetColumnIndex(text, ref num2))
				{
					throw Contracts.ExceptUserArg(env, "column", "Source column '{0}' not found", new object[] { text });
				}
				ColumnType columnType = schema.GetColumnType(num2);
				if (columnType.IsVector && !columnType.IsKnownSizeVector)
				{
					throw Contracts.ExceptUserArg(env, "column", "Variable length column '{0}' is not allowed", new object[] { text });
				}
				CS$<>8__locals1.activeInput[num2] = true;
				array[i] = num2;
				array2[i] = columnType;
				colSizes[i] = columnType.VectorSize;
			}
			CountFeatureSelectionUtils.CountAggregator[] array3 = new CountFeatureSelectionUtils.CountAggregator[num];
			CS$<>8__locals1.rowCur = 0L;
			CountFeatureSelectionUtils.<>c__DisplayClass6 CS$<>8__locals2 = CS$<>8__locals1;
			long? rowCount = input.GetRowCount(true);
			CS$<>8__locals2.rowCount = ((rowCount != null) ? ((double)rowCount.GetValueOrDefault()) : double.NaN);
			using (IProgressChannel progressChannel = env.StartProgressChannel("Aggregating counts"))
			{
				using (IRowCursor rowCursor = input.GetRowCursor((int col) => CS$<>8__locals1.activeInput[col], null))
				{
					ProgressHeader progressHeader = new ProgressHeader(new string[] { "rows" });
					progressChannel.SetHeader(progressHeader, delegate(IProgressEntry e)
					{
						e.SetProgress(0, (double)CS$<>8__locals1.rowCur, CS$<>8__locals1.rowCount);
					});
					for (int j = 0; j < num; j++)
					{
						if (array2[j].IsVector)
						{
							array3[j] = CountFeatureSelectionUtils.GetVecAggregator(rowCursor, array2[j], array[j]);
						}
						else
						{
							array3[j] = CountFeatureSelectionUtils.GetOneAggregator(rowCursor, array2[j], array[j]);
						}
					}
					while (rowCursor.MoveNext())
					{
						for (int k = 0; k < num; k++)
						{
							array3[k].ProcessValue();
						}
						CS$<>8__locals1.rowCur += 1L;
					}
					progressChannel.Checkpoint(new double?[]
					{
						new double?((double)CS$<>8__locals1.rowCur)
					});
				}
			}
			return array3.Select((CountFeatureSelectionUtils.CountAggregator a) => a.Count).ToArray<long[]>();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000123C0 File Offset: 0x000105C0
		private static CountFeatureSelectionUtils.CountAggregator GetOneAggregator(IRow row, ColumnType colType, int colSrc)
		{
			Func<IRow, ColumnType, int, CountFeatureSelectionUtils.CountAggregator> func = new Func<IRow, ColumnType, int, CountFeatureSelectionUtils.CountAggregator>(CountFeatureSelectionUtils.GetOneAggregator<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { colType.RawType });
			return (CountFeatureSelectionUtils.CountAggregator)methodInfo.Invoke(null, new object[] { row, colType, colSrc });
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00012421 File Offset: 0x00010621
		private static CountFeatureSelectionUtils.CountAggregator GetOneAggregator<T>(IRow row, ColumnType colType, int colSrc) where T : IEquatable<T>
		{
			return new CountFeatureSelectionUtils.CountAggregator<T>(colType, row.GetGetter<T>(colSrc));
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00012430 File Offset: 0x00010630
		private static CountFeatureSelectionUtils.CountAggregator GetVecAggregator(IRow row, ColumnType colType, int colSrc)
		{
			Func<IRow, ColumnType, int, CountFeatureSelectionUtils.CountAggregator> func = new Func<IRow, ColumnType, int, CountFeatureSelectionUtils.CountAggregator>(CountFeatureSelectionUtils.GetVecAggregator<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { colType.ItemType.RawType });
			return (CountFeatureSelectionUtils.CountAggregator)methodInfo.Invoke(null, new object[] { row, colType, colSrc });
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00012496 File Offset: 0x00010696
		private static CountFeatureSelectionUtils.CountAggregator GetVecAggregator<T>(IRow row, ColumnType colType, int colSrc) where T : IEquatable<T>
		{
			return new CountFeatureSelectionUtils.CountAggregator<T>(colType, row.GetGetter<VBuffer<T>>(colSrc));
		}

		// Token: 0x020000A0 RID: 160
		private abstract class CountAggregator
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060002E6 RID: 742
			public abstract long[] Count { get; }

			// Token: 0x060002E7 RID: 743
			public abstract void ProcessValue();
		}

		// Token: 0x020000A2 RID: 162
		private sealed class CountAggregator<T> : CountFeatureSelectionUtils.CountAggregator, IColumnAggregator<VBuffer<T>> where T : IEquatable<T>
		{
			// Token: 0x060002EB RID: 747 RVA: 0x000124F8 File Offset: 0x000106F8
			public CountAggregator(ColumnType type, ValueGetter<T> getter)
			{
				CountFeatureSelectionUtils.CountAggregator<T>.<>c__DisplayClass9 CS$<>8__locals1 = new CountFeatureSelectionUtils.CountAggregator<T>.<>c__DisplayClass9();
				CS$<>8__locals1.getter = getter;
				base..ctor();
				CS$<>8__locals1.<>4__this = this;
				this._count = new long[1];
				this._buffer = new VBuffer<T>(1, new T[1], null);
				T t = default(T);
				this._fillBuffer = delegate
				{
					CS$<>8__locals1.getter.Invoke(ref t);
					CS$<>8__locals1.<>4__this._buffer.Values[0] = t;
				};
				this._isDefault = Conversions.Instance.GetIsDefaultPredicate<T>(type);
				this._isMissing = Conversions.Instance.GetIsNAPredicate<T>(type);
			}

			// Token: 0x060002EC RID: 748 RVA: 0x000125AC File Offset: 0x000107AC
			public CountAggregator(ColumnType type, ValueGetter<VBuffer<T>> getter)
			{
				CountFeatureSelectionUtils.CountAggregator<T> <>4__this = this;
				int valueCount = type.ValueCount;
				this._count = new long[valueCount];
				this._fillBuffer = delegate
				{
					getter.Invoke(ref <>4__this._buffer);
				};
				this._isDefault = Conversions.Instance.GetIsDefaultPredicate<T>(type.ItemType);
				this._isMissing = Conversions.Instance.GetIsNAPredicate<T>(type.ItemType);
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0001262B File Offset: 0x0001082B
			public override long[] Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x060002EE RID: 750 RVA: 0x00012633 File Offset: 0x00010833
			public override void ProcessValue()
			{
				this._fillBuffer();
				this.ProcessValue(ref this._buffer);
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0001264C File Offset: 0x0001084C
			public void ProcessValue(ref VBuffer<T> value)
			{
				int num = this._count.Length;
				Contracts.Check(value.Length == num);
				foreach (KeyValuePair<int, T> keyValuePair in value.Items(false))
				{
					T value2 = keyValuePair.Value;
					if (!this._isDefault.Invoke(ref value2) && !this._isMissing.Invoke(ref value2))
					{
						this._count[keyValuePair.Key] += 1L;
					}
				}
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x000126F0 File Offset: 0x000108F0
			public void Finish()
			{
			}

			// Token: 0x04000161 RID: 353
			private readonly long[] _count;

			// Token: 0x04000162 RID: 354
			private readonly Action _fillBuffer;

			// Token: 0x04000163 RID: 355
			private readonly RefPredicate<T> _isDefault;

			// Token: 0x04000164 RID: 356
			private readonly RefPredicate<T> _isMissing;

			// Token: 0x04000165 RID: 357
			private VBuffer<T> _buffer;
		}
	}
}
