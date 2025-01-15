using System;
using System.Reflection;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002A1 RID: 673
	public sealed class RangeFilter : FilterBase
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x000547C5 File Offset: 0x000529C5
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("RNGFILTR", 65537U, 65537U, 65537U, "RangeFilter", null);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000547E8 File Offset: 0x000529E8
		public RangeFilter(RangeFilter.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "RangeFilter", input)
		{
			Contracts.CheckValue<RangeFilter.Arguments>(this._host, args, "args");
			ISchema schema = this._input.Schema;
			if (!schema.TryGetColumnIndex(args.column, ref this._index))
			{
				throw Contracts.ExceptUserArg(this._host, "column", "Source column '{0}' not found", new object[] { args.column });
			}
			using (IChannel channel = this._host.Start("Checking parameters"))
			{
				this._type = schema.GetColumnType(this._index);
				if (!RangeFilter.IsValidRangeFilterColumnType(channel, this._type))
				{
					throw Contracts.ExceptUserArg(channel, "column", "Column '{0}' does not have compatible type", new object[] { args.column });
				}
				if (this._type.IsKey)
				{
					double? min = args.min;
					if (min.GetValueOrDefault() < 0.0 && min != null)
					{
						channel.Warning("specified min less than zero, will be ignored");
						args.min = null;
					}
					double? max = args.max;
					if (max.GetValueOrDefault() > 1.0 && max != null)
					{
						channel.Warning("specified max greater than one, will be ignored");
						args.max = null;
					}
				}
				if (args.min == null && args.max == null)
				{
					throw Contracts.ExceptUserArg(channel, "min", "At least one of min and max must be specified.");
				}
				double? min2 = args.min;
				this._min = ((min2 != null) ? min2.GetValueOrDefault() : double.NegativeInfinity);
				double? max2 = args.max;
				this._max = ((max2 != null) ? max2.GetValueOrDefault() : double.PositiveInfinity);
				if (this._min > this._max)
				{
					throw Contracts.ExceptUserArg(channel, "min", "min must be less than or equal to max");
				}
				this._complement = args.complement;
				this._includeMin = args.includeMin;
				this._includeMax = args.includeMax ?? (args.max == null || (this._type.IsKey && this._max >= 1.0));
				channel.Done();
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00054A70 File Offset: 0x00052C70
		private RangeFilter(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel(RangeFilter.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			string text = ctx.LoadNonEmptyString();
			ISchema schema = this._input.Schema;
			if (!schema.TryGetColumnIndex(text, ref this._index))
			{
				throw Contracts.Except(this._host, "column", new object[] { "Source column '{0}' not found", text });
			}
			this._type = schema.GetColumnType(this._index);
			if (this._type != NumberType.R4 && this._type != NumberType.R8 && this._type.KeyCount == 0)
			{
				throw Contracts.Except(this._host, "column", new object[] { "Column '{0}' does not have compatible type", text });
			}
			this._min = ctx.Reader.ReadDouble();
			this._max = ctx.Reader.ReadDouble();
			if (this._min > this._max)
			{
				throw Contracts.Except(this._host, "min", new object[] { "min must be less than or equal to max" });
			}
			this._complement = Utils.ReadBoolByte(ctx.Reader);
			this._includeMin = Utils.ReadBoolByte(ctx.Reader);
			this._includeMax = Utils.ReadBoolByte(ctx.Reader);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00054C10 File Offset: 0x00052E10
		public static RangeFilter Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("RangeFilter");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(RangeFilter.GetVersionInfo());
			return HostExtensions.Apply<RangeFilter>(h, "Loading Model", (IChannel ch) => new RangeFilter(ctx, h, input));
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00054CA8 File Offset: 0x00052EA8
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(RangeFilter.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.SaveNonEmptyString(this._input.Schema.GetColumnName(this._index));
			ctx.Writer.Write(this._min);
			ctx.Writer.Write(this._max);
			Utils.WriteBoolByte(ctx.Writer, this._complement);
			Utils.WriteBoolByte(ctx.Writer, this._includeMin);
			Utils.WriteBoolByte(ctx.Writer, this._includeMax);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00054D54 File Offset: 0x00052F54
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00054D6C File Offset: 0x00052F6C
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			bool[] array;
			Func<int, bool> active = this.GetActive(predicate, out array);
			IRowCursor rowCursor = this._input.GetRowCursor(active, rand);
			return this.CreateCursorCore(rowCursor, array);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00054D9C File Offset: 0x00052F9C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			bool[] array;
			Func<int, bool> active = this.GetActive(predicate, out array);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, active, n, rand);
			IRowCursor[] array2 = new IRowCursor[rowCursorSet.Length];
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				array2[i] = this.CreateCursorCore(rowCursorSet[i], array);
			}
			return array2;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00054E00 File Offset: 0x00053000
		private IRowCursor CreateCursorCore(IRowCursor input, bool[] active)
		{
			if (this._type == NumberType.R4)
			{
				return new RangeFilter.SingleRowCursor(this, input, active);
			}
			if (this._type == NumberType.R8)
			{
				return new RangeFilter.DoubleRowCursor(this, input, active);
			}
			return RangeFilter.RowCursorBase.CreateKeyRowCursor(this, input, active);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00054E48 File Offset: 0x00053048
		private Func<int, bool> GetActive(Func<int, bool> predicate, out bool[] active)
		{
			active = new bool[this._input.Schema.ColumnCount];
			bool[] activeInput = new bool[this._input.Schema.ColumnCount];
			for (int i = 0; i < active.Length; i++)
			{
				activeInput[i] = (active[i] = predicate(i));
			}
			activeInput[this._index] = true;
			return (int col) => activeInput[col];
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00054ECC File Offset: 0x000530CC
		public static bool IsValidRangeFilterColumnType(IExceptionContext ectx, ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(ectx, type, "type");
			return type == NumberType.R4 || type == NumberType.R8 || type.KeyCount > 0;
		}

		// Token: 0x04000876 RID: 2166
		internal const string Summary = "Filters a dataview on a column of type Single, Double or Key (contiguous). Keeps the values that are in the specified min/max range. NaNs are always filtered out. If the input is a Key type, the min/max are considered percentages of the number of values.";

		// Token: 0x04000877 RID: 2167
		public const string LoaderSignature = "RangeFilter";

		// Token: 0x04000878 RID: 2168
		private const string RegistrationName = "RangeFilter";

		// Token: 0x04000879 RID: 2169
		public readonly int _index;

		// Token: 0x0400087A RID: 2170
		public readonly ColumnType _type;

		// Token: 0x0400087B RID: 2171
		public readonly double _min;

		// Token: 0x0400087C RID: 2172
		public readonly double _max;

		// Token: 0x0400087D RID: 2173
		public readonly bool _complement;

		// Token: 0x0400087E RID: 2174
		public readonly bool _includeMin;

		// Token: 0x0400087F RID: 2175
		public readonly bool _includeMax;

		// Token: 0x020002A2 RID: 674
		public sealed class Arguments
		{
			// Token: 0x04000880 RID: 2176
			[Argument(4, HelpText = "Column", ShortName = "col", SortOrder = 1, Purpose = "ColumnName")]
			public string column;

			// Token: 0x04000881 RID: 2177
			[Argument(4, HelpText = "Minimum value (0 to 1 for key types)")]
			public double? min;

			// Token: 0x04000882 RID: 2178
			[Argument(4, HelpText = "Maximum value (0 to 1 for key types)")]
			public double? max;

			// Token: 0x04000883 RID: 2179
			[Argument(4, HelpText = "If true, keep the values that fall outside the range.")]
			public bool complement;

			// Token: 0x04000884 RID: 2180
			[Argument(4, HelpText = "If true, include in the range the values that are equal to min.")]
			public bool includeMin = true;

			// Token: 0x04000885 RID: 2181
			[Argument(4, HelpText = "If true, include in the range the values that are equal to max.")]
			public bool? includeMax;
		}

		// Token: 0x020002A3 RID: 675
		private abstract class RowCursorBase : LinkedRowFilterCursorBase
		{
			// Token: 0x06000F80 RID: 3968 RVA: 0x00054F04 File Offset: 0x00053104
			protected RowCursorBase(RangeFilter parent, IRowCursor input, bool[] active)
				: base(parent._host, input, parent.Schema, active)
			{
				this._parent = parent;
				this._min = this._parent._min;
				this._max = this._parent._max;
				if (this._parent._includeMin)
				{
					if (this._parent._includeMax)
					{
						this._checkBounds = (this._parent._complement ? new Func<double, bool>(this.TestNCC) : new Func<double, bool>(this.TestCC));
						return;
					}
					this._checkBounds = (this._parent._complement ? new Func<double, bool>(this.TestNCO) : new Func<double, bool>(this.TestCO));
					return;
				}
				else
				{
					if (this._parent._includeMax)
					{
						this._checkBounds = (this._parent._complement ? new Func<double, bool>(this.TestNOC) : new Func<double, bool>(this.TestOC));
						return;
					}
					this._checkBounds = (this._parent._complement ? new Func<double, bool>(this.TestNOO) : new Func<double, bool>(this.TestOO));
					return;
				}
			}

			// Token: 0x06000F81 RID: 3969 RVA: 0x0005502C File Offset: 0x0005322C
			private bool TestOO(double value)
			{
				return this._min < value && value < this._max;
			}

			// Token: 0x06000F82 RID: 3970 RVA: 0x00055042 File Offset: 0x00053242
			private bool TestCO(double value)
			{
				return this._min <= value && value < this._max;
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x00055058 File Offset: 0x00053258
			private bool TestOC(double value)
			{
				return this._min < value && value <= this._max;
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x00055071 File Offset: 0x00053271
			private bool TestCC(double value)
			{
				return this._min <= value && value <= this._max;
			}

			// Token: 0x06000F85 RID: 3973 RVA: 0x0005508A File Offset: 0x0005328A
			private bool TestNOO(double value)
			{
				return this._min >= value || value >= this._max;
			}

			// Token: 0x06000F86 RID: 3974 RVA: 0x000550A3 File Offset: 0x000532A3
			private bool TestNCO(double value)
			{
				return this._min > value || value >= this._max;
			}

			// Token: 0x06000F87 RID: 3975 RVA: 0x000550BC File Offset: 0x000532BC
			private bool TestNOC(double value)
			{
				return this._min >= value || value > this._max;
			}

			// Token: 0x06000F88 RID: 3976 RVA: 0x000550D2 File Offset: 0x000532D2
			private bool TestNCC(double value)
			{
				return this._min > value || value > this._max;
			}

			// Token: 0x06000F89 RID: 3977
			protected abstract Delegate GetGetter();

			// Token: 0x06000F8A RID: 3978 RVA: 0x000550E8 File Offset: 0x000532E8
			public override ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < base.Schema.ColumnCount);
				Contracts.Check(this._ch, base.IsColumnActive(col));
				if (col != this._parent._index)
				{
					return base.Input.GetGetter<TValue>(col);
				}
				ValueGetter<TValue> valueGetter = this.GetGetter() as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000F8B RID: 3979 RVA: 0x00055178 File Offset: 0x00053378
			public static IRowCursor CreateKeyRowCursor(RangeFilter filter, IRowCursor input, bool[] active)
			{
				Func<RangeFilter, IRowCursor, bool[], IRowCursor> func = new Func<RangeFilter, IRowCursor, bool[], IRowCursor>(RangeFilter.RowCursorBase.CreateKeyRowCursor<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { filter._type.RawType });
				return (IRowCursor)methodInfo.Invoke(null, new object[] { filter, input, active });
			}

			// Token: 0x06000F8C RID: 3980 RVA: 0x000551D9 File Offset: 0x000533D9
			private static IRowCursor CreateKeyRowCursor<TSrc>(RangeFilter filter, IRowCursor input, bool[] active)
			{
				return new RangeFilter.KeyRowCursor<TSrc>(filter, input, active);
			}

			// Token: 0x04000886 RID: 2182
			protected readonly RangeFilter _parent;

			// Token: 0x04000887 RID: 2183
			protected readonly Func<double, bool> _checkBounds;

			// Token: 0x04000888 RID: 2184
			private readonly double _min;

			// Token: 0x04000889 RID: 2185
			private readonly double _max;
		}

		// Token: 0x020002A4 RID: 676
		private sealed class SingleRowCursor : RangeFilter.RowCursorBase
		{
			// Token: 0x06000F8D RID: 3981 RVA: 0x00055200 File Offset: 0x00053400
			public SingleRowCursor(RangeFilter parent, IRowCursor input, bool[] active)
				: base(parent, input, active)
			{
				this._srcGetter = base.Input.GetGetter<float>(this._parent._index);
				this._getter = delegate(ref float value)
				{
					Contracts.Check(this._ch, base.IsGood);
					value = this._value;
				};
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x0005524B File Offset: 0x0005344B
			protected override Delegate GetGetter()
			{
				return this._getter;
			}

			// Token: 0x06000F8F RID: 3983 RVA: 0x00055253 File Offset: 0x00053453
			protected override bool Accept()
			{
				this._srcGetter.Invoke(ref this._value);
				return this._checkBounds((double)this._value);
			}

			// Token: 0x0400088A RID: 2186
			private readonly ValueGetter<float> _srcGetter;

			// Token: 0x0400088B RID: 2187
			private readonly ValueGetter<float> _getter;

			// Token: 0x0400088C RID: 2188
			private float _value;
		}

		// Token: 0x020002A5 RID: 677
		private sealed class DoubleRowCursor : RangeFilter.RowCursorBase
		{
			// Token: 0x06000F91 RID: 3985 RVA: 0x00055294 File Offset: 0x00053494
			public DoubleRowCursor(RangeFilter parent, IRowCursor input, bool[] active)
				: base(parent, input, active)
			{
				this._srcGetter = base.Input.GetGetter<double>(this._parent._index);
				this._getter = delegate(ref double value)
				{
					Contracts.Check(this._ch, base.IsGood);
					value = this._value;
				};
			}

			// Token: 0x06000F92 RID: 3986 RVA: 0x000552DF File Offset: 0x000534DF
			protected override Delegate GetGetter()
			{
				return this._getter;
			}

			// Token: 0x06000F93 RID: 3987 RVA: 0x000552E7 File Offset: 0x000534E7
			protected override bool Accept()
			{
				this._srcGetter.Invoke(ref this._value);
				return this._checkBounds(this._value);
			}

			// Token: 0x0400088D RID: 2189
			private readonly ValueGetter<double> _srcGetter;

			// Token: 0x0400088E RID: 2190
			private readonly ValueGetter<double> _getter;

			// Token: 0x0400088F RID: 2191
			private double _value;
		}

		// Token: 0x020002A6 RID: 678
		private sealed class KeyRowCursor<T> : RangeFilter.RowCursorBase
		{
			// Token: 0x06000F95 RID: 3989 RVA: 0x0005532C File Offset: 0x0005352C
			public KeyRowCursor(RangeFilter parent, IRowCursor input, bool[] active)
				: base(parent, input, active)
			{
				this._count = this._parent._type.KeyCount;
				this._srcGetter = base.Input.GetGetter<T>(this._parent._index);
				this._getter = delegate(ref T dst)
				{
					Contracts.Check(this._ch, base.IsGood);
					dst = this._value;
				};
				bool flag;
				this._conv = Conversions.Instance.GetStandardConversion<T, ulong>(this._parent._type, NumberType.U8, out flag);
			}

			// Token: 0x06000F96 RID: 3990 RVA: 0x000553AF File Offset: 0x000535AF
			protected override Delegate GetGetter()
			{
				return this._getter;
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x000553B8 File Offset: 0x000535B8
			protected override bool Accept()
			{
				this._srcGetter.Invoke(ref this._value);
				ulong num = 0UL;
				this._conv.Invoke(ref this._value, ref num);
				return num != 0UL && num <= (ulong)((long)this._count) && this._checkBounds(((uint)num - 0.5) / (double)this._count);
			}

			// Token: 0x04000890 RID: 2192
			private readonly ValueGetter<T> _srcGetter;

			// Token: 0x04000891 RID: 2193
			private readonly ValueGetter<T> _getter;

			// Token: 0x04000892 RID: 2194
			private T _value;

			// Token: 0x04000893 RID: 2195
			private readonly ValueMapper<T, ulong> _conv;

			// Token: 0x04000894 RID: 2196
			private readonly int _count;
		}
	}
}
