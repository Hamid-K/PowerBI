using System;
using Microsoft.MachineLearning.Model;
using Microsoft.MachineLearning.Numeric;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000080 RID: 128
	public sealed class ClusteringScorer : PredictedLabelScorerBase
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CLSTRSCR", 65539U, 65539U, 65539U, "ClusteringScoreTrans", null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public ClusteringScorer(ClusteringScorer.Arguments args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper)
			: base(args, env, data, mapper, "ClusteringScore", "Clustering", "Score", new Func<ColumnType, bool>(ClusteringScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(ClusteringScorer.GetPredColType))
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DA07 File Offset: 0x0000BC07
		private ClusteringScorer(IHostEnvironment env, ClusteringScorer transform, IDataView newSource)
			: base(env, transform, newSource, "ClusteringScore")
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DA17 File Offset: 0x0000BC17
		private ClusteringScorer(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, bool>(ClusteringScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(ClusteringScorer.GetPredColType))
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public static ClusteringScorer Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("ClusteringScore");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ClusteringScorer.GetVersionInfo());
			return HostExtensions.Apply<ClusteringScorer>(h, "Loading Model", (IChannel ch) => new ClusteringScorer(ctx, h, input));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000DAF1 File Offset: 0x0000BCF1
		protected override void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(ClusteringScorer.GetVersionInfo());
			base.SaveCore(ctx);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DB05 File Offset: 0x0000BD05
		public override IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(newSource, "newSource");
			return new ClusteringScorer(env, this, newSource);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		protected override Delegate GetPredictedLabelGetter(IRow output, out Delegate scoreGetter)
		{
			ValueGetter<VBuffer<float>> mapperScoreGetter = output.GetGetter<VBuffer<float>>(this._bindings.ScoreColumnIndex);
			long cachedPosition = -1L;
			VBuffer<float> score = default(VBuffer<float>);
			int scoreLength = this._bindings.PredColType.KeyCount;
			ValueGetter<uint> valueGetter = delegate(ref uint dst)
			{
				this.EnsureCachedPosition<VBuffer<float>>(ref cachedPosition, ref score, output, mapperScoreGetter);
				Contracts.Check(score.Length == scoreLength);
				int num = VectorUtils.ArgMin(ref score);
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
				Contracts.Check(score.Length == scoreLength);
				score.CopyTo(ref dst);
			};
			scoreGetter = valueGetter2;
			return valueGetter;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DC6D File Offset: 0x0000BE6D
		private static ColumnType GetPredColType(ColumnType scoreType)
		{
			return new KeyType(6, 0UL, scoreType.VectorSize, true);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DC7E File Offset: 0x0000BE7E
		private static bool OutputTypeMatches(ColumnType scoreType)
		{
			return scoreType.IsKnownSizeVector && scoreType.ItemType == NumberType.Float;
		}

		// Token: 0x040000D3 RID: 211
		public const string LoaderSignature = "ClusteringScoreTrans";

		// Token: 0x040000D4 RID: 212
		private const string RegistrationName = "ClusteringScore";

		// Token: 0x02000081 RID: 129
		public sealed class Arguments : ScorerArgumentsBase
		{
		}
	}
}
