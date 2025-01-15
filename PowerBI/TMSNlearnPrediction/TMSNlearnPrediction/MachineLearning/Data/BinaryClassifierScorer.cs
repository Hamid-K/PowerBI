using System;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000250 RID: 592
	public sealed class BinaryClassifierScorer : PredictedLabelScorerBase
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x00049B7D File Offset: 0x00047D7D
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BINCLSCR", 65540U, 65540U, 65540U, "BinClassScoreTransform", null);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00049BA0 File Offset: 0x00047DA0
		public BinaryClassifierScorer(BinaryClassifierScorer.Arguments args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper)
			: base(args, env, data, mapper, "BinaryClassifierScore", "BinaryClassification", Contracts.CheckRef<BinaryClassifierScorer.Arguments>(args, "args").thresholdColumn, new Func<ColumnType, bool>(BinaryClassifierScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(BinaryClassifierScorer.GetPredColType))
		{
			this._threshold = args.threshold;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00049BF6 File Offset: 0x00047DF6
		private BinaryClassifierScorer(IHostEnvironment env, BinaryClassifierScorer transform, IDataView newSource)
			: base(env, transform, newSource, "BinaryClassifierScore")
		{
			this._threshold = transform._threshold;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00049C14 File Offset: 0x00047E14
		private BinaryClassifierScorer(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, bool>(BinaryClassifierScorer.OutputTypeMatches), new Func<ColumnType, ColumnType>(BinaryClassifierScorer.GetPredColType))
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(num == 4);
			this._threshold = Utils.ReadFloat(ctx.Reader);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00049C8C File Offset: 0x00047E8C
		public static BinaryClassifierScorer Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("BinaryClassifierScore");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(BinaryClassifierScorer.GetVersionInfo());
			return HostExtensions.Apply<BinaryClassifierScorer>(h, "Loading Model", (IChannel ch) => new BinaryClassifierScorer(ctx, h, input));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00049D21 File Offset: 0x00047F21
		protected override void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(BinaryClassifierScorer.GetVersionInfo());
			base.SaveCore(ctx);
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._threshold);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00049D52 File Offset: 0x00047F52
		public override IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, newSource, "newSource");
			return new BinaryClassifierScorer(env, this, newSource);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00049DE0 File Offset: 0x00047FE0
		protected override Delegate GetPredictedLabelGetter(IRow output, out Delegate scoreGetter)
		{
			ValueGetter<float> mapperScoreGetter = output.GetGetter<float>(this._bindings.ScoreColumnIndex);
			long cachedPosition = -1L;
			float score = 0f;
			ValueGetter<DvBool> valueGetter = delegate(ref DvBool dst)
			{
				this.EnsureCachedPosition<float>(ref cachedPosition, ref score, output, mapperScoreGetter);
				this.GetPredictedLabelCore(score, ref dst);
			};
			ValueGetter<float> valueGetter2 = delegate(ref float dst)
			{
				this.EnsureCachedPosition<float>(ref cachedPosition, ref score, output, mapperScoreGetter);
				dst = score;
			};
			scoreGetter = valueGetter2;
			return valueGetter;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00049E4E File Offset: 0x0004804E
		private void GetPredictedLabelCore(float score, ref DvBool value)
		{
			value = ((score > this._threshold) ? DvBool.True : ((score <= this._threshold) ? DvBool.False : DvBool.NA));
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00049E7B File Offset: 0x0004807B
		private static ColumnType GetPredColType(ColumnType scoreType)
		{
			return BoolType.Instance;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00049E82 File Offset: 0x00048082
		private static bool OutputTypeMatches(ColumnType scoreType)
		{
			return scoreType == NumberType.Float;
		}

		// Token: 0x04000776 RID: 1910
		public const string LoaderSignature = "BinClassScoreTransform";

		// Token: 0x04000777 RID: 1911
		private const string RegistrationName = "BinaryClassifierScore";

		// Token: 0x04000778 RID: 1912
		private readonly float _threshold;

		// Token: 0x02000251 RID: 593
		public sealed class Arguments : PredictedLabelScorerBase.ThresholdArgumentsBase
		{
		}
	}
}
