using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200029C RID: 668
	public sealed class SkipTakeFilter : FilterBase, ITransformTemplate, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000F5F RID: 3935 RVA: 0x00054328 File Offset: 0x00052528
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("SKIPTKFL", 65537U, 65537U, 65537U, "SkipTakeFilter", null);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00054349 File Offset: 0x00052549
		private SkipTakeFilter(long skip, long take, IHostEnvironment env, IDataView input)
			: base(env, "SkipTakeFilter", input)
		{
			this._skip = skip;
			this._take = take;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00054367 File Offset: 0x00052567
		public IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			return new SkipTakeFilter(this._skip, this._take, env, newSource);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0005437C File Offset: 0x0005257C
		public static SkipTakeFilter Create(SkipTakeFilter.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<SkipTakeFilter.Arguments>(env, args, "args");
			long num = args.skip ?? 0L;
			long num2 = args.take ?? long.MaxValue;
			Contracts.CheckUserArg(env, num >= 0L, "skip", "should be non-negative");
			Contracts.CheckUserArg(env, num2 >= 0L, "take", "should be non-negative");
			return new SkipTakeFilter(num, num2, env, input);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00054418 File Offset: 0x00052618
		public static SkipTakeFilter Create(SkipTakeFilter.SkipArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<SkipTakeFilter.SkipArguments>(env, args, "args");
			Contracts.CheckUserArg(env, args.count >= 0L, "count", "should be non-negative");
			return new SkipTakeFilter(args.count, long.MaxValue, env, input);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00054470 File Offset: 0x00052670
		public static SkipTakeFilter Create(SkipTakeFilter.TakeArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<SkipTakeFilter.TakeArguments>(env, args, "args");
			Contracts.CheckUserArg(env, args.count >= 0L, "take", "should be non-negative");
			return new SkipTakeFilter(0L, args.count, env, input);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000544E8 File Offset: 0x000526E8
		public static SkipTakeFilter Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("SkipTakeFilter");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(SkipTakeFilter.GetVersionInfo());
			long skip = ctx.Reader.ReadInt64();
			Contracts.CheckDecode(h, skip >= 0L);
			long take = ctx.Reader.ReadInt64();
			Contracts.CheckDecode(h, take >= 0L);
			return HostExtensions.Apply<SkipTakeFilter>(h, "Loading Model", (IChannel ch) => new SkipTakeFilter(skip, take, h, input));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000545A8 File Offset: 0x000527A8
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(SkipTakeFilter.GetVersionInfo());
			ctx.Writer.Write(this._skip);
			ctx.Writer.Write(this._take);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000545F9 File Offset: 0x000527F9
		public override bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000545FC File Offset: 0x000527FC
		public override long? GetRowCount(bool lazy = true)
		{
			if (this._take == 0L)
			{
				return new long?(0L);
			}
			long? rowCount = this._input.GetRowCount(lazy);
			if (rowCount == null)
			{
				return null;
			}
			long num = rowCount.GetValueOrDefault() - this._skip;
			return new long?(Math.Min(Math.Max(0L, num), this._take));
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00054662 File Offset: 0x00052862
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(false);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0005466C File Offset: 0x0005286C
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			IRowCursor rowCursor = this._input.GetRowCursor(predicate, null);
			bool[] array = Utils.BuildArray<bool>(this.Schema.ColumnCount, predicate);
			return new SkipTakeFilter.RowCursor(this._host, rowCursor, this.Schema, array, this._skip, this._take);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000546B8 File Offset: 0x000528B8
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursorCore(predicate, null) };
		}

		// Token: 0x04000862 RID: 2146
		public const string LoaderSignature = "SkipTakeFilter";

		// Token: 0x04000863 RID: 2147
		private const string ModelSignature = "SKIPTKFL";

		// Token: 0x04000864 RID: 2148
		private const string RegistrationName = "SkipTakeFilter";

		// Token: 0x04000865 RID: 2149
		internal const string SkipTakeFilterSummary = "Allows limiting input to a subset of rows at an optional offset.  Can be used to implement data paging.";

		// Token: 0x04000866 RID: 2150
		internal const string TakeFilterSummary = "Allows limiting input to a subset of rows by taking N first rows.";

		// Token: 0x04000867 RID: 2151
		internal const string SkipFilterSummary = "Allows limiting input to a subset of rows by skipping a number of rows.";

		// Token: 0x04000868 RID: 2152
		private readonly long _skip;

		// Token: 0x04000869 RID: 2153
		private readonly long _take;

		// Token: 0x0200029D RID: 669
		public sealed class Arguments
		{
			// Token: 0x0400086A RID: 2154
			internal const string SkipHelp = "Number of items to skip";

			// Token: 0x0400086B RID: 2155
			internal const string TakeHelp = "Number of items to take";

			// Token: 0x0400086C RID: 2156
			internal const long DefaultSkip = 0L;

			// Token: 0x0400086D RID: 2157
			internal const long DefaultTake = 9223372036854775807L;

			// Token: 0x0400086E RID: 2158
			[Argument(0, HelpText = "Number of items to skip", ShortName = "s", SortOrder = 1)]
			public long? skip;

			// Token: 0x0400086F RID: 2159
			[Argument(0, HelpText = "Number of items to take", ShortName = "t", SortOrder = 2)]
			public long? take;
		}

		// Token: 0x0200029E RID: 670
		public sealed class TakeArguments
		{
			// Token: 0x04000870 RID: 2160
			[Argument(1, HelpText = "Number of items to take", ShortName = "c,n,t", SortOrder = 1)]
			public long count;
		}

		// Token: 0x0200029F RID: 671
		public sealed class SkipArguments
		{
			// Token: 0x04000871 RID: 2161
			[Argument(1, HelpText = "Number of items to skip", ShortName = "c,n,s", SortOrder = 1)]
			public long count;
		}

		// Token: 0x020002A0 RID: 672
		private sealed class RowCursor : LinkedRowRootCursorBase
		{
			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00054704 File Offset: 0x00052904
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000F70 RID: 3952 RVA: 0x00054708 File Offset: 0x00052908
			public RowCursor(IChannelProvider provider, IRowCursor input, ISchema schema, bool[] active, long skip, long take)
				: base(provider, input, schema, active)
			{
				this._skip = skip;
				this._take = take;
			}

			// Token: 0x06000F71 RID: 3953 RVA: 0x00054725 File Offset: 0x00052925
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return base.Input.GetIdGetter();
			}

			// Token: 0x06000F72 RID: 3954 RVA: 0x00054732 File Offset: 0x00052932
			protected override bool MoveNextCore()
			{
				return this.MoveManyCore(1L);
			}

			// Token: 0x06000F73 RID: 3955 RVA: 0x0005473C File Offset: 0x0005293C
			protected override bool MoveManyCore(long count)
			{
				if (count > this._take - this._rowsTaken)
				{
					this._rowsTaken = this._take;
					return false;
				}
				this._rowsTaken += count;
				if (this._started)
				{
					return base.Root.MoveMany(count);
				}
				this._started = true;
				if (count > 9223372036854775807L - this._skip)
				{
					this._rowsTaken = this._take;
					return false;
				}
				return base.Root.MoveMany(this._skip + count);
			}

			// Token: 0x04000872 RID: 2162
			private readonly long _skip;

			// Token: 0x04000873 RID: 2163
			private readonly long _take;

			// Token: 0x04000874 RID: 2164
			private long _rowsTaken;

			// Token: 0x04000875 RID: 2165
			private bool _started;
		}
	}
}
