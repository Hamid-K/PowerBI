using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200024E RID: 590
	public sealed class BinaryClassifierMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00048CAB File Offset: 0x00046EAB
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00048CB4 File Offset: 0x00046EB4
		public BinaryClassifierMamlEvaluator(BinaryClassifierMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "BinaryClassification", "BinaryClassifierMamlEvaluator")
		{
			Contracts.CheckValue<BinaryClassifierMamlEvaluator.Arguments>(this._host, args, "args");
			Utils.CheckOptionalUserDirectory(args.prFilename, "prFilename");
			BinaryClassifierEvaluator.Arguments arguments = new BinaryClassifierEvaluator.Arguments();
			arguments.threshold = args.threshold;
			arguments.useRawScoreThreshold = args.useRawScoreThreshold;
			arguments.maxAucExamples = args.maxAucExamples;
			arguments.numRocExamples = (string.IsNullOrEmpty(args.prFilename) ? 0 : args.numRocExamples);
			arguments.numAuPrcExamples = args.numAuPrcExamples;
			this._prFileName = args.prFilename;
			this._probCol = args.probabilityColumn;
			this._evaluator = new BinaryClassifierEvaluator(arguments, this._host);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00048D7C File Offset: 0x00046F7C
		protected override IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRolesCore(RoleMappedSchema schema)
		{
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = base.GetInputColumnRolesCore(schema);
			ColumnInfo scoreColumnInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "BinaryClassification", "Score", null);
			ColumnInfo optAuxScoreColumnInfo = EvaluateUtils.GetOptAuxScoreColumnInfo(this._host, schema.Schema, this._probCol, "probabilityColumn", scoreColumnInfo.Index, "Probability", (ColumnType t) => t == NumberType.Float);
			if (optAuxScoreColumnInfo != null)
			{
				enumerable = MetadataUtils.Prepend<KeyValuePair<RoleMappedSchema.ColumnRole, string>>(enumerable, new KeyValuePair<RoleMappedSchema.ColumnRole, string>[] { RoleMappedSchema.CreatePair("Probability", optAuxScoreColumnInfo.Name) });
			}
			return enumerable;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00048E34 File Offset: 0x00047034
		protected override void PrintFoldResultsCore(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (!metrics.TryGetValue("OverallMetrics", out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			IDataView dataView2;
			if (!metrics.TryGetValue("ConfusionMatrix", out dataView2))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			ChooseColumnsTransform.Arguments arguments = new ChooseColumnsTransform.Arguments();
			List<ChooseColumnsTransform.Column> list = new List<ChooseColumnsTransform.Column>
			{
				new ChooseColumnsTransform.Column
				{
					name = "OVERALL 0/1 ACCURACY",
					source = "Accuracy"
				},
				new ChooseColumnsTransform.Column
				{
					name = "LOG LOSS/instance",
					source = "Log-loss"
				},
				new ChooseColumnsTransform.Column
				{
					name = "Test-set entropy (prior Log-Loss/instance)"
				},
				new ChooseColumnsTransform.Column
				{
					name = "LOG-LOSS REDUCTION (RIG)",
					source = "Log-loss reduction"
				},
				new ChooseColumnsTransform.Column
				{
					name = "AUC"
				}
			};
			int num;
			if (dataView.Schema.TryGetColumnIndex("IsWeighted", ref num))
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					name = "IsWeighted"
				});
			}
			if (dataView.Schema.TryGetColumnIndex("StratCol", ref num))
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					name = "StratCol"
				});
			}
			if (dataView.Schema.TryGetColumnIndex("StratVal", ref num))
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					name = "StratVal"
				});
			}
			arguments.column = list.ToArray();
			dataView = new ChooseColumnsTransform(arguments, this._host, dataView);
			string text;
			string confusionTable = MetricWriter.GetConfusionTable(this._host, dataView2, out text, true, -1);
			string text2;
			string perFoldResults = MetricWriter.GetPerFoldResults(this._host, dataView, out text2);
			if (!string.IsNullOrEmpty(text))
			{
				ch.Info(text);
				ch.Info(text2);
			}
			ch.Info(confusionTable);
			ch.Info(perFoldResults);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00049034 File Offset: 0x00047234
		protected override void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			IDataView dataView;
			if (!base.TryGetOverallMetrics(metrics, out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			dataView = new DropColumnsTransform(new DropColumnsTransform.Arguments
			{
				column = new string[] { "Test-set entropy (prior Log-Loss/instance)" }
			}, this._host, dataView);
			MetricWriter.PrintOverallMetrics(this._host, ch, filename, dataView);
			if (!string.IsNullOrEmpty(this._prFileName))
			{
				IDataView nonStratifiedMetrics;
				if (!this.TryGetPrMetrics(metrics, out nonStratifiedMetrics))
				{
					throw Contracts.Except(this._host, "Did not find p/r metrics");
				}
				ch.Trace("Saving p/r data view");
				nonStratifiedMetrics = MetricWriter.GetNonStratifiedMetrics(this._host, nonStratifiedMetrics);
				MetricWriter.SavePerInstance(this._host, ch, this._prFileName, nonStratifiedMetrics, true, true);
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00049370 File Offset: 0x00047570
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("Accuracy", "Accuracy", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("PosPrec", "Positive precision", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("PosRecall", "Positive recall", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("NegPrec", "Negative precision", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("NegRecall", "Negative recall", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("Auc", "AUC", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("LogLoss", "Log-loss", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("LogLossReduction", "Log-loss reduction", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("F1", "F1 Score", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AuPrc", "AUPRC", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00049390 File Offset: 0x00047590
		private bool TryGetPrMetrics(Dictionary<string, IDataView>[] metrics, out IDataView pr)
		{
			if (metrics.Length == 1)
			{
				return metrics[0].TryGetValue("PrCurve", out pr);
			}
			pr = null;
			List<IDataView> list = new List<IDataView>();
			for (int i = 0; i < metrics.Length; i++)
			{
				Dictionary<string, IDataView> dictionary = metrics[i];
				IDataView dataView;
				if (!dictionary.TryGetValue("PrCurve", out dataView))
				{
					return false;
				}
				string columnName = dataView.Schema.GetColumnName(0);
				ColumnType columnType = dataView.Schema.GetColumnType(0);
				dataView = Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(EvaluateUtils.AddKeyColumn<int>), columnType.RawType, this._host, dataView, columnName, "Fold Index", columnType, metrics.Length, i + 1, "FoldIndex", null);
				list.Add(dataView);
			}
			pr = AppendRowsDataView.Create(this._host, list[0].Schema, list.ToArray());
			this.SavePrPlots(list);
			return true;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00049460 File Offset: 0x00047660
		private void SavePrPlots(List<IDataView> prList)
		{
			XYPlot xyplot = new XYPlot();
			xyplot.LegendX = "Recall";
			xyplot.LegendY = "Precision";
			xyplot.MinX = 0.0;
			xyplot.MaxX = 1.0;
			xyplot.MinY = 0.0;
			xyplot.MaxY = 1.0;
			xyplot.InitializeChart(false, false, false, false);
			IEnumerable<XYPlot.XYPoint> enumerable = this.GetCurve(prList, "Recall", "Precision", 1.0);
			xyplot.AddCurveXY(enumerable, "", ChartDashStyle.Solid);
			string text = this._prFileName;
			if (text.Length > 4 && text[text.Length - 4] == '.')
			{
				text = text.Substring(0, text.Length - 4);
			}
			xyplot.Save(text + ".pr.jpg");
			enumerable = this.GetCurve(prList, "FPR", "Recall", 0.0);
			XYPlot xyplot2 = new XYPlot();
			xyplot2.LegendX = "FPR";
			xyplot2.LegendY = "Recall=TPR";
			xyplot2.MinX = 0.0;
			xyplot2.MaxX = 1.0;
			xyplot2.MinY = 0.0;
			xyplot2.MaxY = 1.0;
			xyplot2.InitializeChart(false, false, false, false);
			xyplot2.AddCurveXY(enumerable, "", ChartDashStyle.Solid);
			xyplot2.Save(text + ".roc.jpg");
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000495F4 File Offset: 0x000477F4
		private IEnumerable<XYPlot.XYPoint> GetCurve(List<IDataView> prList, string xAxisName, string yAxisName, double yInit = 0.0)
		{
			IRowCursor[] array = new IRowCursor[prList.Count];
			ValueGetter<double>[] array2 = new ValueGetter<double>[prList.Count];
			ValueGetter<double>[] array3 = new ValueGetter<double>[prList.Count];
			for (int i = 0; i < prList.Count; i++)
			{
				int xIndex;
				if (!prList[i].Schema.TryGetColumnIndex(xAxisName, ref xIndex))
				{
					throw Contracts.Except(this._host, "Data view does not contain column '{0}'", new object[] { xAxisName });
				}
				int yIndex;
				if (!prList[i].Schema.TryGetColumnIndex(yAxisName, ref yIndex))
				{
					throw Contracts.Except(this._host, "Data view does not contain column '{0}'", new object[] { yAxisName });
				}
				array[i] = prList[i].GetRowCursor((int col) => col == xIndex || col == yIndex, null);
				array2[i] = array[i].GetGetter<double>(xIndex);
				array3[i] = array[i].GetGetter<double>(yIndex);
			}
			List<XYPlot.XYPoint> list = new List<XYPlot.XYPoint>();
			double[] array4 = new double[prList.Count];
			double[] array5 = new double[prList.Count];
			double[] array6 = new double[prList.Count];
			double[] array7 = new double[prList.Count];
			if (yInit != 0.0)
			{
				for (int j = 0; j < array6.Length; j++)
				{
					array6[j] = yInit;
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k].MoveNext())
				{
					array2[k].Invoke(ref array5[k]);
					array3[k].Invoke(ref array7[k]);
				}
			}
			for (;;)
			{
				int num = -1;
				double num2 = 2.0;
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].State != 2 && array5[l] < num2)
					{
						num2 = array5[l];
						num = l;
					}
				}
				if (num < 0)
				{
					break;
				}
				double num3 = array7[num];
				for (int m = 0; m < array7.Length; m++)
				{
					if (m != num)
					{
						double num4 = array5[m] - array5[num];
						double num5 = array5[num] - array4[m];
						num3 += (num4 * array6[m] + num5 * array7[m]) / (num4 + num5);
					}
				}
				num3 /= (double)prList.Count;
				list.Add(new XYPlot.XYPoint
				{
					x = num2,
					y = num3
				});
				array4[num] = array5[num];
				array6[num] = array7[num];
				if (array[num].MoveNext())
				{
					array2[num].Invoke(ref array5[num]);
					array3[num].Invoke(ref array7[num]);
				}
				array[num].MoveNext();
			}
			foreach (IRowCursor rowCursor in array)
			{
				rowCursor.Dispose();
			}
			return list;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00049B38 File Offset: 0x00047D38
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			yield return schema.Label.Name;
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "BinaryClassification", "Score", null);
			yield return scoreInfo.Name;
			ColumnInfo probInfo = EvaluateUtils.GetOptAuxScoreColumnInfo(this._host, schema.Schema, this._probCol, "probabilityColumn", scoreInfo.Index, "Probability", (ColumnType t) => t == NumberType.Float);
			if (probInfo != null)
			{
				yield return probInfo.Name;
				yield return "Log-loss";
			}
			yield return "Assigned";
			yield break;
		}

		// Token: 0x04000767 RID: 1895
		private const string FoldAccuracy = "OVERALL 0/1 ACCURACY";

		// Token: 0x04000768 RID: 1896
		private const string FoldLogLoss = "LOG LOSS/instance";

		// Token: 0x04000769 RID: 1897
		private const string FoldLogLosRed = "LOG-LOSS REDUCTION (RIG)";

		// Token: 0x0400076A RID: 1898
		private readonly BinaryClassifierEvaluator _evaluator;

		// Token: 0x0400076B RID: 1899
		private readonly string _prFileName;

		// Token: 0x0400076C RID: 1900
		private readonly string _probCol;

		// Token: 0x0200024F RID: 591
		public class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x0400076F RID: 1903
			[Argument(0, HelpText = "Probability column name", ShortName = "prob")]
			public string probabilityColumn;

			// Token: 0x04000770 RID: 1904
			[Argument(0, HelpText = "Probability value for classification thresholding")]
			public float threshold;

			// Token: 0x04000771 RID: 1905
			[Argument(0, HelpText = "Use raw score value instead of probability for classification thresholding", ShortName = "useRawScore")]
			public bool useRawScoreThreshold = true;

			// Token: 0x04000772 RID: 1906
			[Argument(0, HelpText = "The number of samples to use for p/r curve generation. Specify 0 for no p/r curve generation", ShortName = "numpr")]
			public int numRocExamples = 100000;

			// Token: 0x04000773 RID: 1907
			[Argument(0, HelpText = "The number of samples to use for AUC calculation. Specify 0 for using the whole dataset", ShortName = "numauc")]
			public int maxAucExamples = -1;

			// Token: 0x04000774 RID: 1908
			[Argument(0, HelpText = "The number of samples to use for AUPRC calculation. Specify 0 for no AUPRC calculation", ShortName = "numauprc")]
			public int numAuPrcExamples;

			// Token: 0x04000775 RID: 1909
			[Argument(0, HelpText = "Precision-Recall results filename", ShortName = "pr")]
			public string prFilename;
		}
	}
}
