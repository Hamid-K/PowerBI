using System;
using System.Reflection;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000EF RID: 239
	public static class LambdaFilter
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public static IDataView Create<TSrc>(IHostEnvironment env, string name, IDataView input, string src, ColumnType typeSrc, RefPredicate<TSrc> predicate)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonEmpty(env, name, "name");
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckNonEmpty(env, src, "src");
			Contracts.CheckValue<ColumnType>(env, typeSrc, "typeSrc");
			Contracts.CheckValue<RefPredicate<TSrc>>(env, predicate, "predicate");
			if (typeSrc.RawType != typeof(TSrc))
			{
				throw Contracts.ExceptParam(env, "predicate", "The source column type '{0}' doesn't match the input type of the predicate", new object[] { typeSrc });
			}
			int num;
			if (!input.Schema.TryGetColumnIndex(src, ref num))
			{
				throw Contracts.ExceptParam(env, "src", "The input data doesn't have a column named '{0}'", new object[] { src });
			}
			ColumnType columnType = input.Schema.GetColumnType(num);
			bool flag;
			Delegate @delegate;
			if (columnType.SameSizeAndItemType(typeSrc))
			{
				flag = true;
				@delegate = null;
			}
			else if (!Conversions.Instance.TryGetStandardConversion(columnType, typeSrc, out @delegate, out flag))
			{
				throw Contracts.ExceptParam(env, "predicate", "The type of column '{0}', '{1}', cannot be converted to the input type of the predicate '{2}'", new object[] { src, columnType, typeSrc });
			}
			IDataView dataView;
			if (flag)
			{
				dataView = new LambdaFilter.Impl<TSrc, TSrc>(env, name, input, num, predicate, null);
			}
			else
			{
				Func<IHostEnvironment, string, IDataView, int, RefPredicate<int>, ValueMapper<int, int>, LambdaFilter.Impl<int, int>> func = new Func<IHostEnvironment, string, IDataView, int, RefPredicate<int>, ValueMapper<int, int>, LambdaFilter.Impl<int, int>>(LambdaFilter.CreateImpl<int, int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
				{
					columnType.RawType,
					typeof(TSrc)
				});
				dataView = (IDataView)methodInfo.Invoke(null, new object[] { env, name, input, num, predicate, @delegate });
			}
			return new OpaqueDataView(dataView);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001ABB5 File Offset: 0x00018DB5
		private static LambdaFilter.Impl<T1, T2> CreateImpl<T1, T2>(IHostEnvironment env, string name, IDataView input, int colSrc, RefPredicate<T2> pred, ValueMapper<T1, T2> conv)
		{
			return new LambdaFilter.Impl<T1, T2>(env, name, input, colSrc, pred, conv);
		}

		// Token: 0x020000F0 RID: 240
		private sealed class Impl<T1, T2> : FilterBase
		{
			// Token: 0x060004E1 RID: 1249 RVA: 0x0001ABC4 File Offset: 0x00018DC4
			public Impl(IHostEnvironment env, string name, IDataView input, int colSrc, RefPredicate<T2> pred, ValueMapper<T1, T2> conv = null)
				: base(env, name, input)
			{
				this._colSrc = colSrc;
				this._pred = pred;
				this._conv = conv;
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x0001ABE7 File Offset: 0x00018DE7
			public override void Save(ModelSaveContext ctx)
			{
				throw Contracts.ExceptNotSupp(this._host, "Shouldn't serialize this");
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x0001ABFC File Offset: 0x00018DFC
			protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
			{
				return null;
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0001AC14 File Offset: 0x00018E14
			protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
			{
				bool[] array;
				Func<int, bool> active = this.GetActive(predicate, out array);
				IRowCursor rowCursor = this._input.GetRowCursor(active, rand);
				return new LambdaFilter.Impl<T1, T2>.RowCursor(this, rowCursor, array);
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x0001AC44 File Offset: 0x00018E44
			public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				bool[] array;
				Func<int, bool> active = this.GetActive(predicate, out array);
				IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, active, n, rand);
				IRowCursor[] array2 = new IRowCursor[rowCursorSet.Length];
				for (int i = 0; i < rowCursorSet.Length; i++)
				{
					array2[i] = new LambdaFilter.Impl<T1, T2>.RowCursor(this, rowCursorSet[i], array);
				}
				return array2;
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x0001ACBC File Offset: 0x00018EBC
			private Func<int, bool> GetActive(Func<int, bool> predicate, out bool[] active)
			{
				active = new bool[this._input.Schema.ColumnCount];
				bool[] activeInput = new bool[this._input.Schema.ColumnCount];
				for (int i = 0; i < active.Length; i++)
				{
					activeInput[i] = (active[i] = predicate(i));
				}
				activeInput[this._colSrc] = true;
				return (int col) => activeInput[col];
			}

			// Token: 0x0400024E RID: 590
			private readonly int _colSrc;

			// Token: 0x0400024F RID: 591
			private readonly RefPredicate<T2> _pred;

			// Token: 0x04000250 RID: 592
			private readonly ValueMapper<T1, T2> _conv;

			// Token: 0x020000F1 RID: 241
			private sealed class RowCursor : LinkedRowFilterCursorBase
			{
				// Token: 0x060004E7 RID: 1255 RVA: 0x0001AD78 File Offset: 0x00018F78
				public RowCursor(LambdaFilter.Impl<T1, T2> parent, IRowCursor input, bool[] active)
					: base(parent._host, input, parent.Schema, active)
				{
					this._getSrc = base.Input.GetGetter<T1>(parent._colSrc);
					if (parent._conv == null)
					{
						this._pred = (RefPredicate<T1>)parent._pred;
						return;
					}
					T2 val = default(T2);
					RefPredicate<T2> pred = parent._pred;
					ValueMapper<T1, T2> conv = parent._conv;
					this._pred = delegate(ref T1 src)
					{
						conv.Invoke(ref this._src, ref val);
						return pred.Invoke(ref val);
					};
				}

				// Token: 0x060004E8 RID: 1256 RVA: 0x0001AE0D File Offset: 0x0001900D
				protected override bool Accept()
				{
					this._getSrc.Invoke(ref this._src);
					return this._pred.Invoke(ref this._src);
				}

				// Token: 0x04000251 RID: 593
				private readonly ValueGetter<T1> _getSrc;

				// Token: 0x04000252 RID: 594
				private readonly RefPredicate<T1> _pred;

				// Token: 0x04000253 RID: 595
				private T1 _src;
			}
		}
	}
}
