using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002D4 RID: 724
	public sealed class MultiClassMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0005C462 File Offset: 0x0005A662
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005C46C File Offset: 0x0005A66C
		public MultiClassMamlEvaluator(MultiClassMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "MultiClassClassification", "MultiClassMamlEvaluator")
		{
			Contracts.CheckValue<MultiClassMamlEvaluator.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, 2 <= args.numTopClassesToOutput, "numTopClassesToOutput");
			Contracts.CheckUserArg(this._host, 2 <= args.numClassesConfusionMatrix, "numClassesConfusionMatrix");
			Contracts.CheckUserArg(this._host, args.outputTopKAcc == null || args.outputTopKAcc > 0, "outputTopKAcc");
			Contracts.CheckUserArg(this._host, 2 <= args.numClassesConfusionMatrix, "numClassesConfusionMatrix");
			this._numTopClasses = args.numTopClassesToOutput;
			this._outputPerClass = args.outputPerClassStatistics;
			this._numConfusionTableClasses = args.numClassesConfusionMatrix;
			this._outputTopKAcc = args.outputTopKAcc;
			this._evaluator = new MultiClassClassifierEvaluator(new MultiClassClassifierEvaluator.Arguments
			{
				outputTopKAcc = this._outputTopKAcc
			}, this._host);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0005C580 File Offset: 0x0005A780
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
			if (this._outputTopKAcc != null)
			{
				dataView = this.ChangeTopKAccColumnName(dataView);
			}
			if (!this._outputPerClass)
			{
				dataView = this.DropPerClassColumn(dataView);
			}
			string text;
			string confusionTable = MetricWriter.GetConfusionTable(this._host, dataView2, out text, false, this._numConfusionTableClasses);
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0005C644 File Offset: 0x0005A844
		protected override void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			IDataView dataView;
			if (!base.TryGetOverallMetrics(metrics, out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			if (this._outputTopKAcc != null)
			{
				dataView = this.ChangeTopKAccColumnName(dataView);
			}
			if (!this._outputPerClass)
			{
				dataView = this.DropPerClassColumn(dataView);
			}
			MetricWriter.PrintOverallMetrics(this._host, ch, filename, dataView);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0005C6A4 File Offset: 0x0005A8A4
		private IDataView ChangeTopKAccColumnName(IDataView input)
		{
			input = new CopyColumnsTransform(new CopyColumnsTransform.Arguments
			{
				column = new CopyColumnsTransform.Column[]
				{
					new CopyColumnsTransform.Column
					{
						name = string.Format("Top-{0}-accuracy", this._outputTopKAcc),
						source = "Top K accuracy"
					}
				}
			}, this._host, input);
			return new DropColumnsTransform(new DropColumnsTransform.Arguments
			{
				column = new string[] { "Top K accuracy" }
			}, this._host, input);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0005C730 File Offset: 0x0005A930
		private IDataView DropPerClassColumn(IDataView input)
		{
			int num;
			if (input.Schema.TryGetColumnIndex("Per class log-loss", ref num))
			{
				input = new DropColumnsTransform(new DropColumnsTransform.Arguments
				{
					column = new string[] { "Per class log-loss" }
				}, this._host, input);
			}
			return input;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0005C97C File Offset: 0x0005AB7C
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("AccuracyMicro", "Accuracy(micro-avg)", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AccuracyMacro", "Accuracy(macro-avg)", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("TopKAccuracy", string.Format("Top-{0}-accuracy", this._outputTopKAcc), MetricColumn.Objective.Maximize, true, false, null, null, null);
			if (this._outputPerClass)
			{
				yield return new MetricColumn("LogLoss<class name>", "Per class log-loss", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("^{0}(?<class>.+)", "Log-loss"), RegexOptions.IgnoreCase), null, null);
			}
			yield return new MetricColumn("LogLoss", "Log-loss", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("LogLossReduction", "Log-loss-reduction", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "perInstance", "Data must contain a label column");
			yield return schema.Label.Name;
			yield return "Assigned";
			yield return "Log-loss";
			yield return "SortedScores";
			yield return "SortedClasses";
			yield break;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0005CB60 File Offset: 0x0005AD60
		protected override IDataView GetPerInstanceMetricsCore(IDataView perInst)
		{
			int num;
			if (perInst.Schema.TryGetColumnIndex("SortedClasses", ref num))
			{
				ColumnType columnType = perInst.Schema.GetColumnType(num);
				if (this._numTopClasses < columnType.VectorSize)
				{
					perInst = new DropSlotsTransform(new DropSlotsTransform.Arguments
					{
						column = new DropSlotsTransform.Column[]
						{
							new DropSlotsTransform.Column
							{
								name = "SortedClasses",
								slots = new DropSlotsTransform.Range[]
								{
									new DropSlotsTransform.Range
									{
										min = this._numTopClasses
									}
								}
							}
						}
					}, this._host, perInst);
				}
			}
			if (perInst.Schema.TryGetColumnIndex("SortedScores", ref num))
			{
				ColumnType columnType2 = perInst.Schema.GetColumnType(num);
				if (this._numTopClasses < columnType2.VectorSize)
				{
					perInst = new DropSlotsTransform(new DropSlotsTransform.Arguments
					{
						column = new DropSlotsTransform.Column[]
						{
							new DropSlotsTransform.Column
							{
								name = "SortedScores",
								slots = new DropSlotsTransform.Range[]
								{
									new DropSlotsTransform.Range
									{
										min = this._numTopClasses
									}
								}
							}
						}
					}, this._host, perInst);
				}
			}
			return perInst;
		}

		// Token: 0x04000952 RID: 2386
		private const string TopKAccuracyFormat = "Top-{0}-accuracy";

		// Token: 0x04000953 RID: 2387
		private readonly bool _outputPerClass;

		// Token: 0x04000954 RID: 2388
		private readonly int _numTopClasses;

		// Token: 0x04000955 RID: 2389
		private readonly int _numConfusionTableClasses;

		// Token: 0x04000956 RID: 2390
		private readonly int? _outputTopKAcc;

		// Token: 0x04000957 RID: 2391
		private readonly MultiClassClassifierEvaluator _evaluator;

		// Token: 0x020002D5 RID: 725
		public class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x04000958 RID: 2392
			[Argument(0, HelpText = "Output top-K accuracy", ShortName = "topkacc")]
			public int? outputTopKAcc;

			// Token: 0x04000959 RID: 2393
			[Argument(0, HelpText = "Output top-K classes", ShortName = "topk")]
			public int numTopClassesToOutput = 3;

			// Token: 0x0400095A RID: 2394
			[Argument(0, HelpText = "Maximum number of classes in confusion matrix", ShortName = "nccf")]
			public int numClassesConfusionMatrix = 10;

			// Token: 0x0400095B RID: 2395
			[Argument(0, HelpText = "Output per class statistics and confusion matrix", ShortName = "opcs", Hide = true)]
			public bool outputPerClassStatistics;
		}
	}
}
