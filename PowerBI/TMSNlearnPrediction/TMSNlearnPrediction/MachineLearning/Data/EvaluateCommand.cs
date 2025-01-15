using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000291 RID: 657
	public sealed class EvaluateCommand : ICommand
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x00053629 File Offset: 0x00051829
		public EvaluateCommand(EvaluateCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new EvaluateCommand.Impl(args, env);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0005363E File Offset: 0x0005183E
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000847 RID: 2119
		internal const string Summary = "Evaluates the metrics for a scored data file.";

		// Token: 0x04000848 RID: 2120
		private readonly EvaluateCommand.Impl _impl;

		// Token: 0x02000292 RID: 658
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000849 RID: 2121
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x0400084A RID: 2122
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x0400084B RID: 2123
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x0400084C RID: 2124
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 6)]
			public string nameColumn = "Name";

			// Token: 0x0400084D RID: 2125
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x0400084E RID: 2126
			[Argument(4, HelpText = "Evaluator to use", ShortName = "eval")]
			public SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator;

			// Token: 0x0400084F RID: 2127
			[Argument(0, HelpText = "Results summary filename", ShortName = "sf")]
			public string summaryFilename;

			// Token: 0x04000850 RID: 2128
			[Argument(4, HelpText = "File to save per-instance predictions and metrics to", ShortName = "dout")]
			public string outputDataFile;
		}

		// Token: 0x02000293 RID: 659
		private sealed class Impl : DataCommand.ImplBase<EvaluateCommand.Arguments>
		{
			// Token: 0x06000F3C RID: 3900 RVA: 0x00053680 File Offset: 0x00051880
			public Impl(EvaluateCommand.Arguments args, IHostEnvironment env)
				: base("EvaluateCommand", args, env, null)
			{
				Utils.CheckOptionalUserDirectory(args.summaryFilename, "summaryFilename");
				Utils.CheckOptionalUserDirectory(args.outputDataFile, "outputDataFile");
			}

			// Token: 0x06000F3D RID: 3901 RVA: 0x000536C4 File Offset: 0x000518C4
			public override void Run()
			{
				using (IChannel channel = this._host.Start("Evaluate"))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x06000F3E RID: 3902 RVA: 0x0005370C File Offset: 0x0005190C
			private void RunCore(IChannel ch)
			{
				ch.Trace("Creating loader");
				IDataView dataView = base.CreateAndSaveLoader("BinaryLoader");
				ch.Trace("Binding columns");
				ISchema schema = dataView.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "labelColumn", this._args.labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._args.groupColumn, "GroupId");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "weightColumn", this._args.weightColumn, "Weight");
				string text4 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "nameColumn", this._args.nameColumn, "Name");
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ch, this._args.customColumn);
				ch.Trace("Creating evaluator");
				SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent = this._args.evaluator;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					subComponent = EvaluateUtils.GetEvaluatorType(ch, dataView.Schema);
				}
				IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent, new object[] { this._host });
				RoleMappedData roleMappedData = TrainUtils.CreateExamples(dataView, text, null, text2, text3, text4, enumerable);
				Dictionary<string, IDataView> dictionary = mamlEvaluator.Evaluate(roleMappedData);
				MetricWriter.PrintWarnings(ch, dictionary);
				mamlEvaluator.PrintFoldResults(ch, dictionary);
				mamlEvaluator.PrintOverallResults(ch, this._args.summaryFilename, new Dictionary<string, IDataView>[] { dictionary });
				if (!string.IsNullOrWhiteSpace(this._args.outputDataFile))
				{
					IDataTransform perInstanceMetrics = mamlEvaluator.GetPerInstanceMetrics(roleMappedData);
					RoleMappedData roleMappedData2 = TrainUtils.CreateExamples(perInstanceMetrics, text, null, text2, text3, text4, enumerable);
					IDataView perInstanceDataViewToSave = mamlEvaluator.GetPerInstanceDataViewToSave(roleMappedData2);
					MetricWriter.SavePerInstance(this._host, ch, this._args.outputDataFile, perInstanceDataViewToSave, true, true);
				}
			}
		}
	}
}
