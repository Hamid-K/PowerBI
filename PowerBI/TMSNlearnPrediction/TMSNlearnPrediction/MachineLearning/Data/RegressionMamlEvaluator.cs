using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200029A RID: 666
	public sealed class RegressionMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000540C0 File Offset: 0x000522C0
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000540C8 File Offset: 0x000522C8
		public RegressionMamlEvaluator(RegressionMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "Regression", "RegressionMamlEvaluator")
		{
			Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.lossFunction), "loss", "Loss function must be specified.");
			this._evaluator = new RegressionEvaluator(new RegressionEvaluator.Arguments
			{
				lossFunction = args.lossFunction
			}, this._host);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000542EC File Offset: 0x000524EC
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			yield return schema.Label.Name;
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "Regression", "Score", null);
			yield return scoreInfo.Name;
			yield return "L1-loss";
			yield return "L2-loss";
			yield break;
		}

		// Token: 0x04000860 RID: 2144
		private readonly RegressionEvaluator _evaluator;

		// Token: 0x0200029B RID: 667
		public sealed class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x04000861 RID: 2145
			[Argument(4, HelpText = "Loss function", ShortName = "loss")]
			public SubComponent<IRegressionLoss, SignatureRegressionLoss> lossFunction = new SubComponent<IRegressionLoss, SignatureRegressionLoss>("SquaredLoss");
		}
	}
}
