using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200009B RID: 155
	public sealed class AnomalyDetectionMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00011516 File Offset: 0x0000F716
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011520 File Offset: 0x0000F720
		public AnomalyDetectionMamlEvaluator(AnomalyDetectionMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "AnomalyDetection", "AnomalyDetectionMamlEvaluator")
		{
			AnomalyDetectionEvaluator.Arguments arguments = new AnomalyDetectionEvaluator.Arguments();
			arguments.k = (this._k = args.k);
			arguments.p = (this._p = args.p);
			arguments.numTopResults = args.numTopResults;
			this._topScored = args.numTopResults;
			arguments.stream = args.stream;
			arguments.maxAucExamples = args.maxAucExamples;
			this._evaluator = new AnomalyDetectionEvaluator(arguments, this._host);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000115DC File Offset: 0x0000F7DC
		protected override void PrintFoldResultsCore(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (!metrics.TryGetValue("TopKResults", out dataView))
			{
				throw Contracts.Except(this._host, "Did not find the top-k results data view");
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (IRowCursor rowCursor = dataView.GetRowCursor((int col) => true, null))
			{
				int num;
				if (!dataView.Schema.TryGetColumnIndex("Instance", ref num))
				{
					throw Contracts.Except(this._host, "Data view does not contain the 'Instance' column");
				}
				ValueGetter<DvText> getter = rowCursor.GetGetter<DvText>(num);
				if (!dataView.Schema.TryGetColumnIndex("Anomaly Score", ref num))
				{
					throw Contracts.Except(this._host, "Data view does not contain the 'Anomaly Score' column");
				}
				ValueGetter<float> getter2 = rowCursor.GetGetter<float>(num);
				if (!dataView.Schema.TryGetColumnIndex("Label", ref num))
				{
					throw Contracts.Except(this._host, "Data view does not contain the 'Label' column");
				}
				ValueGetter<float> getter3 = rowCursor.GetGetter<float>(num);
				bool flag = false;
				while (rowCursor.MoveNext())
				{
					if (!flag)
					{
						stringBuilder.AppendFormat("{0} Top-scored Results", this._topScored);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("=================================================");
						stringBuilder.AppendLine("Instance    Anomaly Score     Labeled");
						flag = true;
					}
					DvText dvText = default(DvText);
					float num2 = 0f;
					float num3 = 0f;
					getter.Invoke(ref dvText);
					getter2.Invoke(ref num2);
					getter3.Invoke(ref num3);
					stringBuilder.AppendFormat("{0,-10}{1,12:G4}{2,12}", dvText, num2, num3);
					stringBuilder.AppendLine();
				}
			}
			if (stringBuilder.Length > 0)
			{
				ch.Info(stringBuilder.ToString());
			}
			IDataView dataView2;
			if (!metrics.TryGetValue("OverallMetrics", out dataView2))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			int numAnomIndex;
			if (!dataView2.Schema.TryGetColumnIndex("NumAnomalies", ref numAnomIndex))
			{
				throw Contracts.Except(this._host, "Could not find the 'NumAnomalies' column");
			}
			int stratCol;
			bool hasStrat = dataView2.Schema.TryGetColumnIndex("StratCol", ref stratCol);
			int num4;
			dataView2.Schema.TryGetColumnIndex("StratVal", ref num4);
			DvInt8 dvInt = 0L;
			using (IRowCursor rowCursor2 = dataView2.GetRowCursor((int col) => col == numAnomIndex || (hasStrat && col == stratCol), null))
			{
				ValueGetter<DvInt8> getter4 = rowCursor2.GetGetter<DvInt8>(numAnomIndex);
				ValueGetter<uint> valueGetter = null;
				if (hasStrat)
				{
					ColumnType columnType = rowCursor2.Schema.GetColumnType(stratCol);
					valueGetter = RowCursorUtils.GetGetterAs<uint>(columnType, rowCursor2, stratCol);
				}
				bool flag2 = false;
				while (rowCursor2.MoveNext())
				{
					uint num5 = 0U;
					if (valueGetter != null)
					{
						valueGetter.Invoke(ref num5);
					}
					if (num5 <= 0U)
					{
						if (flag2)
						{
							throw Contracts.Except(this._host, "Found multiple non-stratified rows in overall results data view");
						}
						flag2 = true;
						getter4.Invoke(ref dvInt);
					}
				}
			}
			ChooseColumnsTransform.Arguments arguments = new ChooseColumnsTransform.Arguments();
			List<ChooseColumnsTransform.Column> list = new List<ChooseColumnsTransform.Column>
			{
				new ChooseColumnsTransform.Column
				{
					name = string.Format("Detection rate at {0} false positives", this._k),
					source = "DR @K FP"
				},
				new ChooseColumnsTransform.Column
				{
					name = string.Format("Detection rate at {0} false positive rate", this._p),
					source = "DR @P FPR"
				},
				new ChooseColumnsTransform.Column
				{
					name = string.Format("Detection rate at {0} positive predictions", dvInt),
					source = "DR @NumPos"
				},
				new ChooseColumnsTransform.Column
				{
					name = "Threshold @K FP"
				},
				new ChooseColumnsTransform.Column
				{
					name = "Threshold @P FPR"
				},
				new ChooseColumnsTransform.Column
				{
					name = "Threshold @NumPos"
				},
				new ChooseColumnsTransform.Column
				{
					name = "AUC"
				}
			};
			arguments.column = list.ToArray();
			IDataView dataView3 = new ChooseColumnsTransform(arguments, this._host, dataView2);
			string text;
			ch.Info(MetricWriter.GetPerFoldResults(this._host, dataView3, out text));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011A5C File Offset: 0x0000FC5C
		protected override void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			IDataView dataView;
			if (!base.TryGetOverallMetrics(metrics, out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			dataView = new DropColumnsTransform(new DropColumnsTransform.Arguments
			{
				column = new string[] { "NumAnomalies", "Threshold @K FP", "Threshold @P FPR", "Threshold @NumPos" }
			}, this._host, dataView);
			MetricWriter.PrintOverallMetrics(this._host, ch, filename, dataView);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011C5C File Offset: 0x0000FE5C
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			yield return schema.Label.Name;
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "AnomalyDetection", "Score", null);
			yield return scoreInfo.Name;
			yield break;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("DrAtK", "DR @K FP", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield return new MetricColumn("DrAtPFpr", "DR @P FPR", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield return new MetricColumn("DrAtNumPos", "DR @NumPos", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield break;
		}

		// Token: 0x0400014F RID: 335
		private const string FoldDrAtKFormat = "Detection rate at {0} false positives";

		// Token: 0x04000150 RID: 336
		private const string FoldDrAtPFormat = "Detection rate at {0} false positive rate";

		// Token: 0x04000151 RID: 337
		private const string FoldDrAtNumAnomaliesFormat = "Detection rate at {0} positive predictions";

		// Token: 0x04000152 RID: 338
		private readonly AnomalyDetectionEvaluator _evaluator;

		// Token: 0x04000153 RID: 339
		private readonly int _topScored;

		// Token: 0x04000154 RID: 340
		private readonly int _k;

		// Token: 0x04000155 RID: 341
		private readonly double _p;

		// Token: 0x0200009C RID: 156
		public sealed class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x04000157 RID: 343
			[Argument(0, HelpText = "Expected number of false positives")]
			public int k = 10;

			// Token: 0x04000158 RID: 344
			[Argument(0, HelpText = "Expected false positive rate")]
			public double p = 0.01;

			// Token: 0x04000159 RID: 345
			[Argument(0, HelpText = "Number of top-scored predictions to display", ShortName = "n")]
			public int numTopResults = 50;

			// Token: 0x0400015A RID: 346
			[Argument(0, HelpText = "Whether to calculate metrics in a streaming fashion")]
			public bool stream;

			// Token: 0x0400015B RID: 347
			[Argument(0, HelpText = "The number of samples to use for AUC calculation. If 0, AUC is not computed. If -1, the whole dataset is used", ShortName = "numauc")]
			public int maxAucExamples = -1;
		}
	}
}
