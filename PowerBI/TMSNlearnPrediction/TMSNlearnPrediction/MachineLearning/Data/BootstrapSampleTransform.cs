using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000017 RID: 23
	public sealed class BootstrapSampleTransform : FilterBase
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003BC7 File Offset: 0x00001DC7
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BTSAMPXF", 65537U, 65537U, 65537U, "BootstrapSampleTransform", null);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003BEC File Offset: 0x00001DEC
		public BootstrapSampleTransform(BootstrapSampleTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "BootstrapSample", input)
		{
			Contracts.CheckValue<BootstrapSampleTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, args.poolSize >= 0, "poolSize", "Cannot be negative");
			this._complement = args.complement;
			this._state = new TauswortheHybrid.State(args.seed ?? ((uint)this._host.Rand.Next()));
			this._shuffleInput = args.shuffleInput;
			this._poolSize = args.poolSize;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C90 File Offset: 0x00001E90
		private BootstrapSampleTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._complement = Utils.ReadBoolByte(ctx.Reader);
			this._state = TauswortheHybrid.State.Load(ctx.Reader);
			this._shuffleInput = Utils.ReadBoolByte(ctx.Reader);
			this._poolSize = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._poolSize >= 0);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D00 File Offset: 0x00001F00
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BootstrapSampleTransform.GetVersionInfo());
			Utils.WriteBoolByte(ctx.Writer, this._complement);
			this._state.Save(ctx.Writer);
			Utils.WriteBoolByte(ctx.Writer, this._shuffleInput);
			ctx.Writer.Write(this._poolSize);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D98 File Offset: 0x00001F98
		public static BootstrapSampleTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("BootstrapSample");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(BootstrapSampleTransform.GetVersionInfo());
			return HostExtensions.Apply<BootstrapSampleTransform>(h, "Loading Model", (IChannel ch) => new BootstrapSampleTransform(ctx, h, input));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003E2D File Offset: 0x0000202D
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(false);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E38 File Offset: 0x00002038
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			TauswortheHybrid tauswortheHybrid = new TauswortheHybrid(this._state);
			IRowCursor rowCursor = this._input.GetRowCursor(predicate, this._shuffleInput ? new TauswortheHybrid(tauswortheHybrid) : null);
			IRowCursor rowCursor2 = new BootstrapSampleTransform.RowCursor(this, rowCursor, tauswortheHybrid);
			if (this._poolSize > 1)
			{
				rowCursor2 = ShuffleTransform.GetShuffledCursor(this._host, this._poolSize, rowCursor2, new TauswortheHybrid(tauswortheHybrid));
			}
			return rowCursor2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E9C File Offset: 0x0000209C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			IRowCursor rowCursorCore = this.GetRowCursorCore(predicate, rand);
			consolidator = null;
			return new IRowCursor[] { rowCursorCore };
		}

		// Token: 0x04000031 RID: 49
		internal const string Summary = "Approximate bootstrap sampling.";

		// Token: 0x04000032 RID: 50
		public const string LoaderSignature = "BootstrapSampleTransform";

		// Token: 0x04000033 RID: 51
		private const string RegistrationName = "BootstrapSample";

		// Token: 0x04000034 RID: 52
		private readonly bool _complement;

		// Token: 0x04000035 RID: 53
		private readonly TauswortheHybrid.State _state;

		// Token: 0x04000036 RID: 54
		private readonly bool _shuffleInput;

		// Token: 0x04000037 RID: 55
		private readonly int _poolSize;

		// Token: 0x02000018 RID: 24
		public sealed class Arguments
		{
			// Token: 0x04000038 RID: 56
			[Argument(0, HelpText = "Whether this is the out-of-bag sample, that is, all those rows that are not selected by the trans", ShortName = "comp")]
			public bool complement;

			// Token: 0x04000039 RID: 57
			[Argument(0, HelpText = "The random seed. If unspecified random state will be instead derived from the environment.")]
			public uint? seed;

			// Token: 0x0400003A RID: 58
			[Argument(0, HelpText = "Whether we should attempt to shuffle the source data. By default on, but can be turned off for efficiency.", ShortName = "si")]
			public bool shuffleInput = true;

			// Token: 0x0400003B RID: 59
			[Argument(4, HelpText = "When shuffling the output, the number of output rows to keep in that pool. Note that shuffling of output is completely distinct from shuffling of input.", ShortName = "pool")]
			public int poolSize = 1000;
		}

		// Token: 0x02000019 RID: 25
		private sealed class RowCursor : LinkedRootCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00003EDC File Offset: 0x000020DC
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000078 RID: 120 RVA: 0x00003EE0 File Offset: 0x000020E0
			public ISchema Schema
			{
				get
				{
					return base.Input.Schema;
				}
			}

			// Token: 0x06000079 RID: 121 RVA: 0x00003EED File Offset: 0x000020ED
			public RowCursor(BootstrapSampleTransform parent, IRowCursor input, IRandom rgen)
				: base(parent._host, input)
			{
				this._parent = parent;
				this._rgen = rgen;
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003F40 File Offset: 0x00002140
			public override ValueGetter<UInt128> GetIdGetter()
			{
				ValueGetter<UInt128> inputIdGetter = base.Input.GetIdGetter();
				return delegate(ref UInt128 val)
				{
					inputIdGetter.Invoke(ref val);
					val = val.Combine(new UInt128((ulong)((long)this._remaining), 0UL));
				};
			}

			// Token: 0x0600007B RID: 123 RVA: 0x00003F77 File Offset: 0x00002177
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				return base.Input.GetGetter<TValue>(col);
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00003F85 File Offset: 0x00002185
			public bool IsColumnActive(int col)
			{
				return base.Input.IsColumnActive(col);
			}

			// Token: 0x0600007D RID: 125 RVA: 0x00003F94 File Offset: 0x00002194
			protected override bool MoveNextCore()
			{
				while (this._remaining == 0 && base.Input.MoveNext())
				{
					this._remaining = Stats.SampleFromPoisson(this._rgen, 1.0);
					if (this._parent._complement)
					{
						this._remaining = ((this._remaining == 0) ? 1 : 0);
					}
				}
				return this._remaining-- > 0;
			}

			// Token: 0x0400003C RID: 60
			private int _remaining;

			// Token: 0x0400003D RID: 61
			private readonly BootstrapSampleTransform _parent;

			// Token: 0x0400003E RID: 62
			private readonly IRandom _rgen;
		}
	}
}
