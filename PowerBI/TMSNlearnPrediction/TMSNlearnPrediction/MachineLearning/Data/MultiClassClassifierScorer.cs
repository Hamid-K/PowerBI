using System;
using Microsoft.MachineLearning.Model;
using Microsoft.MachineLearning.Numeric;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001BE RID: 446
	public sealed class MultiClassClassifierScorer : PredictedLabelScorerBase
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x00035848 File Offset: 0x00033A48
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("MULCLSCR", 65539U, 65539U, 65539U, "MultiClassScoreTrans", null);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0003586C File Offset: 0x00033A6C
		public MultiClassClassifierScorer(MultiClassClassifierScorer.Arguments args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper)
			: base(args, env, data, mapper, "MultiClassClassifierScore", "MultiClassClassification", "Score", new Func<ColumnType, bool>(MultiClassClassifierScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(MultiClassClassifierScorer.GetPredColType))
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000358AB File Offset: 0x00033AAB
		private MultiClassClassifierScorer(IHostEnvironment env, MultiClassClassifierScorer transform, IDataView newSource)
			: base(env, transform, newSource, "MultiClassClassifierScore")
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000358BB File Offset: 0x00033ABB
		private MultiClassClassifierScorer(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, bool>(MultiClassClassifierScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(MultiClassClassifierScorer.GetPredColType))
		{
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00035900 File Offset: 0x00033B00
		public static MultiClassClassifierScorer Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("MultiClassClassifierScore");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(MultiClassClassifierScorer.GetVersionInfo());
			return HostExtensions.Apply<MultiClassClassifierScorer>(h, "Loading Model", (IChannel ch) => new MultiClassClassifierScorer(ctx, h, input));
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00035995 File Offset: 0x00033B95
		protected override void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(MultiClassClassifierScorer.GetVersionInfo());
			base.SaveCore(ctx);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000359A9 File Offset: 0x00033BA9
		public override IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(newSource, "newSource");
			return new MultiClassClassifierScorer(env, this, newSource);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00035AA0 File Offset: 0x00033CA0
		protected override Delegate GetPredictedLabelGetter(IRow output, out Delegate scoreGetter)
		{
			ValueGetter<VBuffer<float>> mapperScoreGetter = output.GetGetter<VBuffer<float>>(this._bindings.ScoreColumnIndex);
			long cachedPosition = -1L;
			VBuffer<float> score = default(VBuffer<float>);
			int scoreLength = this._bindings.PredColType.KeyCount;
			ValueGetter<uint> valueGetter = delegate(ref uint dst)
			{
				this.EnsureCachedPosition<VBuffer<float>>(ref cachedPosition, ref score, output, mapperScoreGetter);
				Contracts.Check(this._host, score.Length == scoreLength);
				int num = VectorUtils.ArgMax(ref score);
				if (num < 0)
				{
					dst = 0U;
					return;
				}
				dst = (uint)(num + 1);
			};
			ValueGetter<VBuffer<float>> valueGetter2 = delegate(ref VBuffer<float> dst)
			{
				this.EnsureCachedPosition<VBuffer<float>>(ref cachedPosition, ref score, output, mapperScoreGetter);
				Contracts.Check(this._host, score.Length == scoreLength);
				score.CopyTo(ref dst);
			};
			scoreGetter = valueGetter2;
			return valueGetter;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00035B25 File Offset: 0x00033D25
		private static ColumnType GetPredColType(ColumnType scoreType)
		{
			return new KeyType(6, 0UL, scoreType.VectorSize, true);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00035B36 File Offset: 0x00033D36
		private static bool OutputTypeMatches(ColumnType scoreType)
		{
			return scoreType.IsKnownSizeVector && scoreType.ItemType == NumberType.Float;
		}

		// Token: 0x04000523 RID: 1315
		public const string LoaderSignature = "MultiClassScoreTrans";

		// Token: 0x04000524 RID: 1316
		private const string RegistrationName = "MultiClassClassifierScore";

		// Token: 0x020001BF RID: 447
		public sealed class Arguments : ScorerArgumentsBase
		{
		}
	}
}
