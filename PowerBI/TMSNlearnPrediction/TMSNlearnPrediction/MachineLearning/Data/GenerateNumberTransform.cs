using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000235 RID: 565
	public sealed class GenerateNumberTransform : RowToRowTransformBase
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x00045489 File Offset: 0x00043689
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("GEN NUMT", 65537U, 65537U, 65537U, "GenNumTransform", null);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000454AC File Offset: 0x000436AC
		public GenerateNumberTransform(GenerateNumberTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "GenerateNumber", input)
		{
			Contracts.CheckValue<GenerateNumberTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<GenerateNumberTransform.Column>(args.column) > 0, "columns");
			this._bindings = GenerateNumberTransform.Bindings.Create(args, this._input.Schema);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0004550C File Offset: 0x0004370C
		private GenerateNumberTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = GenerateNumberTransform.Bindings.Create(ctx, this._input.Schema);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00045574 File Offset: 0x00043774
		public static GenerateNumberTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("GenerateNumber");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(GenerateNumberTransform.GetVersionInfo());
			return HostExtensions.Apply<GenerateNumberTransform>(h, "Loading Model", (IChannel ch) => new GenerateNumberTransform(ctx, h, input));
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00045609 File Offset: 0x00043809
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(GenerateNumberTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00045645 File Offset: 0x00043845
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0004564D File Offset: 0x0004384D
		public override bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00045650 File Offset: 0x00043850
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this._bindings.AnyNewColumnsActive(predicate))
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0004567C File Offset: 0x0004387C
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, null);
			return new GenerateNumberTransform.RowCursor(this._host, this._bindings, rowCursor, active);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000456C4 File Offset: 0x000438C4
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor;
			if (n > 1 && this.ShouldUseParallelCursors(predicate) != false)
			{
				IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, dependencies, n, null);
				if (rowCursorSet.Length != 1)
				{
					IRowCursor[] array = new IRowCursor[rowCursorSet.Length];
					for (int i = 0; i < rowCursorSet.Length; i++)
					{
						array[i] = new GenerateNumberTransform.RowCursor(this._host, this._bindings, rowCursorSet[i], active);
					}
					return array;
				}
				rowCursor = rowCursorSet[0];
			}
			else
			{
				rowCursor = this._input.GetRowCursor(dependencies, null);
			}
			consolidator = null;
			return new IRowCursor[]
			{
				new GenerateNumberTransform.RowCursor(this._host, this._bindings, rowCursor, active)
			};
		}

		// Token: 0x040006F0 RID: 1776
		internal const string Summary = "Adds a column with a generated number sequence.";

		// Token: 0x040006F1 RID: 1777
		public const string LoadName = "GenerateNumberTransform";

		// Token: 0x040006F2 RID: 1778
		public const string LoaderSignature = "GenNumTransform";

		// Token: 0x040006F3 RID: 1779
		private const string RegistrationName = "GenerateNumber";

		// Token: 0x040006F4 RID: 1780
		private readonly GenerateNumberTransform.Bindings _bindings;

		// Token: 0x02000236 RID: 566
		public sealed class Column
		{
			// Token: 0x06000CBD RID: 3261 RVA: 0x000457AC File Offset: 0x000439AC
			public static GenerateNumberTransform.Column Parse(string str)
			{
				GenerateNumberTransform.Column column = new GenerateNumberTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x000457CC File Offset: 0x000439CC
			private bool TryParse(string str)
			{
				int num = str.IndexOf(':');
				if (num < 0)
				{
					this.name = str;
					return true;
				}
				if (0 < num && num < str.Length - 1)
				{
					this.name = str.Substring(0, num);
					uint num2;
					bool flag = uint.TryParse(str.Substring(num + 1), out num2);
					if (flag)
					{
						this.seed = new uint?(num2);
					}
					return flag;
				}
				return false;
			}

			// Token: 0x040006F5 RID: 1781
			[Argument(0, HelpText = "Name of the new column", ShortName = "name")]
			public string name;

			// Token: 0x040006F6 RID: 1782
			[Argument(0, HelpText = "Use an auto-incremented integer starting at zero instead of a random number", ShortName = "cnt")]
			public bool? useCounter;

			// Token: 0x040006F7 RID: 1783
			[Argument(0, HelpText = "The random seed")]
			public uint? seed;
		}

		// Token: 0x02000237 RID: 567
		public sealed class Arguments
		{
			// Token: 0x040006F8 RID: 1784
			[Argument(4, HelpText = "New column definition(s) (optional form: name:seed)", ShortName = "col", SortOrder = 1)]
			public GenerateNumberTransform.Column[] column;

			// Token: 0x040006F9 RID: 1785
			[Argument(0, HelpText = "Use an auto-incremented integer starting at zero instead of a random number", ShortName = "cnt")]
			public bool useCounter;

			// Token: 0x040006FA RID: 1786
			[Argument(0, HelpText = "The random seed")]
			public uint seed = 42U;
		}

		// Token: 0x02000238 RID: 568
		private sealed class Bindings : ColumnBindingsBase
		{
			// Token: 0x06000CC1 RID: 3265 RVA: 0x00045847 File Offset: 0x00043A47
			private Bindings(bool[] useCounter, TauswortheHybrid.State[] states, ISchema input, bool user, string[] names)
				: base(input, user, names)
			{
				this.UseCounter = useCounter;
				this.States = states;
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x00045864 File Offset: 0x00043A64
			public static GenerateNumberTransform.Bindings Create(GenerateNumberTransform.Arguments args, ISchema input)
			{
				string[] array = new string[args.column.Length];
				bool[] array2 = new bool[args.column.Length];
				TauswortheHybrid.State[] array3 = new TauswortheHybrid.State[args.column.Length];
				for (int i = 0; i < args.column.Length; i++)
				{
					GenerateNumberTransform.Column column = args.column[i];
					array[i] = column.name;
					array2[i] = column.useCounter ?? args.useCounter;
					if (!array2[i])
					{
						array3[i] = new TauswortheHybrid.State(column.seed ?? args.seed);
					}
				}
				return new GenerateNumberTransform.Bindings(array2, array3, input, true, array);
			}

			// Token: 0x06000CC3 RID: 3267 RVA: 0x00045928 File Offset: 0x00043B28
			public static GenerateNumberTransform.Bindings Create(ModelLoadContext ctx, ISchema input)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num > 0);
				string[] array = new string[num];
				bool[] array2 = new bool[num];
				TauswortheHybrid.State[] array3 = new TauswortheHybrid.State[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ctx.LoadNonEmptyString();
					array2[i] = Utils.ReadBoolByte(ctx.Reader);
					if (!array2[i])
					{
						array3[i] = TauswortheHybrid.State.Load(ctx.Reader);
					}
				}
				return new GenerateNumberTransform.Bindings(array2, array3, input, true, array);
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x000459B0 File Offset: 0x00043BB0
			public void Save(ModelSaveContext ctx)
			{
				int infoCount = base.InfoCount;
				ctx.Writer.Write(infoCount);
				for (int i = 0; i < infoCount; i++)
				{
					ctx.SaveNonEmptyString(base.GetColumnNameCore(i));
					Utils.WriteBoolByte(ctx.Writer, this.UseCounter[i]);
					if (!this.UseCounter[i])
					{
						this.States[i].Save(ctx.Writer);
					}
				}
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x00045A1D File Offset: 0x00043C1D
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				if (!this.UseCounter[iinfo])
				{
					return NumberType.Float;
				}
				return NumberType.I8;
			}

			// Token: 0x06000CC6 RID: 3270 RVA: 0x00045A34 File Offset: 0x00043C34
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				IEnumerable<KeyValuePair<string, ColumnType>> metadataTypesCore = base.GetMetadataTypesCore(iinfo);
				if (!this.UseCounter[iinfo])
				{
					MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(metadataTypesCore, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(BoolType.Instance, "IsNormalized") });
				}
				return metadataTypesCore;
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x00045A7E File Offset: 0x00043C7E
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				if (kind == "IsNormalized" && !this.UseCounter[iinfo])
				{
					return BoolType.Instance;
				}
				return base.GetMetadataTypeCore(kind, iinfo);
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x00045AA5 File Offset: 0x00043CA5
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				if (kind == "IsNormalized" && !this.UseCounter[iinfo])
				{
					MetadataUtils.Marshal<DvBool, TValue>(new MetadataUtils.MetadataGetter<DvBool>(this.IsNormalized), iinfo, ref value);
					return;
				}
				base.GetMetadataCore<TValue>(kind, iinfo, ref value);
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x00045ADB File Offset: 0x00043CDB
			private void IsNormalized(int iinfo, ref DvBool dst)
			{
				dst = DvBool.True;
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x00045B0C File Offset: 0x00043D0C
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = base.GetActiveInput(predicate);
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x040006FB RID: 1787
			public readonly bool[] UseCounter;

			// Token: 0x040006FC RID: 1788
			public readonly TauswortheHybrid.State[] States;
		}

		// Token: 0x02000239 RID: 569
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x06000CCB RID: 3275 RVA: 0x00045B38 File Offset: 0x00043D38
			public RowCursor(IChannelProvider provider, GenerateNumberTransform.Bindings bindings, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				Contracts.CheckValue<GenerateNumberTransform.Bindings>(this._ch, bindings, "bindings");
				Contracts.CheckValue<IRowCursor>(this._ch, input, "input");
				Contracts.CheckParam(this._ch, active == null || active.Length == bindings.ColumnCount, "active");
				this._bindings = bindings;
				this._active = active;
				int infoCount = this._bindings.InfoCount;
				this._getters = new Delegate[infoCount];
				this._values = new float[infoCount];
				this._rngs = new TauswortheHybrid[infoCount];
				this._lastCounters = new long[infoCount];
				for (int i = 0; i < infoCount; i++)
				{
					this._getters[i] = (this._bindings.UseCounter[i] ? this.MakeGetter() : this.MakeGetter(i));
					if (!this._bindings.UseCounter[i] && this.IsColumnActive(this._bindings.MapIinfoToCol(i)))
					{
						this._rngs[i] = new TauswortheHybrid(this._bindings.States[i]);
						this._lastCounters[i] = -1L;
					}
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00045C5F File Offset: 0x00043E5F
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00045C67 File Offset: 0x00043E67
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x00045C9C File Offset: 0x00043E9C
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				ValueGetter<TValue> valueGetter = this._getters[num] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x00045D3B File Offset: 0x00043F3B
			private ValueGetter<DvInt8> MakeGetter()
			{
				return delegate(ref DvInt8 value)
				{
					Contracts.Check(this._ch, base.IsGood);
					value = base.Input.Position;
				};
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x00045D49 File Offset: 0x00043F49
			private void EnsureValue(ref long lastCounter, ref float value, TauswortheHybrid rng)
			{
				while (lastCounter < base.Input.Position)
				{
					value = RandomUtils.NextFloat(rng);
					lastCounter += 1L;
				}
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x00045DFC File Offset: 0x00043FFC
			private ValueGetter<float> MakeGetter(int iinfo)
			{
				return delegate(ref float value)
				{
					Contracts.Check(this._ch, this.IsGood);
					this.EnsureValue(ref this._lastCounters[iinfo], ref this._values[iinfo], this._rngs[iinfo]);
					value = this._values[iinfo];
				};
			}

			// Token: 0x040006FD RID: 1789
			private readonly GenerateNumberTransform.Bindings _bindings;

			// Token: 0x040006FE RID: 1790
			private readonly bool[] _active;

			// Token: 0x040006FF RID: 1791
			private readonly Delegate[] _getters;

			// Token: 0x04000700 RID: 1792
			private readonly float[] _values;

			// Token: 0x04000701 RID: 1793
			private readonly TauswortheHybrid[] _rngs;

			// Token: 0x04000702 RID: 1794
			private readonly long[] _lastCounters;
		}
	}
}
