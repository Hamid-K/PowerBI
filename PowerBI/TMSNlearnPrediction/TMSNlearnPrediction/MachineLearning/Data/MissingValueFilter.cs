using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003C0 RID: 960
	public sealed class MissingValueFilter : FilterBase
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x00077043 File Offset: 0x00075243
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("MISFETFL", 65537U, 65537U, 65537U, "MissingValueFilter", "MissingFeatureFilter");
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00077068 File Offset: 0x00075268
		public MissingValueFilter(MissingValueFilter.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "MissingValueFilter", input)
		{
			Contracts.CheckValue<MissingValueFilter.Arguments>(this._host, args, "args");
			Contracts.CheckValue<IDataView>(this._host, input, "input");
			Contracts.CheckUserArg(this._host, Utils.Size<string>(args.column) > 0, "columns");
			Contracts.CheckValue<IHostEnvironment>(this._host, env, "env");
			this._infos = new MissingValueFilter.ColInfo[args.column.Length];
			this._srcIndexToInfoIndex = new Dictionary<int, int>(this._infos.Length);
			this._complement = args.complement;
			ISchema schema = this._input.Schema;
			for (int i = 0; i < this._infos.Length; i++)
			{
				string text = args.column[i];
				int num;
				if (!schema.TryGetColumnIndex(text, ref num))
				{
					throw Contracts.ExceptUserArg(this._host, "column", "Source column '{0}' not found", new object[] { text });
				}
				if (this._srcIndexToInfoIndex.ContainsKey(num))
				{
					throw Contracts.ExceptUserArg(this._host, "column", "Source column '{0}' specified multiple times", new object[] { text });
				}
				ColumnType columnType = schema.GetColumnType(num);
				if (!MissingValueFilter.TestType(columnType))
				{
					throw Contracts.ExceptUserArg(this._host, "column", "Column '{0}' does not have compatible numeric type", new object[] { text });
				}
				this._infos[i] = new MissingValueFilter.ColInfo(num, columnType);
				this._srcIndexToInfoIndex.Add(num, i);
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000771EC File Offset: 0x000753EC
		public MissingValueFilter(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel(MissingValueFilter.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4 || num == 8);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num2 > 0);
			this._infos = new MissingValueFilter.ColInfo[num2];
			this._srcIndexToInfoIndex = new Dictionary<int, int>(this._infos.Length);
			ISchema schema = this._input.Schema;
			for (int i = 0; i < num2; i++)
			{
				string text = ctx.LoadNonEmptyString();
				int num3;
				if (!schema.TryGetColumnIndex(text, ref num3))
				{
					throw Contracts.Except(this._host, "Source column '{0}' not found", new object[] { text });
				}
				if (this._srcIndexToInfoIndex.ContainsKey(num3))
				{
					throw Contracts.Except(this._host, "Source column '{0}' specified multiple times", new object[] { text });
				}
				ColumnType columnType = schema.GetColumnType(num3);
				if (!MissingValueFilter.TestType(columnType))
				{
					throw Contracts.Except(this._host, "Column '{0}' does not have compatible numeric type", new object[] { text });
				}
				this._infos[i] = new MissingValueFilter.ColInfo(num3, columnType);
				this._srcIndexToInfoIndex.Add(num3, i);
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00077370 File Offset: 0x00075570
		public static MissingValueFilter Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("MissingValueFilter");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(MissingValueFilter.GetVersionInfo());
			return HostExtensions.Apply<MissingValueFilter>(h, "Loading Model", (IChannel ch) => new MissingValueFilter(ctx, h, input));
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00077408 File Offset: 0x00075608
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(MissingValueFilter.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._infos.Length);
			foreach (MissingValueFilter.ColInfo colInfo in this._infos)
			{
				ctx.SaveNonEmptyString(this._input.Schema.GetColumnName(colInfo.Index));
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0007748C File Offset: 0x0007568C
		private static bool TestType(ColumnType type)
		{
			ColumnType itemType = type.ItemType;
			if (itemType.IsNumber)
			{
				switch (itemType.RawKind)
				{
				case 1:
				case 3:
				case 5:
				case 7:
				case 9:
				case 10:
					return true;
				}
				return false;
			}
			return itemType.IsText || itemType.IsBool || itemType.IsKey || itemType.IsTimeSpan || itemType.IsDateTime || itemType.IsDateTimeZone;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00077524 File Offset: 0x00075724
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0007753C File Offset: 0x0007573C
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			bool[] array;
			Func<int, bool> active = this.GetActive(predicate, out array);
			IRowCursor rowCursor = this._input.GetRowCursor(active, rand);
			return new MissingValueFilter.RowCursor(this, rowCursor, array);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0007756C File Offset: 0x0007576C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			bool[] array;
			Func<int, bool> active = this.GetActive(predicate, out array);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, active, n, rand);
			IRowCursor[] array2 = new IRowCursor[rowCursorSet.Length];
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				array2[i] = new MissingValueFilter.RowCursor(this, rowCursorSet[i], array);
			}
			return array2;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000775E4 File Offset: 0x000757E4
		private Func<int, bool> GetActive(Func<int, bool> predicate, out bool[] active)
		{
			active = new bool[this._input.Schema.ColumnCount];
			bool[] activeInput = new bool[this._input.Schema.ColumnCount];
			for (int i = 0; i < active.Length; i++)
			{
				activeInput[i] = (active[i] = predicate(i));
			}
			for (int j = 0; j < this._infos.Length; j++)
			{
				activeInput[this._infos[j].Index] = true;
			}
			return (int col) => activeInput[col];
		}

		// Token: 0x04000C56 RID: 3158
		internal const string Summary = "Filters out rows that contain missing values.";

		// Token: 0x04000C57 RID: 3159
		public const string LoaderSignature = "MissingValueFilter";

		// Token: 0x04000C58 RID: 3160
		private const string RegistrationName = "MissingValueFilter";

		// Token: 0x04000C59 RID: 3161
		private readonly MissingValueFilter.ColInfo[] _infos;

		// Token: 0x04000C5A RID: 3162
		private readonly Dictionary<int, int> _srcIndexToInfoIndex;

		// Token: 0x04000C5B RID: 3163
		private readonly bool _complement;

		// Token: 0x020003C1 RID: 961
		public sealed class Arguments
		{
			// Token: 0x04000C5C RID: 3164
			[Argument(4, HelpText = "Column", ShortName = "col", SortOrder = 1)]
			public string[] column;

			// Token: 0x04000C5D RID: 3165
			[Argument(4, HelpText = "If true, keep only rows that contain NA values, and filter the rest.")]
			public bool complement;
		}

		// Token: 0x020003C2 RID: 962
		private sealed class ColInfo
		{
			// Token: 0x06001491 RID: 5265 RVA: 0x0007768A File Offset: 0x0007588A
			public ColInfo(int index, ColumnType type)
			{
				this.Index = index;
				this.Type = type;
			}

			// Token: 0x04000C5E RID: 3166
			public readonly int Index;

			// Token: 0x04000C5F RID: 3167
			public readonly ColumnType Type;
		}

		// Token: 0x020003C3 RID: 963
		private sealed class RowCursor : LinkedRowFilterCursorBase
		{
			// Token: 0x06001492 RID: 5266 RVA: 0x000776A0 File Offset: 0x000758A0
			public RowCursor(MissingValueFilter parent, IRowCursor input, bool[] active)
				: base(parent._host, input, parent.Schema, active)
			{
				this._parent = parent;
				this._values = new MissingValueFilter.RowCursor.Value[this._parent._infos.Length];
				for (int i = 0; i < this._parent._infos.Length; i++)
				{
					this._values[i] = MissingValueFilter.RowCursor.Value.Create(this, this._parent._infos[i]);
				}
			}

			// Token: 0x06001493 RID: 5267 RVA: 0x00077714 File Offset: 0x00075914
			public override ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, base.IsColumnActive(col));
				ValueGetter<TValue> valueGetter;
				if (this.TryGetColumnValueGetter<TValue>(col, out valueGetter))
				{
					return valueGetter;
				}
				return base.Input.GetGetter<TValue>(col);
			}

			// Token: 0x06001494 RID: 5268 RVA: 0x0007774C File Offset: 0x0007594C
			private bool TryGetColumnValueGetter<TValue>(int col, out ValueGetter<TValue> fn)
			{
				int num;
				if (!this._parent._srcIndexToInfoIndex.TryGetValue(col, out num))
				{
					fn = null;
					return false;
				}
				fn = this._values[num].GetGetter() as ValueGetter<TValue>;
				if (fn == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return true;
			}

			// Token: 0x06001495 RID: 5269 RVA: 0x000777B4 File Offset: 0x000759B4
			protected override bool Accept()
			{
				for (int i = 0; i < this._parent._infos.Length; i++)
				{
					if (!this._values[i].Refresh())
					{
						return this._parent._complement;
					}
				}
				return !this._parent._complement;
			}

			// Token: 0x04000C60 RID: 3168
			private readonly MissingValueFilter _parent;

			// Token: 0x04000C61 RID: 3169
			private readonly MissingValueFilter.RowCursor.Value[] _values;

			// Token: 0x020003C4 RID: 964
			private abstract class Value
			{
				// Token: 0x06001496 RID: 5270 RVA: 0x00077802 File Offset: 0x00075A02
				protected Value(MissingValueFilter.RowCursor cursor)
				{
					this._cursor = cursor;
				}

				// Token: 0x06001497 RID: 5271
				public abstract bool Refresh();

				// Token: 0x06001498 RID: 5272
				public abstract Delegate GetGetter();

				// Token: 0x06001499 RID: 5273 RVA: 0x00077814 File Offset: 0x00075A14
				public static MissingValueFilter.RowCursor.Value Create(MissingValueFilter.RowCursor cursor, MissingValueFilter.ColInfo info)
				{
					MethodInfo methodInfo;
					if (!info.Type.IsVector)
					{
						Func<MissingValueFilter.RowCursor, MissingValueFilter.ColInfo, MissingValueFilter.RowCursor.Value.ValueOne<int>> func = new Func<MissingValueFilter.RowCursor, MissingValueFilter.ColInfo, MissingValueFilter.RowCursor.Value.ValueOne<int>>(MissingValueFilter.RowCursor.Value.CreateOne<int>);
						methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { info.Type.RawType });
					}
					else
					{
						Func<MissingValueFilter.RowCursor, MissingValueFilter.ColInfo, MissingValueFilter.RowCursor.Value.ValueVec<int>> func2 = new Func<MissingValueFilter.RowCursor, MissingValueFilter.ColInfo, MissingValueFilter.RowCursor.Value.ValueVec<int>>(MissingValueFilter.RowCursor.Value.CreateVec<int>);
						methodInfo = func2.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { info.Type.ItemType.RawType });
					}
					return (MissingValueFilter.RowCursor.Value)methodInfo.Invoke(null, new object[] { cursor, info });
				}

				// Token: 0x0600149A RID: 5274 RVA: 0x000778C0 File Offset: 0x00075AC0
				private static MissingValueFilter.RowCursor.Value.ValueOne<T> CreateOne<T>(MissingValueFilter.RowCursor cursor, MissingValueFilter.ColInfo info)
				{
					ValueGetter<T> getter = cursor.Input.GetGetter<T>(info.Index);
					RefPredicate<T> isNAPredicate = Conversions.Instance.GetIsNAPredicate<T>(info.Type);
					return new MissingValueFilter.RowCursor.Value.ValueOne<T>(cursor, getter, isNAPredicate);
				}

				// Token: 0x0600149B RID: 5275 RVA: 0x000778F8 File Offset: 0x00075AF8
				private static MissingValueFilter.RowCursor.Value.ValueVec<T> CreateVec<T>(MissingValueFilter.RowCursor cursor, MissingValueFilter.ColInfo info)
				{
					ValueGetter<VBuffer<T>> getter = cursor.Input.GetGetter<VBuffer<T>>(info.Index);
					RefPredicate<VBuffer<T>> hasMissingPredicate = Conversions.Instance.GetHasMissingPredicate<T>((VectorType)info.Type);
					return new MissingValueFilter.RowCursor.Value.ValueVec<T>(cursor, getter, hasMissingPredicate);
				}

				// Token: 0x04000C62 RID: 3170
				protected readonly MissingValueFilter.RowCursor _cursor;

				// Token: 0x020003C5 RID: 965
				private abstract class TypedValue<T> : MissingValueFilter.RowCursor.Value
				{
					// Token: 0x0600149C RID: 5276 RVA: 0x00077935 File Offset: 0x00075B35
					protected TypedValue(MissingValueFilter.RowCursor cursor, ValueGetter<T> getSrc, RefPredicate<T> hasBad)
						: base(cursor)
					{
						this._getSrc = getSrc;
						this._hasBad = hasBad;
					}

					// Token: 0x0600149D RID: 5277 RVA: 0x0007794C File Offset: 0x00075B4C
					public override bool Refresh()
					{
						this._getSrc.Invoke(ref this.Src);
						return !this._hasBad.Invoke(ref this.Src);
					}

					// Token: 0x04000C63 RID: 3171
					private readonly ValueGetter<T> _getSrc;

					// Token: 0x04000C64 RID: 3172
					private readonly RefPredicate<T> _hasBad;

					// Token: 0x04000C65 RID: 3173
					public T Src;
				}

				// Token: 0x020003C6 RID: 966
				private sealed class ValueOne<T> : MissingValueFilter.RowCursor.Value.TypedValue<T>
				{
					// Token: 0x0600149E RID: 5278 RVA: 0x00077973 File Offset: 0x00075B73
					public ValueOne(MissingValueFilter.RowCursor cursor, ValueGetter<T> getSrc, RefPredicate<T> hasBad)
						: base(cursor, getSrc, hasBad)
					{
						this._getter = new ValueGetter<T>(this.GetValue);
					}

					// Token: 0x0600149F RID: 5279 RVA: 0x00077990 File Offset: 0x00075B90
					public void GetValue(ref T dst)
					{
						Contracts.Check(this._cursor.IsGood);
						dst = this.Src;
					}

					// Token: 0x060014A0 RID: 5280 RVA: 0x000779AE File Offset: 0x00075BAE
					public override Delegate GetGetter()
					{
						return this._getter;
					}

					// Token: 0x04000C66 RID: 3174
					private readonly ValueGetter<T> _getter;
				}

				// Token: 0x020003C7 RID: 967
				private sealed class ValueVec<T> : MissingValueFilter.RowCursor.Value.TypedValue<VBuffer<T>>
				{
					// Token: 0x060014A1 RID: 5281 RVA: 0x000779B6 File Offset: 0x00075BB6
					public ValueVec(MissingValueFilter.RowCursor cursor, ValueGetter<VBuffer<T>> getSrc, RefPredicate<VBuffer<T>> hasBad)
						: base(cursor, getSrc, hasBad)
					{
						this._getter = new ValueGetter<VBuffer<T>>(this.GetValue);
					}

					// Token: 0x060014A2 RID: 5282 RVA: 0x000779D3 File Offset: 0x00075BD3
					public void GetValue(ref VBuffer<T> dst)
					{
						Contracts.Check(this._cursor.IsGood);
						this.Src.CopyTo(ref dst);
					}

					// Token: 0x060014A3 RID: 5283 RVA: 0x000779F1 File Offset: 0x00075BF1
					public override Delegate GetGetter()
					{
						return this._getter;
					}

					// Token: 0x04000C67 RID: 3175
					private readonly ValueGetter<VBuffer<T>> _getter;
				}
			}
		}
	}
}
