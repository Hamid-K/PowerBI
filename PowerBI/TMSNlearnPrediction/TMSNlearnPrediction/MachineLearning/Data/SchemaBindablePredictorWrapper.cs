using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000223 RID: 547
	public sealed class SchemaBindablePredictorWrapper : SchemaBindablePredictorWrapperBase
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00042D15 File Offset: 0x00040F15
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("SCH BIND", 65538U, 65538U, 65538U, "SchemaBindableWrapper", null);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00042D36 File Offset: 0x00040F36
		public SchemaBindablePredictorWrapper(IPredictor predictor)
			: base(predictor)
		{
			this._scoreColumnKind = SchemaBindablePredictorWrapper.GetScoreColumnKind(this._predictor);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00042D50 File Offset: 0x00040F50
		private SchemaBindablePredictorWrapper(ModelLoadContext ctx, IHostEnvironment env)
			: base(ctx, env)
		{
			this._scoreColumnKind = SchemaBindablePredictorWrapper.GetScoreColumnKind(this._predictor);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00042D6B File Offset: 0x00040F6B
		public static SchemaBindablePredictorWrapper Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(SchemaBindablePredictorWrapper.GetVersionInfo());
			return new SchemaBindablePredictorWrapper(ctx, env);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00042D96 File Offset: 0x00040F96
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.SetVersionInfo(SchemaBindablePredictorWrapper.GetVersionInfo());
			base.Save(ctx);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00042DB8 File Offset: 0x00040FB8
		protected override ISchemaBoundMapper BindCore(IChannel ch, RoleMappedSchema schema)
		{
			ScoreMapperSchema scoreMapperSchema = new ScoreMapperSchema(this._scoreType, this._scoreColumnKind);
			return new SchemaBindablePredictorWrapperBase.SingleValueRowMapper(schema, this, scoreMapperSchema);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00042DE0 File Offset: 0x00040FE0
		private static string GetScoreColumnKind(IPredictor predictor)
		{
			switch (predictor.PredictionKind)
			{
			case 3:
				return "MultiClassClassification";
			case 4:
				return "Regression";
			case 5:
				return "MultiOutputRegression";
			case 6:
				return "Ranking";
			case 8:
				return "AnomalyDetection";
			case 9:
				return "Clustering";
			}
			throw Contracts.Except("Unknown prediction kind, can't map to score column kind: {0}", new object[] { predictor.PredictionKind });
		}

		// Token: 0x040006B9 RID: 1721
		public const string LoaderSignature = "SchemaBindableWrapper";

		// Token: 0x040006BA RID: 1722
		private readonly string _scoreColumnKind;
	}
}
