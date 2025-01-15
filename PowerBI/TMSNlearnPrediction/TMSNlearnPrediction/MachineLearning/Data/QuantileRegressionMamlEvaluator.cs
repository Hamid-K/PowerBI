using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001E3 RID: 483
	public sealed class QuantileRegressionMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00038F6D File Offset: 0x0003716D
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00038F78 File Offset: 0x00037178
		public QuantileRegressionMamlEvaluator(QuantileRegressionMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "QuantileRegression", "QuantilsRegressionMamlEvaluator")
		{
			this._index = args.index;
			Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.lossFunction), "loss", "Loss function must be specified.");
			this._evaluator = new QuantileRegressionEvaluator(new QuantileRegressionEvaluator.Arguments
			{
				lossFunction = args.lossFunction
			}, this._host);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00038FE8 File Offset: 0x000371E8
		protected override void PrintFoldResultsCore(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (!metrics.TryGetValue("OverallMetrics", out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			dataView = this.ExtractRelevantIndex(dataView);
			string text;
			string perFoldResults = MetricWriter.GetPerFoldResults(this._host, dataView, out text);
			if (!string.IsNullOrEmpty(text))
			{
				ch.Info(text);
			}
			ch.Info(perFoldResults);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00039044 File Offset: 0x00037244
		protected override void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			IDataView dataView;
			if (!base.TryGetOverallMetrics(metrics, out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			dataView = this.ExtractRelevantIndex(dataView);
			MetricWriter.PrintOverallMetrics(this._host, ch, filename, dataView);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003909C File Offset: 0x0003729C
		private IDataView ExtractRelevantIndex(IDataView data)
		{
			IDataView dataView = data;
			for (int i = 0; i < data.Schema.ColumnCount; i++)
			{
				ColumnType columnType = data.Schema.GetColumnType(i);
				if (columnType.IsKnownSizeVector && columnType.ItemType == NumberType.R8)
				{
					string columnName = data.Schema.GetColumnName(i);
					int index = this._index ?? (columnType.VectorSize / 2);
					dataView = LambdaColumnMapper.Create<VBuffer<double>, double>(this._host, "Quantile Regression", dataView, columnName, columnName, columnType, NumberType.R8, delegate(ref VBuffer<double> src, ref double dst)
					{
						dst = src.GetItemOrDefault(index);
					}, null);
				}
			}
			return dataView;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000392E8 File Offset: 0x000374E8
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("L1", "L1(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("L2", "L2(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("Rms", "RMS(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("Loss", "Loss-fn(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("RSquared", "R Squared", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000394C8 File Offset: 0x000376C8
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			yield return schema.Label.Name;
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "QuantileRegression", "Score", null);
			yield return scoreInfo.Name;
			yield return "L1-loss";
			yield return "L2-loss";
			yield break;
		}

		// Token: 0x0400058E RID: 1422
		private readonly int? _index;

		// Token: 0x0400058F RID: 1423
		private readonly QuantileRegressionEvaluator _evaluator;

		// Token: 0x020001E4 RID: 484
		public sealed class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x04000590 RID: 1424
			[Argument(4, HelpText = "Loss function", ShortName = "loss")]
			public SubComponent<IRegressionLoss, SignatureRegressionLoss> lossFunction = new SubComponent<IRegressionLoss, SignatureRegressionLoss>("SquaredLoss");

			// Token: 0x04000591 RID: 1425
			[Argument(4, HelpText = "Quantile index to select", ShortName = "ind")]
			public int? index;
		}
	}
}
